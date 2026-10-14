using System;
using System.Collections.Generic;

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

    /// <summary>De qué stock de un lote sale o a cuál vuelve el material.</summary>
    public static class EstadosMaterial
    {
        public const string Nuevo = "N";
        public const string Usado = "U";

        public static string Nombre(string estado) => estado == Usado ? "Usado" : "Nuevo";
    }

    /// <summary>Qué pasa con material que sale de un modelo.</summary>
    public enum DestinoMaterial
    {
        /// <summary>Roto o consumido: no vuelve al stock.</summary>
        Descartar,
        /// <summary>No se llegó a usar: vuelve al stock nuevo (sólo si salió del nuevo).</summary>
        Nuevo,
        /// <summary>Se recicla: vuelve al stock usado de su lote.</summary>
        Usado
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
        public int IdTipoUnidad { get; set; }
        public string TipoUnidad { get; set; }
        public decimal? CantidadAComprar { get; set; }
        public int CantidadLotes { get; set; }
        public decimal Cantidad { get; set; }
        public decimal DisponibleNuevo { get; set; }
        public decimal DisponibleUsado { get; set; }
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

    /// <summary>
    /// Un ingreso de stock de un material (antes "Acopio"). Lo que queda se lleva separado
    /// en nuevo y usado: lo reciclado de los modelos vuelve como usado.
    /// </summary>
    public class Lote
    {
        public int IdLote { get; set; }
        public int IdMaterial { get; set; }
        public string Fabricante { get; set; }
        public string Ubicacion { get; set; }
        public decimal Cantidad { get; set; }

        /// <summary>Se compró usado: todo entra como usado.</summary>
        public bool CompradoUsado { get; set; }

        public decimal DisponibleNuevo { get; set; }
        public decimal DisponibleUsado { get; set; }
        public decimal Disponible => DisponibleNuevo + DisponibleUsado;

        /// <summary>Lo que salió del lote y no volvió (usado en modelos o descartado).</summary>
        public decimal Consumido => Cantidad - Disponible;

        public string Compra => CompradoUsado ? "Usado" : "Nuevo";

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
        public string Estado { get; set; }
        public string Condicion => EstadosMaterial.Nombre(Estado);
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

        /// <summary>Cuántos renglones de material tiene; sólo al leer.</summary>
        public int CantidadMateriales { get; set; }

        /// <summary>Valor de sus materiales (cantidad × precio del lote); sólo al leer.</summary>
        public decimal ValorMateriales { get; set; }
    }

    public class CambioDeCantidad
    {
        public int IdMaterialModelo { get; set; }
        public decimal Cantidad { get; set; }

        /// <summary>Si la cantidad baja, qué pasa con lo que se quita.</summary>
        public DestinoMaterial Destino { get; set; }
    }

    public class MaterialQuitado
    {
        public int IdMaterialModelo { get; set; }
        public DestinoMaterial Destino { get; set; }
    }

    /// <summary>Al eliminar un modelo: cuánto de uno de sus materiales vuelve al stock y cómo.</summary>
    public class MaterialADevolver
    {
        public int IdMaterialModelo { get; set; }
        public decimal Cantidad { get; set; }
        public DestinoMaterial Destino { get; set; } = DestinoMaterial.Usado;

        public MaterialADevolver() { }
        public MaterialADevolver(int idMaterialModelo, decimal cantidad, DestinoMaterial destino = DestinoMaterial.Usado)
        {
            IdMaterialModelo = idMaterialModelo;
            Cantidad = cantidad;
            Destino = destino;
        }
    }

    /// <summary>Cambios de materiales de un modelo que se guardan juntos.</summary>
    public class CambiosDeMateriales
    {
        public List<LoteAsignado> Agregados { get; } = new List<LoteAsignado>();
        public List<CambioDeCantidad> Cantidades { get; } = new List<CambioDeCantidad>();
        public List<MaterialQuitado> Quitados { get; } = new List<MaterialQuitado>();

        public bool HayCambios => Agregados.Count > 0 || Cantidades.Count > 0 || Quitados.Count > 0;
    }

    /// <summary>Lo que se pide al armar un modelo: cuánto de qué lote y de qué stock (nuevo o usado).</summary>
    public class LoteAsignado
    {
        public int IdLote { get; set; }
        public string Estado { get; set; } = EstadosMaterial.Nuevo;
        public decimal Cantidad { get; set; }

        public LoteAsignado() { }
        public LoteAsignado(int idLote, decimal cantidad, string estado = EstadosMaterial.Nuevo)
        {
            IdLote = idLote;
            Cantidad = cantidad;
            Estado = estado;
        }
    }

    /// <summary>Un material usado en un modelo, con los datos para mostrarlo.</summary>
    public class MaterialDeModelo
    {
        public int IdMaterialModelo { get; set; }
        public int IdModelo { get; set; }
        public int IdLote { get; set; }
        public string Estado { get; set; }
        public string Condicion => EstadosMaterial.Nombre(Estado);
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
