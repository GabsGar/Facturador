using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facturador.Models.ViewModels
{
    public class VentaViewModel
    {
        public Ventas Venta { get; set; }

        public Clientes Clientes { get; set; }

        public IEnumerable<Transacciones> Transacciones { get; set; }

        public IEnumerable<Productos> Productos { get; set; }
    }
}
