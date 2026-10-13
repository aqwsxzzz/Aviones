using Aviones_2._0.Forms.Materiales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aviones_2._0.Forms.Compras;

namespace Aviones_2._0.Common
{
    public partial class frmAvisos : Form
    {
        public String tipoDeAviso = "";
        private frmMateriales materialesForm;
        private frmComprasCom frmComprasCom ;
       
        public frmAvisos(frmComprasCom comprasForm)  

        {
            InitializeComponent();
            this.frmComprasCom =comprasForm ;
        }

        private void botCancela_Click(object sender, EventArgs e)
        {
            if (frmComprasCom != null)
            {
                frmComprasCom.labAvisoCompra.Visible = true;
                frmComprasCom.panAvisoCompra.Visible=true;

                frmComprasCom.botIngresoCom.Enabled = true;
                frmComprasCom.botModificaCom.Enabled = true;
                frmComprasCom.botEliminaCom.Enabled = true;
                frmComprasCom.botFaltanteCom.Enabled = true;
                frmComprasCom.botSalirCom.Enabled = true;
                frmComprasCom.botSalirCom.Focus();

                frmComprasCom.BeginInvoke((Action)(() => frmComprasCom.botSalirCom.Focus()));  //Fuerza el método para que haga foco 
            }
            this.Close();
        }

        private void frmAvisos_Shown(object sender, EventArgs e)
        {
            if (tipoDeAviso == "Compras sin datos")
            {   
                labTitulo1.Text  = "No hay registros de compra";
                labTitulo1.Location = new Point(75,50);
                labTitulo1 .BackColor = SystemColors.ActiveCaption;
                labTitulo2.Text = "para modificar";
                labTitulo2.Location = new Point(130, 70);
                labTitulo2.BackColor = SystemColors.ActiveCaption;
                labTitulo3 .Visible = false;
                botElimina .Visible = false;
                botCancela.Text = "ACEPTAR";
                botCancela.Location = new Point(133, 140);
                picboxRecuaElimina.BackColor = SystemColors.ActiveCaption;
            }
            else if(tipoDeAviso == "Eliminación de compra") 
            {
                labTitulo1.Text = "No hay registros de compra";
                labTitulo1.Location = new Point(75, 50);
                labTitulo1.BackColor = SystemColors.ActiveCaption;
                labTitulo2.Text = "para eliminar";
                labTitulo2.Location = new Point(140, 70);
                labTitulo2.BackColor = SystemColors.ActiveCaption;
                labTitulo3.Visible = false;
                botElimina.Visible = false;
                botCancela.Text = "ACEPTAR";
                botCancela.Location = new Point(133, 140);
                picboxRecuaElimina.BackColor = SystemColors.ActiveCaption;
            }
        }
    }
}