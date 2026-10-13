using Aviones_2._0.Common;
using Aviones_2._0.Forms.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Aviones_2._0.Forms.Compras
{
    public partial class frmIngresoCom : Form
    {
        public DataGridView dgvMateriales { get; set; }
        public DataGridView dgvMaterialesacomprar;
        private funcCompras funCompras;
        private frmComprasCom frmComprasCom;
        private int selectedRowIndex = -1; // Guardar el índice de la fila seleccionada

        public frmIngresoCom(frmComprasCom infoCompras)
        {
            InitializeComponent();
                    
            dgvMateriales = Componentes.CrearDgvMateriales();
            this.Controls.Add(dgvMateriales);
            ConfigurarDataGridView(dgvMateriales);
            dgvMateriales.CellClick += new DataGridViewCellEventHandler(dgvMateriales_CellClick);

            dgvMaterialesacomprar = Componentes.CrearDgvMateriales();
            this.Controls.Add(dgvMaterialesacomprar);
            ConfigurarDataGridViewCompras(dgvMaterialesacomprar);

            funCompras = new funcCompras();
            funCompras.mostrarFamilias(lisFamiliaSelCom);
            frmComprasCom = infoCompras;

            // Asociar el manejador de ACEPTAR para procesar las compras y escribir Cantacomprar
            botAceptarCompraCom.Click += BotAceptarCompraCom_Click;
        }

        private void BotAceptarCompraCom_Click(object sender, EventArgs e)
        {
            // Recorrer filas del dgvMaterialesacomprar y actualizar el campo Cantacomprar en la tabla Materiales
            try
            {
                var conexionHelper = new Common.ConexionSQL();
                using (SqlConnection conn = conexionHelper.establecerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Materiales SET Cantacomprar = @cant WHERE IdMaterial = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cant", System.Data.SqlDbType.Decimal));
                        cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int));

                        foreach (DataGridViewRow row in dgvMaterialesacomprar.Rows)
                        {
                            if (row.IsNewRow) continue;

                            // Obtener el IdMaterial (columna 14)
                            var idObj = row.Cells[14].Value;
                            if (idObj == null) continue;
                            if (!int.TryParse(idObj.ToString(), out int idMaterial)) continue;

                            // Obtener la cantidad a comprar (columna 10 es "Disponible" renombrada a "CantidadAComprar")
                            decimal cant = 0m;
                            try
                            {
                                var cantObj = row.Cells[10].Value; // Índice directo de la columna "Disponible"/"CantidadAComprar"
                                if (cantObj != null)
                                {
                                    if (decimal.TryParse(cantObj.ToString(), out decimal cantParse))
                                        cant = cantParse;
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Error obteniendo cantidad para IdMaterial {idMaterial}: {ex.Message}");
                                continue;
                            }

                            // Ejecutar UPDATE
                            cmd.Parameters["@cant"].Value = cant;
                            cmd.Parameters["@id"].Value = idMaterial;

                            try
                            {
                                cmd.ExecuteNonQuery();
                                System.Diagnostics.Debug.WriteLine($"UPDATE Materiales SET Cantacomprar = {cant} WHERE IdMaterial = {idMaterial}");
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Error ejecutando UPDATE para IdMaterial {idMaterial}: {ex.Message}");
                            }
                        }
                    }
                }
                conexionHelper.cerrarconexion();

                // Cerrar sin mensaje
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar las compras: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmIngresoCom_Load(object sender, EventArgs e)
        {
            this.Location = new Point(320, 130);

            // Configurar el título "FAMILIAS"
            labListadoFamiliaCom.Visible = true;
            labListadoFamiliaCom.Location = new Point(578, 210);

            botListaFamiCom.Visible = false;
            botSalirFamiliasCom.Location = new Point(1170, 820);

            // Ajustar altura del ListBox según cantidad de familias (máximo 10 items visibles)
            lisFamiliaSelCom.Visible = true;
            funCompras.AjustarAlturaListBox(lisFamiliaSelCom);

            // El ListBox debe estar dentro del panel pequeño
            // Si no está como hijo del panel, agregarlo
            if (lisFamiliaSelCom.Parent != panFamiliaSelCom)
            {
                panFamiliaSelCom.Controls.Add(lisFamiliaSelCom);
            }

            // Posicionar el ListBox dentro del panel pequeño con margen
            lisFamiliaSelCom.Location = new Point(10, 30); // Relativo al panel

            // Configurar el panel pequeño (panFamiliaSelCom) que contiene el ListBox
            panFamiliaSelCom.Visible = true;

            // Calcular tamaño del panel pequeño basado en ListBox
            int panelSmallWidth = lisFamiliaSelCom.Width + 20; // Margen horizontal
            int panelSmallHeight = lisFamiliaSelCom.Height + 40; // Margen vertical (30 arriba + 10 abajo)
            panFamiliaSelCom.Size = new Size(panelSmallWidth, panelSmallHeight);

            // Configurar el panel grande (panRecuaFamilia) que contiene todo
            // Si no contiene al panel pequeño, agregarlo
            if (panFamiliaSelCom.Parent != panRecuaFamilia)
            {
                panRecuaFamilia.Controls.Add(panFamiliaSelCom);
            }

            // Calcular tamaño del panel grande basado en el panel pequeño
            int recuaWidth = panelSmallWidth + 40; // Margen adicional
            int recuaHeight = panelSmallHeight + 80; // Espacio para título y márgenes
            panRecuaFamilia.Size = new Size(recuaWidth, recuaHeight);

            // Centrar el panel pequeño dentro del panel grande (posición relativa)
            int leftMargin = (recuaWidth - panelSmallWidth) / 2;
            int topMargin = 60; // Espacio para el título (FAMILIAS) arriba
            panFamiliaSelCom.Location = new Point(leftMargin, topMargin);

            // Posicionar el panel grande en forma centrada
            panRecuaFamilia.Visible = true;
            panRecuaFamilia.Location = new Point(377, 100);

            dgvMateriales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            frmComprasCom.botIngresoCom.Enabled = false;
            frmComprasCom.botModificaCom.Enabled = false;
            frmComprasCom.botEliminaCom.Enabled = false;
            frmComprasCom.botFaltanteCom.Enabled = false;
        }

        private void dgvMateriales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verifica que se haya hecho clic en una fila válida
            {
                selectedRowIndex = e.RowIndex; // Guardar el índice de la fila seleccionada
                string idMaterial = dgvMateriales.Rows[e.RowIndex].Cells[14].Value?.ToString();

                if (!string.IsNullOrEmpty(idMaterial))
                {
                    funCompras.BuscarFotoPorIdMaterial(idMaterial, picFotoCom);
                    MostrarCamposCantidad();
                    picFotoCom.Location = new Point(1350, 100);

                    // Rellenar la cantidad solicitada si ya existe en la tabla para evitar duplicados
                    try
                    {
                        decimal cantPendiente = funCompras.ObtenerCantidadAComprar(idMaterial);
                        texCantSolicitada.Text = cantPendiente > 0m ? cantPendiente.ToString() : string.Empty;
                    }
                    catch { texCantSolicitada.Text = string.Empty; }

                    // Asegurar que el textbox de cantidad solicitable reciba foco después de actualizar UI
                    this.BeginInvoke((Action)(() =>
                    {
                        try { texCantSolicitada.Focus(); texCantSolicitada.SelectAll(); } catch { }
                        try { dgvMateriales.ClearSelection(); dgvMateriales.CurrentCell = null; } catch { }
                    }));
                }
            }
        }

        private void MostrarCamposCantidad()
        {
            labCantSolicitada.Visible = true;

            texCantSolicitada.Visible = true;
            texCantSolicitada.Focus();

            panCamtSolicitada.Visible = true;

            picFotoCom.Visible = true;
            picFotoCom.BackColor = Color.Transparent;
            picFotoCom.SizeMode = PictureBoxSizeMode.Zoom;
            picFotoCom.Parent = this;
        }

        private void ConfigurarDataGridView(DataGridView dgv)
        {
            dgv.Location = new Point(40, 45);
            dgv.Size = new Size(1280, 435);
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Silver;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.BackgroundColor = Color.SteelBlue ;
            dgv.Visible = false;

            dgv.Columns[0].Visible = false;
            dgv.Columns[1].Width = 140;
            dgv.Columns[2].Width = 140;
            dgv.Columns[4].Width = 140;
            dgv.Columns[5].Width = 120;
            dgv.Columns[7].Visible = false;
            dgv.Columns[10].Width = 60;
            dgv.Columns[10].HeaderText = "Disponible";
            dgv.Columns[11].Width = 90;
            dgv.Columns[13].Visible = false;
        }

        private void ConfigurarDataGridViewCompras(DataGridView dgv)
        {
            dgv.Columns["Disponible"].HeaderText = "A Comprar";
            dgv.Columns["Disponible"].Name = "CantidadAComprar";
            dgv.Visible = false;
            dgv.Location = new Point(210, 510);
            dgv.Size = new Size(1200, 304);
            dgv.EnableHeadersVisualStyles = false;
            dgv.BackgroundColor = Color.LightGray;
        }

        private void lisboxFamiliaSelcom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lisFamiliaSelCom.SelectedItem != null)
            {
                string familiaSeleccionada = lisFamiliaSelCom.SelectedItem.ToString();
                funCompras.cargarMaterialesPorFamilia(familiaSeleccionada, dgvMateriales);

                dgvMateriales.Visible = true;
                dgvMateriales.ClearSelection();
                dgvMateriales.CurrentCell = null;
                dgvMateriales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMateriales.MultiSelect = false;
                dgvMateriales.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect; 
                this.HandleCreated += (s, ev) =>
                {
                    this.BeginInvoke((Action)(() =>
                    {
                        dgvMateriales.ClearSelection();
                        dgvMateriales.CurrentCell = null;
                    }));
                };

                lisFamiliaSelCom.Visible = false;
                botListaFamiCom.Visible = true;
                botListaFamiCom.Text = familiaSeleccionada;
                botListaFamiCom.Location = new Point(32, 5);
                botListaFamiCom.Size = new Size(236, 40);
                botListaFamiCom.FlatStyle = FlatStyle.Flat;
                botListaFamiCom.FlatAppearance.BorderSize = 0;
                botListaFamiCom.FlatAppearance.MouseOverBackColor = Color.SteelBlue;  // Mismo color al hover
                botListaFamiCom.Font = botListaFamiCom.Font;
                botListaFamiCom.TextAlign = ContentAlignment.MiddleLeft;
                botListaFamiCom.BackColor = Color.SteelBlue;
                botListaFamiCom.ForeColor = Color.White;
                botListaFamiCom.AutoSize = false;

                panFamiliaSelCom.Visible = false;
                panRecuaFamilia.Visible = false;
                labListadoFamiliaCom.Visible = false;

                panRecuadroTitulo.Visible = false;
            }
        }

        private void botListaFamiCom_Click(object sender, EventArgs e)
        {
            // Ocultar todos los elementos de compra para volver a la selección de familia
            dgvMateriales.Visible = false;
            dgvMaterialesacomprar.Visible = false;
            botAceptarCompraCom.Visible = false;
            panTotalPedido.Visible = false;
            labCantSolicitada.Visible = false;
            texCantSolicitada.Visible = false;
            picFotoCom.Visible = false;
            panCamtSolicitada.Visible = false;

            // Mostrar el panel de selección de familias
            botListaFamiCom.Visible = false;
            panFamiliaSelCom.Visible = true;
            panFamiliaSelCom.Location = new Point(498, 200); // Mismo que al inicio
            panRecuadroTitulo1.Visible = false;

            lisFamiliaSelCom.Visible = true;
            lisFamiliaSelCom.BringToFront();
            lisFamiliaSelCom.Location = new Point(520, 270); // Mismo que al inicio

            labListadoFamiliaCom.Visible = true;
            labListadoFamiliaCom.BringToFront();
            labListadoFamiliaCom.Location = new Point(578, 210); // Mismo que al inicio

            // Limpiar la variable de índice seleccionado
            selectedRowIndex = -1;
        }

        private void texCantSolicitada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Si se presiona Enter
            {
                e.Handled = true; // Evita el sonido bip al presionar Enter

                // Usar el índice guardado en lugar de CurrentRow
                if (selectedRowIndex >= 0 && selectedRowIndex < dgvMateriales.Rows.Count)
                {
                    DataGridViewRow currentRow = dgvMateriales.Rows[selectedRowIndex];

                    // Agregar una nueva fila vacía en dgvMaterialesacomprar
                    int newRowIdx = dgvMaterialesacomprar.Rows.Add();
                    DataGridViewRow newRow = dgvMaterialesacomprar.Rows[newRowIdx];

                    // Copiar los valores de la fila seleccionada en dgvMateriales a la nueva fila en dgvMaterialesacomprar
                    for (int i = 0; i < currentRow.Cells.Count; i++)
                    {
                        newRow.Cells[i].Value = currentRow.Cells[i].Value;
                    }

                    // Asignar la cantidad ingresada en "CantidadAComprar"
                    newRow.Cells["CantidadAComprar"].Value = texCantSolicitada.Text;

                    // Obtener el valor unitario y calcular el total
                    if (decimal.TryParse(texCantSolicitada.Text, out decimal cantidad) &&
                        decimal.TryParse(newRow.Cells["Valor"].Value?.ToString(), out decimal valorUnitario))
                    {
                        decimal total = cantidad * valorUnitario;
                        newRow.Cells["Total"].Value = total;

                        // Guardar en la tabla Compras
                        string idMaterial = newRow.Cells["IdMaterial"].Value?.ToString();
                        if (!string.IsNullOrEmpty(idMaterial))
                        {
                            funCompras.guardarCompra(idMaterial, cantidad, valorUnitario, total);
                        }
                    }
                    else
                    {
                        newRow.Cells["Total"].Value = 0; // En caso de error, asignar 0
                    }

                    // Ocultar los controles innecesarios después de agregar la fila
                    labCantSolicitada.Visible = false;
                    texCantSolicitada.Visible = false;
                    texCantSolicitada.Text = "";
                    picFotoCom.Visible = false;

                    // Limpiar la selección azul del dgvMateriales
                    dgvMateriales.ClearSelection();
                    dgvMateriales.CurrentCell = null;

                    dgvMaterialesacomprar.Visible = true;
                    dgvMaterialesacomprar.Location = new Point(15, 510);
                    dgvMaterialesacomprar.Size = new Size(1330, 304);
                    dgvMaterialesacomprar.EnableHeadersVisualStyles = false;
                    dgvMaterialesacomprar.ColumnHeadersDefaultCellStyle.BackColor = Color.Silver;
                    dgvMaterialesacomprar.BackgroundColor = Color.SteelBlue;
                    dgvMaterialesacomprar.Columns[2].Width = 130;
                    dgvMaterialesacomprar.Columns[7].Visible = false;
                    dgvMaterialesacomprar.Columns[8].Visible = false;
                    dgvMaterialesacomprar.Refresh();
                    panCamtSolicitada.Visible = false;

                    if (dgvMaterialesacomprar.RowCount == 1)
                    {
                        botSalirFamiliasCom.Location = new Point(1250, 821);
                    }
                    if (dgvMaterialesacomprar.Rows.Count > 0)
                    {
                        dgvMaterialesacomprar.Rows[0].Selected = false;
                    }

                    dgvMaterialesacomprar.Visible = true;

                    botAceptarCompraCom.Visible = true;
                    botAceptarCompraCom.Location = new Point(1060, 821);

                    decimal TotalCostoCompra = 0;
                    foreach (DataGridViewRow row in dgvMaterialesacomprar.Rows)
                    {
                        decimal costoMaterial = decimal.Parse(row.Cells["CantidadAComprar"].Value.ToString()) * decimal.Parse(row.Cells["Valor"].Value.ToString());
                        row.Cells["Total"].Value = costoMaterial;
                        TotalCostoCompra += costoMaterial;
                    }
                    labValorPedido.Text = TotalCostoCompra.ToString();
                    if (!panTotalPedido.Visible)
                    {
                        panTotalPedido.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, selecciona un material antes de ingresar la cantidad.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

      

        private void botSalirFamiliasCom_Click(object sender, EventArgs e)
        {
            // Limpiar estado visual y datos temporales antes de cerrar para evitar que queden controles visibles
            try
            {
                // Ocultar y limpiar campos de ingreso de cantidad
                labCantSolicitada.Visible = false;
                texCantSolicitada.Visible = false;
                texCantSolicitada.Text = string.Empty;
                panCamtSolicitada.Visible = false;

                // Limpiar y ocultar la foto seleccionada
                try { picFotoCom.Image = null; } catch { }
                picFotoCom.Visible = false;

                // Ocultar el grid de materiales y limpiar selección
                dgvMateriales.Visible = false;
                try { dgvMateriales.ClearSelection(); dgvMateriales.CurrentCell = null; } catch { }

                // Rehabilitar botones del formulario principal
                frmComprasCom.botIngresoCom.Enabled = true;
                frmComprasCom.botModificaCom.Enabled = true;
                frmComprasCom.botEliminaCom.Enabled = true;
                frmComprasCom.botFaltanteCom.Enabled = true;
            }
            catch { }

            this.Close();
        }
    }
}