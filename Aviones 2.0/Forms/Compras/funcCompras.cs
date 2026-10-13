using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Aviones_2._0.Forms.Compras
{
    internal class funcCompras
    {
        private Common.ConexionSQL SQLconnect = new Common.ConexionSQL();

        public void cargarMaterialesPorFamilia(string familia, DataGridView dgvMateriales)
        {
            // Limpiar los registros previos en el DataGridView
            dgvMateriales.DataSource = null;
            string idFamilia = string.Empty;

            string query1 = "Select IdFamilia from Familiares where Familia = @Familia";
            using (SqlConnection conn = SQLconnect.establecerConexion())
            using (SqlCommand command1 = new SqlCommand(query1, conn))
            {
                command1.Parameters.AddWithValue("@Familia", familia);
                object famObj = command1.ExecuteScalar();
                if (famObj != null && famObj != DBNull.Value)
                {
                    idFamilia = famObj.ToString();
                }
            }

            if (string.IsNullOrWhiteSpace(idFamilia))
            {
                dgvMateriales.DataSource = new DataTable();
                return;
            }

            // Verificar si la columna IdAcopioUltimo existe en Materiales
            bool tieneIdAcopioUltimo = false;
            using (SqlConnection conn = SQLconnect.establecerConexion())
            using (SqlCommand chk = new SqlCommand("SELECT COUNT(*) FROM sys.columns WHERE [object_id]=OBJECT_ID('dbo.Materiales') AND name='IdAcopioUltimo'", conn))
            {
                object obj = chk.ExecuteScalar();
                int c = 0;
                if (obj != null && obj != DBNull.Value) int.TryParse(obj.ToString(), out c);
                tieneIdAcopioUltimo = c > 0;
            }

            // Construir la consulta SQL basada en si existe IdAcopioUltimo
            string idAcopioExpr = tieneIdAcopioUltimo ? "M.IdAcopioUltimo AS IdAcopio" : "0 AS IdAcopio";
            string query = $"select F.Familia, M.Grupo, M.Caracteristica, M.Color, M.Medidas, M.Codigo, M.Fabricante, M.Estado, M.Ubicacion, M.Cantidad, M.Disponible, M.Tipo, M.Valor, M.IdMaterial, {idAcopioExpr} " +
                "from Familiares F Inner Join Materiales M on F.IdFamilia = M.idFamilia where M.idFamilia = @IdFamilia;";

            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn2 = SQLconnect.establecerConexion())
                using (SqlCommand cmd = new SqlCommand(query, conn2))
                {
                    cmd.Parameters.AddWithValue("@IdFamilia", idFamilia);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en consulta: {ex.Message}");
                dt = new DataTable();
            }

            // Guardar: sólo proceder si las columnas esperadas existen
            if (dt.Columns.Contains("Grupo") && dt.Columns.Contains("Caracteristica") && dt.Columns.Contains("Medidas"))
            {
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

                // If Familia column exists, include it in sort; otherwise sort by Grupo/SortOrder
                if (dt.Columns.Contains("Familia"))
                    dt.DefaultView.Sort = "Familia ASC, Grupo ASC, SortOrder ASC";
                else
                    dt.DefaultView.Sort = "Grupo ASC, SortOrder ASC";
            }

            dgvMateriales.DataSource = dt;
            SQLconnect.cerrarconexion();
        }

        public void mostrarFamilias(ListBox lisboxFamiliasSel)
        {
            lisboxFamiliasSel.Items.Clear();

            // Traer todas las familias (sin excluir) para que se muestren en el listado de compras.
            // Si se desea filtrar algunas familias se puede reintroducir la condición aquí.
            string query = "SELECT Familia FROM Familiares";

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
            int maxVisibleItems = 10; // Mostrar hasta 10 ítems antes de habilitar scrollbar

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
            int maxVisibleItems = 10;
            int extraRows = 3; // espacio adicional para título / márgenes
            int panelPadding = 10; // Margen adicional para evitar recortes visuales

            // Si hay menos que maxVisibleItems, panel crece con los items; si hay más, panel se ajusta al máximo y ListBox tendrá scrollbar
            int visibleItems = Math.Min(totalItems, maxVisibleItems);
            int panelHeight = (visibleItems + extraRows) * itemHeight + panelPadding;

            panel.Height = panelHeight;  // Ajustar la altura del Panel
        }

        // Devuelve la cantidad pendiente de compra para un material (0 si no existe o es NULL)
        public decimal ObtenerCantidadAComprar(string idMaterial)
        {
            if (string.IsNullOrWhiteSpace(idMaterial)) return 0m;

            string query = "SELECT Cantacomprar FROM Materiales WHERE IdMaterial = @Id";
            try
            {
                using (SqlConnection conn = SQLconnect.establecerConexion())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", idMaterial);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        if (decimal.TryParse(result.ToString(), out decimal cant)) return cant;
                    }
                }
            }
            catch { }

            return 0m;
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
                if (!newReader.IsDBNull(newReader.GetOrdinal("ImageData")))  // Verifica si es NULL
                // if (newReader["ImageData"].ToString() != "")
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

        public void mostrarMateriales(DataGridView dgvMateriales)
        {
            dgvMateriales.DataSource = null;
            string query = "SELECT F.Familia, M.Grupo, M.Caracteristica, M.Color, M.Medidas, M.Codigo, " +
                 "M.Fabricante, M.Estado, M.Ubicacion, M.Cantidad, M.Disponible, M.Tipo, M.Valor, " +
                 "M.IdMaterial, M.IdAcopioUltimo AS IdAcopio " +
                 "FROM Familiares F " +
                 "INNER JOIN Materiales M ON F.IdFamilia = M.IdFamilia " +
                 "WHERE (F.Familia NOT IN ('Aeronaves', 'OtraFamiliaAExcluir')) " +
                 "AND NOT (F.Familia = 'Sistema de poder' AND M.Grupo = 'Motor') " +
                 "AND M.Disponible <= 1";

            SqlDataAdapter adapter = new SqlDataAdapter(query, SQLconnect.establecerConexion());
            DataTable dt = new DataTable();
            try
            {
                adapter.Fill(dt);
            }
            catch (SqlException)
            {
                try
                {
                    string safeQuery = "SELECT F.Familia, M.Grupo, M.Caracteristica, M.Color, M.Medidas, M.Codigo, M.Fabricante, M.Estado, M.Ubicacion, M.Cantidad, M.Disponible, M.Tipo, M.Valor, M.IdMaterial, ISNULL(M.IdAcopioUltimo,0) AS IdAcopio FROM Materiales M JOIN Familiares F ON M.IdFamilia = F.IdFamilia WHERE M.Disponible <= 1";
                    using (SqlConnection conn3 = SQLconnect.establecerConexion())
                    using (SqlCommand cmdSafe = new SqlCommand(safeQuery, conn3))
                    using (SqlDataAdapter safeAdapter = new SqlDataAdapter(cmdSafe))
                    {
                        dt.Clear();
                        safeAdapter.Fill(dt);
                    }
                }
                catch
                {
                    dt = new DataTable();
                }
            }

            // Guardar: sólo proceder si las columnas esperadas existen
            if (dt.Columns.Contains("Grupo") && dt.Columns.Contains("Caracteristica") && dt.Columns.Contains("Medidas"))
            {
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

                if (dt.Columns.Contains("Familia"))
                    dt.DefaultView.Sort = "Familia ASC, Grupo ASC, SortOrder ASC";
                else
                    dt.DefaultView.Sort = "Grupo ASC, SortOrder ASC";
            }

            dgvMateriales.DataSource = dt;
            SQLconnect.cerrarconexion();
        }

        public void mostrarFaltantes(DataGridView dgvMateriales)
        {
            dgvMateriales.DataSource = null;

            // Verificar si la columna IdAcopioUltimo existe en Materiales
            bool tieneIdAcopioUltimo = false;
            using (SqlConnection conn = SQLconnect.establecerConexion())
            using (SqlCommand chk = new SqlCommand("SELECT COUNT(*) FROM sys.columns WHERE [object_id]=OBJECT_ID('dbo.Materiales') AND name='IdAcopioUltimo'", conn))
            {
                object obj = chk.ExecuteScalar();
                int c = 0;
                if (obj != null && obj != DBNull.Value) int.TryParse(obj.ToString(), out c);
                tieneIdAcopioUltimo = c > 0;
            }

            // Construir la consulta para materiales faltantes
            // Cargar materiales con bajo stock (Cantidad <= 1), sin filtrar por Cantacomprar
            // ShowAll materiales que tengan cantidad<=1, independientemente de si ya se solicitó compra
            string idAcopioExpr = tieneIdAcopioUltimo ? "M.IdAcopioUltimo AS IdAcopio" : "0 AS IdAcopio";
            string query = $"SELECT F.Familia, M.Grupo, M.Caracteristica, M.Color, M.Medidas, M.Codigo, M.Fabricante, M.Estado, M.Ubicacion, M.Cantidad, M.Disponible, M.Tipo, CAST(ISNULL(M.Valor, 0) AS DECIMAL(18,2)) AS Valor, M.IdMaterial, {idAcopioExpr}, ISNULL(M.Cantacomprar, 0) AS CantidadAComprar " +
                "FROM Familiares F " +
                "INNER JOIN Materiales M ON F.IdFamilia = M.IdFamilia " +
                "WHERE M.Cantidad <= 1 " +
                "ORDER BY F.Familia ASC, M.Grupo ASC, M.Caracteristica ASC";

            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = SQLconnect.establecerConexion())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en mostrarFaltantes: {ex.Message}");
                dt = new DataTable();
            }

            dgvMateriales.DataSource = dt;
            SQLconnect.cerrarconexion();
        }

        public bool guardarCompra(string idMaterial, decimal cantidadSolicitada, decimal valorUnitario, decimal total)
        {
            // Nueva política: no insertar en la tabla Compras. Mantener el estado de compra en la tabla Materiales
            try
            {
                string query = @"UPDATE Materiales SET Cantacomprar = ISNULL(Cantacomprar,0) + @CantidadSolicitada WHERE IdMaterial = @IdMaterial";

                using (SqlConnection conn = SQLconnect.establecerConexion())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdMaterial", idMaterial);
                    cmd.Parameters.AddWithValue("@CantidadSolicitada", cantidadSolicitada);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al actualizar Cantacomprar en Materiales: {ex.Message}");
                return false;
            }
            finally
            {
                SQLconnect.cerrarconexion();
            }
        }

        // Cargar los materiales que tienen Cantacomprar > 0 para mostrar en el grid de modificaciones
        public void cargarComprasPendientes(DataGridView dgv)
        {
            dgv.DataSource = null; // Limpiar el DataGridView primero
            string query = @"
                SELECT 
                    F.Familia, 
                    M.Grupo, 
                    M.Caracteristica, 
                    M.Color, 
                    M.Medidas, 
                    M.Codigo, 
                    M.Fabricante, 
                    M.Estado, 
                    M.Ubicacion, 
                    M.Cantidad, 
                    M.Disponible,
                    M.Tipo, 
                    CAST(ISNULL(M.Valor, 0) AS DECIMAL(18,2)) AS Valor, 
                    M.IdMaterial,
                    ISNULL(M.IdAcopioUltimo, 0) AS IdAcopio,
                    CAST(ISNULL(M.Cantacomprar, 0) AS DECIMAL(18,2)) AS CantidadAComprar
                FROM Materiales M 
                INNER JOIN Familiares F ON M.IdFamilia = F.IdFamilia 
                WHERE M.Cantacomprar IS NOT NULL AND CAST(ISNULL(M.Cantacomprar, 0) AS DECIMAL(18,2)) > 0
                ORDER BY F.Familia ASC, M.Grupo ASC, M.Caracteristica ASC";

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = SQLconnect.establecerConexion())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
                System.Diagnostics.Debug.WriteLine($"cargarComprasPendientes: Se cargaron {dt.Rows.Count} registros");
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en cargarComprasPendientes: {ex.Message}");
                dt = new DataTable();
            }

            dgv.DataSource = dt;
            System.Diagnostics.Debug.WriteLine($"cargarComprasPendientes: dgv.Rows.Count = {dgv.Rows.Count}");
            SQLconnect.cerrarconexion();
        }
    }
}