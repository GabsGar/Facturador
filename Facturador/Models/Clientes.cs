using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Facturador.Models
{
    public class Clientes
    {
        public int Id { get; set; }

        [Required]
        public string RFC { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string UsoCFDI { get; set; }
    }
}