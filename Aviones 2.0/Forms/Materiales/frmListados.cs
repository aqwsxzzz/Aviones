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

namespace Aviones_2._0.Forms.Materiales
{
    public partial class frmListados : Form
    {
        private frmMateriales materialesForm;
        private funcMateriales funcionesMateriales;
        private Componentes componentesGenericos;

        public frmListados(frmMateriales materiales)
        {
            InitializeComponent();
            this.materialesForm = materiales;
            funcionesMateriales = new funcMateriales();
            decimal valorTotalGral = 0;
            componentesGenericos = new Componentes();

            if (materialesForm.dgvMateriales.Rows.Count > 0)
            {
                foreach (DataGridViewRow Row in materialesForm.dgvMateriales.Rows)
                {
                    decimal valorAAgregar = Decimal.Parse(Row.Cells[9].Value.ToString()) * Decimal.Parse(Row.Cells[12].Value.ToString());
                    valorTotalGral += valorAAgregar;
                }
                valorTotalGral = Math.Round(valorTotalGral, 0);
                labValorTotalGeneral.Text = valorTotalGral.ToString();
            }
            texFamAEnviar.Visible = false;

            botCancelarMat.Visible = false;

            lisFamListMat.Visible = false;

            panRecuaTotalFamilia.Visible = false;

            panGrande.Visible = true;
            panGrande.Location = new Point(310, 140);
            panGrande.Size = new Size(1000, 600);
        }

        private void botSalirListados_Click(object sender, EventArgs e)
        {
            if (materialesForm != null)
            {
                materialesForm.botSalir.Visible = true;
                materialesForm.dgvMateriales.Visible = true;
                materialesForm.botIngresar.Visible = true;
                materialesForm.botBuscar.Visible = true;
                materialesForm.botModifica.Visible = true;
                materialesForm.botEliminar.Visible = true;
                materialesForm.botInforma.Visible = true;
                materialesForm.botImprimir.Visible = true;
            }
            this.Close();
        }

        private void lisFamListMat_SelectedIndexChanged(object sender, EventArgs e)
        {
            funcionesMateriales.valorPorFamilia(lisFamListMat.SelectedItem.ToString(), labValorFamilia);

            labTituloValorFamilia.Visible = true;
            labTituloValorFamilia.Location = new Point(670, 464);
            labValorFamilia.Location = new Point(870, 459);
            labValorFamilia.Visible = true;
            labListaFam.Text = "Familia:";
            labListaFam.Font = new Font(texFamAEnviar.Font.FontFamily, 16);
            labListaFam.Location = new Point(630, 350);
            labListaFam.Font = new Font(labListaFam.Font.FontFamily, labListaFam.Font.Size, FontStyle.Regular);  //Saca el subrayado de la palabra
            labListaFam.BackColor = SystemColors.ActiveCaption;

            ritexFamAEnviar.Text = lisFamListMat.SelectedItem.ToString();
            ritexFamAEnviar.Visible = true;
            ritexFamAEnviar.Font = new Font(texFamAEnviar.Font.FontFamily, 22);
            ritexFamAEnviar.Location = new Point(740, 345);
            ritexFamAEnviar.BringToFront();
            ritexFamAEnviar.BorderStyle = BorderStyle.None;
            ritexFamAEnviar.Select(texFamAEnviar.Text.Length, 0);  //Elimina el color azul correspondiente a la activación.
            ritexFamAEnviar.BackColor = SystemColors.ActiveCaption;

            botListadoFamilia.Visible = false;
            botCancelarMat.Location = new Point(1263, 732);
            botCancelarMat.Visible = false;

            panRecuaTotalFamilia.Visible = true;
            panRecuaTotalFamilia.Location = new Point(610, 435);
            panRecuaTotalFamilia.BringToFront();
            panRecuaFamilia.Size = new Size(580, 300);
            panRecuaFamilia.Location = new Point(510, 285);
            panRecuaFamilia.BackColor = SystemColors.ActiveCaption;
            panRecuaFamilia.BorderStyle = BorderStyle.FixedSingle; 

            labValorFamilia.BringToFront();
            labTituloValorFamilia.BringToFront();

            lisFamListMat.Visible = false;

            botSalirListados.Visible = true;
            botSalirListados.Focus();
        }

        private void botCancelarMat_Click(object sender, EventArgs e)
        {
            this.Close();
            materialesForm.botIngresar.Visible = true;
            materialesForm.botBuscar.Visible = true;
            materialesForm.botModifica.Visible = true;
            materialesForm.botEliminar.Visible = true;
            materialesForm.botInforma.Visible = true;
            materialesForm.botImprimir.Visible = true;
            materialesForm.dgvMateriales.Visible = true;
            materialesForm.botSalir.Visible = true;
        }

        private void botListadoFamilia_Click(object sender, EventArgs e)
        {
            labTituloTotalGeneral.Visible = false;
            labListaFam.Visible = true;
            labListaFam.Location = new Point(630, 412);

            labValorTotalGeneral.Visible = false;

            materialesForm.botSalir.Visible = false;
            botListadoFamilia.Visible = false;
            botCancelarMat.Visible = true;
            botCancelarMat.BringToFront();
            botCancelarMat.Location = new Point(1147, 750);
            botSalirListados.Visible = false;

            panRecuaTotalGral.Visible = false;
            panRecuaFamilia.Visible = true;
            panRecuaFamilia.Location = new Point(585, 292);
            panRecuaFamilia.Size = new Size(492, 247);
           
            funcionesMateriales.mostrarFamilia(lisFamListMat);

            lisFamListMat.Visible = true;
            lisFamListMat.BringToFront();
            lisFamListMat.Font = new Font(lisFamListMat.Font.FontFamily, 14); // Cambiar el tamaño según sea necesario
            lisFamListMat.Location = new Point(833, 294);
            lisFamListMat.Height = 254;
            lisFamListMat.Width = 190;
            picboxRecuaMat_AutoSize();
        }

        private void picboxRecuaMat_AutoSize()
        {
            if (lisFamListMat.Items.Count == 0)
            {
                lisFamListMat.Location = new Point(954, 298);
                lisFamListMat.Visible = true;
                lisFamListMat.Height = 69;
            }
            else
            {
                labTituloListado.Text = "Listado por familia";

                int alturaTotalLista = (lisFamListMat.Items.Count * 24) + 5;

                panRecuaFamilia.Location = new Point((componentesGenericos.puntoMedioHorizontal - 400), (componentesGenericos.puntoMedioVertical - (alturaTotalLista / 2) - 116));
                panRecuaFamilia.Height = alturaTotalLista + 6;

                lisFamListMat.Location = new Point((componentesGenericos.puntoMedioHorizontal - 136), (componentesGenericos.puntoMedioVertical - ((alturaTotalLista / 2) + 112)));
                lisFamListMat.Height = alturaTotalLista;
                lisFamListMat.Width = 225;
            }
        }

        private void frmListados_Load(object sender, EventArgs e)
        {
            labListaFam.Location = new Point(630, 380);
        }
    }
}