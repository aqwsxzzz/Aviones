using Aviones_2._0.Common;
using Aviones_2._0.Forms.Compras;
using Aviones_2._0.Forms.Materiales.EliminarFamilias;
using Aviones_2._0.Forms.Materiales.Modificaciones;
using System;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace Aviones_2._0.Forms.Materiales
{
    public partial class frmMateriales : Form
    {
        private Componentes componentesGenericos;
        private frmAvisos componentesAvisos;
        private funcMateriales funciones;
        private frmIngreso frmIngreso;
        private frmEliminarFamilia frmEliminarFamilia;
        
        public DataGridView dgvMateriales { get; set; }

        private string NuevoModificacionOEliminacion = "";
        private bool Buscar = false;
        public bool EliminarFamilia = false;

        private int rowIndex = 0;  // Para llevar un conteo de la fila actual
        private int startY = 100;  // Posición inicial en Y
        private int rowHeight = 20; // Altura de cada celda
        private string famStatus = "";
        private bool modificarFam = false;
        private string nombreFamilia = "";

        public frmMateriales()
        {
            InitializeComponent();

            componentesGenericos = new Componentes();
            funciones = new funcMateriales();
            frmIngreso = new frmIngreso(this);
            frmEliminarFamilia = new frmEliminarFamilia(this);

            this.FormBorderStyle = FormBorderStyle.None;
            dgvMateriales = Componentes.CrearDgvMateriales();
            this.Controls.Add(dgvMateriales);

            labTituloPrincipal.Location = new Point((componentesGenericos.puntoMedioHorizontal - 102), (componentesGenericos.puntoMedioVertical - 457));
            labTituloDgv.Location = new Point((componentesGenericos.puntoMedioHorizontal + 505), (componentesGenericos.puntoMedioVertical - 394));

            botIngresar.Location = new Point(423, 956);
            botBuscar.Location = new Point(601, 956);
            botModifica.Location = new Point(779, 956);
            botEliminar.Location = new Point(957, 956);
            botInforma.Location = new Point(1135, 956);
            botImprimir.Location = new Point(1313, 956);
            botSalir.Location = new Point((componentesGenericos.puntoMedioHorizontal + 596), (componentesGenericos.puntoMedioVertical + 415));
            botFamilias.Location = new Point(300, 800);

            dgvMateriales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMateriales.Location = new Point(204, 190);
            dgvMateriales.Size = new Size(1510, 745);
            dgvMateriales.EnableHeadersVisualStyles = false; // Deshabilitar estilos visuales
            dgvMateriales.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ButtonShadow;
            dgvMateriales.DefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
            // Hacer que cuando el encabezado de columna esté 'seleccionado' use el mismo color de fondo del dgv
            dgvMateriales.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvMateriales.DefaultCellStyle.BackColor;
            dgvMateriales.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgvMateriales.ColumnHeadersDefaultCellStyle.ForeColor;
            // Configurar el color de selección para que sea azul nítido en las filas
            dgvMateriales.DefaultCellStyle.SelectionBackColor = Color.CornflowerBlue;
            dgvMateriales.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvMateriales.MultiSelect = false;
            dgvMateriales.Columns[13].Visible = false;
            dgvMateriales.Columns[14].Visible = false;
            dgvMateriales.Columns[15].Visible = false;
            dgvMateriales.CellClick += new DataGridViewCellEventHandler(dgvMateriales_CellClick);
            dgvMateriales.CellDoubleClick += new DataGridViewCellEventHandler(dgvMateriales_CellDoubleClick);

            listFamiliaMat.DrawMode = DrawMode.OwnerDrawFixed;
            listFamiliaMat.DrawItem += new DrawItemEventHandler(listBox_DrawItem);

            picboxRecuadroGrande.Visible = false;
            funciones.mostrarMateriales(dgvMateriales);

            // Asegurar formato de visualización para las columnas numéricas (una cifra decimal)
            try
            {
                if (dgvMateriales.Columns.Contains("Valor"))
                {
                    dgvMateriales.Columns["Valor"].DefaultCellStyle.Format = "0.0";
                }
                if (dgvMateriales.Columns.Contains("Total"))
                {
                    dgvMateriales.Columns["Total"].DefaultCellStyle.Format = "0.0";
                }
            }
            catch { }
        }

        private void frmMateriales_Load(object sender, EventArgs e)
        {
            dgvMateriales.ClearSelection();  //Evita que al abrir el dgvMateriales haya un registro seleccionado
            dgvMateriales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMateriales.MultiSelect = false;
        }

        private void Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void botIngresar_Click(object sender, EventArgs e)
        {
            // Asegurar que el puntero del mouse esté visible al entrar al modo ingreso
            try { Cursor.Show(); this.Cursor = Cursors.Default; } catch { }
            dgvMateriales.Visible = false;

            labTituloDgv.Visible = false;
            labListaFamilia1.Visible = false;
            labListaFamilia2.Visible = false;
       /*    labIngresoMat.Visible = true;
            labIngresoMat.Text = "Ingresos / Modificaciones / Bajas";
            labIngresoMat.Location = new Point(850, 280);
            labIngresoMat.Font = new Font("Arial", 20, FontStyle.Regular | FontStyle.Underline);
            labIngresoMat.BackColor = Color.FromArgb(153, 180, 209);*/

            frmIngreso.texFamilia.Visible = false;

            botIngresar.Visible = false;
            botBuscar.Visible = false;
            botCancelarFam.Visible = true;
            botModifica.Visible = false;
            botEliminar.Visible = false;
            botInforma.Visible = false;
            botImprimir.Visible = false;
            botFamilias.Visible = true;
            botFamilias.Location = new Point(790, 495);
            botRegistros.Visible = true;
            botRegistros.Location = new Point(990, 495);
            botCancelarMat.Location = new Point((componentesGenericos.puntoMedioHorizontal + 377), (componentesGenericos.puntoMedioVertical + 357));
            botCancelarMat.Visible = true;
            botCancelarMat.Visible = false;
            botRegresa.Visible = true;
            botRegresa.Location = new Point(1555, 955);
            botSalir.Visible = false;
            botCancelarFam.Visible = false;

            panRecuaIngreso.Visible = true;
            panRecuaIngreso.Location = new Point((componentesGenericos.puntoMedioHorizontal - 575), (componentesGenericos.puntoMedioVertical - 340));
            panRecuaIngreso.Size = new Size(1150, 680);
            panFamiliasUnidades.Visible = true;
            panFamiliasUnidades.Location = new Point(710, 400);
            panFamiliasUnidades.Size = new Size(500, 250);

            picboxRecuadroGrande.Height = 300;
            picboxRecuadroGrande.Width = 460;
            picboxRecuadroGrande.Location = new Point(720, 302);
            picboxRecuadroGrande.Visible = false;
            picboxRecuadroTitulodgv.Visible = false;

            listFamiliaMat.Location = new Point(978, 304);

            NuevoModificacionOEliminacion = "Nuevo";
            funciones.mostrarFamilia(listFamiliaMat);
           // picboxRecuaMat_AutoSize();     //Era el recuadro que aparecía cuando se entraba por primera vez una familia en la tabla
        }

        private void botCancelarFamilia_Click(object sender, EventArgs e)
        {
            labFamiliaMat.Visible = false;
            labTituloDgv.Visible = true;

            botIngresar.Visible = true;
            botBuscar.Visible = true;
            botModifica.Visible = true;
            botEliminar.Visible = true;
            botIngresarFam.Visible = false;
            botCancelarFam.Visible = false;
            botSalir.Visible = true;

            dgvMateriales.Visible = true;

            listFamiliaMat.Visible = false;

            picboxRecuaMat.Visible = false;
            picboxRecuadroTitulodgv.Visible = true;
        }

        private void picboxRecuaMat_AutoSize()
        {
            if (listFamiliaMat.Items.Count == 0)
            {
                listFamiliaMat.Location = new Point(954, 398);
                listFamiliaMat.Visible = true;
                listFamiliaMat.Height = 69;
            }
            else
            {
                int alturaTotalLista = (listFamiliaMat.Items.Count * 20) +13;

                labFamiliaMat.Location = new Point((componentesGenericos.puntoMedioHorizontal - 167), (componentesGenericos.puntoMedioVertical - 22));

                picboxRecuaMat.Location = new Point((componentesGenericos.puntoMedioHorizontal - 225), (componentesGenericos.puntoMedioVertical - (alturaTotalLista / 2) - 8));
                picboxRecuaMat.Height = alturaTotalLista + 6;

                listFamiliaMat.Location = new Point((componentesGenericos.puntoMedioHorizontal - 2), (componentesGenericos.puntoMedioVertical - ((alturaTotalLista / 2) - 2)));
                listFamiliaMat.Height = alturaTotalLista;
                listFamiliaMat.Width = 225;
            }
        }

        private void dgvMateriales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Limpiar selección anterior
                dgvMateriales.ClearSelection();

                // Seleccionar solo la fila actual sin afectar el encabezado de columna
                dgvMateriales.Rows[e.RowIndex].Selected = true;

                // No dejar ninguna celda como "CurrentCell" para que no se muestre el estilo de celda activa
                // al hacer clic sobre la columna "Familia". Mantener la fila seleccionada (FullRowSelect) sólo.
                // Obtener IdMaterial de la fila clicada ANTES de anular la celda actual.
                // Así evitamos que CurrentRow sea null tras asignar CurrentCell = null.
                var fila = dgvMateriales.Rows[e.RowIndex];
                string IdMaterial = null;
                if (fila.Cells.Count > 14 && fila.Cells[14].Value != null)
                {
                    IdMaterial = fila.Cells[14].Value.ToString();
                }

                // Intentar establecer una celda válida como CurrentCell para preservar apariencia de selección (azul)
                try
                {
                    DataGridViewCell firstVisibleCell = null;
                    for (int i = 0; i < fila.Cells.Count; i++)
                    {
                        var c = fila.Cells[i];
                        if (c.Visible)
                        {
                            firstVisibleCell = c;
                            break;
                        }
                    }
                    if (firstVisibleCell != null)
                    {
                        dgvMateriales.CurrentCell = firstVisibleCell;
                    }
                }
                catch
                {
                    // Ignorar InvalidOperationException u otros estados del control.
                }

                if (!string.IsNullOrEmpty(IdMaterial))
                {
                    funciones.BuscarFotoPorIdMaterial(IdMaterial, picboxFotoMat);
                }
                else
                {
                    picboxFotoMat.Image = null;
                }

                labListaFamilia1.Visible = false;
                labListaFamilia2.Visible = false;
                labTituloDgv.Location = new Point(1467, 147);

                botIngresar.BackColor = Color.Transparent;
                botBuscar.BackColor = Color.Transparent;
                botModifica.Enabled = true;
                botModifica.BackColor = Color.CadetBlue;
                botModifica.FlatStyle = FlatStyle.Standard;
                botEliminar.Enabled = true;
                botEliminar.BackColor = Color.CadetBlue;
                botEliminar.FlatStyle = FlatStyle.Standard;
                botInforma.BackColor = Color.Transparent;
                botInforma.FlatStyle = FlatStyle.Flat;
                botInforma.Enabled = false;
                botSalir.Visible = false;
                botRegresa.Visible = true;
                botRegresa.Location = new Point((componentesGenericos.puntoMedioHorizontal + 596), (componentesGenericos.puntoMedioVertical + 415));
                botIngresar.Enabled = false;
                botIngresar.FlatStyle = FlatStyle.Flat;
                botBuscar.Enabled = false;
                botBuscar.FlatStyle = FlatStyle.Flat;
                botImprimir.FlatStyle = FlatStyle.Flat;
                botImprimir.Enabled = false;
                botImprimir.BackColor = Color.Transparent;

                listFamiliaMat.Visible = false;

                picboxRecuadroGrande.Visible = false;
                picboxRecuadroTitulodgv.Location = new Point(1433, 127);
                picboxFotoMat.Visible = true;
                picboxFotoMat.Location = new Point(13, 223);
                picboxFotoMat.Size = new Size(180, 180);

                dgvMateriales.Location = new Point(204, 190);
            }
        }

        private void botIngresarFam_Click(object sender, EventArgs e)
        {
            famStatus = "Ingresar";

            labFamiliaMat.Visible = false;
            labIngresoMat.Visible = false;
            labIngresoFamilia.Visible = true;
            labIngresoFamilia.Text = "Ingrese nueva familia";
            labIngresoFamilia.Font = new Font("Arial", 20, FontStyle.Regular | FontStyle.Underline);
            labIngresoFamilia.Location = new Point((componentesGenericos.puntoMedioHorizontal - 135), (componentesGenericos.puntoMedioVertical - 250));

            texNuevoFam.Text = "";
            texNuevoFam.Visible = true;
            texNuevoFam.BringToFront();
            texNuevoFam.Location = new Point((componentesGenericos.puntoMedioHorizontal - 120), (componentesGenericos.puntoMedioVertical - 20));
            texNuevoFam.Focus();

            botIngresarFam.Visible = false;
            botGuardarFam.Text = "GUARDAR";
            botGuardarFam.Location = new Point(picboxRecuaMat.Location.X, (componentesGenericos.puntoMedioVertical + 135));
            botCancelarMat.Visible = false;
            botCancelarFam.Visible = true;
            botCancelarFam.Location = new Point((componentesGenericos.puntoMedioHorizontal + 430), (componentesGenericos.puntoMedioVertical + 360));
            botModificarFam .Visible = false;   
            botEliminarFam.Visible = false;
            botRegresa.Visible = false;

            listFamiliaMat.Visible = false;

            panFamiliasUnidades.Visible = false;

            picboxRecuaMat.Visible = true;
            picboxRecuaMat.Height = 220;
            picboxRecuaMat.Location = new Point(picboxRecuaMat.Location.X+10, (componentesGenericos.puntoMedioVertical - (picboxRecuaMat.Height / 2)));

            EliminarFamilia = false;
        }

        private void botGuardarFam_Click(object sender, EventArgs e)
        {
            if (EliminarFamilia == false && modificarFam == false)
            {
                funciones.GuardarFamilia(texNuevoFam);

                labIngresoFamilia.Visible = false;
                labIngresoMat.Visible = true;

                texNuevoFam.Visible = false;

                botFamilias.Visible = true;
                botRegistros.Visible = true;
                botGuardarFam.Visible = false;
                botCancelarFam.Visible = false;
                botRegresa.Visible = true;

                panFamiliasUnidades.Visible = true;
                panFamiliasUnidades.Location = new Point(710, 390);
                panFamiliasUnidades.Size = new Size(500, 250);

                picboxRecuaMat.Visible = false;

                funciones.mostrarFamilia(listFamiliaMat);
            }
            else if (EliminarFamilia == false && modificarFam == true)
            {
                funciones.modificarFamilia(nombreFamilia, texNuevoFam.Text);
                modificarFam = false;

                // Restaurar UI después de modificar familia
                labIngresoFamilia.Visible = false;
                labIngresoMat.Visible = true;
                labIngresoMat.Location = new Point(835, 280);
                labIngresoMat.Font = new Font("Arial", 20, FontStyle.Regular | FontStyle.Underline);
                labIngresoMat.BackColor = Color.FromArgb(153, 180, 209);

                texNuevoFam.Visible = false;

                botFamilias.Visible = true;
                botFamilias.Location = new Point(790, 485);
                botRegistros.Visible = true;
                botRegistros.Location = new Point(990, 485);
                botGuardarFam.Visible = false;
                botCancelarFam.Visible = false;
                botModificarFam.Visible = false;
                botRegresa.Visible = true;
                botRegresa.Location = new Point(1556, 950);

                panFamiliasUnidades.Visible = true;
                panFamiliasUnidades.Location = new Point(710, 390);
                panFamiliasUnidades.Size = new Size(500, 250);
                panRecuaIngreso.Location = new Point((componentesGenericos.puntoMedioHorizontal - 575), (componentesGenericos.puntoMedioVertical - 340));
                panRecuaIngreso.Size = new Size(1150, 680);

                picboxRecuaMat.Visible = false;

                // Recargar lista de familias para reflejar los cambios
                funciones.mostrarFamilia(listFamiliaMat);
                listFamiliaMat.Visible = false;
            }
            else if (EliminarFamilia == true)
            {
                bool existe = dgvMateriales.Rows.Cast<DataGridViewRow>().Any(r => r.Cells["Familia"].Value.ToString() == listFamiliaMat.SelectedItem.ToString());
                if (existe)
                {
                    MessageBox.Show("Esta familia tiene materiales existentes, primero borre los materiales.");
                } else
                {
                    funciones.eliminarFamilia(listFamiliaMat.SelectedItem.ToString());
                    
                    // Restaurar UI después de eliminar familia
                    labIngresoFamilia.Visible = false;
                    labIngresoMat.Visible = true;
                    labIngresoMat.Location = new Point(835, 280);
                    labIngresoMat.Font = new Font("Arial", 20, FontStyle.Regular | FontStyle.Underline);
                    labIngresoMat.BackColor = Color.FromArgb(153, 180, 209);
                    
                    labEliminarFamilia.Visible = false;

                    botFamilias.Visible = true;
                    botFamilias.Location = new Point(790, 485);
                    botRegistros.Visible = true;
                    botRegistros.Location = new Point(990, 485);
                    botGuardarFam.Visible = false;
                    botCancelarFam.Visible = false;
                    botEliminarFam.Visible = false;
                    botRegresa.Visible = true;
                    botRegresa.Location = new Point(1556, 950);

                    panFamiliasUnidades.Visible = true;
                    panFamiliasUnidades.Location = new Point(710, 390);
                    panFamiliasUnidades.Size = new Size(500, 250);
                    panRecuaIngreso.Location = new Point((componentesGenericos.puntoMedioHorizontal - 575), (componentesGenericos.puntoMedioVertical - 340));
                    panRecuaIngreso.Size = new Size(1150, 680);

                    picboxRecuaMat.Visible = false;

                    // Recargar lista de familias
                    funciones.mostrarFamilia(listFamiliaMat);
                    listFamiliaMat.Visible = false;
                    
                    EliminarFamilia = false;
                }
            }
        }

        private void botCancelarFam_Click(object sender, EventArgs e)
        {
            labIngresoFamilia.Visible = false;
            labIngresoMat.Visible = true;
            labEliminarFamilia.Visible = false;

            texNuevoFam.Visible = false;

            botFamilias.Visible = true;
            botRegistros.Visible = true;
            botGuardarFam.Visible = false;
            botCancelarFam.Visible = false;
            botRegresa.Visible = true;
            botModificarFam.Visible = false;  
           
            panFamiliasUnidades.Visible = true;
            panFamiliasUnidades.Location = new Point(710, 390);
            panFamiliasUnidades.Size = new Size(500, 250);
            panRecuaIngreso.Location = new Point((componentesGenericos.puntoMedioHorizontal - 575), (componentesGenericos.puntoMedioVertical - 340));
            panRecuaIngreso.Size = new Size(1150, 680);

            listFamiliaMat.Visible = false;

            EliminarFamilia = false;

            picboxRecuaMat.Visible = false;
        }

        private void botCancelarMat_Click(object sender, EventArgs e)
        {
            labIngresoMat.Visible = true;
            labIngresoMat.Location = new Point(835, 280);
            labIngresoMat.Font = new Font("Arial", 20, FontStyle.Regular | FontStyle.Underline);
            labIngresoMat.BackColor = Color.FromArgb(153, 180, 209);

            labModificaMat.Visible = false;
            labEliminaMat.Visible = false;
            labFamiliaMat.Visible = false;

            picboxRecuaTituloMat.Visible = false;
            picboxFotoMat.Visible = false;
            picboxRecuadroGrande.Visible = false;

            botFamilias.Visible = true;
            botFamilias.Location = new Point(790, 485);
            botRegistros.Visible = true;
            botRegistros.Location = new Point(990, 485);
            botEliminarFam.Visible = false;
            botCancelarMat.Visible = false;
            botRegresa.Visible = true;
            botRegresa.Location = new Point(1556, 950);
            botModificarFam .Visible = false;   

            panFamiliasUnidades.Visible = true;
            panFamiliasUnidades.Location = new Point(710, 390);
            panFamiliasUnidades.Size = new Size(500, 250);

            picboxRecuaMat.Visible = false;

            listFamiliaMat.Visible = false;
        }

        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox list = (ListBox)sender;
            if (e.Index > -1)
            {
                object item = list.Items[e.Index];
                bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

                // Dibujar fondo personalizado: usar mismo celeste que el resto (113,173,210)
                Color selectedBack = Color.FromArgb(113, 173, 210);
                Color backColor = selected ? selectedBack : list.BackColor;
                using (SolidBrush backBrush = new SolidBrush(backColor))
                {
                    e.Graphics.FillRectangle(backBrush, e.Bounds);
                }

                // Dibujar foco (borde)
                e.DrawFocusRectangle();

                // Texto: mantener negro siempre según requerimiento
                Color textColor = Color.Black;
                using (Brush brush = new SolidBrush(textColor))
                {
                    SizeF size = e.Graphics.MeasureString(item.ToString(), e.Font);
                    e.Graphics.DrawString(item.ToString(), e.Font, brush, e.Bounds.Left + (e.Bounds.Width / 2 - size.Width / 2), e.Bounds.Top + (e.Bounds.Height / 2 - size.Height / 2));
                }
            }
        }

        private void botEliminarFam_Click(object sender, EventArgs e)
        {
            EliminarFamilia = true;

            labIngresoFamilia.Visible = false;
            labEliminarFamilia.Visible = true;
            labEliminarFamilia.Location = new Point(863, 160);
            labIngresoMat.Visible = false;
           
            botIngresarFam.Visible = false;
            botModificarFam.Visible = false;
            botEliminarFam.Visible = false;
            botCancelarFam.Location = new Point(1076, 890);
            botCancelarFam.Visible = true;
            
            
            botGuardarFam.Text = "Eliminar";
            botGuardarFam.ForeColor = Color.Red;

            botRegresa.Visible = false;

            panRecuaIngreso.Visible = false;
            panFamiliasUnidades.Visible = false;

            listFamiliaMat.Visible = true;
          
        }

        private void botEliminar_Click(object sender, EventArgs e)
        {
            funciones.EliminarMaterial(dgvMateriales.CurrentRow.Cells[14].Value.ToString());
            funciones.mostrarMateriales(dgvMateriales);
        }

        private void botBuscar_Click(object sender, EventArgs e)
        {
            Buscar = true;
            funciones.mostrarFamilia(listFamiliaMat);

            labTituloDgv.Location = new Point(1617, 147);
            labListaFamilia1.Visible = true;
            labListaFamilia1.Location = new Point(135, 220);
            labListaFamilia1.BackColor = Color.FromArgb(113, 173, 210);
            labListaFamilia2.Visible = true;
            labListaFamilia2.Location = new Point(137, 240);
            labListaFamilia2.BackColor = Color.FromArgb(113, 173, 210);

            botIngresar.Location = new Point(423, 956);
            botIngresar.Enabled = false;
            botIngresar.FlatStyle = FlatStyle.Flat;
            botIngresar.BackColor = Color.Transparent;
            botBuscar.Enabled = false;
            botBuscar.FlatStyle = FlatStyle.Flat;
            botBuscar.BackColor = Color.Transparent;
            botModifica.Enabled = false;
            botModifica.FlatStyle = FlatStyle.Flat;
            botModifica.BackColor = Color.LightSteelBlue;
            botInforma.Enabled = false;
            botInforma.FlatStyle = FlatStyle.Flat;
            botInforma.BackColor = Color.LightSteelBlue;
            botEliminar.Location = new Point(957, 956);
            botEliminar.Enabled = false;
            botEliminar.FlatStyle = FlatStyle.Flat;
            botEliminar.BackColor = Color.LightSteelBlue;
            botImprimir.FlatStyle = FlatStyle.Flat;
            botImprimir.BackColor = Color.LightSteelBlue;
            botImprimir.Enabled = false;
            botSalir.Visible = false;
            botRegresa.Visible = true;
            botRegresa.Location = new Point((componentesGenericos.puntoMedioHorizontal + 596), (componentesGenericos.puntoMedioVertical + 415));

            picboxRecuadroGrande.Visible = true;
            int nuevoHeight = (listFamiliaMat.Items.Count * 20) + 5;
            picboxRecuadroGrande.BackColor = Color.FromArgb(113, 173, 210);
          
            if (nuevoHeight > 668)
            {
                picboxRecuadroGrande.Height = 745;
                listFamiliaMat.Height = 668;
            }
            else
            {
                picboxRecuadroGrande.Height = nuevoHeight + 77;
                listFamiliaMat.Height = nuevoHeight;
            }
            picboxRecuadroGrande.Width = 185;
            picboxRecuadroGrande.Location = new Point(90, 200);
            picboxRecuadroGrande.SendToBack();
            picboxRecuadroGrande.BorderStyle = BorderStyle.Fixed3D;
            picboxRecuadroGrande.BackColor = Color.FromArgb(113, 173, 210);
            picboxFotoMat.Visible = false;
            picboxRecuadroTitulodgv.Location = new Point(1583, 127);

            listFamiliaMat.Visible = true;
            listFamiliaMat.Width = 180;
            listFamiliaMat.Location = new Point(92, 277);
            listFamiliaMat.BackColor = Color.FromArgb(113, 173, 210);

            dgvMateriales.ClearSelection();
            dgvMateriales.CurrentCell = null;
            dgvMateriales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMateriales.MultiSelect = false;
            dgvMateriales.Location = new Point(354, 190);
            dgvMateriales.BackgroundColor = Color.LightSteelBlue;
            dgvMateriales.BorderStyle = BorderStyle.None;
            dgvMateriales.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
        }

        private void texNuevoFam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                botGuardarFam.Visible = true;
                botGuardarFam.Location = new Point((componentesGenericos.puntoMedioHorizontal + 180), (componentesGenericos.puntoMedioVertical + 361));
                botGuardarFam.Focus();
            }
        }

        private void botModifica_Click(object sender, EventArgs e)
        {
            frmModificaciones Modifica = new frmModificaciones(this, componentesGenericos);
            Modifica.StartPosition = FormStartPosition.Manual;

            Modifica.Left = 200;
            Modifica.Top = 150;

            labTituloDgv.Visible = false;

            Modifica.texFamilia.BackColor = Color.LightSteelBlue;
            Modifica.texFamilia.ForeColor = Color.DarkGreen;
            Modifica.texGrupo.BackColor = Color.LightSteelBlue;
            Modifica.texCaracteristica.BackColor = Color.LightSteelBlue;
            Modifica.texColor.BackColor = Color.LightSteelBlue;
            Modifica.texMedidas.BackColor = Color.LightSteelBlue;
            Modifica.texCodigo.BackColor = Color.LightSteelBlue;
            Modifica.texFabricante.BackColor = Color.LightSteelBlue;
            Modifica.texEstado.BackColor = Color.LightSteelBlue;
            Modifica.texUbicacion.BackColor = Color.LightSteelBlue;
            Modifica.texCantidad.BackColor = Color.LightSteelBlue;
            Modifica.texDisponible.BackColor = Color.LightSteelBlue;
            Modifica.texTipo.BackColor = Color.LightSteelBlue;
            Modifica.texValor.BackColor = Color.LightSteelBlue;
            Modifica.cantOriginal = dgvMateriales.CurrentRow.Cells[9].Value.ToString();

            botIngresar.Visible = false;
            botModifica.Visible = false;
            botEliminar.Visible = false;
            botInforma.Visible = false;
            botImprimir.Visible = false;
            botBuscar.Visible = false;
            botSalir.Visible = false;
            botRegresa.Visible = false;
            Modifica.botGuardar.Visible = false;

            picboxRecuadroTitulodgv.Visible = false;
            picboxRecuadroGrande.Visible = false;
            picboxRecuaMat.Visible = false;
            picboxFotoMat.Size = new Size(604, 579);
            picboxFotoMat.Location = new Point(1220, 253);

            panRecuaIngreso.Visible = false;
            panFamiliasUnidades.Visible = false;

            listUbicaciones.Visible = false;

            dgvMateriales.Visible = false;

            Modifica.ShowDialog();
        }

        private void picboxFotoMat_DoubleClick(object sender, EventArgs e)
        {
            labTituloDgv.Visible = false;

            botIngresar.Visible = false;
            botModifica.Visible = false;
            botEliminar.Visible = false;
            botInforma.Visible = false;
            botImprimir.Visible = false;
            botBuscar.Visible = false;
            botSalir.Visible = false;
            botSalirFotoAgrandada.Visible = true;
            botSalirFotoAgrandada.Location = new Point(1359, 902);
            botSalirFotoAgrandada.BringToFront();
            botRegresa.Visible = false;

            panRecuaIngreso.Visible = false;

            picboxFotoMat.Size = new Size(900, 900);
            picboxFotoMat.Location = new Point(500, 140);
            picboxRecuadroTitulodgv.Visible = false;

            dgvMateriales.Visible = false;
        }

        private void botSalirFotoAgrandada_Click(object sender, EventArgs e)
        {
            labTituloDgv.Visible = true;

            botIngresar.Visible = true;
            botModifica.Visible = true;
            botEliminar.Visible = true;
            botInforma.Visible = true;
            botImprimir.Visible = true;
            botBuscar.Visible = true;
            botSalir.Visible = true;
            botSalirFotoAgrandada.Visible = false;
            botRegresa.Visible = true;

            picboxRecuadroTitulodgv.Visible = true;
            picboxFotoMat.Location = new Point(13, 223);
            picboxFotoMat.Size = new Size(180, 180);

            dgvMateriales.Visible = true;
        }

        private void botImprimir_Click(object sender, EventArgs e)
        {
            printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(printDocument1_BeginPrint);
            printPreviewDialog1.Document = printDocument1;

            printPreviewDialog1.ClientSize = new Size(1400, 1000); // Define el tamaño de la ventana
            printPreviewDialog1.StartPosition = FormStartPosition.CenterScreen; // Centra la ventana en la pantalla

            printPreviewDialog1.ShowDialog();
        }

        /*    Esto es lo que me dio la inteligencia para que el listado salga recuadrado como Excel pero me da error Graphics
            {
            int startX = 50; // Margen izquierdo
            int startY = 50; // Margen superior
            int cellHeight = 30; // Altura de cada celda
            int cellPadding = 5; // Espaciado interno de la celda
            int currentY = startY; // Coordenada Y actual

            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage);
            // Dibujar encabezado
            using (Font headerFont = new Font("Arial", 10, FontStyle.Bold))
            using (Pen pen = new Pen(Color.Black))
            {
                int currentX = startX;

                foreach (DataGridViewColumn col in dgvMateriales.Columns)
                {
                    int cellWidth = col.Width; // Puedes usar un tamaño fijo o basado en el ancho de la columna del DataGridView

                    // Dibujar recuadro del encabezado
                    e.Graphics.DrawRectangle(pen, currentX, currentY, cellWidth, cellHeight);

                    // Dibujar texto del encabezado
                    e.Graphics.DrawString(col.HeaderText, headerFont, Brushes.Black,
                        new RectangleF(currentX + cellPadding, currentY + cellPadding, cellWidth - cellPadding, cellHeight - cellPadding));

                    currentX += cellWidth; // Avanzar a la siguiente columna
                }

                currentY += cellHeight; // Mover a la siguiente fila (cuerpo de datos)
            }

            // Dibujar datos
            using (Font bodyFont = new Font("Arial", 10))
            using (Pen pen = new Pen(Color.Black))
            {
                foreach (DataGridViewRow row in dgvMateriales.Rows)
                {
                    if (row.IsNewRow) continue; // Ignorar la fila de entrada de datos

                    int currentX = startX;

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        int cellWidth = dgvMateriales.Columns[cell.ColumnIndex].Width; // Usar ancho de la columna original

                        // Dibujar recuadro de la celda
                        e.Graphics.DrawRectangle(pen, currentX, currentY, cellWidth, cellHeight);

                        // Dibujar texto dentro de la celda
                        e.Graphics.DrawString(cell.Value?.ToString(), bodyFont, Brushes.Black,
                            new RectangleF(currentX + cellPadding, currentY + cellPadding, cellWidth - cellPadding, cellHeight - cellPadding));

                        currentX += cellWidth; // Avanzar a la siguiente celda
                    }

                    currentY += cellHeight; // Mover a la siguiente fila
                }
            }

            // Indicar si hay más páginas (en este caso asumimos que cabe todo en una página)
            e.HasMorePages = false;
        }*/

        private void botRegresa_Click(object sender, EventArgs e)
        {
            labFamiliaMat.Visible = false;
            labTituloDgv.Visible = true;
            labTituloDgv.Location = new Point(1467, 147);
            labListaFamilia1.Visible = false;
            labListaFamilia2.Visible = false;
            labTituloPrincipal.Location = new Point((componentesGenericos.puntoMedioHorizontal - 102), (componentesGenericos.puntoMedioVertical - 457));
            labIngresoMat.Visible = false;

            texFamiliaBuscar.Visible = false;

            botIngresar.Visible = true;
            botIngresar.Enabled = true;
            botIngresar.FlatStyle = FlatStyle.Standard;
            botIngresar.BackColor = Color.CadetBlue;
            botIngresar.Location = new Point(423, 956);
            botIngresarFam.Visible = false;
            botIngresarTipo.Visible = false;
            botBuscar.Visible = true;
            botBuscar.Enabled = true;
            botBuscar.FlatStyle = FlatStyle.Standard;
            botBuscar.BackColor = Color.CadetBlue;
            botBuscar.Location = new Point(601, 956);
            botModifica.Visible = true;
            botModifica.Enabled = false;
            botModifica.FlatStyle = FlatStyle.Flat;
            botModifica.BackColor = Color.Transparent;
            botEliminar.Visible = true;
            botEliminar.Enabled = false;
            botEliminar.FlatStyle = FlatStyle.Flat;
            botEliminar.BackColor = Color.Transparent;
            botEliminar.Location = new Point(957, 956);
            botInforma.Visible = true;
            botInforma.Enabled = true;
            botInforma.FlatStyle = FlatStyle.Standard;
            botInforma.BackColor = Color.CadetBlue;
            botInforma.Location = new Point(1135, 956);
            botImprimir.Visible = true;
            botImprimir.FlatStyle = FlatStyle.Flat;
            botImprimir.BackColor = Color.Transparent;
            botImprimir.Location = new Point(1313, 956);
            botFamilias.Visible = false;
            botRegistros.Visible = false;
            botEliminarFam.Visible = false;
            botModificarFam.Visible = false;
            botRegresa.Visible = false;
            botSalir.Visible = true;
            botSalir.Location = new Point((componentesGenericos.puntoMedioHorizontal + 596), (componentesGenericos.puntoMedioVertical + 415));

            picboxFotoMat.Visible = false;
            picboxRecuadroGrande.Visible = false;
            picboxRecuadroTitulodgv.Visible = true;
            picboxRecuadroTitulodgv.Location = new Point(1433, 127);
            picboxRecuaMat.Visible = false;

            listFamiliaMat.Visible = false;
            listUbicaciones.Visible = false;

            dgvMateriales.Visible = true;
            dgvMateriales.Location = new Point(204, 190);
            dgvMateriales.ClearSelection();
            dgvMateriales.CurrentCell = null;
            dgvMateriales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMateriales.MultiSelect = false;
            dgvMateriales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMateriales.Location = new Point(204, 190);
            dgvMateriales.Size = new Size(1510, 745);
            dgvMateriales.EnableHeadersVisualStyles = false; // Deshabilitar estilos visuales
            dgvMateriales.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ButtonShadow;
            dgvMateriales.DefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
            dgvMateriales.Columns[13].Visible = false;
            dgvMateriales.Columns[14].Visible = false;
            dgvMateriales.Columns[15].Visible = false;

            Buscar = false;

            funciones.mostrarMateriales(dgvMateriales);
            dgvMateriales.ClearSelection();
            dgvMateriales.CurrentCell = null;
        }

        private void listFamiliaMat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EliminarFamilia == false && modificarFam == false)  //Ingreso de familia
            {
                 if (NuevoModificacionOEliminacion == "Nuevo" && listFamiliaMat.SelectedIndex >= 0 && Buscar == false)
                {
                    labIngresoMat.Visible = true;
                    labIngresoMat.Location = new Point(840, 135);
                    labIngresoMat.Font = new Font(labIngresoMat.Font.FontFamily, 20, FontStyle.Underline);
                    labIngresoMat.BackColor = Color.LightSteelBlue;

                    botIngresarFam.Visible = false;
                    botCancelarMat.Visible = false;
                    botModifica.Enabled = false;
                    botEliminar.Enabled = false;
                    botRegresa.Visible = false;

                    picboxRecuaMat.Visible = false;
                    picboxRecuaTituloMat.Location = new Point(826, 143);
                    picboxRecuaTituloMat.SendToBack();

                    frmIngreso.texGrupo.Select();
                    frmIngreso.ShowDialog();
                }
                else if (listFamiliaMat.SelectedIndex >= 0 && Buscar == true)
                {
                    labListaFamilia1.Visible = false;
                    labListaFamilia2.Visible = false;
                    labTituloDgv.Location = new Point(1467, 147);
                    
                    texFamiliaBuscar.Visible = true;
                    texFamiliaBuscar.Text = "FAMILIA";
                    texFamiliaBuscar.Font = new Font(labFamiliaMat.Font, FontStyle.Bold);
                    texFamiliaBuscar.ForeColor = Color.DarkOliveGreen;
                    // Aumentar ancho del textbox para familias largas
                    texFamiliaBuscar.Width = 400;
                    texFamiliaBuscar.Text = listFamiliaMat.SelectedItem.ToString();
                    texFamiliaBuscar.Location = new Point(205, 158);
                    texFamiliaBuscar.BorderStyle = BorderStyle.None;
                    texFamiliaBuscar.BackColor = Color.LightSteelBlue;
                    texFamiliaBuscar.TabStop = false;  //Hace que el text pierda el foco y no lo pinte de azul+

                    botImprimir.Enabled = true;
                    botImprimir.FlatStyle = FlatStyle.Standard;
                    botImprimir.BackColor = Color.CadetBlue;

                    picboxRecuadroGrande.Visible = false;
                    picboxRecuadroTitulodgv.Location = new Point(1433, 127);

                    listFamiliaMat.Visible = false;

                    funciones.llenarDGVSeleccionStock(dgvMateriales, listFamiliaMat.SelectedItem.ToString());
                    dgvMateriales.ClearSelection();
                    dgvMateriales.CurrentCell = null;
                    dgvMateriales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvMateriales.MultiSelect = false;
                    dgvMateriales.Columns[0].Visible = false;
                    dgvMateriales.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                }
                else
                {
                    botGuardarFam.Visible = true;
                }
            }
            else if (modificarFam == true)
            {
                nombreFamilia = listFamiliaMat.SelectedItem.ToString();

                texNuevoFam.Visible = true;
                texNuevoFam.Text = listFamiliaMat.SelectedItem.ToString();

                // **1. Otorga el foco al TextBox para que esté activo.**
                texNuevoFam.Focus();

                // **2. Mueve el cursor al final del texto.**
                texNuevoFam.SelectionStart = texNuevoFam.Text.Length;

                // **3. Anula cualquier texto seleccionado (quita el azul).**
                texNuevoFam.SelectionLength = 0;

                botGuardarFam.Visible = true;
                botCancelarFam.Visible = true;

                listFamiliaMat.Visible = false;

                // Cargar materiales de la familia seleccionada en el DataGridView
                string familiaSeleccionada = listFamiliaMat.SelectedItem.ToString();
                funciones.MaterialesEnExistencia(dgvMateriales, familiaSeleccionada);
                dgvMateriales.Visible = true;
                dgvMateriales.Location = new Point(355, 300);
                dgvMateriales.Size = new Size(1300, 400);
            }
            else if (EliminarFamilia == true)
            {
                nombreFamilia = listFamiliaMat.SelectedItem.ToString();

                // Cargar materiales de la familia seleccionada en el DataGridView
                string familiaSeleccionada = listFamiliaMat.SelectedItem.ToString();
                funciones.MaterialesEnExistencia(dgvMateriales, familiaSeleccionada);
                dgvMateriales.Visible = true;
                dgvMateriales.Location = new Point(355, 300);
                dgvMateriales.Size = new Size(1300, 400);

                listFamiliaMat.Visible = false;
                botGuardarFam.Visible = true;
                botCancelarFam.Visible = true;
            }
        }

        private void botFamilias_Click(object sender, EventArgs e)
        {
            labIngresoMat .Visible = true;
            labIngresoMat.Text = "Familias";
            labIngresoMat.Location = new Point(900, 300);
            labIngresoMat.Font = new Font("Arial", 20, FontStyle.Regular | FontStyle.Underline);
            labIngresoMat.BackColor = Color.FromArgb(153, 180, 209);
            labEliminarFamilia.Font = new Font("Arial", 20, FontStyle.Regular | FontStyle.Underline);
            labEliminarFamilia.Location = new Point(600, 200);

            botFamilias.Visible = false;
            botRegistros.Visible = false;

            botIngresarFam.Visible = true;
            botIngresarFam.Location = new Point(894, 480);

            botModificarFam.Visible = true;
            botModificarFam.Location = new Point(805, 560);

            botEliminarFam.Visible = true;
            botEliminarFam.Location = new Point(985, 560);

            panFamiliasUnidades.Location = new Point(716, 440);
            panFamiliasUnidades.Size = new Size(490, 220);
        }

        private void botRegistros_Click(object sender, EventArgs e)
        {
            labFamiliaMat.Visible = true;
            labFamiliaMat.BackColor = Color.FromArgb(113, 173, 210);
            labIngresoMat.Location = new Point(840, 270);
            labIngresoMat.Text = "Ingreso de registro";

            botFamilias.Visible = false;
            botRegistros.Visible = false;

            picboxRecuaMat.Visible = true;
            picboxRecuaMat.BackColor = Color.FromArgb(113, 173, 210);

            listFamiliaMat.Visible = true;
            listFamiliaMat.BackColor = Color.FromArgb(113, 173, 210);

            panFamiliasUnidades.Visible = false;
            picboxRecuaMat_AutoSize();
        }

        private void botTipoDeUnidades_Click(object sender, EventArgs e)
        {
            botFamilias.Visible = false;
            botRegistros.Visible = false;
            botIngresarTipo.Visible = true;
            botIngresarTipo.Location = new Point(880, 650);
            botEliminarTipo.Visible = true;
            botEliminarTipo.Location = new Point(1054, 650);
        }

        private void dgvMateriales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                listUbicaciones.Location = new Point(13, 500);
                listUbicaciones.Visible = true;
            }
        }

        private void botInforma_Click(object sender, EventArgs e)
        {
            Buscar = false;
            labTituloDgv.Visible = false;

            texFamiliaBuscar.Visible = false;

            botIngresar.Visible = false;
            botBuscar.Visible = false;
            botModifica.Visible = false;
            botEliminar.Visible = false;
            botInforma.Visible = false;
            botImprimir.Visible = false;
            botSalir.Visible = false;

            picboxRecuadroTitulodgv.Visible = false;
            picboxFotoMat.Visible = false;

            dgvMateriales.Visible = false;

            listFamiliaMat.Visible = false;

            frmListados Listados = new frmListados(this);
            Listados.StartPosition = FormStartPosition.Manual;
            Listados.Location = new Point(150, 170);
            Listados.botSalirListados.Visible = true;
            Listados.botSalirListados.Focus();

            Listados.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int yPos = e.MarginBounds.Top;
            int xPos = e.MarginBounds.Left;
            int cellHeight = 0;
            int rowCount = 0;
            int colCount = 0;

            // Imprimir encabezados de las primeras 5 columnas
            foreach (DataGridViewColumn col in dgvMateriales.Columns)
            {
                if (colCount >= 5) break;  // Limitar a 5 columnas

                e.Graphics.DrawString(col.HeaderText, dgvMateriales.Font, Brushes.Black, xPos, yPos);
                xPos += col.Width;
                colCount++;
            }

            yPos += dgvMateriales.ColumnHeadersHeight;  // Mueve la posición vertical para las filas

            // Imprimir las primeras 5 filas y 5 columnas
            foreach (DataGridViewRow row in dgvMateriales.Rows)
            {
                if (rowCount >= 5) break;  // Limitar a 5 filas

                xPos = e.MarginBounds.Left;
                cellHeight = row.Height;
                colCount = 0;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (colCount >= 5) break;  // Limitar a 5 columnas

                    string cellValue = cell.Value == null ? "" : cell.Value.ToString();
                    e.Graphics.DrawString(cellValue, dgvMateriales.Font, Brushes.Black, xPos, yPos);
                    xPos += cell.OwningColumn.Width;
                    colCount++;
                }

                yPos += cellHeight;
                rowCount++;
            }

            e.HasMorePages = false;  // No hay más páginas, ya que estamos limitando la impresión
        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int colCount = Math.Min(dgvMateriales.Columns.Count, dgvMateriales.Columns.Count); // Puedes cambiar 5 a dgvMateriales.Columns.Count si quieres todas las columnas
            int cellWidth = 100; // Ancho de cada celda
            int maxRowsPerPage = e.MarginBounds.Height / rowHeight; // Filas máximas por página
            int startX = 10; // Posición inicial en X

            Font printFont = new Font("Arial", 9);
            
            if (rowIndex == 0)  // Imprimir cabeceras de columna solo en la primera página
            {
                for (int col = 0; col < colCount; col++)
                {
                    string headerText = dgvMateriales.Columns[col].HeaderText;
                    e.Graphics.DrawString(headerText, printFont, Brushes.Black, startX + (col * cellWidth), startY);
                }
                startY += rowHeight; // Deja espacio para la cabecera
            }
            
            while (rowIndex < dgvMateriales.Rows.Count)  // Imprime cada fila comenzando desde la fila actual
            {
                // Chequea si aún cabe otra fila en la página actual
                if (startY + rowHeight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true; // Indica que hay más páginas por imprimir
                    startY = 100; // Reinicia la posición en Y para la siguiente página
                    return; // Sale del método para procesar la siguiente página
                }
                for (int col = 0; col < colCount; col++)  // Imprimir celdas de la fila actual
                {
                    string cellValue = dgvMateriales.Rows[rowIndex].Cells[col].FormattedValue?.ToString();
                    e.Graphics.DrawString(cellValue, printFont, Brushes.Black, startX + (col * cellWidth), startY);
                }

                startY += rowHeight; // Mueve la posición en Y para la siguiente fila
                rowIndex++; // Avanza a la siguiente fila
            }

            // Si se imprimieron todas las filas, reinicia las variables
            e.HasMorePages = false;
            rowIndex = 0;
            startY = 100;
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printDocument1.DefaultPageSettings.Landscape = true;  // Asegura que la orientación esté en vertical
        }
              
        private void botModificarFam_Click(object sender, EventArgs e)
        {
            modificarFam = true;
            labFamiliaMat.Visible = false;
            labIngresoMat.Visible = false;
            labIngresoFamilia.Visible = true;
            labIngresoFamilia.Text = "Modifica familia";
            labIngresoFamilia.Font = new Font("Arial", 20, FontStyle.Regular | FontStyle.Underline);
            labIngresoFamilia.Location = new Point((componentesGenericos.puntoMedioHorizontal - 135), (componentesGenericos.puntoMedioVertical - 250));

            texNuevoFam.Text = "";
            texNuevoFam.BringToFront();
            texNuevoFam.Location = new Point((componentesGenericos.puntoMedioHorizontal - 120), (componentesGenericos.puntoMedioVertical - 20));
            texNuevoFam.Focus();

            botIngresarFam.Visible = false;
            botGuardarFam.Text = "GUARDAR";
            botGuardarFam.Location = new Point(picboxRecuaMat.Location.X, (componentesGenericos.puntoMedioVertical + 135));
            botCancelarMat.Visible = false;
            botCancelarFam.Visible = true;
            botCancelarFam.Location = new Point((componentesGenericos.puntoMedioHorizontal + 430), (componentesGenericos.puntoMedioVertical + 360));
            botModificarFam.Visible = false;
            botEliminarFam.Visible = false;
            botRegresa.Visible = false;

            listFamiliaMat.Location = new Point(850, 530);
            listFamiliaMat.BackColor = SystemColors.ActiveCaption;
            listFamiliaMat.AutoSize=true;
            listFamiliaMat.Visible = true;
            
            panFamiliasUnidades.Visible = false;

            picboxRecuaMat.Visible = true;
            picboxRecuaMat.Height = 220;
            picboxRecuaMat.Location = new Point(picboxRecuaMat.Location.X, (componentesGenericos.puntoMedioVertical - (picboxRecuaMat.Height / 2)));

            EliminarFamilia = false;
        }

        private void botIngresarTipo_Click(object sender, EventArgs e)
        {

        }

        /* private void botModificarFam_Enter(object sender, EventArgs e)
          {
              // Causa que el cursor se mueva al final y anula la selección
              texNuevoFam.SelectionStart = texNuevoFam.Text.Length;
              texNuevoFam.SelectionLength = 0;
          }*/
    }
}