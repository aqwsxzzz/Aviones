using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Aviones_2._0.Data
{
    /// <summary>Punto único de acceso a la base de datos.</summary>
    public static class Db
    {
        private static string connectionString;

        /// <summary>
        /// Se lee de App.config ("Aviones"). Las pruebas la reemplazan para usar otra base.
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if (connectionString == null)
                {
                    connectionString = ConfigurationManager.ConnectionStrings["Aviones"]?.ConnectionString
                        ?? throw new InvalidOperationException("Falta la cadena de conexión 'Aviones' en App.config.");
                }
                return connectionString;
            }
            set { connectionString = value; }
        }

        public static SqlConnection Abrir()
        {
            var conexion = new SqlConnection(ConnectionString);
            conexion.Open();
            return conexion;
        }
    }
}
