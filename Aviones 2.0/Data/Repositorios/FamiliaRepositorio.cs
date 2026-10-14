using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Aviones_2._0.Data.Repositorios
{
    public class FamiliaRepositorio
    {
        public List<Familia> Listar()
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<Familia>("SELECT IdFamilia, Nombre FROM dbo.Familias ORDER BY Nombre").ToList();
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
                        "INSERT INTO dbo.Familias (Nombre) VALUES (@nombre); SELECT CAST(SCOPE_IDENTITY() AS int);",
                        new { nombre });
                }
                catch (SqlException ex) when (SqlErrores.EsDuplicado(ex))
                {
                    throw new ReglaNegocioException($"Ya existe la familia \"{nombre}\".");
                }
            }
        }

        public void Renombrar(int idFamilia, string nombre)
        {
            nombre = Validar(nombre);
            using (var cn = Db.Abrir())
            {
                try
                {
                    cn.Execute("UPDATE dbo.Familias SET Nombre = @nombre WHERE IdFamilia = @idFamilia", new { idFamilia, nombre });
                }
                catch (SqlException ex) when (SqlErrores.EsDuplicado(ex))
                {
                    throw new ReglaNegocioException($"Ya existe la familia \"{nombre}\".");
                }
            }
        }

        public void Eliminar(int idFamilia)
        {
            using (var cn = Db.Abrir())
            {
                int materiales = cn.ExecuteScalar<int>("SELECT COUNT(*) FROM dbo.Materiales WHERE IdFamilia = @idFamilia", new { idFamilia });
                if (materiales > 0)
                {
                    throw new ReglaNegocioException($"La familia tiene {materiales} materiales. Elimínelos o muévalos primero.");
                }
                cn.Execute("DELETE FROM dbo.Familias WHERE IdFamilia = @idFamilia", new { idFamilia });
            }
        }

        private static string Validar(string nombre)
        {
            nombre = nombre?.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ReglaNegocioException("El nombre de la familia no puede quedar vacío.");
            }
            return nombre;
        }
    }
}
