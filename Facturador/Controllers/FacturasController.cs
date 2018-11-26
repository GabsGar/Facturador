using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facturador.Models;
using Facturador.Models.ViewModels;
using Facturador.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Facturador.Controllers
{
    public class FacturasController : Controller
    {
        public IActionResult Index(Ventas ventas)
        {
            /***** Generar Factura *****
             * 1. Datos de venta
             * 2. Certificados
             * 3. Cadena Original
             * 4. Sello Digital
             * 5. XML
             * 6. PDF
             * 7. QR
             */

            FacturaViewModel facturaViewModel = new FacturaViewModel();

            //Asociar a la factura con la venta que se acaba de realizar
            facturaViewModel.Factura.VentaId = ventas.Id;

            facturaViewModel.Factura.FechaExpedicion = DateTime.Now.ToUniversalTime();

            facturaViewModel.Factura.TipoDeComprobante = SD.TipoDeComprobante;

            //facturaViewModel.Factura.Certificado



            return View();
        }
    }
}