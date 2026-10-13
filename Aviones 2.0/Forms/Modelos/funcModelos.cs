using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Drawing;
using System.IO;
using System.Collections;

namespace Aviones_2._0.Forms.Modelos
{
    internal class funcModelos
    {
        private Common.ConexionSQL SQLconnect = new Common.ConexionSQL();

        public void mostrarModelos(DataGridView dgvModelos)
        {
            dgvModelos.DataSource = null;
            string query = "select IdModelo, Modelo, Alias, Estado, Desde from Modelos";
            SqlDataAdapter adapter = new SqlDataAdapter(query, SQLconnect.establecerConexion());
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dt.DefaultView.Sort = "Modelo ASC";
            dgvModelos.DataSource = dt;
            SQLconnect.cerrarconexion();
        }

        public void mostrarFamilias(ListBox lisboxFamiliasSel)
        {
            lisboxFamiliasSel.Items.Clear();

            string query = "SELECT Familia FROM Familiares WHERE Familia NOT IN ('Aeronaves') ORDER BY Familia ASC";

            SqlCommand command = new SqlCommand(query, SQLconnect.establecerConexion());

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lisboxFamiliasSel.Items.Add(reader["Familia"].ToString());
            }
            reader.Close();

            SQLconnect.cerrarconexion();

            // Ajustar la altura del ListBox después de llenarlo
            AjustarAlturaListBox(lisboxFamiliasSel);
        }

        public void AjustarAlturaListBox(ListBox listBox)
        {
            int itemCount = listBox.Items.Count;
            int maxVisibleItems = 15;

            // Evitar ajuste automático que provoque recortes visuales
            listBox.IntegralHeight = false;

            if (itemCount > 0)
            {
                if (itemCount <= maxVisibleItems)
                {
                    listBox.Height = itemCount * listBox.ItemHeight + 10; // Ajusta el alto según la cantidad de ítems
                    listBox.ScrollAlwaysVisible = false;
                }
                else
                {
                    // Limitar la altura a maxVisibleItems y dejar que aparezca el scrollbar
                    listBox.Height = maxVisibleItems * listBox.ItemHeight + 10;
                    listBox.ScrollAlwaysVisible = true;
                }
            }
        }

        public void AjustarTamañoPanel(Panel panel, ListBox listBox)
        {
            int itemHeight = listBox.ItemHeight; // Altura de un ítem del ListBox
            int totalItems = listBox.Items.Count; // Total de ítems en el ListBox
            int maxVisibleItems = 15;
            int extraRows = 3; // espacio adicional para título / márgenes
            int panelPadding = 10; // Margen adicional para evitar recortes visuales

            // Si hay menos que maxVisibleItems, panel crece con los items; si hay más, panel se ajusta al máximo y ListBox tendrá scrollbar
            int visibleItems = Math.Min(totalItems, maxVisibleItems);
            int panelHeight = (visibleItems + extraRows) * itemHeight + panelPadding;

            panel.Height = panelHeight;  // Ajustar la altura del Panel
        }

        private void cargarMaterialesporFamilia(string familia, DataGridView dgv)
        {
            dgv.DataSource = null;

            string query = @"
        SELECT F.Familia, M.Grupo, M.Caracteristica, M.Color, M.Medidas, M.Codigo,
               M.Fabricante, M.Estado, M.Ubicacion, M.Cantidad, M.Disponible,
               M.Tipo, M.Valor, M.IdMaterial
        FROM Familiares F
        INNER JOIN Materiales M ON F.IdFamilia = M.idFamilia
        WHERE F.Familia = @Familia";

            SqlDataAdapter adapter = new SqlDataAdapter(query, SQLconnect.establecerConexion());
            adapter.SelectCommand.Parameters.AddWithValue("@Familia", familia);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dgv.DataSource = dt;

            SQLconnect.cerrarconexion();
        }

