using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Facturador.Models
{
    public class Facturas
    {
        public int Id { get; set; }

        [Required]
        public string Sello { get; set; }

        [Required]
        public string NoCertificado { get; set; }

        [Required]
        public string Certificado { get; set; }

        public string CondicionesDePago { get; set; }

        [Required]
        public DateTime FechaExpedicion { get; set; }

        [Required]
        public string TipoDeComprobante { get; set; }

        [Required]
        public int VentaId { get; set; }

        [ForeignKey("VentaId")]
        public virtual Ventas Ventas { get; set; }
    }
}
