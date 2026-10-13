using Aviones_2._0.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aviones_2._0.Forms.Compras;
using Aviones_2._0.Forms.Materiales;

namespace Aviones_2._0.Forms.Compras
{
    public partial class frmComprasCom : Form
    {
        public  Componentes componentesGenericos;
        private frmIngresoCom frmIngresoCom;
        private funcCompras funcCompras;
        private frmFaltanteCom frmFaltanteCom;
        public DataGridView dgvMaterialesacomprar;
        private frmAvisos componentesAvisos;

        public frmComprasCom()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;

            frmIngresoCom = new frmIngresoCom(this);
            frmIngresoCom.StartPosition = FormStartPosition.Manual;
            frmIngresoCom.Location = new Point(100, 130);



            frmFaltanteCom = new frmFaltanteCom();
            dgvMaterialesacomprar = Componentes.CrearDgvMateriales();
            // No añadir el DataGridView de compras al formulario por defecto (evita que aparezca en la esquina)
            // Se añadirá a Controls sólo cuando sea necesario en los manejadores (botModificaCom / botEliminaCom).
            funcCompras = new funcCompras();
        }

        private void frmComprasCom_Load(object sender, EventArgs e)
        {
            botIngresoCom.Location = new Point(37, 50);
            botModificaCom.Location = new Point(37, 130);
            botEliminaCom.Location = new Point(37, 210);
            botFaltanteCom.Location = new Point(37, 290);
            botSalirCom.Location = new Point(1490, 950);
            pnaRecuaBotones.Location = new Point(60, 240);

            // Verificar si hay compras pendientes (Cantacomprar > 0) y habilitar botones
            try
            {
                var conexionHelper = new Common.ConexionSQL();
                using (var conn = conexionHelper.establecerConexion())
                {
                    using (var cmd = new System.Data.SqlClient.SqlCommand(
                        "SELECT COUNT(*) FROM Materiales WHERE Cantacomprar IS NOT NULL AND CAST(ISNULL(Cantacomprar, 0) AS DECIMAL) > 0", conn))
                    {
                        object result = cmd.ExecuteScalar();
                        int comprasCount = result != null ? int.Parse(result.ToString()) : 0;

                        // Habilitar botones si hay compras pendientes
                        botModificaCom.Enabled = comprasCount > 0;
                        botEliminaCom.Enabled = comprasCount > 0;
                    }
                }
                conexionHelper.cerrarconexion();
            }
            catch
            {
                // Si hay error, dejar los botones deshabilitados
                botModificaCom.Enabled = false;
                botEliminaCom.Enabled = false;
            }
        }

        private void botIngresoCom_Click(object sender, EventArgs e)
        {
            botIngresoCom.BackColor = SystemColors.Control;
            botModificaCom.BackColor = SystemColors.Control;
            botEliminaCom.BackColor = SystemColors.Control;
            botFaltanteCom.BackColor = SystemColors.Control;

            frmIngresoCom.ShowDialog();
        }

