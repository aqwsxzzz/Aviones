using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace Aviones_2._0.Common
{
    internal class ConexionSQL
    {
        private SqlConnection con = null;

        private string Cadenaconexion = "Data Source=localhost;" +
            "Initial Catalog=Aviones;Integrated Security=True;MultipleActiveResultSets=True;";

        public SqlConnection establecerConexion()
        {
            try
            {
                // If connection is null, closed or broken create a new one and open it
                if (con == null || con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
                {
                    con = new SqlConnection(Cadenaconexion);
                    con.Open();
                }
                // If it's already open, just return it
            }
            catch (Exception ex)
            {
                MessageBox.Show("No conectado, error:" + ex.ToString());
            }
            return con;
        }

        public void cerrarconexion()
        {
            try
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                    con = null;
                }
            }
            catch (Exception)
            {
                // ignore close errors
            }
        }
    }
}