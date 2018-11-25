using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Facturador.Models
{
    public class ClaveProdServ
    {
        public int Id { get; set; }

        [Required]
        public int Clave { get; set; }

        [Required]
        public string Descripcion { get; set; }
    }
}