        private void botModificaCom_Click(object sender, EventArgs e)
        {
            labAvisoCompra.Visible = false;
            panAvisoCompra.Visible = false;

            // Cargar compras pendientes desde Materiales (Cantacomprar > 0)
            funcCompras.cargarComprasPendientes(dgvMaterialesacomprar);

            if (dgvMaterialesacomprar.Rows.Count == 0)
            {
                // No hay compras pendientes: mostrar aviso
                botSalirCom.Enabled = false;
                botIngresoCom.Enabled = false;
                botModificaCom.Enabled = false;
                botEliminaCom.Enabled = false;
                botFaltanteCom.Enabled = false;

                frmAvisos aviso = new frmAvisos(this);
                aviso.tipoDeAviso = "Compras sin datos";
                aviso.ShowDialog();
            }
            else
            {
                // Mostrar el grid de compras pendientes en la zona superior
                dgvMaterialesacomprar.Location = new Point(355, 180);
                dgvMaterialesacomprar.Size = new Size(1300, 415);
                dgvMaterialesacomprar.EnableHeadersVisualStyles = false;
                dgvMaterialesacomprar.ColumnHeadersDefaultCellStyle.BackColor = Color.Silver;
                dgvMaterialesacomprar.BackgroundColor = Color.SteelBlue;
                // Ajustes de columnas básicos
                try
                {
                    dgvMaterialesacomprar.Columns[1].Width = 140;
                    dgvMaterialesacomprar.Columns[2].Width = 130;
                    dgvMaterialesacomprar.Columns[3].Width = 120;
                    dgvMaterialesacomprar.Columns[4].Width = 100;
                    dgvMaterialesacomprar.Columns[5].Width = 100;
                    dgvMaterialesacomprar.Columns[6].Width = 100;
                }
                catch { }

                // Mostrar la columna de cantidad a comprar si existe
                try
                {
                    if (dgvMaterialesacomprar.Columns.Contains("CantidadAComprar"))
                        dgvMaterialesacomprar.Columns["CantidadAComprar"].Visible = true;
                }
                catch { }

                dgvMaterialesacomprar.Visible = true;
                // Asegurar que el control esté en la colección del formulario
                if (!this.Controls.Contains(dgvMaterialesacomprar))
                {
                    this.Controls.Add(dgvMaterialesacomprar);
                }
                // Asegurar que el dgv de seleción no se muestre simultáneamente
                foreach (Control c in this.Controls)
                {
                    if (c is DataGridView dgv && dgv != dgvMaterialesacomprar)
                    {
                        try { dgv.Visible = false; } catch { }
                    }
                }
            }
        }

        private void botEliminaCom_Click(object sender, EventArgs e)
        {
            labAvisoCompra.Visible = false;
            panAvisoCompra.Visible = false;

            // Cargar compras pendientes antes de mostrar
            funcCompras.cargarComprasPendientes(dgvMaterialesacomprar);

            if (dgvMaterialesacomprar.Rows.Count == 0)
            {
                botSalirCom.Enabled = false;
                botIngresoCom.Enabled = false;
                botModificaCom.Enabled = false;
                botEliminaCom.Enabled = false;
                botFaltanteCom.Enabled = false;

                frmAvisos aviso = new frmAvisos(this);
                aviso.tipoDeAviso = "Eliminación de compra";
                aviso.ShowDialog();
                return;
            }

            // Configurar y mostrar el grid
            dgvMaterialesacomprar.Location = new Point(355, 180);
            dgvMaterialesacomprar.Size = new Size(1300, 415);
            dgvMaterialesacomprar.EnableHeadersVisualStyles = false;
            dgvMaterialesacomprar.ColumnHeadersDefaultCellStyle.BackColor = Color.Silver;
            dgvMaterialesacomprar.BackgroundColor = Color.SteelBlue;

            try
            {
                dgvMaterialesacomprar.Columns[1].Width = 140;
                dgvMaterialesacomprar.Columns[2].Width = 130;
                dgvMaterialesacomprar.Columns[3].Width = 120;
                dgvMaterialesacomprar.Columns[4].Width = 100;
                dgvMaterialesacomprar.Columns[5].Width = 100;
                dgvMaterialesacomprar.Columns[6].Width = 100;
            }
            catch { }

            try
            {
                if (dgvMaterialesacomprar.Columns.Contains("CantidadAComprar"))
                    dgvMaterialesacomprar.Columns["CantidadAComprar"].Visible = true;
            }
            catch { }

            dgvMaterialesacomprar.Visible = true;
            if (!this.Controls.Contains(dgvMaterialesacomprar))
            {
                this.Controls.Add(dgvMaterialesacomprar);
            }
            foreach (Control c in this.Controls)
            {
                if (c is DataGridView dgv && dgv != dgvMaterialesacomprar)
                {
                    try { dgv.Visible = false; } catch { }
                }
            }
        }

        private void botFaltanteCom_Click(object sender, EventArgs e)
        {
            frmFaltanteCom.ShowDialog();
        }

        private void Salir_Click(object sender, EventArgs e)
        {
             this.Close();
        }
    }
}
