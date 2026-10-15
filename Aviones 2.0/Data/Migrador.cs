using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Aviones_2._0.Data
{
    /// <summary>
    /// Crea la base si no existe y le aplica las migraciones pendientes (DB\Migraciones\NNNN_nombre.sql,
    /// incluidas dentro del programa), en orden y una sola vez cada una. Antes de cambiar una base
    /// que ya tiene datos hace una copia de seguridad.
    /// </summary>
    public static class Migrador
    {
        public class Migracion
        {
            public int Numero { get; set; }
            public string Nombre { get; set; }
            public string Script { get; set; }
        }

        public class Resultado
        {
            public List<Migracion> Aplicadas { get; } = new List<Migracion>();

            /// <summary>Copia hecha antes de migrar; null si no hizo falta.</summary>
            public string Copia { get; set; }
        }

        private const string PrefijoRecurso = "Migraciones.";

        /// <summary>Las migraciones incluidas en el programa, en orden.</summary>
        public static List<Migracion> Todas()
        {
            var ensamblado = Assembly.GetExecutingAssembly();
            var lista = new List<Migracion>();
            foreach (string recurso in ensamblado.GetManifestResourceNames().Where(r => r.StartsWith(PrefijoRecurso)))
            {
                var m = Regex.Match(recurso.Substring(PrefijoRecurso.Length), @"^(\d+)_(.+)\.sql$");
                if (!m.Success)
                {
                    continue;
                }
                using (var lector = new StreamReader(ensamblado.GetManifestResourceStream(recurso)))
                {
                    lista.Add(new Migracion { Numero = int.Parse(m.Groups[1].Value), Nombre = m.Groups[2].Value, Script = lector.ReadToEnd() });
                }
            }
            return lista.OrderBy(x => x.Numero).ToList();
        }

        /// <param name="carpetaDatos">Dónde crear los archivos de una base nueva; null para la carpeta del servidor.</param>
        /// <param name="carpetaCopias">Dónde dejar la copia previa; null (o si el servidor no puede escribir ahí) para la carpeta de copias del servidor.</param>
        public static Resultado Actualizar(string connectionString, string carpetaDatos = null, string carpetaCopias = null)
        {
            carpetaDatos = string.IsNullOrWhiteSpace(carpetaDatos) ? null : carpetaDatos;
            carpetaCopias = string.IsNullOrWhiteSpace(carpetaCopias) ? null : carpetaCopias;
            var datos = new SqlConnectionStringBuilder(connectionString);
            string nombreBase = datos.InitialCatalog;
            if (string.IsNullOrEmpty(nombreBase))
            {
                throw new InvalidOperationException("La cadena de conexión no indica la base (Initial Catalog).");
            }

            CrearBaseSiFalta(datos, nombreBase, carpetaDatos);

            var resultado = new Resultado();
            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();
                var aplicadas = PrepararRegistro(cn);
                var todas = Todas();
                int ultimaConocida = todas.Count == 0 ? 0 : todas.Max(m => m.Numero);
                if (aplicadas.Count > 0 && aplicadas.Max() > ultimaConocida)
                {
                    throw new InvalidOperationException(
                        "La base de datos es de una versión más nueva del programa. Actualice el programa antes de usarla.");
                }

                var pendientes = todas.Where(m => !aplicadas.Contains(m.Numero)).ToList();
                if (pendientes.Count == 0)
                {
                    return resultado;
                }
                if (aplicadas.Count > 0)
                {
                    resultado.Copia = HacerCopia(cn, nombreBase, carpetaCopias, $"antes_de_migracion_{pendientes[0].Numero}");
                }
                foreach (var migracion in pendientes)
                {
                    Aplicar(cn, migracion);
                    resultado.Aplicadas.Add(migracion);
                }
            }
            return resultado;
        }

        /// <summary>Copia de seguridad completa de la base. Devuelve dónde quedó.</summary>
        public static string HacerCopia(SqlConnection cn, string nombreBase, string carpeta, string motivo)
        {
            string archivo = $"{nombreBase}_{DateTime.Now:yyyyMMdd_HHmmss}_{motivo}.bak";
            if (carpeta != null)
            {
                try
                {
                    Directory.CreateDirectory(carpeta);
                    string ruta = Path.Combine(carpeta, archivo);
                    Copiar(cn, nombreBase, ruta);
                    return ruta;
                }
                catch (SqlException)
                {
                    // El servicio de SQL no puede escribir en esa carpeta: se usa la suya
                }
            }
            Copiar(cn, nombreBase, archivo);
            return cn.ExecuteScalarTexto(
                "SELECT TOP 1 physical_device_name FROM msdb.dbo.backupmediafamily F " +
                "JOIN msdb.dbo.backupset S ON S.media_set_id = F.media_set_id " +
                "WHERE S.database_name = @db ORDER BY S.backup_finish_date DESC", nombreBase) ?? archivo;
        }

        private static void Copiar(SqlConnection cn, string nombreBase, string ruta)
        {
            using (var cmd = new SqlCommand("BACKUP DATABASE @db TO DISK = @ruta WITH COPY_ONLY, INIT", cn) { CommandTimeout = 0 })
            {
                cmd.Parameters.AddWithValue("@db", nombreBase);
                cmd.Parameters.AddWithValue("@ruta", ruta);
                cmd.ExecuteNonQuery();
            }
        }

        private static void CrearBaseSiFalta(SqlConnectionStringBuilder datos, string nombreBase, string carpetaDatos)
        {
            var master = new SqlConnectionStringBuilder(datos.ConnectionString) { InitialCatalog = "master" };
            // La primera vez LocalDB tarda en arrancar
            master.ConnectTimeout = Math.Max(master.ConnectTimeout, 60);
            using (var cn = new SqlConnection(master.ConnectionString))
            {
                cn.Open();
                if (cn.ExecuteScalarTexto("SELECT CAST(DB_ID(@db) AS NVARCHAR(10))", nombreBase) != null)
                {
                    return;
                }

                // Los nombres de archivo van dentro del texto, con las comillas duplicadas
                const string Archivo = "N''' + REPLACE({0}, '''', '''''') + N'''";
                string sql = "DECLARE @sql NVARCHAR(MAX) = N'CREATE DATABASE ' + QUOTENAME(@db)";
                string mdf = null, ldf = null;
                if (carpetaDatos != null)
                {
                    Directory.CreateDirectory(carpetaDatos);
                    mdf = Path.Combine(carpetaDatos, nombreBase + ".mdf");
                    ldf = Path.Combine(carpetaDatos, nombreBase + "_log.ldf");
                    if (File.Exists(mdf))
                    {
                        // Quedó de antes (p. ej. se reinstaló LocalDB): se vuelve a conectar en vez de crear otra
                        sql += " + N' ON (FILENAME = " + string.Format(Archivo, "@mdf") + ")"
                             + (File.Exists(ldf) ? " FOR ATTACH'" : " FOR ATTACH_REBUILD_LOG'");
                    }
                    else
                    {
                        sql += " + N' ON (NAME = ' + QUOTENAME(@db) + N', FILENAME = " + string.Format(Archivo, "@mdf") + ")"
                             + " LOG ON (NAME = ' + QUOTENAME(@db + N'_log') + N', FILENAME = " + string.Format(Archivo, "@ldf") + ")'";
                    }
                }
                sql += "; EXEC (@sql);";

                using (var cmd = new SqlCommand(sql, cn) { CommandTimeout = 120 })
                {
                    cmd.Parameters.AddWithValue("@db", nombreBase);
                    cmd.Parameters.AddWithValue("@mdf", (object)mdf ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ldf", (object)ldf ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Crea la tabla de migraciones si falta. Una base de antes de las migraciones que ya tiene
        /// el esquema inicial se marca con la migración 1 aplicada.
        /// </summary>
        private static HashSet<int> PrepararRegistro(SqlConnection cn)
        {
            new SqlCommand(@"
                IF OBJECT_ID(N'dbo.Migraciones', N'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.Migraciones (
                        Numero   INT NOT NULL CONSTRAINT PK_Migraciones PRIMARY KEY,
                        Nombre   NVARCHAR(200) NOT NULL,
                        Aplicada DATETIME2(0) NOT NULL CONSTRAINT DF_Migraciones_Aplicada DEFAULT SYSDATETIME()
                    );
                    IF OBJECT_ID(N'dbo.Materiales', N'U') IS NOT NULL
                        INSERT INTO dbo.Migraciones (Numero, Nombre) VALUES (1, N'esquema_inicial');
                END", cn).ExecuteNonQuery();

            var aplicadas = new HashSet<int>();
            using (var lector = new SqlCommand("SELECT Numero FROM dbo.Migraciones", cn).ExecuteReader())
            {
                while (lector.Read())
                {
                    aplicadas.Add(lector.GetInt32(0));
                }
            }
            return aplicadas;
        }

        /// <summary>Aplica una migración entera o nada.</summary>
        private static void Aplicar(SqlConnection cn, Migracion migracion)
        {
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    foreach (string lote in Lotes(migracion.Script))
                    {
                        new SqlCommand(lote, cn, tx) { CommandTimeout = 0 }.ExecuteNonQuery();
                    }
                    using (var cmd = new SqlCommand("INSERT INTO dbo.Migraciones (Numero, Nombre) VALUES (@n, @nombre)", cn, tx))
                    {
                        cmd.Parameters.AddWithValue("@n", migracion.Numero);
                        cmd.Parameters.AddWithValue("@nombre", migracion.Nombre);
                        cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
                catch (SqlException ex)
                {
                    tx.Rollback();
                    throw new InvalidOperationException($"Falló la migración {migracion.Numero} ({migracion.Nombre}): {ex.Message}", ex);
                }
            }
        }

        /// <summary>Separa un script en lotes por las líneas "GO", como hace SQL Server Management Studio.</summary>
        public static IEnumerable<string> Lotes(string script) =>
            Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase)
                .Where(l => !string.IsNullOrWhiteSpace(l));

        private static string ExecuteScalarTexto(this SqlConnection cn, string sql, string db)
        {
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@db", db);
                return cmd.ExecuteScalar() as string;
            }
        }
    }
}
