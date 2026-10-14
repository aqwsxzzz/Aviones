using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Aviones_2._0.Data.Repositorios
{
    public class EstadoModeloRepositorio
    {
        public List<EstadoModelo> Listar()
        {
            using (var cn = Db.Abrir())
            {
                return cn.Query<EstadoModelo>("SELECT IdEstadoModelo, Nombre FROM dbo.EstadosModelo ORDER BY IdEstadoModelo").ToList();
            }
        }
    }
}
