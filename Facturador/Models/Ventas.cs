using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Facturador.Models
{
    public class Ventas
    {
        public int Id { get; set;}

        [Required]
        public double SubTotal { get; set; }

        [Required]
        public double Total { get; set; }

        [Required]
        public double DescuentoTotal { get; set; }

        [Required]
        public double TotalImpuestosRetenidos { get; set; }

        [Required]
        public double TotalImpuestosTrasladados { get; set; }

        [Required]
        public DateTime FechaVenta { get; set; }
        
        public double TipoCambio { get; set; }

        public string CondicionesDePago { get; set; }

        [Required]
        public int MonedaId { get; set; }

        [ForeignKey("MonedaId")]
        public virtual Monedas Moneda { get; set; }

        [Required]
        public int MetodoPagoId { get; set; }

        [ForeignKey("MetodoPagoId")]
        public virtual MetodosPago MetodosPago { get; set; }

        [Required]
        public int FormaPagoId { get; set; }

        [ForeignKey("FormaPagoId")]
        public virtual FormasPago FormasPago { get; set; }
    }
}
