using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aviones_2._0.Common
{
    public class Componentes
    {
        public DataGridView dgvMateriales { get; set; }

        public DataGridView dgvModelos { get; set; }

        public int alturaPantallaUsuario { get; set; }
        public int anchoPantallaUsuario { get; set; }

        public int puntoMedioVertical { get; set; }
        public int puntoMedioHorizontal { get; set; }

        public Panel panMensajeConfirmacion { get; set; }
        public Button botMensajeAviso1 { get; set; }
        public Button botMensajeAviso2 { get; set; }
        public static DataGridView CrearDgvMateriales() {
            //Declaracion DGV generico Materiales.
            DataGridView dgvMateriales = new DataGridView
            {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            //Aplicacion de configuracion de columnas de DGV Materiales.
            ConfigurardgvMateriales(dgvMateriales);

            return dgvMateriales;
        }

        public static DataGridView CrearDgvModelos()
        {
            //Declaracion DGV generico Modelos.
            DataGridView dgvModelos = new DataGridView
            {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            //Aplicacion de configuracion de columnas de DGV Modelos.
            ConfigurardgvModelos(dgvModelos);

            return dgvModelos;
        }

        public Componentes()
        {


            

            //Medidas genericas por Resolucion de Usuario.
            alturaPantallaUsuario = Screen.PrimaryScreen.Bounds.Height;
            anchoPantallaUsuario = Screen.PrimaryScreen.Bounds.Width;
            puntoMedioHorizontal = Screen.PrimaryScreen.Bounds.Width / 2;
            puntoMedioVertical = Screen.PrimaryScreen.Bounds.Height / 2;

            //Declaracion de Panel generico para uso de Mensajes de Avisos.
            panMensajeConfirmacion = new Panel
            {
                Size = new Size(378, 210),
                BackColor = Color.Firebrick,
                BorderStyle = BorderStyle.Fixed3D,
            };

            //Declaracion Label1 de Panel de Mensaje de Avisos.
            Label labTitulo1 = new Label
            {
                ForeColor = SystemColors.ButtonFace,
                BackColor = Color.Firebrick,
                Font = new Font("Microsoft Sans Serif", 14.25f),
            };

            //Declaracion Label2 de Panel de Mensaje de Avisos.
            Label labTitulo2 = new Label
            {
                ForeColor = SystemColors.ButtonFace,
                BackColor = Color.Firebrick,
                Font = new Font("Microsoft Sans Serif", 14.25f),
            };

            //Declaracion de Button1 de Panel de Mensaje de Avisos.
            Button botMensajeAviso1 = new Button
            {
                Size = new Size(124, 42),
                BackColor = Color.Firebrick,
                Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold),
                Text = "Eliminar",
            };

            //Declaracion de Button2 de Panel de Mensaje de Avisos.
            Button botMensajeAviso2 = new Button
            {
                Size = new Size(124, 42),
                BackColor = Color.Firebrick,
                Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold),
                Text = "Cancelar",
            };

            //Agregar Labels y Botones a Panel de Mensaje de Avisos.
            panMensajeConfirmacion.Controls.Add(labTitulo1);
            panMensajeConfirmacion.Controls.Add(labTitulo2);
            panMensajeConfirmacion.Controls.Add(botMensajeAviso1);
            panMensajeConfirmacion.Controls.Add(botMensajeAviso2);
        }

        public static void ConfigurardgvMateriales(DataGridView dgvMateriales)
        {
            dgvMateriales.AutoGenerateColumns = false;
            // ColumnCount debe reflejar el número real de columnas definidas (0..16 => 17 columnas)
            dgvMateriales.ColumnCount = 17;

            dgvMateriales.Columns[0].Name = "Familia";
            dgvMateriales.Columns[0].HeaderText = "      Familia";
            dgvMateriales.Columns[0].DataPropertyName = "Familia";
            dgvMateriales.Columns[0].Width = 15;
            dgvMateriales.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvMateriales.Columns[1].Name = "Grupo";
            dgvMateriales.Columns[1].HeaderText = "       Grupo";
            dgvMateriales.Columns[1].DataPropertyName = "Grupo";
            dgvMateriales.Columns[1].Width = 19;
            dgvMateriales.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvMateriales.Columns[2].Name = "Caracteristica";
            dgvMateriales.Columns[2].HeaderText = "       Característica";
            dgvMateriales.Columns[2].DataPropertyName = "Caracteristica";
            dgvMateriales.Columns[2].Width = 21;
            dgvMateriales.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvMateriales.Columns[3].Name = "Color";
            dgvMateriales.Columns[3].HeaderText = "      Color";
            dgvMateriales.Columns[3].DataPropertyName = "Color";
            dgvMateriales.Columns[3].Width = 14;
            dgvMateriales.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvMateriales.Columns[4].Name = "Medidas";
            dgvMateriales.Columns[4].HeaderText = "   Medidas";
            dgvMateriales.Columns[4].DataPropertyName = "Medidas";
            dgvMateriales.Columns[4].Width = 10;
            dgvMateriales.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvMateriales.Columns[5].Name = "Codigo";
            dgvMateriales.Columns[5].HeaderText = "    Codigo";
            dgvMateriales.Columns[5].DataPropertyName = "Codigo";
            dgvMateriales.Columns[5].Width = 14;
            dgvMateriales.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvMateriales.Columns[6].Name = "Fabricante";
            dgvMateriales.Columns[6].HeaderText = "    Fabricante";
            dgvMateriales.Columns[6].DataPropertyName = "Fabricante";
            dgvMateriales.Columns[6].Width = 12;
            dgvMateriales.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvMateriales.Columns[7].Name = "Estado";
            dgvMateriales.Columns[7].HeaderText = "Estado";
            dgvMateriales.Columns[7].DataPropertyName = "Estado";
            dgvMateriales.Columns[7].Width = 4;
            dgvMateriales.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // No mostrar Estado en el grid principal de materiales (se usa en otras vistas)
            dgvMateriales.Columns[7].Visible = false;

            dgvMateriales.Columns[8].Name = "Ubicacion";
            dgvMateriales.Columns[8].HeaderText = "  Ubicación";
            dgvMateriales.Columns[8].DataPropertyName = "Ubicacion";
            dgvMateriales.Columns[8].Width = 11;
            dgvMateriales.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // No mostrar Ubicación en el grid principal de materiales
            dgvMateriales.Columns[8].Visible = false;

            dgvMateriales.Columns[9].Name = "Cantidad";
            dgvMateriales.Columns[9].HeaderText = " Cant";
            dgvMateriales.Columns[9].DataPropertyName = "Cantidad";
            dgvMateriales.Columns[9].Width = 5;
            dgvMateriales.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvMateriales.Columns[10].Name = "Disponible";
            dgvMateriales.Columns[10].HeaderText = "Disponi";
            dgvMateriales.Columns[10].DataPropertyName = "Disponible";
            dgvMateriales.Columns[10].Width = 4;
            dgvMateriales.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvMateriales.Columns[11].Name = "Tipo";
            dgvMateriales.Columns[11].HeaderText = "   Tipo";
            dgvMateriales.Columns[11].DataPropertyName = "Tipo";
            dgvMateriales.Columns[11].Width = 7;
            dgvMateriales.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dgvMateriales.Columns[12].Name = "Valor";
            dgvMateriales.Columns[12].HeaderText = "Valor Unitario";
            dgvMateriales.Columns[12].DataPropertyName = "Valor";
            dgvMateriales.Columns[12].Width = 7;
            dgvMateriales.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            // Mostrar el valor unitario con un decimal (sin separador de miles)
            dgvMateriales.Columns[12].DefaultCellStyle.Format = "0.0";

            dgvMateriales.Columns[13].Name = "Total";
            dgvMateriales.Columns[13].HeaderText = " Total";
            dgvMateriales.Columns[13].DataPropertyName = "Total";
            dgvMateriales.Columns[13].Width = 5;
            dgvMateriales.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dgvMateriales.Columns[14].Name = "IdMaterial";
            dgvMateriales.Columns[14].HeaderText = "IdMaterial";
            dgvMateriales.Columns[14].DataPropertyName = "IdMaterial";
            dgvMateriales.Columns[14].Visible = false;

            dgvMateriales.Columns[15].Name = "IdAcopio";
            dgvMateriales.Columns[15].HeaderText = "IdAcopio";
            dgvMateriales.Columns[15].DataPropertyName = "IdAcopio";
            dgvMateriales.Columns[15].Visible = false;

            // Columna para mostrar cantidad a comprar (mapeada desde Materiales.Cantacomprar o alias en consultas)
            dgvMateriales.Columns[16].Name = "CantidadAComprar";
            dgvMateriales.Columns[16].HeaderText = "A Comprar";
            dgvMateriales.Columns[16].DataPropertyName = "CantidadAComprar";
            dgvMateriales.Columns[16].Width = 7;
            dgvMateriales.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            // Por defecto ocultar la columna de pedido; se mostrará en vistas de Compras cuando corresponda
            dgvMateriales.Columns[16].Visible = false;
            dgvMateriales.Columns[14].Visible = false;

            dgvMateriales.RowHeadersVisible = false;
            dgvMateriales.AllowUserToAddRows = false;
            dgvMateriales.ColumnHeadersHeight = 40;
            dgvMateriales.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            dgvMateriales.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMateriales.DefaultCellStyle.Font = new Font("Arial", 11);
            dgvMateriales.BackgroundColor = Color.LightSteelBlue;
            dgvMateriales.BorderStyle = BorderStyle.None;
        }

        private static void ConfigurardgvModelos(DataGridView dgvModelos)
        {
            dgvModelos.AutoGenerateColumns = false;
            dgvModelos.ColumnCount = 5;

            dgvModelos.Columns[0].Name = "Modelo";
            dgvModelos.Columns[0].HeaderText = "Modelo";
            dgvModelos.Columns[0].DataPropertyName = "Modelo";
            dgvModelos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvModelos.Columns[1].Name = "Alias";
            dgvModelos.Columns[1].HeaderText = "Alias";
            dgvModelos.Columns[1].DataPropertyName = "Alias";
            dgvModelos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvModelos.Columns[2].Name = "Estado";
            dgvModelos.Columns[2].HeaderText = "Estado";
            dgvModelos.Columns[2].DataPropertyName = "Estado";
            dgvModelos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvModelos.Columns[3].Name = "Desde";
            dgvModelos.Columns[3].HeaderText = "Desde";
            dgvModelos.Columns[3].DataPropertyName = "Desde";
            dgvModelos.Columns[3].Width = 20;
            dgvModelos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvModelos.Columns[4].Name = "IdModelo";
            dgvModelos.Columns[4].HeaderText = "IdModelo";
            dgvModelos.Columns[4].DataPropertyName = "IdModelo";
            dgvModelos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvModelos.RowHeadersVisible = false;
            dgvModelos.AllowUserToAddRows = false;
            dgvModelos.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            dgvModelos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvModelos.DefaultCellStyle.Font = new Font("Arial", 11);
        }
    }
}