        public void cargarMaterialesPorFamilia(string familia, DataGridView dgvMateriales)
        {
            // Limpiar los registros previos en el DataGridView
            dgvMateriales.DataSource = null;

            string query = @"
                SELECT F.Familia, M.Grupo, M.Caracteristica, M.Color, M.Medidas, M.Codigo,
                       M.Fabricante, M.Estado, M.Ubicacion, M.Cantidad, M.Disponible,
                       M.Tipo, M.Valor, M.IdMaterial
                FROM Familiares F
                INNER JOIN Materiales M ON F.IdFamilia = M.idFamilia
                WHERE F.Familia = @Familia
                ORDER BY F.Familia ASC, M.Grupo ASC, 
                         CASE WHEN M.Grupo = 'Helice' THEN M.Medidas ELSE M.Caracteristica END ASC";

            SqlDataAdapter adapter = new SqlDataAdapter(query, SQLconnect.establecerConexion());
            adapter.SelectCommand.Parameters.AddWithValue("@Familia", familia);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

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
            dgvMateriales.DataSource = dt;
            SQLconnect.cerrarconexion();
        }

        public void BuscarFotoPorIdMaterial(string IdMaterial, PictureBox picBox)
        {
            object bytesDeImagenDesdeBD;
            byte[] bytesDeImagen;
            Image imagen;
            string query = "select ImageData from Materiales where IdMaterial='" + IdMaterial + "';";
            SqlCommand comando = new SqlCommand(query, SQLconnect.establecerConexion());
            SqlDataReader newReader = comando.ExecuteReader();
            Image ConvertirBytesAImagen(byte[] bytesDI)
            {
                using (MemoryStream ms = new MemoryStream(bytesDI))
                {
                    return Image.FromStream(ms);
                }
            }

            while (newReader.Read())
            {
                if (newReader["ImageData"].ToString() != "")
                {
                    bytesDeImagenDesdeBD = newReader["ImageData"];
                    bytesDeImagen = (byte[])bytesDeImagenDesdeBD;
                    imagen = ConvertirBytesAImagen(bytesDeImagen);
                    picBox.Image = imagen;
                }
                else picBox.Image = null;
            }

            SQLconnect.cerrarconexion();
        }

        private Image ConvertirBytesAImagen(byte[] bytesDI)
        {
            using (MemoryStream ms = new MemoryStream(bytesDI))
            {
                return Image.FromStream(ms);
            }
        }

        public void GuardarModelos(TextBox paraModelo, TextBox paraAlias, TextBox paraEstado, TextBox paraDesde, byte[] paraImagen, string[] acopiosArray, string[] cantidadArray, string[] cantidadDisponibleActualizada)
        {
            string Query;
            if (paraImagen != null)
            {
                Query = "insert into Modelos (Modelo,Alias, Estado, Desde, Foto)  values  (@Modelo,@Alias, @Estado, @Desde, @Foto);";
            }
            else
            {
                Query = "insert into Modelos (Modelo,Alias, Estado, Desde)  values  (@Modelo,@Alias, @Estado, @Desde);";
            }

            using (SqlCommand elComando = new SqlCommand(Query, SQLconnect.establecerConexion()))
            {
                elComando.Parameters.AddWithValue("@Modelo", paraModelo.Text);
                elComando.Parameters.AddWithValue("@Alias", paraAlias.Text);
                elComando.Parameters.AddWithValue("@Estado", paraEstado.Text);
                elComando.Parameters.AddWithValue("@Desde", paraDesde.Text);
                if (paraImagen != null)
                {
                    elComando.Parameters.AddWithValue("@Foto", paraImagen);
                }
                SqlDataReader tworeader = elComando.ExecuteReader();

                while (tworeader.Read())
                {
                }
                tworeader.Close();
            }

            SQLconnect.cerrarconexion();

            string queryIdModelo = "SELECT idModelo FROM Modelos WHERE idModelo = (SELECT MAX(idModelo) FROM Modelos)";
            SqlCommand com = new SqlCommand(queryIdModelo, SQLconnect.establecerConexion());
            string ModId = null;
            SqlDataReader myreader = com.ExecuteReader();
            while (myreader.Read())
            {
                ModId = myreader["idModelo"].ToString();
            }
            SQLconnect.cerrarconexion();
            myreader.Close();

            for (int i = 0; i < acopiosArray.Length; i++)
            {
                string cantidadxModelo = cantidadArray[i];
                string MatId = acopiosArray[i];

                string queryInter = "insert into MaterialesModelo (IdModelo, IdMaterial, CantidadxModelo) values (@IdModelo, @IdMaterial, @CantidadxModelo);";
                using (SqlCommand cmdInter = new SqlCommand(queryInter, SQLconnect.establecerConexion()))
                {
                    cmdInter.Parameters.AddWithValue("@IdModelo", ModId);
                    cmdInter.Parameters.AddWithValue("@IdMaterial", MatId);
                    cmdInter.Parameters.AddWithValue("@CantidadxModelo", cantidadArray[i]);
                    cmdInter.ExecuteReader().Close();
                }
                SQLconnect.cerrarconexion();

                string queryActualizacionDisponible = "update Materiales set Disponible = @Disponible where IdMaterial = @IdMaterial;";
                using (SqlCommand cmdDisponible = new SqlCommand(queryActualizacionDisponible, SQLconnect.establecerConexion()))
                {
                    cmdDisponible.Parameters.AddWithValue("@Disponible", cantidadDisponibleActualizada[i]);
                    cmdDisponible.Parameters.AddWithValue("@IdMaterial", acopiosArray[i]);
                    cmdDisponible.ExecuteReader().Close();
                }
                SQLconnect.cerrarconexion();
            }
        }

