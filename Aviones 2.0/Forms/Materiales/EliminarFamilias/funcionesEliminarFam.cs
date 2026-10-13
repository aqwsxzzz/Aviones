using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace Aviones_2._0.Forms.Materiales.EliminarFamilias
{
    internal class funcionesEliminarFam
    {
        private Common.ConexionSQL SQLconnect = new Common.ConexionSQL();

        public void mostrarMaterialesPorFamilia(string nombreFamilia, DataGridView dgvMaterialesAMostrar)
        {
            string idFamilia = "";
            string query1 = "Select IdFamilia from Familiares where Familia ='" + nombreFamilia + "';";
            SqlCommand comando1 = new SqlCommand(query1, SQLconnect.establecerConexion());
            SqlDataReader reader1 = comando1.ExecuteReader();

            while (reader1.Read())
            {
                idFamilia = reader1["IdFamilia"].ToString();
            }
            SQLconnect.cerrarconexion();

            dgvMaterialesAMostrar.DataSource = null;
            string query2 = "select F.Familia, M.Grupo, M.Caracteristica, M.Color, M.Medidas, M.Codigo, A.Fabricante, M.Estado, A.Ubicacion, A.Cantidad, A.Disponible, M.Tipo, A.Valor, M.IdMaterial, A.IdAcopio from Familiares F Inner Join Materiales M on F.IdFamilia =M.idFamilia Inner join Acopio A on M.IdMaterial = A.IdMaterial where M.idFamilia ='" + idFamilia + "';";
            SqlDataAdapter adapter1 = new SqlDataAdapter(query2, SQLconnect.establecerConexion());
            DataTable dt = new DataTable();
            adapter1.Fill(dt);

            dt.Columns.Add("SortOrder", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                if (row["Grupo"].ToString() == "Helice")
                {
                    row["SortOrder"] = row["Medidas"].ToString();
                }
                else
                {
                    row["SortOrder"] = row["Caracteristica"].ToString();
                }
            }

            dt.DefaultView.Sort = "Familia ASC, Grupo ASC, SortOrder ASC";
            dgvMaterialesAMostrar.DataSource = dt;
            SQLconnect.cerrarconexion();
        }

        public void eliminarFamilia(string nombreFamilia)
        {
            string query = "Delete from Familiares where Familia ='" + nombreFamilia + "';";
            SqlCommand comando = new SqlCommand(query, SQLconnect.establecerConexion());
            SqlDataReader reader = comando.ExecuteReader();

            while (reader.Read()) { }
            SQLconnect.cerrarconexion();
        }

        public void mostrarFamilias(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            string query = "SELECT Familia FROM Familiares";
            SqlCommand command = new SqlCommand(query, SQLconnect.establecerConexion());

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBox.Items.Add(reader["Familia"].ToString());
            }
            reader.Close();

            SQLconnect.cerrarconexion();
        }

        public void modificarFamiliaMaterial(string nombreFamilia, string idMaterial)
        {
            string idFamilia = "";
            string query = "Select IdFamilia from Familiares where Familia ='" + nombreFamilia + "';";
            SqlCommand comando = new SqlCommand(query, SQLconnect.establecerConexion());
            SqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                idFamilia = reader["IdFamilia"].ToString();
            }
            reader.Close();

            SQLconnect.cerrarconexion();
            if (idFamilia != "")
            {
                string query2 = "Update Materiales SET idFamilia = " + idFamilia + " where IdMaterial = " + idMaterial + ";";
                SqlCommand comando2 = new SqlCommand(query2, SQLconnect.establecerConexion());
                int reader2 = comando2.ExecuteNonQuery();
                SQLconnect.cerrarconexion();
            }
        }

        //Revisa si el material enviado tiene algun vinculo con algun modelo previo a ser eliminado. Si tiene vinculo da aviso al usuario para saber que prefiere hacer.
        public void eliminarMaterialPorFamilia(string IdMaterial)
        {
            string query = "Select COUNT(*) from MaterialesModelo where IdMaterial ='" + IdMaterial + "';";
            SqlCommand comando = new SqlCommand(query, SQLconnect.establecerConexion());
            int count = (int)comando.ExecuteScalar();
            SQLconnect.cerrarconexion();

            if (count == 0)
            {
                string query2 = "Delete from Materiales where IdMaterial ='" + IdMaterial + "';";
                SqlCommand comando2 = new SqlCommand(query2, SQLconnect.establecerConexion());
                comando2.ExecuteNonQuery();
                SQLconnect.cerrarconexion();
            }
            else
            {
                string matInfo = "";
                string query3 = "Select Grupo, Caracteristica, Medidas, Codigo from Materiales where IdMaterial ='" + IdMaterial + "';";
                SqlCommand comando3 = new SqlCommand(query3, SQLconnect.establecerConexion());
                SqlDataReader reader3 = comando3.ExecuteReader();

                while (reader3.Read())
                {
                    matInfo += reader3.GetString(0) + " " + reader3.GetString(1) + " " + reader3.GetString(2) + " " + reader3.GetString(3);
                }
                reader3.Close();
                SQLconnect.cerrarconexion();
                DialogResult result = MessageBox.Show("El material " + matInfo + " esta siendo utilizado en algunos modelos, desea eliminarlo igualmente?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string query4 = "Delete from Materiales where IdMaterial ='" + IdMaterial + "';";
                    SqlCommand comando4 = new SqlCommand(query4, SQLconnect.establecerConexion());
                    comando4.ExecuteNonQuery();
                    SQLconnect.cerrarconexion();

                    string query5 = "Delete from MaterialesModelo where IdMaterial ='" + IdMaterial + "';";
                    SqlCommand comando5 = new SqlCommand(query5, SQLconnect.establecerConexion());
                    comando5.ExecuteNonQuery();
                    SQLconnect.cerrarconexion();
                }
            }
        }
    }
}