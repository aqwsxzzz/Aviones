using Aviones_2._0.Data;

namespace Aviones_2._0.Forms.Modelos
{
    /// <summary>
    /// Un material del modelo mientras se edita. Los cambios se guardan todos juntos al confirmar.
    /// </summary>
    public class FilaMaterial
    {
        /// <summary>null si se agregó en esta edición y todavía no está guardado.</summary>
        public int? IdMaterialModelo { get; set; }
        public int IdLote { get; set; }

        /// <summary>De qué stock del lote sale: nuevo o usado.</summary>
        public string Estado { get; set; } = EstadosMaterial.Nuevo;

        public string Condicion => EstadosMaterial.Nombre(Estado);
        public int IdMaterial { get; set; }
        public string Familia { get; set; }
        public string Material { get; set; }
        public string Fabricante { get; set; }
        public string Unidad { get; set; }
        public decimal Cantidad { get; set; }

        /// <summary>Cantidad guardada en la base (0 para los nuevos).</summary>
        public decimal CantidadOriginal { get; set; }

        /// <summary>Si la cantidad quedó por debajo de la guardada, cuánto de la diferencia vuelve nuevo y usado.</summary>
        public Devolucion DevolucionBaja { get; set; }

        public decimal ValorUnitario { get; set; }
        public decimal Subtotal => Cantidad * ValorUnitario;
        public bool EsNueva => !IdMaterialModelo.HasValue;
    }
}