        public void mostrarMaterialesxModelo(DataGridView dgvListadoMaterialesLis, string IdModelo, TextBox texTotal)

        {
            try
            {
                dgvListadoMaterialesLis.DataSource = null;

                string query = @"select Fam.Familia, Mat.Grupo, Mat.Caracteristica, Mat.Color, Mat.Medidas, Mat.Codigo, Mat.Fabricante, MatMod.CantidadxModelo, Mat.Tipo, Mat.IdMaterial,
                               MatMod.IdMaterialesModelo, Mat.Valor
                               from MaterialesModelo MatMod
                               join Materiales Mat on MatMod.IdMaterial = Mat.IdMaterial
                               join Familiares Fam on Mat.idFamilia = Fam.IdFamilia
                               where MatMod.IdModelo = @IdModelo";

                using (SqlConnection connection = SQLconnect.establecerConexion())
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdModelo", IdModelo); // Uso de parámetros

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvListadoMaterialesLis.DataSource = dt;

                        // Asignar valores a la columna "Seleccionado" (si ya está en el DataGridView)
                        if (dgvListadoMaterialesLis.Columns.Contains("Seleccionado"))
                        {
                            foreach (DataGridViewRow row in dgvListadoMaterialesLis.Rows)
                            {
                                row.Cells["Seleccionado"].Value = "Sí"; // Puedes cambiarlo según necesidad
                            }
                        }

                        // Sum the values in the "valor" column
                        /*decimal total = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["Valor"] != DBNull.Value)
                            {
                                total += (Convert.ToDecimal(row["Valor"]) * Convert.ToDecimal(row["CantidadxModelo"]));
                            }
                        }

                        texTotal.Text = total.ToString("N2"); // Format the total as a decimal with two decimal places*/
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se logró mostrar registros, error: " + ex.ToString());
            }
        }

        public void BuscarFotoPorIdModelo(string IdModelo, PictureBox picBox)
        {
            object bytesDeImagenDesdeBD;
            byte[] bytesDeImagen;
            Image imagen;
            string query = "select Foto from Modelos where IdModelo='" + IdModelo + "';";
            SqlCommand comando = new SqlCommand(query, SQLconnect.establecerConexion());
            SqlDataReader newReader = comando.ExecuteReader();
            Image ConvertirBytesAImagen(byte[] bytesDI)
            {
                using (MemoryStream ms = new MemoryStream(bytesDI))
                {
                    return Image.FromStream(ms);
                }
            }

            while (newReader.Read())
            {
                if (newReader["Foto"].ToString() != "")
                {
                    bytesDeImagenDesdeBD = newReader["Foto"];
                    bytesDeImagen = (byte[])bytesDeImagenDesdeBD;
                    imagen = ConvertirBytesAImagen(bytesDeImagen);
                    picBox.Image = imagen;
                }
                else picBox.Image = null;
            }
            newReader.Close();
            SQLconnect.cerrarconexion();
        }

