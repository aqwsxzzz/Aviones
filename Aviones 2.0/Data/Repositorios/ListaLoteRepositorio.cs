using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Aviones_2._0.Data.Repositorios
{
    /// <summary>
    /// Una lista de nombres que usan los lotes: los fabricantes o las ubicaciones.
    /// Las dos funcionan igual; sólo cambia la tabla.
    /// </summary>
    public class ListaLoteRepositorio
    {
        private readonly string tabla;
        private readonly string columnaId;
        private readonly string elemento;

        private ListaLoteRepositorio(string tabla, string columnaId, string elemento)
        {
            this.tabla = tabla;
            this.columnaId = columnaId;
            this.elemento = elemento;
        }

        public static ListaLoteRepositorio Fabricantes() => new ListaLoteRepositorio("Fabricantes", "IdFabricante", "el fabricante");

        public static ListaLoteRepositorio Ubicaciones() => new ListaLoteRepositorio("Ubicaciones", "IdUbicacion", "la ubicación");

        public List<ElementoLista> Listar()
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<ElementoLista>($"SELECT {columnaId} AS Id, Nombre FROM dbo.{tabla} ORDER BY Nombre").ToList();
            }
        }

        public int Crear(string nombre)
        {
            nombre = Validar(nombre);
            using (var cn = Db.Abrir())
            {
                try
                {
                    return cn.ExecuteScalar<int>(
                        $"INSERT INTO dbo.{tabla} (Nombre) VALUES (@nombre); SELECT CAST(SCOPE_IDENTITY() AS int);",
                        new { nombre });
                }
                catch (SqlException ex) when (SqlErrores.EsDuplicado(ex))
                {
                    throw new ReglaNegocioException($"Ya existe {elemento} \"{nombre}\".");
                }
            }
        }

        /// <summary>Le cambia el nombre en todos los lotes que lo usan.</summary>
        public void Renombrar(int id, string nombre)
        {
            nombre = Validar(nombre);
            using (var cn = Db.Abrir())
            {
                try
                {
                    cn.Execute($"UPDATE dbo.{tabla} SET Nombre = @nombre WHERE {columnaId} = @id", new { id, nombre });
                }
                catch (SqlException ex) when (SqlErrores.EsDuplicado(ex))
                {
                    throw new ReglaNegocioException($"Ya existe {elemento} \"{nombre}\".");
                }
            }
        }

        public void Eliminar(int id)
        {
            using (var cn = Db.Abrir())
            {
                int lotes = cn.ExecuteScalar<int>($"SELECT COUNT(*) FROM dbo.Lotes WHERE {columnaId} = @id", new { id });
                if (lotes > 0)
                {
                    throw new ReglaNegocioException($"Hay {lotes} lotes con {elemento}. Cámbielo en esos lotes primero, o renómbrelo.");
                }
                cn.Execute($"DELETE FROM dbo.{tabla} WHERE {columnaId} = @id", new { id });
            }
        }

        private string Validar(string nombre)
        {
            nombre = nombre?.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ReglaNegocioException($"El nombre de {elemento} no puede quedar vacío.");
            }
            if (nombre.Length > 100)
            {
                throw new ReglaNegocioException("El nombre no puede tener más de 100 letras.");
            }
            return nombre;
        }
    }
}
