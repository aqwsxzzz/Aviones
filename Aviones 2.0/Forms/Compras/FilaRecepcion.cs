using System;
using Aviones_2._0.Data;

namespace Aviones_2._0.Forms.Compras
{
    /// <summary>
    /// Un renglón del pedido que se está recibiendo: los datos del material (que se pueden corregir,
    /// incluidos nota, unidad y foto, que no se pidieron al crearlo), el vendedor y el lote que entra al stock.
    /// </summary>
    public class FilaRecepcion
    {
        private readonly Material original;
        private readonly int idVendedorOriginal;

        public FilaRecepcion(PedidoCompra pedido, Material material, Lote ultimoLote, bool tieneFoto)
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
            Nota = material.Nota;
            IdTipoUnidad = material.IdTipoUnidad;
            Pedido = pedido.Cantidad;
            Llego = pedido.Cantidad;
            ValorUnitario = pedido.Precio ?? 0m;
            IdVendedor = idVendedorOriginal = pedido.IdVendedor ?? 0;
            // Lo más probable es que venga del mismo fabricante y vaya al mismo lugar que la última vez
            IdFabricante = ultimoLote?.IdFabricante ?? 0;
            IdUbicacion = ultimoLote?.IdUbicacion ?? 0;
            TeniaFoto = tieneFoto;
        }

        public int IdPedido { get; }
        public int IdMaterial { get; }
        public int IdFamilia { get; set; }
        public string Grupo { get; set; }
        public string Caracteristica { get; set; }
        public string Color { get; set; }
        public string Medidas { get; set; }
        public string Codigo { get; set; }
        public string Nota { get; set; }
        public int IdTipoUnidad { get; set; }

        /// <summary>Lo que se pidió.</summary>
        public decimal Pedido { get; }

        /// <summary>Lo que llegó; empieza igual a lo pedido. 0: no llegó.</summary>
        public decimal Llego { get; set; }

        public decimal ValorUnitario { get; set; }

        /// <summary>0: sin vendedor.</summary>
        public int IdVendedor { get; set; }

        /// <summary>0: sin fabricante.</summary>
        public int IdFabricante { get; set; }

        /// <summary>0: sin ubicación.</summary>
        public int IdUbicacion { get; set; }

        private bool TeniaFoto { get; }

        /// <summary>La foto nueva (null: se quitó); sólo cuenta si <see cref="FotoCambiada"/>.</summary>
        public byte[] FotoNueva { get; private set; }

        public bool FotoCambiada { get; private set; }

        /// <summary>Para la lista: si tiene foto.</summary>
        public string Foto => (FotoCambiada ? FotoNueva != null : TeniaFoto) ? "Sí" : "";

        public void CambiarFoto(byte[] foto)
        {
            FotoNueva = foto;
            FotoCambiada = true;
        }

        /// <summary>Se cambió algún dato del material.</summary>
        public bool MaterialCambiado =>
            IdFamilia != original.IdFamilia || IdTipoUnidad != original.IdTipoUnidad
            || !Igual(Grupo, original.Grupo) || !Igual(Caracteristica, original.Caracteristica)
            || !Igual(Color, original.Color) || !Igual(Medidas, original.Medidas) || !Igual(Codigo, original.Codigo)
            || !Igual(Nota, original.Nota);

        public Recepcion ARecepcion(DateTime fecha) => new Recepcion
        {
            IdPedido = IdPedido,
            Material = MaterialCambiado ? MaterialCorregido() : null,
            CambiarVendedor = IdVendedor != idVendedorOriginal,
            IdVendedor = IdVendedor == 0 ? (int?)null : IdVendedor,
            CambiarFoto = FotoCambiada,
            Foto = FotoNueva,
            Lote = new Lote
            {
                Cantidad = Llego,
                ValorUnitario = ValorUnitario,
                IdFabricante = IdFabricante == 0 ? (int?)null : IdFabricante,
                IdUbicacion = IdUbicacion == 0 ? (int?)null : IdUbicacion,
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
            IdTipoUnidad = IdTipoUnidad,
            StockMinimo = original.StockMinimo,
            SeRepone = original.SeRepone,
            Nota = Nota
        };

        private static bool Igual(string a, string b) =>
            string.Equals((a ?? "").Trim(), (b ?? "").Trim(), StringComparison.Ordinal);
    }
}
