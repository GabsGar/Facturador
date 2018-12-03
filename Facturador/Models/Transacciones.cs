using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Facturador.Models
{
    public class Transacciones
    {
        public int Id { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public virtual Productos Productos { get; set; }

        [Required]
        public int CantidadVendida { get; set; }

        [Required]
        public double SubTotal { get; set; }

        [Required]
        public double Total { get; set; }

        [Required]
        public double Descuento { get; set; }

        [Required]
        public double ImpuestosRetenidos { get; set; }

        [Required]
        public double ImpuestosTrasladados { get; set; }

        [Required]
        public int VentaId { get; set; }

        [ForeignKey("VentaId")]
        public virtual Ventas Ventas { get; set; }
    }
}
