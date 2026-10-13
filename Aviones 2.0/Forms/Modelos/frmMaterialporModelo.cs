using Aviones_2._0.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aviones_2._0.Forms.Modelos
{
    public partial class frmMaterialporModelo : Form
    {
        private Componentes componentesGenericos;
        private funcModelos funcModelos;
        private frmModelos frmModelos;
        private frmIngreso frmIngreso;
        private frmCondicion frmCondicion;
        private frmModificacionMod frmModificacionMod;

        public DataGridView dgvMateriales { get; set; }
        public DataGridView dgvMaterialesSeleccionadosLocal;

        // Nuevo Modelo
        private List<string> ListaMaterialesAEnviar = new List<string>();

        private List<string> CantidadDeMaterialAEnviar = new List<string>();
        private List<string> CantidadDisponibleActualizadaAEnviar = new List<string>();

        // Modificacion de cantidad de material ya usado en un modelo.
        private List<string> NuevaCantidadxModeloList = new List<string>();

        private List<string> texCantidadViejaList = new List<string>();
        private List<string> IdMaterialList = new List<string>();

        // Eliminacion de material reciclable en modelo.
        private List<string> IdMaterialReciclableAEliminar = new List<string>();

        private List<string> CantidadUsadaDeMaterial = new List<string>();
        private List<string> IdAcopioDeMaterialReciblableAEliminar = new List<string>();

        // Eliminacion de material NO reciclable en modelo.
        private List<string> IdMaterialNoReciclableAEliminar = new List<string>();

        //variables de configuracion
        string nombreModelo;
        string nombreAlias;
        string nuevoOModificacion;

        public frmMaterialporModelo(frmIngreso principal, string nombreModelo, string nombreAlias, string nuevoOModificacion, frmModificacionMod frmModificacionMod, frmModelos frmModelosParent)
        {
            InitializeComponent();
            this.nombreAlias = nombreAlias;
            this.nombreModelo = nombreModelo;
            configuracionInicial(nombreModelo, nombreAlias, principal, frmModelosParent);
            this.nuevoOModificacion = nuevoOModificacion;
            this.frmModificacionMod = frmModificacionMod;
        }

        private void MaterialesporModelo_Shown(object sender, EventArgs e)
        {
            //configuracionInicial(nombreModelo, nombreAlias, frmIngreso);

            if (dgvMaterialesSeleccionadosLocal != null)
            {
                // Para evitar que se entre en modo edición (y aparezca el azul por defecto)
                dgvMaterialesSeleccionadosLocal.ReadOnly = true;
                // o: dgvModelos.EditMode = DataGridViewEditMode.EditProgrammatically;

                // Selección de fila completa, sin multiselección
                dgvMaterialesSeleccionadosLocal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMaterialesSeleccionadosLocal.MultiSelect = false;

                // Colores de la fila seleccionada
                dgvMaterialesSeleccionadosLocal.DefaultCellStyle.SelectionBackColor = Color.Tan;   // Fondo de la fila seleccionada
                dgvMaterialesSeleccionadosLocal.DefaultCellStyle.SelectionForeColor = Color.Black; // Texto de la fila seleccionada

                // (Opcional) Color de fondo normal de todas las filas
                dgvMaterialesSeleccionadosLocal.DefaultCellStyle.BackColor = Color.White;

                // Desactiva el estilo visual de encabezados si quieres un color personalizado allí
                dgvMaterialesSeleccionadosLocal.EnableHeadersVisualStyles = false;
                dgvMaterialesSeleccionadosLocal.ColumnHeadersDefaultCellStyle.BackColor = Color.Peru;
                dgvMaterialesSeleccionadosLocal.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvMaterialesSeleccionadosLocal.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Peru;
                dgvMaterialesSeleccionadosLocal.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

                // Limpia cualquier selección previa
                dgvMaterialesSeleccionadosLocal.ClearSelection();
                dgvMaterialesSeleccionadosLocal.CurrentCell = null;

                // ¡Listo! Sin más eventos extra
                dgvMaterialesSeleccionadosLocal.Refresh();
            }

            if (frmIngreso != null)
            {
                frmIngreso.Hide();
            }
        }

        public frmMaterialporModelo(Image imagen)
        {
            InitializeComponent();
            //  picboxFotoModFinal.Image = imagen;  // Asigna la imagen recibida al picboxFotoModFinal
        }


        public void frmMaterialporModelo_Load(object sender, EventArgs e)
        {
            funcModelos modelo = new funcModelos();

            // Cargar familias en el ListBox
            modelo.mostrarFamilias(lisboxFamiliasSel);

            // Forzar que el ListBox sea hijo del panel contenedor para que el panel se dimensione y muestre correctamente
            if (lisboxFamiliasSel.Parent != panRecuaFamilia)
            {
                // Convertir coordenadas y mover el listbox dentro del panel
                this.Controls.Remove(lisboxFamiliasSel);
                panRecuaFamilia.Controls.Add(lisboxFamiliasSel);
            }

            // Ajustar la altura del ListBox (la función ya limita a 15 ítems y activa scrollbar si hace falta)
            modelo.AjustarAlturaListBox(lisboxFamiliasSel);

            // Ajustar panel que contiene el listado para que quede centrado respecto al título y se adapte a la altura
            panRecuaFamilia.Width = Math.Max(lisboxFamiliasSel.Width + 20, 220);
            modelo.AjustarTamañoPanel(panRecuaFamilia, lisboxFamiliasSel);

            // Posicionar el listbox dentro del panel con un pequeño margen
            lisboxFamiliasSel.Left = 8;
            lisboxFamiliasSel.Top = 40;

            dgvMateriales.Visible = false;

            lisboxFamiliasSel.Visible = true;

            panRecuaFamilia.Visible = true;
            panRecuaFamilia.Location = new Point(1, 130);

            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void texFamiliaSel_Click(object sender, EventArgs e)
        {
            texFamiliaSel.Text = "MATERIAL";
            texFamiliaSel.TextAlign = HorizontalAlignment.Center;
            texFamiliaSel.Font = new Font(texFamiliaSel.Font, FontStyle.Regular);

            texFamiliaSel.BackColor = Color.LightSalmon;
            dgvMateriales.Visible = false;

            lisboxFamiliasSel.Visible = true;

            panRecuaFamilia.Visible = true;
            panRecuaFamilia.Location = new Point(1, 210);
            panRecuaCantAsignada.Visible = false;

            funcModelos funcModelosInstance = new funcModelos();

            // Llamada a la función mostrarFamilias, pasando el listBox como argumento
            funcModelosInstance.mostrarFamilias(lisboxFamiliasSel);
        }

        private void texFamiliaSel_Enter(object sender, EventArgs e)
        {
            // Desactiva la selección del texto para evitar el resaltado azul
            texFamiliaSel.SelectionStart = texFamiliaSel.Text.Length;
            texFamiliaSel.SelectionLength = 0;
        }

        private void lisboxFamiliasSel_Click(object sender, EventArgs e)
        {
            if (lisboxFamiliasSel.SelectedItem != null)
            {
                texFamiliaSel.Text = lisboxFamiliasSel.SelectedItem.ToString();
                texFamiliaSel.Font = new Font(texFamiliaSel.Font, FontStyle.Underline);
                texFamiliaSel.TextAlign = HorizontalAlignment.Right;

                lisboxFamiliasSel.Visible = false;

                texFamiliaSel.Visible = true;
                texFamiliaSel.BackColor = Color.SeaShell;
                texFamiliaSel.Focus();

                panRecuaFamilia.Visible = false;

                dgvMateriales.Visible = true;
                dgvMateriales.Location = new Point(20, 350);
                dgvMateriales.Size = new Size(1200, 349);
                dgvMateriales.BackgroundColor = System.Drawing.Color.SeaShell;
                dgvMateriales.Columns[0].Visible = false;
                dgvMateriales.Columns[2].Width = 130;
                dgvMateriales.Columns[4].Width = 130;
                dgvMateriales.Columns[5].Width = 110;
                dgvMateriales.Columns[8].Visible = false;
                dgvMateriales.Columns[9].Visible = false;
                dgvMateriales.Columns[12].Visible = false;
                dgvMateriales.Columns[13].Visible = false;
                dgvMateriales.EnableHeadersVisualStyles = false; //habilita el cambio de color de los títulos
                dgvMateriales.ColumnHeadersDefaultCellStyle.BackColor = Color.Tan;
                dgvMateriales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMateriales.MultiSelect = false; // Si deseas que solo se pueda seleccionar una fila a la vez
                dgvMateriales.BringToFront();
            }
        }

        private void dgvMateriales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verifica que se haya hecho clic en una fila válida
            {
                // Obtener el IdMaterial de la fila seleccionada
                string idMaterial = dgvMateriales.Rows[e.RowIndex].Cells[14].Value.ToString();

                funcModelos.BuscarFotoPorIdMaterial(idMaterial, picFoto);

                texCantidadMaterialSel.Visible = true;
                panRecuaCantAsignada.Visible = true;
                labCantAsignada.Visible = true;
                texCantidadMaterialSel.Focus();
                picFoto.Visible = true;
                picFoto.BackColor = Color.Transparent;
                picFoto.SizeMode = PictureBoxSizeMode.Zoom; // O prueba StretchImage según necesidad
                picFoto.Parent = this; // Asegurar que respeta la transparencia del formulario
            }
        }

        private void botAceptarSel_Click(object sender, EventArgs e)
        {
            if(nuevoOModificacion == "Nuevo") { 
            frmModelos frmModelos2 = new frmModelos();
            frmIngreso.panCajaGrande.Size = new Size(1400, 650);
            frmIngreso.panCajaGrande.Location = new Point(150, 141);
            frmIngreso.panCajaTextos.Size = new Size(600, 70);
            frmIngreso.panCajaTextos.Location = new Point(400, 43);
            frmIngreso.panCajaEstadoFecha.Size = new Size(600, 70);
            frmIngreso.panCajaEstadoFecha.Location = new Point(550, 680);

            frmIngreso.picboxFotoMod.Visible = false;

            frmIngreso.labNombre.Visible = false;
            frmIngreso.labAlias.Visible = false;

            frmIngreso.texNombre.Location = new Point(53, 15);
            frmIngreso.texNombre.Font = new Font(frmIngreso.texNombre.Font.FontFamily, 24);
            frmIngreso.texAlias.Location = new Point(330, 15);
            frmIngreso.texAlias.Font = new Font(frmIngreso.texNombre.Font.FontFamily, 24);

            //  frmIngreso.texCondicion.Visible = true;
            frmIngreso.texDesde.Visible = true;

            frmIngreso.dgvMaterialesSeleccionadosFinal.Columns.Clear();

            foreach (DataGridViewColumn column in dgvMaterialesSeleccionadosLocal.Columns)
            {
                frmIngreso.dgvMaterialesSeleccionadosFinal.Columns.Add(column.Clone() as DataGridViewColumn);
            }
            frmIngreso.dgvMaterialesSeleccionadosFinal.Visible = true;
            frmIngreso.dgvMaterialesSeleccionadosFinal.BringToFront();
            frmIngreso.dgvMaterialesSeleccionadosFinal.Location = new Point(255, 290);
            frmIngreso.dgvMaterialesSeleccionadosFinal.Size = new Size(1200, 350);
            frmIngreso.dgvMaterialesSeleccionadosFinal.EnableHeadersVisualStyles = false; // Permite que el estilo personalizado se aplique
            frmIngreso.dgvMaterialesSeleccionadosFinal.ColumnHeadersDefaultCellStyle.BackColor = Color.Tan;
            frmIngreso.dgvMaterialesSeleccionadosFinal.BackgroundColor = System.Drawing.Color.FromArgb(255, 224, 180);
            frmIngreso.dgvMaterialesSeleccionadosFinal.ClearSelection();
            frmIngreso.dgvMaterialesSeleccionadosFinal.CurrentCell = null;
            frmIngreso.dgvMaterialesSeleccionadosFinal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            frmIngreso.dgvMaterialesSeleccionadosFinal.MultiSelect = false;
            frmIngreso.dgvMaterialesSeleccionadosFinal.FirstDisplayedCell = null;

            // Ajusta el color de selección para que coincida con el fondo
            frmIngreso.dgvMaterialesSeleccionadosFinal.DefaultCellStyle.SelectionBackColor = frmIngreso.dgvMaterialesSeleccionadosFinal.DefaultCellStyle.BackColor;
            frmIngreso.dgvMaterialesSeleccionadosFinal.DefaultCellStyle.SelectionForeColor = frmIngreso.dgvMaterialesSeleccionadosFinal.DefaultCellStyle.ForeColor;

            frmIngreso.dgvMaterialesSeleccionadosFinal.EnableHeadersVisualStyles = false;
            frmIngreso.dgvMaterialesSeleccionadosFinal.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 192, 128); // Mismo color de fondo
            frmIngreso.dgvMaterialesSeleccionadosFinal.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 192, 128);
            frmIngreso.dgvMaterialesSeleccionadosFinal.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; // Ajusta si es necesario

            // Asegura que no haya selección en celdas o columnas
            frmIngreso.dgvMaterialesSeleccionadosFinal.ClearSelection();
            frmIngreso.dgvMaterialesSeleccionadosFinal.CurrentCell = null;

            // Copia los datos de dataGridView1 a dataGridView2
            foreach (DataGridViewRow row in dgvMaterialesSeleccionadosLocal.Rows)
            {
                if (!row.IsNewRow) // Evita copiar la fila vacía de edición
                {
                    //int rowIndex = componentesGenericos.dgvMateriales.Rows.Add();
                    int rowIndex = frmIngreso.dgvMaterialesSeleccionadosFinal.Rows.Add();
                    for (int columnIndex = 0; columnIndex < dgvMaterialesSeleccionadosLocal.Columns.Count; columnIndex++)
                    {
                        if (columnIndex < frmIngreso.dgvMaterialesSeleccionadosFinal.Columns.Count) // Evita error de índice
                        {
                            frmIngreso.dgvMaterialesSeleccionadosFinal.Rows[rowIndex].Cells[columnIndex].Value = row.Cells[columnIndex].Value;
                        }
                    }
                }
            }

            frmIngreso.ListaMateriales.Clear();
            frmIngreso.CantidadDeMaterial.Clear();
            frmIngreso.CantidadDisponibleActualizada.Clear();
            frmIngreso.IdMaterialNoReciclableAEliminarMod.Clear();
            frmIngreso.IdMaterialReciclableAEliminarMod.Clear();
            frmIngreso.CantidadUsadaDeMaterialMod.Clear();

            if (ListaMaterialesAEnviar.Count() > 0)
            {
                for (int i = 0; i < ListaMaterialesAEnviar.Count; i++)
                {
                    frmIngreso.ListaMateriales.Add(ListaMaterialesAEnviar[i]);
                    frmIngreso.CantidadDeMaterial.Add(CantidadDeMaterialAEnviar[i]);
                    frmIngreso.CantidadDisponibleActualizada.Add(CantidadDisponibleActualizadaAEnviar[i]);
                }
                if (IdMaterialReciclableAEliminar.Count() > 0)
                {
                    for (int i = 0; i < IdMaterialReciclableAEliminar.Count; i++)
                    {
                        frmIngreso.IdMaterialReciclableAEliminarMod.Add(IdMaterialReciclableAEliminar[i]);
                        frmIngreso.CantidadUsadaDeMaterialMod.Add(CantidadUsadaDeMaterial[i]);
                        frmIngreso.IdAcopioDeMaterialReciblableAEliminarMod.Add(IdAcopioDeMaterialReciblableAEliminar[i]);
                    }
                }
                frmIngreso.botGuardar.Visible = true;
            }
            if (IdMaterialNoReciclableAEliminar.Count > 0)
            {
                for (int i = 0; i < IdMaterialNoReciclableAEliminar.Count; i++)
                {
                    frmIngreso.IdMaterialNoReciclableAEliminarMod.Add(IdMaterialNoReciclableAEliminar[i]);
                }
                frmIngreso.botGuardar.Visible = true;
            }
            if (IdMaterialList.Count() > 0)
            {
                for (int i = 0; i < IdMaterialList.Count; i++)
                {
                    frmIngreso.NuevaCantidadxModelo.Add(NuevaCantidadxModeloList[i]);
                    frmIngreso.texCantidadVieja.Add(texCantidadViejaList[i]);
                    frmIngreso.IdMaterial.Add(IdMaterialList[i]);
                }
                frmIngreso.botGuardar.Visible = true;
            }
            if (nuevoOModificacion == "Nuevo")
            {
            }
            else if (nuevoOModificacion == "Modificacion")
            {
                dgvMaterialesSeleccionadosLocal.Visible = true;
            }

            frmIngreso.Show();
            frmCondicion.Location = new Point(580, 785);

            frmCondicion.radbotActivo.Visible = true;
            frmCondicion.radbotConstruccion.Visible = true;
            frmCondicion.radbotReparacion.Visible = true;
            frmCondicion.radbotInactivo.Visible = true;
            frmCondicion.texCondicion.Visible = false;

            frmIngreso.datDesde.Visible = false;
            frmCondicion.Show();
            } else if (nuevoOModificacion == "Modificacion")
            {
                this.frmModificacionMod.dgvMateriales.DataSource = null;
                this.frmModificacionMod.dgvMateriales.Columns.Clear();
                foreach (DataGridViewColumn column in dgvMaterialesSeleccionadosLocal.Columns)
                {
                    this.frmModificacionMod.dgvMateriales.Columns.Add((DataGridViewColumn)column.Clone());
                }

                // Ahora copiamos las filas
                foreach (DataGridViewRow row in dgvMaterialesSeleccionadosLocal.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        int rowIndex = this.frmModificacionMod.dgvMateriales.Rows.Add();
                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            this.frmModificacionMod.dgvMateriales.Rows[rowIndex].Cells[i].Value = row.Cells[i].Value;
                        }
                    }
                }
                if (ListaMaterialesAEnviar.Count() > 0)
                {
                    for (int i = 0; i < ListaMaterialesAEnviar.Count; i++)
                    {
                        frmModificacionMod.ListaMateriales.Add(ListaMaterialesAEnviar[i]);
                        frmModificacionMod.CantidadDeMaterial.Add(CantidadDeMaterialAEnviar[i]);
                        frmModificacionMod.CantidadDisponibleActualizada.Add(CantidadDisponibleActualizadaAEnviar[i]);
                    }
                    if (IdMaterialReciclableAEliminar.Count() > 0)
                    {
                        for (int i = 0; i < IdMaterialReciclableAEliminar.Count; i++)
                        {
                            frmModificacionMod.IdMaterialReciclableAEliminarMod.Add(IdMaterialReciclableAEliminar[i]);
                            frmModificacionMod.CantidadUsadaDeMaterialMod.Add(CantidadUsadaDeMaterial[i]);
                            frmModificacionMod.IdAcopioDeMaterialReciblableAEliminarMod.Add(IdAcopioDeMaterialReciblableAEliminar[i]);
                        }
                    }
                }
                if (IdMaterialNoReciclableAEliminar.Count > 0)
                {
                    for (int i = 0; i < IdMaterialNoReciclableAEliminar.Count; i++)
                    {
                        frmModificacionMod.IdMaterialNoReciclableAEliminarMod.Add(IdMaterialNoReciclableAEliminar[i]);
                    }
                }
                if (IdMaterialList.Count() > 0)
                {
                    for (int i = 0; i < IdMaterialList.Count; i++)
                    {
                        frmModificacionMod.NuevaCantidadxModelo.Add(NuevaCantidadxModeloList[i]);
                        frmModificacionMod.texCantidadVieja.Add(texCantidadViejaList[i]);
                        frmModificacionMod.IdMaterial.Add(IdMaterialList[i]);
                    }
                }
                frmModificacionMod.botConfirmar.Visible = true;
            frmModificacionMod.CalcularTotal();
            }
            this.Hide();
        }

        private void botBorrarMaterialSel_Click(object sender, EventArgs e)
        {
            if (frmModelos.NuevoOModificacion == "Nuevo" && dgvMaterialesSeleccionadosLocal.CurrentCell != null)
            {
                botBorrarMaterialSel.ForeColor = Color.Black;
                for (int i = 0; i < ListaMaterialesAEnviar.Count(); i++)
                {
                    if (ListaMaterialesAEnviar[i] == dgvMaterialesSeleccionadosLocal.CurrentRow.Cells[8].Value.ToString())
                    {
                        ListaMaterialesAEnviar.RemoveAt(i);
                        CantidadDeMaterialAEnviar.RemoveAt(i);
                        CantidadDisponibleActualizadaAEnviar.RemoveAt(i);
                    }
                }
                dgvMaterialesSeleccionadosLocal.Rows.Remove(dgvMaterialesSeleccionadosLocal.CurrentRow);
                botBorrarMaterialSel.Enabled = false;
            }
            else if (nuevoOModificacion == "Modificacion" && dgvMaterialesSeleccionadosLocal.CurrentCell != null || nuevoOModificacion == "Eliminacion" && dgvMaterialesSeleccionadosLocal.CurrentCell != null)
            {
                bool existe = false;
                for (int i = 0; i < ListaMaterialesAEnviar.Count(); i++)
                {
                    if (ListaMaterialesAEnviar[i] == dgvMaterialesSeleccionadosLocal.CurrentRow.Cells[15].Value.ToString())
                    {
                        ListaMaterialesAEnviar.RemoveAt(i);
                        CantidadDeMaterialAEnviar.RemoveAt(i);
                        CantidadDisponibleActualizadaAEnviar.RemoveAt(i);
                        existe = true;
                    }
                }
                if (!existe)
                {
                    // Mostrar un cuadro de diálogo con botones de Sí y No
                    DialogResult resultado = MessageBox.Show("¿Este material se recicla?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    botAceptarSel.Visible = true;

                    // Verificar la decisión del usuario
                    if (resultado == DialogResult.Yes)
                    {
                        IdMaterialReciclableAEliminar.Add(dgvMaterialesSeleccionadosLocal.CurrentRow.Cells[11].Value.ToString());
                        CantidadUsadaDeMaterial.Add(dgvMaterialesSeleccionadosLocal.CurrentRow.Cells[6].Value.ToString());
                        IdAcopioDeMaterialReciblableAEliminar.Add(dgvMaterialesSeleccionadosLocal.CurrentRow.Cells[15].Value.ToString());
                    }
                    else
                    {
                        IdMaterialNoReciclableAEliminar.Add(dgvMaterialesSeleccionadosLocal.CurrentRow.Cells[11].Value.ToString());
                    }
                }
                dgvMaterialesSeleccionadosLocal.Rows.Remove(dgvMaterialesSeleccionadosLocal.CurrentRow);
                dgvMaterialesSeleccionadosLocal.ClearSelection();
                dgvMaterialesSeleccionadosLocal.CurrentCell = null;
                dgvMaterialesSeleccionadosLocal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMaterialesSeleccionadosLocal.MultiSelect = false;
            }
        }

        private void botSalirSel_Click(object sender, EventArgs e)
        {
            // Buscar si ya existe una instancia abierta de frmModelos
            Form frmExistente = Application.OpenForms.OfType<frmModelos>().FirstOrDefault();

            if (frmExistente != null)
            {
                frmExistente.BringToFront();
            }

            this.Hide();
        }

        private void lisboxFamiliasSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lisboxFamiliasSel.SelectedItem != null && lisboxFamiliasSel.SelectedIndex >= 0)
            {
                string familiaSeleccionada = lisboxFamiliasSel.SelectedItem.ToString();
                funcModelos.cargarMaterialesPorFamilia(familiaSeleccionada, dgvMateriales);
                dgvMateriales.Visible = true;
            }
        }

        private void texCantidadMaterialSel_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo procesar cuando se presiona Enter
            if (e.KeyChar != '\r')
                return;

            string texto = texCantidadMaterialSel.Text;
            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show("La cantidad a seleccionar no puede estar vacía");
                return;
            }

            // Normalizar separador decimal y parsear de forma segura (es-ES)
            texto = texto.Replace(".", ",");
            var cultura = System.Globalization.CultureInfo.GetCultureInfo("es-ES");
            if (!decimal.TryParse(texto, System.Globalization.NumberStyles.Number, cultura, out decimal cantSeleccionada))
            {
                MessageBox.Show("Cantidad inválida");
                return;
            }

            if (cantSeleccionada <= 0)
            {
                MessageBox.Show("La cantidad a seleccionar no puede ser de 0 o menor");
                return;
            }

            if (dgvMateriales == null || dgvMateriales.RowCount == 0 || dgvMateriales.CurrentRow == null)
            {
                MessageBox.Show("La familia seleccionada no tiene materiales ingresados");
                return;
            }

            DataGridViewRow currentRow = dgvMateriales.CurrentRow;

            // Obtener disponible de forma segura
            decimal disponible = 0;
            if (currentRow.Cells.Count <= 10 || currentRow.Cells[10].Value == null ||
                !decimal.TryParse(Convert.ToString(currentRow.Cells[10].Value).Replace(".", ","), System.Globalization.NumberStyles.Number, cultura, out disponible))
            {
                MessageBox.Show("No se pudo obtener la cantidad disponible del material seleccionado");
                return;
            }

            if (cantSeleccionada > disponible)
            {
                MessageBox.Show("La cantidad supera lo disponible");
                return;
            }

            // Obtener identificador del material (celda 15) de forma segura
            string idMaterial = string.Empty;
            if (currentRow.Cells.Count > 15)
                idMaterial = Convert.ToString(currentRow.Cells[15].Value);

            ListaMaterialesAEnviar.Add(idMaterial);
            CantidadDeMaterialAEnviar.Add(texto);
            CantidadDisponibleActualizadaAEnviar.Add((disponible - cantSeleccionada).ToString(cultura));

            // Asegurar dgvMaterialesSeleccionadosLocal existe
            if (dgvMaterialesSeleccionadosLocal == null)
            {
                dgvMaterialesSeleccionadosLocal = Componentes.CrearDgvMateriales();
                this.Controls.Add(dgvMaterialesSeleccionadosLocal);
            }

            // Agregar fila clonada y copiar valores de forma segura
            dgvMaterialesSeleccionadosLocal.Rows.Add((DataGridViewRow)currentRow.Clone());
            int lastIndex = dgvMaterialesSeleccionadosLocal.Rows.Count - 1;
            for (int i = 0; i < currentRow.Cells.Count && i < dgvMaterialesSeleccionadosLocal.Rows[lastIndex].Cells.Count; i++)
            {
                dgvMaterialesSeleccionadosLocal.Rows[lastIndex].Cells[i].Value = currentRow.Cells[i].Value;
            }

            if (dgvMaterialesSeleccionadosLocal.Columns.Contains("CantidadSeleccionada"))
                dgvMaterialesSeleccionadosLocal.Rows[lastIndex].Cells["CantidadSeleccionada"].Value = texto;
            else if (dgvMaterialesSeleccionadosLocal.Columns.Count > 10)
                dgvMaterialesSeleccionadosLocal.Rows[lastIndex].Cells[10].Value = texto;

            // Actualizar interfaz (mantener comportamiento previo)
            labCantAsignada.Visible = false;
            texCantidadMaterialSel.Visible = false;
            texCantidadMaterialSel.Text = string.Empty;
            picFoto.Visible = false;

            dgvMaterialesSeleccionadosLocal.Visible = true;

            dgvMaterialesSeleccionadosLocal.Location = new Point(210, 510);
            dgvMaterialesSeleccionadosLocal.EnableHeadersVisualStyles = false;
            dgvMaterialesSeleccionadosLocal.ColumnHeadersDefaultCellStyle.BackColor = Color.Tan;
            dgvMaterialesSeleccionadosLocal.BackgroundColor = System.Drawing.Color.SeaShell;
            dgvMaterialesSeleccionadosLocal.Size = new Size(1200, 304);
            dgvMaterialesSeleccionadosLocal.Refresh();

            panRecuaCantAsignada.Visible = false;

            botAceptarSel.Focus();
            botBorrarMaterialSel.Visible = true;

            if (dgvMaterialesSeleccionadosLocal.RowCount == 1)
            {
                botAceptarSel.Visible = true;
                botAceptarSel.Location = new Point(1002, 835);
                botSalirSel.Location = new Point(1220, 835);
            }

            decimal nuevoDisponible = disponible - cantSeleccionada;
            currentRow.Cells[10].Value = nuevoDisponible;
            dgvMateriales.ClearSelection();

            if (dgvMaterialesSeleccionadosLocal.Rows.Count > 0)
                dgvMaterialesSeleccionadosLocal.Rows[0].Selected = false;

            dgvMaterialesSeleccionadosLocal.Visible = true;
            dgvMaterialesSeleccionadosLocal.ClearSelection();
            dgvMaterialesSeleccionadosLocal.CurrentCell = null;
            botAceptarSel.Visible = true;
        }

        private void MostrarImagenConTransparencia()
        {
            if (dgvMateriales.SelectedRows.Count > 0)
            {
                // Extraer la imagen desde la celda del DataGridView
                byte[] imageData = (byte[])dgvMateriales.SelectedRows[0].Cells["colImagen"].Value;

                if (imageData != null)
                {
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        Bitmap imagenOriginal = new Bitmap(ms);

                        // IMPORTANTE: Aplicar formato de pixel para asegurar transparencia
                        Bitmap imagenConTransparencia = new Bitmap(imagenOriginal.Width, imagenOriginal.Height, PixelFormat.Format32bppArgb);

                        using (Graphics g = Graphics.FromImage(imagenConTransparencia))
                        {
                            g.Clear(Color.Transparent); // Fija el fondo transparente
                            g.DrawImage(imagenOriginal, 0, 0, imagenOriginal.Width, imagenOriginal.Height);
                        }

                        // Establecer la imagen en el PictureBox
                        picFoto.BackColor = Color.Transparent;
                        picFoto.Image = imagenConTransparencia;
                    }
                }
            }
        }

        private void configuracionInicial(string nombreModelo, string nombreAlias, frmIngreso principal, frmModelos frmModelosParent)
        {
            labModelo.Text = nombreModelo;
            labAlias.Text = nombreAlias;
            picboxFotoModFinal.Visible = false;

            // Instanciar componentesGenericos si no se ha hecho ya
            componentesGenericos = new Componentes();
            funcModelos = new funcModelos();
            frmModelos = frmModelosParent;
            frmIngreso = principal;

            frmCondicion = new frmCondicion(frmModelos, frmIngreso, null, nuevoOModificacion);

            //DgvMateriales
            dgvMateriales = Componentes.CrearDgvMateriales();
            this.Controls.Add(dgvMateriales);
            dgvMateriales.CellClick += new DataGridViewCellEventHandler(dgvMateriales_CellClick);
            dgvMateriales.ReadOnly = true;
            dgvMateriales.Visible = false;


            if (dgvMaterialesSeleccionadosLocal == null)
            {
            dgvMaterialesSeleccionadosLocal = Componentes.CrearDgvMateriales();
            this.Controls.Add(dgvMaterialesSeleccionadosLocal);
            dgvMaterialesSeleccionadosLocal.Columns["Disponible"].HeaderText = "Seleccinada";
            dgvMaterialesSeleccionadosLocal.Columns["Disponible"].Name = "CantidadSeleccionada";

            dgvMaterialesSeleccionadosLocal.Visible = false;
            dgvMaterialesSeleccionadosLocal.Columns[0].Width = 21;
            dgvMaterialesSeleccionadosLocal.Columns[1].Width = 21;
            dgvMaterialesSeleccionadosLocal.Columns[2].Width = 21;
            dgvMaterialesSeleccionadosLocal.Columns[3].Width = 21;
            dgvMaterialesSeleccionadosLocal.Columns[4].Width = 21;
            dgvMaterialesSeleccionadosLocal.Columns[5].Width = 21;
            dgvMaterialesSeleccionadosLocal.Columns[6].Width = 21;
            dgvMaterialesSeleccionadosLocal.Columns[7].Visible = false;
            dgvMaterialesSeleccionadosLocal.Columns[8].Visible = false;
            dgvMaterialesSeleccionadosLocal.Columns[8].Width = 10;
            dgvMaterialesSeleccionadosLocal.Columns[9].Visible = false;
            dgvMaterialesSeleccionadosLocal.Columns[12].Visible = false;
            dgvMaterialesSeleccionadosLocal.Columns[13].Visible = false;

            dgvMaterialesSeleccionadosLocal.Location = new Point(210, 510);
            dgvMaterialesSeleccionadosLocal.EnableHeadersVisualStyles = false;
            dgvMaterialesSeleccionadosLocal.ColumnHeadersDefaultCellStyle.BackColor = Color.Tan;
            dgvMaterialesSeleccionadosLocal.BackgroundColor = System.Drawing.Color.SeaShell;
            dgvMaterialesSeleccionadosLocal.Size = new Size(1200, 304);
            }

            this.Shown += MaterialesporModelo_Shown;

        }
    }
}