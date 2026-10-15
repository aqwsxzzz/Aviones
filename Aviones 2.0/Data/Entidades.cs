using System;
using System.Collections.Generic;
using System.Linq;

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

    /// <summary>
    /// Qué pasa con material que sale de un modelo: cuánto vuelve al stock como nuevo (sólo si salió
    /// del nuevo), cuánto como usado (se recicla); el resto se descarta (roto o consumido).
    /// </summary>
    public struct Devolucion
    {
        public decimal Nuevo { get; }
        public decimal Usado { get; }
        public decimal Total => Nuevo + Usado;

        public Devolucion(decimal nuevo, decimal usado)
        {
            Nuevo = nuevo;
            Usado = usado;
        }

        public static Devolucion Nada => new Devolucion(0m, 0m);
        public static Devolucion ComoNuevo(decimal cantidad) => new Devolucion(cantidad, 0m);
        public static Devolucion ComoUsado(decimal cantidad) => new Devolucion(0m, cantidad);
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

        /// <summary>Con esto o menos disponible es stock crítico. null: se usa el mínimo general.</summary>
        public decimal? StockMinimo { get; set; }
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
        public decimal? StockMinimo { get; set; }

        /// <summary>Suma de los pedidos de compra pendientes.</summary>
        public decimal PedidoPendiente { get; set; }

        public int CantidadLotes { get; set; }
        public decimal Cantidad { get; set; }
        public decimal DisponibleNuevo { get; set; }
        public decimal DisponibleUsado { get; set; }
        public decimal Disponible { get; set; }
        public decimal ValorDisponible { get; set; }

        public string Descripcion => string.Join(" ", new[] { Grupo, Caracteristica, Color, Medidas }
            .Where(t => !string.IsNullOrWhiteSpace(t)));
    }

    public class FiltroMateriales
    {
        public int? IdFamilia { get; set; }

        /// <summary>Busca en grupo, característica, color, medidas, código, fabricante y ubicación.</summary>
        public string Texto { get; set; }

        public bool SoloSinStock { get; set; }

        /// <summary>Sólo los que tienen un pedido de compra pendiente.</summary>
        public bool SoloAComprar { get; set; }
    }

    /// <summary>Un material con stock crítico: disponible menor o igual que su mínimo.</summary>
    public class StockCritico : MaterialStock
    {
        /// <summary>El mínimo que se usó: el del material, o el general si no tiene.</summary>
        public decimal MinimoAplicado { get; set; }

        public bool TienePedido => PedidoPendiente > 0;
    }

    public static class EstadosPedido
    {
        public const string Pendiente = "P";
        public const string Comprado = "C";
        public const string Cancelado = "X";
    }

    /// <summary>Un pedido de compra: cuánto hay que comprar de un material.</summary>
    public class PedidoCompra
    {
        public int IdPedido { get; set; }
        public int IdMaterial { get; set; }
        public string Familia { get; set; }
        public string Grupo { get; set; }
        public string Caracteristica { get; set; }
        public string Color { get; set; }
        public string Medidas { get; set; }
        public string Codigo { get; set; }
        public string TipoUnidad { get; set; }
        public decimal Cantidad { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Nota { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaCierre { get; set; }
        public decimal? CantidadRecibida { get; set; }
        public int? IdLote { get; set; }

        /// <summary>Lo que hay disponible ahora del material (nuevo + usado).</summary>
        public decimal Disponible { get; set; }

        /// <summary>Precio del último lote ingresado, como referencia.</summary>
        public decimal? UltimoPrecio { get; set; }

        public decimal? CostoEstimado => UltimoPrecio * Cantidad;

        public string Material => string.Join(" ", new[] { Grupo, Caracteristica, Color, Medidas }
            .Where(t => !string.IsNullOrWhiteSpace(t)));
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

        /// <summary>Si la cantidad baja, cuánto de lo que se quita vuelve como nuevo y como usado.</summary>
        public Devolucion DevolucionSiBaja { get; set; }
    }

    public class MaterialQuitado
    {
        public int IdMaterialModelo { get; set; }
        public Devolucion Devolucion { get; set; }
    }

    /// <summary>Al eliminar un modelo: cuánto de uno de sus materiales vuelve al stock como nuevo y como usado.</summary>
    public class MaterialADevolver
    {
        public int IdMaterialModelo { get; set; }
        public Devolucion Devolucion { get; set; }

        public MaterialADevolver() { }
        public MaterialADevolver(int idMaterialModelo, Devolucion devolucion)
        {
            IdMaterialModelo = idMaterialModelo;
            Devolucion = devolucion;
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
