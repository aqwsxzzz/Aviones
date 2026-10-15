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

        /// <summary>Reemplaza la imagen y ajusta la caja a su forma, sin pasar el tamaño máximo.</summary>
        public static void Mostrar(PictureBox caja, Image imagen, Size maximo)
        {
            Mostrar(caja, imagen);
            AjustarCaja(caja, maximo);
        }

        /// <summary>
        /// Achica la caja a la forma de la imagen (vertical, horizontal o cuadrada) dentro del máximo,
        /// así no quedan franjas en blanco a los costados. Sin imagen, la caja queda del tamaño máximo.
        /// </summary>
        public static void AjustarCaja(PictureBox caja, Size maximo)
        {
            var imagen = caja.Image;
            var borde = caja.Size - caja.ClientSize;
            int anchoMax = Math.Max(1, maximo.Width - borde.Width);
            int altoMax = Math.Max(1, maximo.Height - borde.Height);
            if (imagen == null || imagen.Width == 0 || imagen.Height == 0)
            {
                caja.Size = maximo;
                return;
            }
            double escala = Math.Min((double)anchoMax / imagen.Width, (double)altoMax / imagen.Height);
            caja.Size = new Size(
                Math.Max(1, (int)Math.Round(imagen.Width * escala)) + borde.Width,
                Math.Max(1, (int)Math.Round(imagen.Height * escala)) + borde.Height);
        }

        /// <summary>Sin imagen, la caja muestra una cámara tachada y "Sin imagen" sobre fondo oscuro.</summary>
        public static void MarcarSinImagen(PictureBox caja)
        {
            caja.Paint += DibujarSinImagen;
            caja.Resize += (s, e) => caja.Invalidate();
        }

        private static void DibujarSinImagen(object sender, PaintEventArgs e)
        {
            var caja = (PictureBox)sender;
            if (caja.Image != null)
            {
                return;
            }
            var g = e.Graphics;
            var r = caja.ClientRectangle;
            g.Clear(Color.FromArgb(34, 34, 34));
            g.SmoothingMode = SmoothingMode.AntiAlias;

            float u = Math.Min(r.Width / 5f, r.Height / 4.2f);
            if (u < 4)
            {
                return;
            }
            float cx = r.Width / 2f;
            float cy = r.Height / 2f - u * 0.5f;
            var cuerpo = new RectangleF(cx - 1.5f * u, cy - u, 3f * u, 2f * u);
            var gris = Color.FromArgb(150, 150, 150);

            using (var trazo = new Pen(gris, Math.Max(2f, u / 7f)) { LineJoin = LineJoin.Round })
            {
                // Cámara: cuerpo, visor y lente
                g.DrawRectangle(trazo, cuerpo.X, cuerpo.Y, cuerpo.Width, cuerpo.Height);
                g.DrawLines(trazo, new[]
                {
                    new PointF(cx - 0.55f * u, cuerpo.Y), new PointF(cx - 0.4f * u, cuerpo.Y - 0.4f * u),
                    new PointF(cx + 0.4f * u, cuerpo.Y - 0.4f * u), new PointF(cx + 0.55f * u, cuerpo.Y)
                });
                g.DrawEllipse(trazo, cx - 0.6f * u, cy - 0.6f * u, 1.2f * u, 1.2f * u);

                // Tachado, con un borde del color del fondo para que se distinga
                var desde = new PointF(cuerpo.Left - 0.2f * u, cuerpo.Top - 0.6f * u);
                var hasta = new PointF(cuerpo.Right + 0.2f * u, cuerpo.Bottom + 0.3f * u);
                using (var borde = new Pen(Color.FromArgb(34, 34, 34), trazo.Width * 3f))
                {
                    g.DrawLine(borde, desde, hasta);
                }
                g.DrawLine(trazo, desde, hasta);
            }

            using (var letra = new Font("Arial", Math.Max(8f, u * 0.38f), FontStyle.Bold, GraphicsUnit.Pixel))
            using (var pincel = new SolidBrush(gris))
            using (var centrado = new StringFormat { Alignment = StringAlignment.Center })
            {
                g.DrawString("SIN IMAGEN", letra, pincel, new RectangleF(0, cuerpo.Bottom + 0.55f * u, r.Width, u), centrado);
            }
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