        public void GuardarMaterialesXModeloEnModificacion(List<String> ListaMaterialesPorAcopio, List<String> CantidadDeMaterial, List<String> CantidadDisponiActualizada, string IdModeloEnModificacion)
        {
            for (int i = 0; i < ListaMaterialesPorAcopio.Count; i++)
            {
                string IdMaterialUsado = ListaMaterialesPorAcopio[i];

                string query2 = "Insert into MaterialesModelo (IdModelo, IdMaterial, CantidadxModelo) values (@IdModelo, @IdMaterial, @CantidadxModelo);";
                using (SqlCommand comando9 = new SqlCommand(query2, SQLconnect.establecerConexion()))
                {
                    comando9.Parameters.AddWithValue("@IdModelo", IdModeloEnModificacion);
                    comando9.Parameters.AddWithValue("@IdMaterial", IdMaterialUsado);
                    comando9.Parameters.AddWithValue("@CantidadxModelo", CantidadDeMaterial[i]);
                    comando9.ExecuteReader().Close();
                }
                SQLconnect.cerrarconexion();

                string query3 = "Update Materiales Set Disponible = @Disponible where IdMaterial = @IdMaterial;";
                using (SqlCommand comando10 = new SqlCommand(query3, SQLconnect.establecerConexion()))
                {
                    comando10.Parameters.AddWithValue("@Disponible", CantidadDisponiActualizada[i]);
                    comando10.Parameters.AddWithValue("@IdMaterial", ListaMaterialesPorAcopio[i]);
                    comando10.ExecuteReader().Close();
                }
                SQLconnect.cerrarconexion();
            }
        }

        public void ModificarModelo(string[] ColumnasDatosAModificar, string[] DatosAModificar, string ModeloId, byte[] paraImagen)
        {
            if (ColumnasDatosAModificar.Length > 0)
            {
                string datos = "";
                for (int i = 0; i < ColumnasDatosAModificar.Length; i++)
                {
                    if (i == (ColumnasDatosAModificar.Length - 1))
                    {
                        datos += ColumnasDatosAModificar[i] + " = '" + DatosAModificar[i] + "'";
                    }
                    else datos += ColumnasDatosAModificar[i] + " = '" + DatosAModificar[i] + "',";
                }
                string queryModificacion = "UPDATE Modelos SET " + datos + "where IdModelo ='" + ModeloId + "';";

                SqlCommand updateCommand = new SqlCommand(queryModificacion, SQLconnect.establecerConexion());
                SqlDataReader ModificacionReader = updateCommand.ExecuteReader();

                while (ModificacionReader.Read())
                {
                }
                ModificacionReader.Close();
                SQLconnect.cerrarconexion();

                if (paraImagen != null)
                {
                    string queryImagen = "UPDATE Modelos SET Foto = @Foto WHERE IdModelo = '" + ModeloId + "';";
                    using (SqlCommand elComando = new SqlCommand(queryImagen, SQLconnect.establecerConexion()))
                    {
                        elComando.Parameters.AddWithValue("@Foto", paraImagen);
                        SqlDataReader tworeader = elComando.ExecuteReader();

                        while (tworeader.Read())
                        {
                        }
                        tworeader.Close();
                    }
                    SQLconnect.cerrarconexion();
                }
            }
            else if (ColumnasDatosAModificar.Length == 0 && paraImagen != null)
            {
                string queryImagen = "UPDATE Modelos SET Foto = @Foto WHERE IdModelo = '" + ModeloId + "';";
                using (SqlCommand elComando = new SqlCommand(queryImagen, SQLconnect.establecerConexion()))
                {
                    elComando.Parameters.AddWithValue("@Foto", paraImagen);
                    SqlDataReader tworeader = elComando.ExecuteReader();

                    while (tworeader.Read())
                    {
                    }
                    tworeader.Close();
                }
                SQLconnect.cerrarconexion();
            }
        }

        public void DevolverMaterialReciclable(string IdMaterialesModelo, string CantidadADevolver, string IdMaterial)
        {
            if (IdMaterial != "No Reciclable")
            {
                string query = "Update Materiales SET Disponible = Disponible + @CantidadADevolver where IdMaterial = @IdMaterial;";
                using (SqlCommand myComando = new SqlCommand(query, SQLconnect.establecerConexion()))
                {
                    myComando.Parameters.AddWithValue("@CantidadADevolver", CantidadADevolver);
                    myComando.Parameters.AddWithValue("@IdMaterial", IdMaterial);
                    myComando.ExecuteReader().Close();
                }
                SQLconnect.cerrarconexion();
            }

            string query2 = "DELETE FROM MaterialesModelo where IdMaterialesModelo = @IdMaterialesModelo;";
            using (SqlCommand myComando2 = new SqlCommand(query2, SQLconnect.establecerConexion()))
            {
                myComando2.Parameters.AddWithValue("@IdMaterialesModelo", IdMaterialesModelo);
                myComando2.ExecuteReader().Close();
            }
            SQLconnect.cerrarconexion();
        }

    }
}