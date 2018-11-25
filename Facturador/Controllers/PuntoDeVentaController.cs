using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Facturador.Controllers
{
    public class PuntoDeVentaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}