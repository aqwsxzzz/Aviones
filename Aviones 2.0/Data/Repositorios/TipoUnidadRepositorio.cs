using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Aviones_2._0.Data.Repositorios
{
    public class TipoUnidadRepositorio
    {
        public List<TipoUnidad> Listar()
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<TipoUnidad>("SELECT IdTipoUnidad, Nombre FROM dbo.TiposUnidad ORDER BY Nombre").ToList();
            }
        }

        public int Crear(string nombre)
        {
            nombre = nombre?.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ReglaNegocioException("El tipo de unidad no puede quedar vacío.");
            }
            using (var cn = Db.Abrir())
            {
                try
                {
                    return cn.ExecuteScalar<int>(
                        "INSERT INTO dbo.TiposUnidad (Nombre) VALUES (@nombre); SELECT CAST(SCOPE_IDENTITY() AS int);",
                        new { nombre });
                }
                catch (SqlException ex) when (SqlErrores.EsDuplicado(ex))
                {
                    throw new ReglaNegocioException($"Ya existe el tipo \"{nombre}\".");
                }
            }
        }

        public void Renombrar(int idTipoUnidad, string nombre)
        {
            nombre = nombre?.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ReglaNegocioException("El tipo de unidad no puede quedar vacío.");
            }
            using (var cn = Db.Abrir())
            {
                try
                {
                    cn.Execute("UPDATE dbo.TiposUnidad SET Nombre = @nombre WHERE IdTipoUnidad = @idTipoUnidad", new { idTipoUnidad, nombre });
                }
                catch (SqlException ex) when (SqlErrores.EsDuplicado(ex))
                {
                    throw new ReglaNegocioException($"Ya existe el tipo \"{nombre}\".");
                }
            }
        }

        public void Eliminar(int idTipoUnidad)
        {
            using (var cn = Db.Abrir())
            {
                int materiales = cn.ExecuteScalar<int>("SELECT COUNT(*) FROM dbo.Materiales WHERE IdTipoUnidad = @idTipoUnidad", new { idTipoUnidad });
                if (materiales > 0)
                {
                    throw new ReglaNegocioException($"Hay {materiales} materiales con este tipo de unidad.");
                }
                cn.Execute("DELETE FROM dbo.TiposUnidad WHERE IdTipoUnidad = @idTipoUnidad", new { idTipoUnidad });
            }
        }
    }
}
