using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Facturador.Models
{
    public class Productos
    {
        public int Id { get; set; }

        [Required]
        public double ValorUnitario { get; set; }

        [Required]
        public int CantidadStock { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public int ClaveProdServId { get; set; }

        [ForeignKey("ClaveProdServId")]
        public virtual ClaveProdServ ClaveProdServ { get; set; }

        [Required]
        public int ClaveUnidadId { get; set; }

        [ForeignKey("ClaveUnidadId")]
        public virtual ClaveUnidad ClaveUnidad { get; set; }
    }
}
