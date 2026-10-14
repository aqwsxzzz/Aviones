using System;

namespace Aviones_2._0.Data
{
    public class Familia
    {
        public int IdFamilia { get; set; }
        public string Nombre { get; set; }
        public override string ToString() => Nombre;
    }

    public class TipoUnidad
    {
        public int IdTipoUnidad { get; set; }
        public string Nombre { get; set; }
        public override string ToString() => Nombre;
    }

    public class EstadoModelo
    {
        public int IdEstadoModelo { get; set; }
        public string Nombre { get; set; }
        public override string ToString() => Nombre;
    }

    public static class EstadosMaterial
    {
        public const string Nuevo = "N";
        public const string Usado = "U";
    }

    /// <summary>Qué es un material (catálogo). El stock está en sus lotes.</summary>
    public class Material
    {
        public int IdMaterial { get; set; }
        public int IdFamilia { get; set; }
        public string Grupo { get; set; }
        public string Caracteristica { get; set; }
        public string Color { get; set; }
        public string Medidas { get; set; }
        public string Codigo { get; set; }
        public string Estado { get; set; } = EstadosMaterial.Nuevo;
        public int IdTipoUnidad { get; set; }
        public decimal? CantidadAComprar { get; set; }
    }

    /// <summary>Un renglón de la vista vMaterialesStock: el material con el stock de todos sus lotes.</summary>
    public class MaterialStock
    {
        public int IdMaterial { get; set; }
        public int IdFamilia { get; set; }
        public string Familia { get; set; }
        public string Grupo { get; set; }
        public string Caracteristica { get; set; }
        public string Color { get; set; }
        public string Medidas { get; set; }
        public string Codigo { get; set; }
        public string Estado { get; set; }
        public int IdTipoUnidad { get; set; }
        public string TipoUnidad { get; set; }
        public decimal? CantidadAComprar { get; set; }
        public int CantidadLotes { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Disponible { get; set; }
        public decimal ValorDisponible { get; set; }
    }

    public class FiltroMateriales
    {
        public int? IdFamilia { get; set; }

        /// <summary>Busca en grupo, característica, color, medidas, código, fabricante y ubicación.</summary>
        public string Texto { get; set; }

        public bool SoloSinStock { get; set; }
        public bool SoloAComprar { get; set; }
    }

    /// <summary>Un ingreso de stock de un material (antes "Acopio").</summary>
    public class Lote
    {
        public int IdLote { get; set; }
        public int IdMaterial { get; set; }
        public string Fabricante { get; set; }
        public string Ubicacion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Disponible { get; set; }
        public decimal ValorUnitario { get; set; }
        public DateTime FechaIngreso { get; set; } = DateTime.Today;
    }

    /// <summary>En qué modelo se usa un material y cuánto.</summary>
    public class UsoDeMaterial
    {
        public int IdModelo { get; set; }
        public string Modelo { get; set; }
        public string Alias { get; set; }
        public int IdLote { get; set; }
        public decimal Cantidad { get; set; }
    }

    public class Modelo
    {
        public int IdModelo { get; set; }
        public string Nombre { get; set; }
        public string Alias { get; set; }
        public int IdEstadoModelo { get; set; }

        /// <summary>Nombre del estado; sólo se llena al leer.</summary>
        public string Estado { get; set; }

        public DateTime? Desde { get; set; }
    }

    /// <summary>Lo que se pide al armar un modelo: cuánto de qué lote.</summary>
    public class LoteAsignado
    {
        public int IdLote { get; set; }
        public decimal Cantidad { get; set; }

        public LoteAsignado() { }
        public LoteAsignado(int idLote, decimal cantidad)
        {
            IdLote = idLote;
            Cantidad = cantidad;
        }
    }

    /// <summary>Un material usado en un modelo, con los datos para mostrarlo.</summary>
    public class MaterialDeModelo
    {
        public int IdMaterialModelo { get; set; }
        public int IdModelo { get; set; }
        public int IdLote { get; set; }
        public int IdMaterial { get; set; }
        public string Familia { get; set; }
        public string Grupo { get; set; }
        public string Caracteristica { get; set; }
        public string Color { get; set; }
        public string Medidas { get; set; }
        public string Codigo { get; set; }
        public string Fabricante { get; set; }
        public string TipoUnidad { get; set; }
        public decimal Cantidad { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Subtotal => Cantidad * ValorUnitario;
    }
}
