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

namespace Aviones_2._0.Forms.Materiales.EliminarFamilias
{
    public partial class frmEliminarFamilia : Form
    {
        private frmMateriales frmMateriales;
        private Componentes componentesGenericos;
        private funcionesEliminarFam funciones;
        private funcMateriales funcionesGenericas;
        private frmAvisos frmAvisos;
        public DataGridView dgvMaterialesAMostrar { get; set; }

        public frmEliminarFamilia(frmMateriales mate)
        {
            InitializeComponent();

            funciones = new funcionesEliminarFam();
            funcionesGenericas = new funcMateriales();
            componentesGenericos = new Componentes();
            frmMateriales = mate;
            funcionesGenericas.mostrarFamilia(listFamiliaElim);
            dgvMaterialesAMostrar = Componentes.CrearDgvMateriales();
            this.Controls.Add(dgvMaterialesAMostrar);

            labFamiliaElim.Visible = true;
            labFamiliaElim.Location = new Point(700, 265);
            labFamiliaElim.BringToFront();

            dgvMaterialesAMostrar.Visible = false;
            dgvMaterialesAMostrar.Size = new Size(1510, 745);
            dgvMaterialesAMostrar.Location = new Point(204, 190);
            dgvMaterialesAMostrar.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            dgvMaterialesAMostrar.BorderStyle = BorderStyle.None;

            listFamiliaElim.BringToFront();
            listFamiliaElim.Location = new Point(850, 260);
            listFamiliaElim.BackColor = Color.FromArgb(113, 173, 210);
            listFamiliaElim.Height = listFamiliaElim.Items.Count * 24;

            panRecuaFamilia.Size = new Size((listFamiliaElim.Width + 1), (listFamiliaElim.Height + 2));
            panRecuaFamilia.Location = new Point(850, 260);
            panRecuaFamilia.BackColor = Color.FromArgb(113, 173, 210);

            panRecuaElimina.Size = new Size(500, 500);
            panRecuaElimina.Location = new Point(602, 50);

         //   dgvMaterialesAMostrar.CellDoubleClick += new DataGridViewCellEventHandler(dgvMateriales_CellDoubleClick);

            funciones.mostrarFamilias(comboxNuevaFam);
        }

        private void botEliminarElim_Click(object sender, EventArgs e)
        {
            if (dgvMaterialesAMostrar.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvMaterialesAMostrar.Rows)
                {
                    funciones.eliminarMaterialPorFamilia(row.Cells[14].Value.ToString());
                }
                funciones.mostrarMaterialesPorFamilia(listFamiliaElim.SelectedItem.ToString(), dgvMaterialesAMostrar);
                if (dgvMaterialesAMostrar.Rows.Count == 0)
                {
                    funciones.eliminarFamilia(listFamiliaElim.SelectedItem.ToString());
                    funcionesGenericas.mostrarFamilia(listFamiliaElim);
                    frmMateriales.labEliminarFamilia.Visible = false;

                    botEliminarElim.Visible = false;
                  
                    frmMateriales.botIngresar.Visible = true;
                    frmMateriales.botBuscar.Visible = true;
                    frmMateriales.botModifica.Visible = true;
                    frmMateriales.botEliminar.Visible = true;
                    frmMateriales.botImprimir.Visible = true;
                    frmMateriales.botSalir.Visible = true;
                    frmMateriales.botModificarFam.Visible = false;
                    
                    dgvMaterialesAMostrar.Visible = false;

                    frmMateriales.dgvMateriales.Visible = true;

                    this.Close();
                }
            }
            else
            {
                funciones.eliminarFamilia(listFamiliaElim.SelectedItem.ToString());
                funcionesGenericas.mostrarFamilia(listFamiliaElim);

                frmMateriales.labEliminarFamilia.Visible = false;

                botEliminarElim.Visible = false;

                frmMateriales.botIngresar.Visible = true;
                frmMateriales.botBuscar.Visible = true;
                frmMateriales.botModifica.Visible = true;
                frmMateriales.botEliminar.Visible = true;
                frmMateriales.botImprimir.Visible = true;
                frmMateriales.botModificarFam.Visible = false;

                frmMateriales.botSalir.Visible = true;

                dgvMaterialesAMostrar.Visible = false;

                frmMateriales.dgvMateriales.Visible = true;

                this.Close();
            }
        }

        private void botCancelarElim_Click(object sender, EventArgs e)
        {
            frmMateriales.labEliminarFamilia.Visible = false;

            frmMateriales.botSalir.Visible = true;
            frmMateriales.botCancelarFam.Visible = false;
            frmMateriales.botIngresar.Visible = true;
            frmMateriales.botBuscar.Visible = true;
            frmMateriales.botModifica.Visible = true;
            frmMateriales.botEliminar.Visible = true;
            frmMateriales.botInforma.Visible = true;
            frmMateriales.botImprimir.Visible = true;
            frmMateriales.botModificarFam.Visible = false;
            

            frmMateriales.dgvMateriales.Visible = true;

            frmMateriales.EliminarFamilia = false;

            this.Close();
        }

        private void listFamiliaElim_SelectedIndexChanged(object sender, EventArgs e)
        {
            funciones.mostrarMaterialesPorFamilia(listFamiliaElim.SelectedItem.ToString(), dgvMaterialesAMostrar);

            labFamiliaElim.Visible = false;

            dgvMaterialesAMostrar.Visible = true;
            dgvMaterialesAMostrar.Location = new Point(105, 50);
            dgvMaterialesAMostrar.Height = 654;
            dgvMaterialesAMostrar.Columns[1].Visible = false;

            listFamiliaElim.Visible = false;

            botModificarElim.Visible = true;
            botEliminarElim.Visible = true;
            
            panRecuaElimina.Visible = false;
            panRecuaFamilia.Visible = false;
        }

  /*      private void dgvMateriales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }*/

        private void botModificarElim_Click(object sender, EventArgs e)
        {
            panelEmergente.Visible = true;
            panelEmergente.BringToFront();
        }

        private void botAceptarNuevaFam_Click(object sender, EventArgs e)
        {
            if (comboxNuevaFam.SelectedIndex >= 0 && comboxNuevaFam.SelectedItem.ToString() != dgvMaterialesAMostrar.CurrentRow.Cells[0].Value.ToString())
            {
                funciones.modificarFamiliaMaterial(comboxNuevaFam.SelectedItem.ToString(), dgvMaterialesAMostrar.CurrentRow.Cells[14].Value.ToString());
                panelEmergente.Visible = false;
                funciones.mostrarMaterialesPorFamilia(listFamiliaElim.SelectedItem.ToString(), dgvMaterialesAMostrar);
            }
        }

        private void comboxNuevaFam_DropDown(object sender, EventArgs e)
        {
            botAceptarNuevaFam.Visible = false;
            botCancelarNuevaFam.Visible = false;
        }

        private void comboxNuevaFam_DropDownClosed(object sender, EventArgs e)
        {
            botAceptarNuevaFam.Visible = true;
            botCancelarNuevaFam.Visible = true;
        }

        private void botCancelarNuevaFam_Click(object sender, EventArgs e)
        {
            panelEmergente.Visible = false;
        }
    }
}