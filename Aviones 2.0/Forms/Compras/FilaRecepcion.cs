using System;
using Aviones_2._0.Data;

namespace Aviones_2._0.Forms.Compras
{
    /// <summary>
    /// Un renglón del pedido que se está recibiendo: los datos del material (que se pueden corregir)
    /// y los del lote que entra al stock.
    /// </summary>
    public class FilaRecepcion
    {
        private readonly Material original;

        public FilaRecepcion(PedidoCompra pedido, Material material, Lote ultimoLote)
        {
            original = material;
            IdPedido = pedido.IdPedido;
            IdMaterial = pedido.IdMaterial;
            IdFamilia = material.IdFamilia;
            Grupo = material.Grupo;
            Caracteristica = material.Caracteristica;
            Color = material.Color;
            Medidas = material.Medidas;
            Codigo = material.Codigo;
            TipoUnidad = pedido.TipoUnidad;
            Pedido = pedido.Cantidad;
            Llego = pedido.Cantidad;
            ValorUnitario = pedido.Precio ?? 0m;
            // Lo más probable es que venga del mismo fabricante y vaya al mismo lugar que la última vez
            IdFabricante = ultimoLote?.IdFabricante ?? 0;
            IdUbicacion = ultimoLote?.IdUbicacion ?? 0;
            Compra = EstadosMaterial.Nombre(EstadosMaterial.Nuevo);
        }

        public int IdPedido { get; }
        public int IdMaterial { get; }
        public int IdFamilia { get; set; }
        public string Grupo { get; set; }
        public string Caracteristica { get; set; }
        public string Color { get; set; }
        public string Medidas { get; set; }
        public string Codigo { get; set; }
        public string TipoUnidad { get; }

        /// <summary>Lo que se pidió.</summary>
        public decimal Pedido { get; }

        /// <summary>Lo que llegó; empieza igual a lo pedido. 0: no llegó.</summary>
        public decimal Llego { get; set; }

        public decimal ValorUnitario { get; set; }

        /// <summary>0: sin fabricante.</summary>
        public int IdFabricante { get; set; }

        /// <summary>0: sin ubicación.</summary>
        public int IdUbicacion { get; set; }

        /// <summary>"Nuevo" o "Usado".</summary>
        public string Compra { get; set; }

        /// <summary>Se cambió algún dato del material.</summary>
        public bool MaterialCambiado =>
            IdFamilia != original.IdFamilia
            || !Igual(Grupo, original.Grupo) || !Igual(Caracteristica, original.Caracteristica)
            || !Igual(Color, original.Color) || !Igual(Medidas, original.Medidas) || !Igual(Codigo, original.Codigo);

        public Recepcion ARecepcion(DateTime fecha) => new Recepcion
        {
            IdPedido = IdPedido,
            Material = MaterialCambiado ? MaterialCorregido() : null,
            Lote = new Lote
            {
                Cantidad = Llego,
                ValorUnitario = ValorUnitario,
                IdFabricante = IdFabricante == 0 ? (int?)null : IdFabricante,
                IdUbicacion = IdUbicacion == 0 ? (int?)null : IdUbicacion,
                CompradoUsado = Compra == EstadosMaterial.Nombre(EstadosMaterial.Usado),
                FechaIngreso = fecha
            }
        };

        private Material MaterialCorregido() => new Material
        {
            IdMaterial = original.IdMaterial,
            IdFamilia = IdFamilia,
            Grupo = Grupo,
            Caracteristica = Caracteristica,
            Color = Color,
            Medidas = Medidas,
            Codigo = Codigo,
            IdTipoUnidad = original.IdTipoUnidad,
            StockMinimo = original.StockMinimo,
            SeRepone = original.SeRepone,
            Nota = original.Nota
        };

        private static bool Igual(string a, string b) =>
            string.Equals((a ?? "").Trim(), (b ?? "").Trim(), StringComparison.Ordinal);
    }
}
