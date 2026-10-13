using Aviones_2._0.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aviones_2._0.Forms.Compras
{
    public partial class frmFaltanteCom : Form
    {
        public DataGridView dgvMateriales;
        public DataGridView dgvMaterialesacomprar;
        private funcCompras funcCompras;
      //  private frmComprasCom frmComprasCom;

        public frmFaltanteCom()
        {
            InitializeComponent();
            this.Location = new Point(50, 120);
            this.Size = new Size(1850, 910);

            labTituloDgvMateriales.Location = new Point(650, 10);
            labTitulodgvMaterialesacomprar.Location = new Point(795, 470);

            dgvMateriales = Componentes.CrearDgvMateriales();
            this.Controls.Add(dgvMateriales);
            dgvMateriales.Location = new Point(255, 40);
            dgvMateriales.Size = new Size(1300, 415);
            dgvMateriales.Columns[2].Width = 140;
            dgvMateriales.Columns[4].Width = 100;
            dgvMateriales.Columns[5].Width = 100;
            dgvMateriales.Columns[7].Visible = false;
            dgvMateriales.Columns[9].Visible = false;
            dgvMateriales.Columns[13].Visible = false;
            dgvMateriales.CellClick += new DataGridViewCellEventHandler(dgvMateriales_CellClick);
         
            //  Procedimiento para hacer que no aparezca pintado de azul una celda
            dgvMateriales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMateriales.MultiSelect = false;

            this.HandleCreated += (s, e) =>
            {
                this.BeginInvoke((Action)(() =>
                {
                    dgvMateriales.ClearSelection();
                    dgvMateriales.CurrentCell = null;
                }));
            };

            dgvMaterialesacomprar = Componentes.CrearDgvMateriales();
            dgvMaterialesacomprar.Location = new Point(230, 500);
            dgvMaterialesacomprar.Size = new Size(1350, 304);
            dgvMaterialesacomprar.EnableHeadersVisualStyles = false;
            dgvMaterialesacomprar.ColumnHeadersDefaultCellStyle.BackColor = Color.Silver;
            dgvMaterialesacomprar.BackgroundColor = Color.SteelBlue ;
            dgvMaterialesacomprar.Columns[1].Width = 140;
            dgvMaterialesacomprar.Columns[2].Width = 130;
            dgvMaterialesacomprar.Columns[3].Width = 120;
            dgvMaterialesacomprar.Columns[4].Width = 100;
            dgvMaterialesacomprar.Columns[5].Width = 100;
            dgvMaterialesacomprar.Columns[6].Width = 100;
            dgvMaterialesacomprar.Columns[7].Visible = false;
            dgvMaterialesacomprar.Columns[9].Visible = false;
            dgvMaterialesacomprar.Columns[10].Width = 55;
            dgvMaterialesacomprar.Columns[12].Width = 50;
            dgvMaterialesacomprar.Columns[13].Width = 62;
            this.Controls.Add(dgvMaterialesacomprar);
            dgvMaterialesacomprar.Columns["Disponible"].HeaderText = "A Comprar";
            dgvMaterialesacomprar.Columns["Disponible"].Name = "CantidadAComprar";

            dgvMaterialesacomprar.ClearSelection();
            dgvMaterialesacomprar.CurrentCell = null;
            dgvMaterialesacomprar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMaterialesacomprar.MultiSelect = false;

            funcCompras = new funcCompras();
            funcCompras.mostrarFaltantes(dgvMateriales);

            // Cargar materiales ya pedidos al iniciar el formulario
            CargarMaterialesYaPedidos();

            botCancelaFaltaCom.Location = new Point(1440, 812);
        }

        private void CargarMaterialesYaPedidos()
        {
            // Cargar materiales que ya tienen cantidad pendiente de compra
            DataTable dtMaterialesPedidos = new DataTable();
            string query = @"
                SELECT F.Familia, M.Grupo, M.Caracteristica, M.Color, M.Medidas, M.Codigo, 
                       M.Fabricante, M.Estado, M.Ubicacion, M.Cantidad, M.Disponible, M.Tipo, M.Valor, 
                       M.IdMaterial, ISNULL(M.IdAcopioUltimo, 0) AS IdAcopio, M.Cantacomprar
                FROM Familiares F 
                INNER JOIN Materiales M ON F.IdFamilia = M.IdFamilia 
                WHERE ISNULL(M.Cantacomprar, 0) > 0
                ORDER BY F.Familia ASC, M.Grupo ASC, M.Caracteristica ASC";

            try
            {
                using (System.Data.SqlClient.SqlConnection conn = new Common.ConexionSQL().establecerConexion())
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn))
                using (System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(cmd))
                {
                    adapter.Fill(dtMaterialesPedidos);
                }

                if (dtMaterialesPedidos.Rows.Count > 0)
                {
                    foreach (DataRow row in dtMaterialesPedidos.Rows)
                    {
                        int newRowIdx = dgvMaterialesacomprar.Rows.Add();
                        DataGridViewRow newRow = dgvMaterialesacomprar.Rows[newRowIdx];

                        // Copiar todos los valores del material a la fila
                        for (int i = 0; i < dtMaterialesPedidos.Columns.Count; i++)
                        {
                            if (i < newRow.Cells.Count)
                            {
                                newRow.Cells[i].Value = row[i];
                            }
                        }

                        // Asignar la cantidad a comprar (que está en Cantacomprar)
                        newRow.Cells["CantidadAComprar"].Value = row["Cantacomprar"];

                        // Calcular el total
                        if (decimal.TryParse(row["Cantacomprar"].ToString(), out decimal cantidad) &&
                            decimal.TryParse(row["Valor"].ToString(), out decimal valorUnitario))
                        {
                            newRow.Cells["Total"].Value = cantidad * valorUnitario;
                        }
                    }

                    // Mostrar los controles relacionados con la tabla de compras
                    if (dgvMaterialesacomprar.RowCount > 0)
                    {
                        dgvMaterialesacomprar.Visible = true;
                        botAceptaFaltaCom.Visible = true;
                        botAceptaFaltaCom.Location = new Point(1260, 812);

                        panTotalPedidoFal.Visible = true;
                        panTotalPedidoFal.Location = new Point(200, 820);

                        // Calcular total
                        decimal TotalCostoCompra = 0;
                        foreach (DataGridViewRow row in dgvMaterialesacomprar.Rows)
                        {
                            decimal costoMaterial = decimal.Parse(row.Cells["CantidadAComprar"].Value.ToString()) * 
                                                   decimal.Parse(row.Cells["Valor"].Value.ToString());
                            row.Cells["Total"].Value = costoMaterial;
                            TotalCostoCompra += costoMaterial;
                        }
                        labValorPedidoFal.Text = TotalCostoCompra.ToString();

                        dgvMaterialesacomprar.ClearSelection();
                        dgvMaterialesacomprar.CurrentCell = null;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cargando materiales ya pedidos: {ex.Message}");
            }
        }

        private void frmFaltanteCom_Load(object sender, EventArgs e)
        {
           // this.Cursor = Cursors.Cross;
        }

        private void dgvMateriales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verifica que se haya hecho clic en una fila válida
            {
                string idMaterial = dgvMateriales.Rows[e.RowIndex].Cells[14].Value?.ToString();

                if (!string.IsNullOrEmpty(idMaterial))
                {
                    funcCompras.BuscarFotoPorIdMaterial(idMaterial, picFotoFaltante);

                    labCantSolicitadaFal.Visible = true;
                    labCantSolicitadaFal.Location = new Point(1630,320);

                    texCantSolicitadaFal .Visible = true;
                    texCantSolicitadaFal.Location = new Point(1663, 380);

                    picFotoFaltante.Visible = true;
                    picFotoFaltante.Location = new Point(1600, 60);

                    picCamtSolicitadaFal.Visible = true;
                    picCamtSolicitadaFal.Location = new Point(1620, 300);

                    // Cargar automáticamente la cantidad pendiente si existe
                    try
                    {
                        decimal cantPendiente = funcCompras.ObtenerCantidadAComprar(idMaterial);
                        texCantSolicitadaFal.Text = cantPendiente > 0m ? cantPendiente.ToString() : string.Empty;
                    }
                    catch { texCantSolicitadaFal.Text = string.Empty; }

                    this.BeginInvoke((Action)(() =>
                    {
                        texCantSolicitadaFal.Focus();
                        // Sin celda activa para evitar el aspecto distinto en una columna
                        try { dgvMateriales.CurrentCell = null; } catch { }
                    }));
                    dgvMateriales.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                }
            }
        }

      

        private void texCantSolicitadaFal_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (e.KeyChar == '\r') // Si se presiona Enter
                {
                    if (dgvMateriales.RowCount > 0) // Verifica si hay filas en dgvMateriales
                    {
                        DataGridViewRow currentRow = dgvMateriales.CurrentRow;


                        if (currentRow != null)
                        {
                            // Obtener el ID del material seleccionado
                            string idMaterialActual = currentRow.Cells[14].Value?.ToString();

                            // Verificar si el material ya existe en dgvMaterialesacomprar
                            bool materialExiste = false;
                            int filaExistente = -1;

                            if (!string.IsNullOrEmpty(idMaterialActual))
                            {
                                for (int i = 0; i < dgvMaterialesacomprar.Rows.Count; i++)
                                {
                                    string idEnTabla = dgvMaterialesacomprar.Rows[i].Cells[14].Value?.ToString();
                                    if (idEnTabla == idMaterialActual)
                                    {
                                        materialExiste = true;
                                        filaExistente = i;
                                        break;
                                    }
                                }
                            }

                            if (materialExiste && filaExistente >= 0)
                            {
                                // Material ya existe, actualizar la cantidad
                                if (decimal.TryParse(texCantSolicitadaFal.Text, out decimal cantidadNueva))
                                {
                                    dgvMaterialesacomprar.Rows[filaExistente].Cells["CantidadAComprar"].Value = cantidadNueva;

                                    // Recalcular el total
                                    if (decimal.TryParse(dgvMaterialesacomprar.Rows[filaExistente].Cells["Valor"].Value?.ToString(), out decimal valorUnitario))
                                    {
                                        dgvMaterialesacomprar.Rows[filaExistente].Cells["Total"].Value = cantidadNueva * valorUnitario;
                                    }
                                }

                                MessageBox.Show("Este material ya estaba en la lista de compras. Se ha actualizado la cantidad.", "Material Duplicado");
                            }
                            else
                            {
                                // Agregar una nueva fila vacía en dgvMaterialesacomprar
                                int newRowIdx = dgvMaterialesacomprar.Rows.Add();
                                DataGridViewRow newRow = dgvMaterialesacomprar.Rows[newRowIdx];

                                // Copiar los valores de la fila seleccionada en dgvMateriales a la nueva fila en dgvMaterialesacomprar
                                for (int i = 0; i < currentRow.Cells.Count; i++)
                                {
                                    newRow.Cells[i].Value = currentRow.Cells[i].Value;
                                }

                                // Asignar la cantidad ingresada en "CantidadAComprar"
                                newRow.Cells["CantidadAComprar"].Value = texCantSolicitadaFal.Text;

                                // Obtener el valor unitario y calcular el total
                                if (decimal.TryParse(texCantSolicitadaFal.Text, out decimal cantidad) &&
                                    decimal.TryParse(newRow.Cells["Valor"].Value?.ToString(), out decimal valorUnitario))
                                {
                                    newRow.Cells["Total"].Value = cantidad * valorUnitario;
                                }
                                else
                                {
                                    newRow.Cells["Total"].Value = 0; // En caso de error, asignar 0
                                }
                            }

                            // Ocultar los controles innecesarios después de agregar la fila
                            labCantSolicitadaFal.Visible = false;

                            texCantSolicitadaFal.Visible = false;
                            texCantSolicitadaFal.Text = "";

                            picFotoFaltante.Visible = false;

                            picCamtSolicitadaFal .Visible = false;

                            dgvMaterialesacomprar.Refresh();

                            // Limpiar la selección del dgv de materiales
                            dgvMateriales.ClearSelection();
                            dgvMateriales.CurrentCell = null;
                            dgvMateriales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            dgvMateriales.MultiSelect = false;



                            if (dgvMaterialesacomprar.RowCount == 1)
                            {
                               // panCamtSolicitadaFal .Visible = false;

                                botAceptaFaltaCom.Visible = true;
                                botAceptaFaltaCom.Location = new Point(1200, 810);

                                panTotalPedidoFal.Visible = true;
                                panTotalPedidoFal.Location = new Point(200, 820);
                            }
                            if (dgvMaterialesacomprar.Rows.Count > 0)
                            {
                                dgvMaterialesacomprar.Rows[0].Selected = false;
                            }

                            dgvMaterialesacomprar.Visible = true;

                            botAceptaFaltaCom.Visible = true;
                            botAceptaFaltaCom.Location = new Point(1260, 812);

                            decimal TotalCostoCompra = 0;
                            foreach (DataGridViewRow row in dgvMaterialesacomprar.Rows)
                            {
                                decimal costoMaterial = decimal.Parse(row.Cells["CantidadAComprar"].Value.ToString()) * decimal.Parse(row.Cells["Valor"].Value.ToString());
                                row.Cells["Total"].Value = costoMaterial;
                                TotalCostoCompra += costoMaterial;
                            }
                            labValorPedidoFal.Text = TotalCostoCompra.ToString();
                        }
                        else
                        {
                            MessageBox.Show("La familia seleccionada no tiene materiales ingresados");
                        }
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}