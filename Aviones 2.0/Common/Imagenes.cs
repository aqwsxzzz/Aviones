using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Aviones_2._0.Common
{
    public static class Imagenes
    {
        private const int LadoMaximo = 1200;

        /// <summary>Pide un archivo de imagen. Devuelve null si se cancela.</summary>
        public static byte[] Elegir(IWin32Window dueño)
        {
            using (var dialogo = new OpenFileDialog
            {
                Title = "Elegir foto",
                Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Todos los archivos|*.*"
            })
            {
                if (dialogo.ShowDialog(dueño) != DialogResult.OK)
                {
                    return null;
                }
                try
                {
                    return LeerArchivo(dialogo.FileName);
                }
                catch (Exception ex) when (ex is OutOfMemoryException || ex is ArgumentException || ex is IOException)
                {
                    MessageBox.Show(dueño, "No se pudo abrir la imagen:\n" + ex.Message, "Foto",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
            }
        }

        /// <summary>
        /// Lee la imagen y la achica si es muy grande, para no llenar la base.
        /// Si tiene transparencia se guarda como PNG, si no como JPEG.
        /// </summary>
        public static byte[] LeerArchivo(string ruta)
        {
            using (var original = Image.FromFile(ruta))
            {
                double escala = Math.Min(1.0, (double)LadoMaximo / Math.Max(original.Width, original.Height));
                int ancho = Math.Max(1, (int)(original.Width * escala));
                int alto = Math.Max(1, (int)(original.Height * escala));
                bool transparente = Image.IsAlphaPixelFormat(original.PixelFormat);

                using (var copia = new Bitmap(ancho, alto, transparente ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb))
                using (var g = Graphics.FromImage(copia))
                using (var ms = new MemoryStream())
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.Clear(transparente ? Color.Transparent : Color.White);
                    g.DrawImage(original, 0, 0, ancho, alto);
                    copia.Save(ms, transparente ? ImageFormat.Png : ImageFormat.Jpeg);
                    return ms.ToArray();
                }
            }
        }

        /// <summary>Convierte los bytes guardados en una imagen independiente del stream.</summary>
        public static Image DesdeBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return null;
            }
            try
            {
                using (var ms = new MemoryStream(bytes))
                using (var imagen = Image.FromStream(ms))
                {
                    return new Bitmap(imagen);
                }
            }
            catch (ArgumentException)
            {
                return null; // datos dañados: se muestra sin foto
            }
        }

        /// <summary>Reemplaza la imagen del PictureBox liberando la anterior.</summary>
        public static void Mostrar(PictureBox caja, Image imagen)
        {
            var anterior = caja.Image;
            caja.Image = imagen;
            anterior?.Dispose();
        }

        /// <summary>Muestra la imagen a pantalla completa; se cierra con un clic o Esc.</summary>
        public static void Ampliar(IWin32Window dueño, Image imagen, string titulo)
        {
            if (imagen == null)
            {
                return;
            }
            using (var form = new Form
            {
                Text = titulo,
                WindowState = FormWindowState.Maximized,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.Black,
                KeyPreview = true,
                ShowInTaskbar = false
            })
            using (var caja = new PictureBox { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom, Image = imagen })
            {
                form.Controls.Add(caja);
                caja.Click += (s, e) => form.Close();
                form.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) form.Close(); };
                form.ShowDialog(dueño);
                caja.Image = null; // la imagen es de quien llama
            }
        }
    }
}
