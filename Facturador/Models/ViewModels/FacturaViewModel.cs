using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facturador.Models.ViewModels
{
    public class FacturaViewModel
    {
        public Ventas Venta { get; set; }        

        public Clientes Cliente { get; set; }
    }
}
