﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Facturador.Models
{
    public class ClaveUnidad
    {
        public int Id { get; set; }

        [Required]
        public string Clave { get; set; }

        [Required]
        public string Descripcion { get; set; }
    }
}
