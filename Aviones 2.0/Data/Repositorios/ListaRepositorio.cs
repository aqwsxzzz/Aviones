using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Aviones_2._0.Data.Repositorios
{
    /// <summary>
    /// Una lista simple de nombres: fabricantes y ubicaciones (de los lotes) o vendedores (de los pedidos).
    /// Todas funcionan igual; sólo cambian la tabla y dónde se usa.
    /// </summary>
    public class ListaRepositorio
    {
        private readonly string tabla;
        private readonly string columnaId;
        private readonly string elemento;
        private readonly string tablaUso;
        private readonly string usos;

        private ListaRepositorio(string tabla, string columnaId, string elemento, string tablaUso, string usos)
        {
            this.tabla = tabla;
            this.columnaId = columnaId;
            this.elemento = elemento;
            this.tablaUso = tablaUso;
            this.usos = usos;
        }

        public static ListaRepositorio Fabricantes() => new ListaRepositorio("Fabricantes", "IdFabricante", "el fabricante", "Lotes", "lotes");

        public static ListaRepositorio Ubicaciones() => new ListaRepositorio("Ubicaciones", "IdUbicacion", "la ubicación", "Lotes", "lotes");

        public static ListaRepositorio Vendedores() => new ListaRepositorio("Vendedores", "IdVendedor", "el vendedor", "PedidosCompra", "pedidos");

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

        /// <summary>Le cambia el nombre en todo lo que lo usa.</summary>
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
                int enUso = cn.ExecuteScalar<int>($"SELECT COUNT(*) FROM dbo.{tablaUso} WHERE {columnaId} = @id", new { id });
                if (enUso > 0)
                {
                    throw new ReglaNegocioException($"Hay {enUso} {usos} con {elemento}. Cámbielo en esos {usos} primero, o renómbrelo.");
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
