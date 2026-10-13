using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using static System.Net.WebRequestMethods;

namespace Aviones_2._0.Forms.Materiales
{
    internal class funcMateriales
    {
        private Common.ConexionSQL SQLconnect = new Common.ConexionSQL();
        // Habilitar para diagnosticar el mapeo de filas (temporal)
        private bool debugMostrarFilas = false;

        public void mostrarMateriales(DataGridView dgvMateriales)
        {
            dgvMateriales.DataSource = null;
            // Selección directa desde Materiales (las columnas de Acopio fueron migradas a Materiales)
            // Construir la consulta detectando si la columna IdAcopioUltimo existe en la tabla Materiales.
            // Si no existe, seleccionar un 0 constante como IdAcopio para evitar excepciones.
            string queryBase =
                "SELECT F.Familia AS Familia, M.Grupo AS Grupo, M.Caracteristica AS Caracteristica, M.Color AS Color, " +
                "M.Medidas AS Medidas, M.Codigo AS Codigo, M.Fabricante AS Fabricante, M.Estado AS Estado, " +
                "M.Ubicacion AS Ubicacion, M.Cantidad AS Cantidad, M.Disponible AS Disponible, M.Tipo AS Tipo, " +
                "CAST(ISNULL(M.Valor,0) AS decimal(18,2)) AS Valor, M.IdMaterial AS IdMaterial, {ID_ACOPIO_EXPRESSION} CAST(ISNULL(M.Cantidad * M.Valor, 0) AS decimal(18,2)) AS Total " +
                "FROM Familiares F INNER JOIN Materiales M ON F.IdFamilia = M.IdFamilia;";

            // Llenar DataTable manualmente desde un DataReader para asegurar mapeo correcto y tipos
            DataTable dt = new DataTable();
            dt.Columns.Add("Familia", typeof(string));
            dt.Columns.Add("Grupo", typeof(string));
            dt.Columns.Add("Caracteristica", typeof(string));
            dt.Columns.Add("Color", typeof(string));
            dt.Columns.Add("Medidas", typeof(string));
            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Fabricante", typeof(string));
            dt.Columns.Add("Estado", typeof(string));
            dt.Columns.Add("Ubicacion", typeof(string));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Disponible", typeof(int));
            dt.Columns.Add("Tipo", typeof(string));
            dt.Columns.Add("Valor", typeof(decimal));
            dt.Columns.Add("IdMaterial", typeof(int));
            // No incluir columna intermedia de Acopio.IdMaterial que no se usa en el grid
            dt.Columns.Add("IdAcopio", typeof(int));
            dt.Columns.Add("Total", typeof(decimal));

            // Obtener conexión y detectar existencia de columna
            SqlConnection conn = SQLconnect.establecerConexion();
            bool tieneIdAcopio = false;
            try
            {
                using (SqlCommand chk = new SqlCommand("SELECT COUNT(*) FROM sys.columns WHERE [object_id]=OBJECT_ID('dbo.Materiales') AND name='IdAcopioUltimo'", conn))
                {
                    object o = chk.ExecuteScalar();
                    int c = 0;
                    if (o != null && o != DBNull.Value) int.TryParse(o.ToString(), out c);
                    tieneIdAcopio = c > 0;
                }
            }
            catch
            {
                // Si falla la comprobación, asumimos que no existe para evitar excepciones posteriores
                tieneIdAcopio = false;
            }

            string idAcopioExpr = tieneIdAcopio ? "M.IdAcopioUltimo AS IdAcopio, " : "0 AS IdAcopio, ";
            string query = queryBase.Replace("{ID_ACOPIO_EXPRESSION}", idAcopioExpr);

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int rowIndex = 0;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.AppendLine("Filas devueltas por la consulta:");
                    while (reader.Read())
                    {
                        string familia = reader["Familia"] == DBNull.Value ? string.Empty : reader["Familia"].ToString();
                        string grupo = reader["Grupo"] == DBNull.Value ? string.Empty : reader["Grupo"].ToString();
                        string caracteristica = reader["Caracteristica"] == DBNull.Value ? string.Empty : reader["Caracteristica"].ToString();
                        string color = reader["Color"] == DBNull.Value ? string.Empty : reader["Color"].ToString();
                        string medidas = reader["Medidas"] == DBNull.Value ? string.Empty : reader["Medidas"].ToString();
                        string codigo = reader["Codigo"] == DBNull.Value ? string.Empty : reader["Codigo"].ToString();
                        string fabricante = reader["Fabricante"] == DBNull.Value ? string.Empty : reader["Fabricante"].ToString();
                        string estado = reader["Estado"] == DBNull.Value ? string.Empty : reader["Estado"].ToString();
                        string ubicacion = reader["Ubicacion"] == DBNull.Value ? string.Empty : reader["Ubicacion"].ToString();
                        int cantidad = reader["Cantidad"] == DBNull.Value ? 0 : (int)Convert.ToDecimal(reader["Cantidad"]);
                        int disponible = reader["Disponible"] == DBNull.Value ? 0 : (int)Convert.ToDecimal(reader["Disponible"]);
                        string tipo = reader["Tipo"] == DBNull.Value ? string.Empty : reader["Tipo"].ToString();
                        decimal valor = reader["Valor"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["Valor"]);
                        int idMaterial = reader["IdMaterial"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdMaterial"]);
                        int idAcopio = reader["IdAcopio"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdAcopio"]);
                        decimal total = reader["Total"] == DBNull.Value ? cantidad * valor : Convert.ToDecimal(reader["Total"]);

                        // Añadir fila respetando las columnas del DataTable (sin columna AcopioIdMaterial)
                        dt.Rows.Add(familia, grupo, caracteristica, color, medidas, codigo, fabricante, estado, ubicacion, cantidad, disponible, tipo, valor, idMaterial, idAcopio, total);

                        // Agregar línea al diagnóstico
                        sb.AppendFormat("Row {0}: IdMaterial={1}, IdAcopio={2}, Familia='{3}', Grupo='{4}', Fabricante='{5}', Cant={6}, Valor={7}\n",
                            rowIndex, idMaterial, idAcopio, familia, grupo, fabricante, cantidad, valor);
                        rowIndex++;
                    }

                    if (debugMostrarFilas)
                    {
                        // Mostrar diagnostico corto para revisar mapping (temporal)
                        MessageBox.Show(sb.ToString(), "Diagnóstico - filas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            // Truncar los valores a 1 decimal sin redondear (mostrar exactamente una cifra después de la coma)
            foreach (DataRow row in dt.Rows)
            {
                // Asegurar que Valor y Total sean decimales y truncarlos a 1 decimal
                decimal val = row["Valor"] == DBNull.Value ? 0m : Convert.ToDecimal(row["Valor"]);
                decimal truncatedVal = Math.Truncate(val * 10m) / 10m;
                row["Valor"] = truncatedVal;

                decimal tot = row["Total"] == DBNull.Value ? 0m : Convert.ToDecimal(row["Total"]);
                decimal truncatedTot = Math.Truncate(tot * 10m) / 10m;
                row["Total"] = truncatedTot;
            }

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
            // Enlazar la DefaultView para respetar el orden aplicado
            dgvMateriales.DataSource = dt.DefaultView;
            SQLconnect.cerrarconexion();
        }

        public void GuardarFamilia(TextBox paraFam)
        {
            string queryNuevaFam = "INSERT into Familiares (Familia) values ('" + paraFam.Text + "')";
            SqlCommand myComando = new SqlCommand(queryNuevaFam, SQLconnect.establecerConexion());
            SqlDataReader myReader;
            myReader = myComando.ExecuteReader();

            while (myReader.Read())
            {
            }
            SQLconnect.cerrarconexion();
        }

        public void mostrarFamilia(ListBox listBox)
        {
            // Limpiar y preparar la lista
            listBox.Items.Clear();
            listBox.BackColor = Color.FromArgb(113, 173, 210);

            string query = "SELECT Familia FROM Familiares ORDER BY Familia";
            using (SqlConnection conn = SQLconnect.establecerConexion())
            using (SqlCommand command = new SqlCommand(query, conn))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                // Use HashSet para evitar duplicados y normalizar nombres
                HashSet<string> seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                while (reader.Read())
                {
                    string fam = reader["Familia"] == DBNull.Value ? string.Empty : reader["Familia"].ToString();
                    if (string.IsNullOrWhiteSpace(fam)) continue;
                    // Normalizar: trim y eliminar espacios repetidos
                    string normalized = System.Text.RegularExpressions.Regex.Replace(fam.Trim(), "\\s+", " ");
                    if (!seen.Contains(normalized))
                    {
                        seen.Add(normalized);
                        listBox.Items.Add(normalized);
                    }
                }
            }

            // Asegurar que no quede ninguna selección activa y refrescar la apariencia
            listBox.ClearSelected();
            listBox.SelectedIndex = -1;
            listBox.Refresh();

            SQLconnect.cerrarconexion();
        }

        public void modificarFamilia(string famViejo, string famModificado)
        {
            using (SqlConnection conn = SQLconnect.establecerConexion())
            {
                // Primero, obtener el IdFamilia de la familia vieja
                int idFamiliaVieja = 0;
                using (SqlCommand cmdGet = new SqlCommand("SELECT IdFamilia FROM Familiares WHERE Familia = @FamViejo", conn))
                {
                    cmdGet.Parameters.AddWithValue("@FamViejo", famViejo);
                    object obj = cmdGet.ExecuteScalar();
                    if (obj != null && obj != DBNull.Value)
                    {
                        int.TryParse(obj.ToString(), out idFamiliaVieja);
                    }
                }

                // Si no existe la familia vieja, no hacer nada
                if (idFamiliaVieja == 0)
                    return;

                // Actualizar el nombre de la familia usando el IdFamilia (evita duplicados por nombre)
                using (SqlCommand cmdUpd = new SqlCommand("UPDATE Familiares SET Familia = @FamModificado WHERE IdFamilia = @IdFamilia", conn))
                {
                    cmdUpd.Parameters.AddWithValue("@FamModificado", famModificado);
                    cmdUpd.Parameters.AddWithValue("@IdFamilia", idFamiliaVieja);
                    cmdUpd.ExecuteNonQuery();  // Usar ExecuteNonQuery para UPDATE
                }

                // Limpiar duplicados: si existe una familia antigua con el mismo nombre, eliminarla
                // (esto ocurre si al renombrar no se actualizaron correctamente todos los registros)
                using (SqlCommand cmdClean = new SqlCommand("DELETE FROM Familiares WHERE Familia = @FamViejo AND IdFamilia != @IdFamilia", conn))
                {
                    cmdClean.Parameters.AddWithValue("@FamViejo", famViejo);
                    cmdClean.Parameters.AddWithValue("@IdFamilia", idFamiliaVieja);
                    cmdClean.ExecuteNonQuery();
                }
            }
            SQLconnect.cerrarconexion();
        }

        public void eliminarFamilia(string nombreFam)
        {
            string query = "Delete from Familiares where Familia='" + nombreFam + "';";
            SqlCommand command = new SqlCommand(query, SQLconnect.establecerConexion());

            command.ExecuteNonQuery();
            SQLconnect.cerrarconexion();
        }

        public void mostrarTipo(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            string query = "SELECT DISTINCT Tipo FROM Materiales";
            SqlCommand command = new SqlCommand(query, SQLconnect.establecerConexion());

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBox.Items.Add(reader["Tipo"].ToString());
            }
            reader.Close();

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

        public void MaterialesEnExistencia(DataGridView dgvMaterialesEnExistencia, string nombreFamilia)
        {
            string query = "SELECT IdFamilia from Familiares where Familia ='" + nombreFamilia + "';";
            SqlCommand comando = new SqlCommand(query, SQLconnect.establecerConexion());
            SqlDataReader reader = comando.ExecuteReader();
            string IdFamilia = "";
            while (reader.Read())
            {
                IdFamilia = reader["IdFamilia"].ToString();
            }
            reader.Close();
            SQLconnect.cerrarconexion();

            dgvMaterialesEnExistencia.DataSource = null;
            dgvMaterialesEnExistencia.BorderStyle = BorderStyle.None;
            dgvMaterialesEnExistencia.ColumnHeadersDefaultCellStyle.BackColor = Color.Red;
            dgvMaterialesEnExistencia.EnableHeadersVisualStyles = false;
            dgvMaterialesEnExistencia.Refresh();

            // Detectar si la columna IdAcopioUltimo existe y construir la consulta en consecuencia
            SqlConnection conn2 = SQLconnect.establecerConexion();
            bool tieneIdAcopio2 = false;
            try
            {
                using (SqlCommand chk2 = new SqlCommand("SELECT COUNT(*) FROM sys.columns WHERE [object_id]=OBJECT_ID('dbo.Materiales') AND name='IdAcopioUltimo'", conn2))
                {
                    object o2 = chk2.ExecuteScalar();
                    int c2 = 0;
                    if (o2 != null && o2 != DBNull.Value) int.TryParse(o2.ToString(), out c2);
                    tieneIdAcopio2 = c2 > 0;
                }
            }
            catch
            {
                tieneIdAcopio2 = false;
            }

            string idAcopioExpr2 = tieneIdAcopio2 ? "M.IdAcopioUltimo AS IdAcopio" : "0 AS IdAcopio";
            string query2 = "SELECT M.Grupo, M.Caracteristica, M.Color, M.Medidas, M.Codigo, M.Fabricante, M.Estado, M.Ubicacion, M.Cantidad, M.Tipo, M.Valor, " + idAcopioExpr2 + " from Materiales M where M.IdFamilia ='" + IdFamilia + "';";
            SqlDataAdapter adapter = new SqlDataAdapter(query2, conn2);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dt.DefaultView.Sort = "Grupo ASC, Caracteristica ASC";
            dgvMaterialesEnExistencia.DataSource = dt;
            SQLconnect.cerrarconexion();
        }

        // Added optional parameter 'registrarEnAcopio' (default false) so new materials are not
        // recorded in the Acopio/historial table unless explicitly requested.
        public void guardarMateriales(TextBox paraFamilia, TextBox paraGrupo, TextBox paraCaracteristica, TextBox paraMedidas,
          TextBox paraColor, TextBox paraCodigo, TextBox paraTipo, TextBox paraUbicacion, TextBox paraFabricante, TextBox paraCantidad,
          TextBox paraValor, TextBox paraEstado, byte[] paraImagen, bool registrarEnAcopio = false)
        {
                string query = "SELECT IdFamilia from Familiares where Familia ='" + paraFamilia.Text + "';";
                SqlCommand comando = new SqlCommand(query, SQLconnect.establecerConexion());
                SqlDataReader reader = comando.ExecuteReader();
                string IdFamilia = "";
                string Estado;
                if (paraEstado.Text == "Nuevo")
                {
                    Estado = "N";
                }
                else Estado = "U";

                while (reader.Read())
                {
                    IdFamilia = reader["IdFamilia"].ToString();
                }
                reader.Close();
                SQLconnect.cerrarconexion();

                string query2;
                if (paraImagen != null)
                {
                    query2 = "insert into Materiales (IdFamilia, Grupo, Caracteristica, Medidas, Color, Codigo, Tipo, Estado, Fabricante, Ubicacion, Cantidad, Disponible, Valor, ImageData) values (@IdFamilia, @Grupo, @Caracteristica, @Medidas, @Color, @Codigo, @Tipo, @Estado, @Fabricante, @Ubicacion, @Cantidad, @Disponible, @Valor, @ImageData);";
                }
                else
                {
                    query2 = "insert into Materiales (IdFamilia, Grupo, Caracteristica, Medidas, Color, Codigo, Tipo, Estado, Fabricante, Ubicacion, Cantidad, Disponible, Valor) values (@IdFamilia, @Grupo, @Caracteristica, @Medidas, @Color, @Codigo, @Tipo, @Estado, @Fabricante, @Ubicacion, @Cantidad, @Disponible, @Valor);";
                }

                int idMaterial = 0;

                // Usar una sola conexión y transacción para insertar Materiales y registrar en historial de Acopio
                string insertWithIdentity = query2 + "; SELECT SCOPE_IDENTITY();";

                // Elegir nombre de tabla histórica (Acopio_Historial si existe, si no Acopio)
                SqlConnection conexion = SQLconnect.establecerConexion();
                string histTableName = "Acopio";
                try
                {
                    using (SqlCommand chk = new SqlCommand("SELECT OBJECT_ID('dbo.Acopio_Historial')", conexion))
                    {
                        object obj = chk.ExecuteScalar();
                        if (obj != null && obj != DBNull.Value)
                        {
                            histTableName = "Acopio_Historial";
                        }
                    }
                }
                catch { }

                SqlTransaction tran = null;
                try
                {
                    tran = conexion.BeginTransaction();

                    using (SqlCommand cmdInsert = new SqlCommand(insertWithIdentity, conexion, tran))
                    {
                        cmdInsert.Parameters.AddWithValue("@IdFamilia", IdFamilia);
                        cmdInsert.Parameters.AddWithValue("@Grupo", paraGrupo.Text);
                        cmdInsert.Parameters.AddWithValue("@Caracteristica", paraCaracteristica.Text);
                        cmdInsert.Parameters.AddWithValue("@Medidas", paraMedidas.Text);
                        cmdInsert.Parameters.AddWithValue("@Color", paraColor.Text);
                        cmdInsert.Parameters.AddWithValue("@Codigo", paraCodigo.Text);
                        cmdInsert.Parameters.AddWithValue("@Tipo", paraTipo.Text);
                        cmdInsert.Parameters.AddWithValue("@Estado", Estado);
                        cmdInsert.Parameters.AddWithValue("@Fabricante", paraFabricante.Text ?? string.Empty);
                        cmdInsert.Parameters.AddWithValue("@Ubicacion", paraUbicacion.Text ?? string.Empty);
                        // Parse cantidad/disponible robustamente as decimal to accept fractional quantities
                        decimal cantidadParsed = 0m;
                        if (!string.IsNullOrWhiteSpace(paraCantidad.Text))
                        {
                            if (!decimal.TryParse(paraCantidad.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out cantidadParsed))
                            {
                                decimal.TryParse(paraCantidad.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out cantidadParsed);
                            }
                        }

                        cmdInsert.Parameters.AddWithValue("@Cantidad", cantidadParsed);
                        cmdInsert.Parameters.AddWithValue("@Disponible", cantidadParsed);

                        // Parse valor robustamente using existing logic
                        decimal valorParsed = 0m;
                        if (!string.IsNullOrWhiteSpace(paraValor.Text))
                        {
                            if (!decimal.TryParse(paraValor.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out valorParsed))
                            {
                                decimal.TryParse(paraValor.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out valorParsed);
                            }
                        }
                        cmdInsert.Parameters.AddWithValue("@Valor", valorParsed);

                        if (paraImagen != null)
                        {
                            cmdInsert.Parameters.AddWithValue("@ImageData", paraImagen);
                        }

                        object scopeResult = cmdInsert.ExecuteScalar();
                        if (scopeResult != null && scopeResult != DBNull.Value)
                        {
                            idMaterial = Convert.ToInt32(Convert.ToDecimal(scopeResult));
                        }
                    }
                    if (idMaterial == 0)
                        throw new Exception("No se obtuvo IdMaterial tras insertar en Materiales.");

                    if (registrarEnAcopio)
                    {
                        // Insertar registro histórico en la tabla de acopio si existe
                        string insertHist = $"INSERT INTO {histTableName} (IdMaterial, Ubicacion, Fabricante, Cantidad, Valor, Disponible) VALUES (@idMaterial, @ubicacion, @fabricante, @cantidad, @valor, @disponible); SELECT SCOPE_IDENTITY();";
                        int idAcopio = 0;
                        using (SqlCommand cmdHist = new SqlCommand(insertHist, conexion, tran))
                        {
                            cmdHist.Parameters.AddWithValue("@idMaterial", idMaterial);
                            cmdHist.Parameters.AddWithValue("@ubicacion", paraUbicacion.Text ?? string.Empty);
                            cmdHist.Parameters.AddWithValue("@fabricante", paraFabricante.Text ?? string.Empty);
                            cmdHist.Parameters.AddWithValue("@cantidad", string.IsNullOrWhiteSpace(paraCantidad.Text) ? 0 : int.Parse(paraCantidad.Text));
                            // Insert historial con mismo parsing robusto
                            decimal valorHist = 0m;
                            if (!string.IsNullOrWhiteSpace(paraValor.Text))
                            {
                                if (!decimal.TryParse(paraValor.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out valorHist))
                                {
                                    decimal.TryParse(paraValor.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out valorHist);
                                }
                            }
                            cmdHist.Parameters.AddWithValue("@valor", valorHist);
                            cmdHist.Parameters.AddWithValue("@disponible", string.IsNullOrWhiteSpace(paraCantidad.Text) ? 0 : int.Parse(paraCantidad.Text));

                            object scopeHist = cmdHist.ExecuteScalar();
                            if (scopeHist != null && scopeHist != DBNull.Value)
                            {
                                idAcopio = Convert.ToInt32(Convert.ToDecimal(scopeHist));
                            }
                        }

                        // Actualizar IdAcopioUltimo en Materiales si obtuvimos idAcopio
                        if (idAcopio > 0)
                        {
                            using (SqlCommand cmdUpd = new SqlCommand("UPDATE Materiales SET IdAcopioUltimo = @idAcopio WHERE IdMaterial = @idMaterial", conexion, tran))
                            {
                                cmdUpd.Parameters.AddWithValue("@idAcopio", idAcopio);
                                cmdUpd.Parameters.AddWithValue("@idMaterial", idMaterial);
                                cmdUpd.ExecuteNonQuery();
                            }
                        }
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    try { tran?.Rollback(); } catch { }
                    // Mostrar la excepción al usuario para diagnóstico y no relanzar
                    MessageBox.Show(ex.ToString(), "Error al guardar material", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    SQLconnect.cerrarconexion();
                }
            }




        public void llenarDGVSeleccionStock(DataGridView dgvSeleccionstockSel, string texFamiliaSel)
        {
            dgvSeleccionstockSel.Visible = true;
            dgvSeleccionstockSel.DataSource = null;

            string query = "Select IdFamilia from Familiares where Familia ='" + texFamiliaSel + "';";

            SqlCommand command = new SqlCommand(query, SQLconnect.establecerConexion());

            dgvSeleccionstockSel.Location = new Point(204, 190);

            string FamId = null;

            SqlDataReader myreader = command.ExecuteReader();
            while (myreader.Read())
            {
                FamId = myreader["IdFamilia"].ToString();
            }
            SQLconnect.cerrarconexion();
            myreader.Close();

            // Detectar existencia de la columna IdAcopioUltimo en la tabla Materiales
            SqlConnection connCheck = SQLconnect.establecerConexion();
            bool tieneIdAcopio = false;
            try
            {
                using (SqlCommand chk = new SqlCommand("SELECT COUNT(*) FROM sys.columns WHERE [object_id]=OBJECT_ID('dbo.Materiales') AND name='IdAcopioUltimo'", connCheck))
                {
                    object o = chk.ExecuteScalar();
                    int c = 0;
                    if (o != null && o != DBNull.Value) int.TryParse(o.ToString(), out c);
                    tieneIdAcopio = c > 0;
                }
            }
            catch { tieneIdAcopio = false; }

            string idAcopioSelect = tieneIdAcopio ? "M.IdAcopioUltimo AS IdAcopio" : "0 AS IdAcopio";
            string query2 = "select F.Familia, M.Grupo, M.Caracteristica, M.Color, M.Medidas, M.Codigo, M.Tipo, M.Ubicacion, M.Estado, M.Cantidad, M.Fabricante, M.Disponible, M.Valor, " + idAcopioSelect + ", M.IdMaterial from Materiales M join Familiares F on M.IdFamilia = F.IdFamilia where M.IdFamilia ='" + FamId + "';";

            SqlDataAdapter adapter = new SqlDataAdapter(query2, connCheck);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dt.DefaultView.Sort = "Grupo ASC, Caracteristica ASC, Medidas ASC";
            dgvSeleccionstockSel.DataSource = dt;
            SQLconnect.cerrarconexion();
        }

        public void ModificarMaterial(string MaterialId, string[] ListaNombreColumnasAcopio, string[] ListaNuevoValorAcopio, string[] ListaNombreColumnasMats, string[] ListaNuevoValorMats, string disponible, byte[] paraImagen)
        {
            // Valid column names to avoid SQL injection via column names
            var columnasValidas = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Grupo","Caracteristica","Color","Medidas","Codigo","Fabricante","Estado","Ubicacion","Cantidad","Disponible","Tipo","Valor"
            };

            ListaNombreColumnasAcopio = ListaNombreColumnasAcopio ?? new string[0];
            ListaNuevoValorAcopio = ListaNuevoValorAcopio ?? new string[0];
            ListaNombreColumnasMats = ListaNombreColumnasMats ?? new string[0];
            ListaNuevoValorMats = ListaNuevoValorMats ?? new string[0];

            // Construir cláusulas SET y parámetros de forma segura
            var setClauses = new List<string>();
            var parametros = new Dictionary<string, object>();

            try
            {
                for (int i = 0; i < ListaNombreColumnasAcopio.Length; i++)
                {
                    var col = ListaNombreColumnasAcopio[i];
                    if (string.IsNullOrWhiteSpace(col) || !columnasValidas.Contains(col)) continue;

                    string paramName = "@ac_" + i;
                    string valor = ListaNuevoValorAcopio.Length > i ? ListaNuevoValorAcopio[i] : string.Empty;

                    if (col.Equals("Valor", StringComparison.OrdinalIgnoreCase))
                    {
                        // parse decimal robustamente
                        decimal d = 0m;
                        if (!string.IsNullOrWhiteSpace(valor))
                        {
                            if (!decimal.TryParse(valor, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out d))
                            {
                                decimal.TryParse(valor, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out d);
                            }
                        }
                        parametros[paramName] = d;
                    }
                    else if (col.Equals("Cantidad", StringComparison.OrdinalIgnoreCase) || col.Equals("Disponible", StringComparison.OrdinalIgnoreCase))
                    {
                        int iv = 0;
                        int.TryParse(valor, out iv);
                        parametros[paramName] = iv;
                    }
                    else
                    {
                        parametros[paramName] = valor ?? string.Empty;
                    }

                    setClauses.Add(col + " = " + paramName);
                }

                for (int i = 0; i < ListaNombreColumnasMats.Length; i++)
                {
                    var col = ListaNombreColumnasMats[i];
                    if (string.IsNullOrWhiteSpace(col) || !columnasValidas.Contains(col)) continue;

                    string paramName = "@m_" + i;
                    string valor = ListaNuevoValorMats.Length > i ? ListaNuevoValorMats[i] : string.Empty;

                    if (col.Equals("Valor", StringComparison.OrdinalIgnoreCase))
                    {
                        decimal d = 0m;
                        if (!string.IsNullOrWhiteSpace(valor))
                        {
                            if (!decimal.TryParse(valor, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out d))
                            {
                                decimal.TryParse(valor, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out d);
                            }
                        }
                        parametros[paramName] = d;
                    }
                    else if (col.Equals("Cantidad", StringComparison.OrdinalIgnoreCase) || col.Equals("Disponible", StringComparison.OrdinalIgnoreCase))
                    {
                        int iv = 0;
                        int.TryParse(valor, out iv);
                        parametros[paramName] = iv;
                    }
                    else
                    {
                        parametros[paramName] = valor ?? string.Empty;
                    }

                    setClauses.Add(col + " = " + paramName);
                }

                using (SqlConnection conn = SQLconnect.establecerConexion())
                {
                    if (setClauses.Count > 0)
                    {
                        string sql = "UPDATE Materiales SET " + string.Join(",", setClauses) + " WHERE IdMaterial = @IdMaterial";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@IdMaterial", MaterialId);
                            foreach (var kv in parametros)
                            {
                                cmd.Parameters.AddWithValue(kv.Key, kv.Value ?? DBNull.Value);
                            }

                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Si se pidió incrementar disponibilidad
                    if (!string.IsNullOrWhiteSpace(disponible))
                    {
                        if (int.TryParse(disponible, out int inc))
                        {
                            string queryDisponible = "UPDATE Materiales SET Disponible = ISNULL(Disponible,0) + @inc WHERE IdMaterial = @IdMaterial";
                            using (SqlCommand cmdDisp = new SqlCommand(queryDisponible, conn))
                            {
                                cmdDisp.Parameters.AddWithValue("@inc", inc);
                                cmdDisp.Parameters.AddWithValue("@IdMaterial", MaterialId);
                                cmdDisp.ExecuteNonQuery();
                            }
                        }
                    }

                    // Actualizar imagen si procede
                    if (paraImagen != null)
                    {
                        string queryImagen = "UPDATE Materiales SET ImageData = @ImageData WHERE IdMaterial = @IdMaterial";
                        using (SqlCommand elComando = new SqlCommand(queryImagen, conn))
                        {
                            elComando.Parameters.AddWithValue("@ImageData", paraImagen);
                            elComando.Parameters.AddWithValue("@IdMaterial", MaterialId);
                            elComando.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch
            {
                // Re-lanzar para que la capa de UI muestre la excepción si es necesario
                throw;
            }
            finally
            {
                SQLconnect.cerrarconexion();
            }
        }

        public void MostrarUbicacionesMaterial(string IdMaterial, ListBox listUbicaciones)
        {
            listUbicaciones.Items.Clear();
            SqlConnection conn = SQLconnect.establecerConexion();
            try
            {
                // Comprobar si la tabla MaterialesModelo existe antes de consultar
                using (SqlCommand chk = new SqlCommand("SELECT OBJECT_ID('dbo.MaterialesModelo')", conn))
                {
                    object obj = chk.ExecuteScalar();
                    if (obj == null || obj == DBNull.Value)
                    {
                        // Tabla no existe, salir sin intentar leer
                        return;
                    }
                }

                string query = "SELECT IdModelo from MaterialesModelo WHERE IdMaterial = @IdMaterial;";
                using (SqlCommand selectCommand = new SqlCommand(query, conn))
                {
                    selectCommand.Parameters.AddWithValue("@IdMaterial", IdMaterial);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listUbicaciones.Items.Add(reader[0].ToString());
                        }
                    }
                }
            }
            finally
            {
                SQLconnect.cerrarconexion();
            }
        }

        public void EliminarMaterial(string IdMaterial)
        {
            SqlConnection conn = SQLconnect.establecerConexion();
            try
            {
                int count = 0;

                // Comprobar si la tabla MaterialesModelo existe antes de consultar
                using (SqlCommand chk = new SqlCommand("SELECT OBJECT_ID('dbo.MaterialesModelo')", conn))
                {
                    object obj = chk.ExecuteScalar();
                    if (obj != null && obj != DBNull.Value)
                    {
                        using (SqlCommand comando = new SqlCommand("Select COUNT(*) from MaterialesModelo where IdMaterial = @IdMaterial;", conn))
                        {
                            comando.Parameters.AddWithValue("@IdMaterial", IdMaterial);
                            object res = comando.ExecuteScalar();
                            if (res != null && res != DBNull.Value)
                            {
                                int.TryParse(res.ToString(), out count);
                            }
                        }
                    }
                    else
                    {
                        // Si la tabla no existe, no hay referencias
                        count = 0;
                    }
                }

                if (count > 0)
                {
                    DialogResult result = MessageBox.Show("El material esta siendo utilizado en " + count + " modelos, desea eliminarlo igualmente?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        using (SqlCommand comando4 = new SqlCommand("Delete from Materiales where IdMaterial = @IdMaterial;", conn))
                        {
                            comando4.Parameters.AddWithValue("@IdMaterial", IdMaterial);
                            comando4.ExecuteNonQuery();
                        }

                        using (SqlCommand comando5 = new SqlCommand("Delete from MaterialesModelo where IdMaterial = @IdMaterial;", conn))
                        {
                            comando5.Parameters.AddWithValue("@IdMaterial", IdMaterial);
                            // Si la tabla no existe, este comando lanzará, así que comprobar de nuevo
                            object obj2 = new SqlCommand("SELECT OBJECT_ID('dbo.MaterialesModelo')", conn).ExecuteScalar();
                            if (obj2 != null && obj2 != DBNull.Value)
                            {
                                comando5.ExecuteNonQuery();
                            }
                        }
                    }
                }
                else
                {
                    string matInfo = "";
                    using (SqlCommand comando2 = new SqlCommand("Select Grupo, Caracteristica, Medidas, Codigo from Materiales where IdMaterial = @IdMaterial;", conn))
                    {
                        comando2.Parameters.AddWithValue("@IdMaterial", IdMaterial);
                        using (SqlDataReader reader2 = comando2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                matInfo += reader2.GetValue(0).ToString() + " " + reader2.GetValue(1).ToString() + " " + reader2.GetValue(2).ToString() + " " + reader2.GetValue(3).ToString();
                            }
                        }

                        DialogResult result = MessageBox.Show("Esta seguro de eliminar " + matInfo + " ?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            using (SqlCommand comando3 = new SqlCommand("Delete from Materiales where IdMaterial = @IdMaterial;", conn))
                            {
                                comando3.Parameters.AddWithValue("@IdMaterial", IdMaterial);
                                comando3.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            finally
            {
                SQLconnect.cerrarconexion();
            }
        }

        public void valorPorFamilia(string nombreFamilia, Label labValorFamilia)
        {
            // Calcular el total por familia usando Disponible (stock disponible) * Valor
            string IdFamilia = "";
            decimal valorTotalFamilia = 0m;

            using (SqlConnection connection0 = SQLconnect.establecerConexion())
            {
                // Obtener IdFamilia
                using (SqlCommand cmdFam = new SqlCommand("SELECT IdFamilia FROM Familiares WHERE Familia = @Familia", connection0))
                {
                    cmdFam.Parameters.AddWithValue("@Familia", nombreFamilia);
                    object famObj = cmdFam.ExecuteScalar();
                    if (famObj != null && famObj != DBNull.Value)
                        IdFamilia = famObj.ToString();
                }

                if (!string.IsNullOrWhiteSpace(IdFamilia))
                {
                    // Usar Disponible (stock disponible) * Valor para calcular el valor total
                    using (SqlCommand cmdSum = new SqlCommand("SELECT SUM(ISNULL(Disponible,0) * ISNULL(Valor,0)) FROM Materiales WHERE IdFamilia = @IdFamilia", connection0))
                    {
                        cmdSum.Parameters.AddWithValue("@IdFamilia", IdFamilia);
                        object res = cmdSum.ExecuteScalar();
                        if (res != null && res != DBNull.Value)
                        {
                            decimal.TryParse(res.ToString(), out valorTotalFamilia);
                        }
                    }
                }
            }

            labValorFamilia.Text = Math.Round(valorTotalFamilia, 0).ToString();
        }
    }
}