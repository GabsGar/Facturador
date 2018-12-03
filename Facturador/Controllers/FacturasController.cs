using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Facturador.Data;
using Facturador.Models;
using Facturador.Models.OpenSSL;
using Facturador.Models.ViewModels;
using Facturador.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Facturador.Controllers
{
    public class FacturasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacturasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(FacturaViewModel facturaViewModel)
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
             
            Facturas Factura = new Facturas();

            //Asociar a la factura con la venta que se acaba de realizar
            Factura.Ventas = facturaViewModel.Venta;

            Factura.VentaId = Factura.Ventas.Id;

            Factura.Clientes = facturaViewModel.Cliente;

            Factura.ClienteID = Factura.Clientes.Id;

            Factura.FechaExpedicion = DateTime.Now.ToUniversalTime();

            Factura.TipoDeComprobante = SD.TipoDeComprobante;

            //Traer las transacciones asociadas a la factura
            List<Transacciones> transacciones = new List<Transacciones>();
            transacciones = _context.Transacciones.Where(x => x.VentaId == Factura.VentaId).ToList();


            //**** Certificados ****//
            X509Certificate2 certEmisor = new X509Certificate2(); // Generas un objeto del tipo de certificado

            FileStream f = new FileStream("C:/Users/Xochipilli/Desktop/CSDAAA010101AAA/CSD01_AAA010101AAA.cer", FileMode.Open, FileAccess.Read);
            int size = (int)f.Length;
            byte[] data = new byte[size];
            size = f.Read(data, 0, size);
            f.Close();
            var strCertificado = data;

            byte[] byteCertData = strCertificado; // Manda llamar la funcion Readfile para cargar el archivo .cer

            certEmisor.Import(byteCertData); // Importa los datos del certificado qeu acabas d
            
            //Recuperar numero de serie de certificado
            Factura.NoCertificado = System.Text.ASCIIEncoding.ASCII.GetString(certEmisor.GetSerialNumber());

            //Convertir certificado a base 64
            Factura.Certificado = Convert.ToBase64String(certEmisor.GetRawCertData());


            //**** Cadena Original ****//
            string CadenaOriginal = "||" + SD.Version + "|" + Factura.FechaExpedicion + "|" + SD.TipoDeComprobante + "|" + Factura.Ventas.FormasPago.Clave + "|";
            if (Factura.Ventas.CondicionesDePago != null)
            {
                CadenaOriginal = CadenaOriginal + Factura.Ventas.CondicionesDePago + "|";
            }
            CadenaOriginal = CadenaOriginal + Factura.Ventas.SubTotal + "|";
            if (!Double.IsNaN(Factura.Ventas.DescuentoTotal))
            {
                CadenaOriginal = CadenaOriginal + Factura.Ventas.DescuentoTotal + "|";
            }
            if (!Double.IsNaN(Factura.Ventas.TipoCambio))
            {
                CadenaOriginal = CadenaOriginal + Factura.Ventas.TipoCambio + "|";
            }
            CadenaOriginal = CadenaOriginal + Factura.Ventas.Moneda.Clave + "|" + Factura.Ventas.Total + "|" + Factura.Ventas.MetodosPago.Clave + "|" + SD.LugarExpedicion + "|";
            CadenaOriginal = CadenaOriginal + SD.RFCEmisor + "|" + SD.NombreEmisor + "|" + SD.DomicilioFiscalCalle + "|" + SD.DomicilioFiscalNoExterior + "|" + SD.DomicilioFiscalColonia + "|" + SD.DomicilioFiscalMunicipio + "|" + SD.DomicilioFiscalEstado + "|" + SD.DomicilioFiscalCodigoPostal + "|";
            CadenaOriginal = CadenaOriginal + SD.RegimenFiscalEmisor + "|" + Factura.Clientes.RFC + "|" + Factura.Clientes.Nombre + "|";
            foreach(var item in transacciones)
            {
                CadenaOriginal = CadenaOriginal + item.CantidadVendida + "|" + item.Productos.ClaveUnidad.Clave + "|" + item.Productos.Descripcion + "|" + item.Productos.ValorUnitario + "|" + item.SubTotal + "|";
            }
            double acumuladorImpuestos = 0;
            foreach (var item in transacciones)
            {
                acumuladorImpuestos = acumuladorImpuestos + item.ImpuestosRetenidos;
                CadenaOriginal = CadenaOriginal + SD.Impuesto + "|" + item.ImpuestosRetenidos + "|";
            }
            CadenaOriginal = CadenaOriginal + acumuladorImpuestos + "||";


            //**** Sello Digital ****//
            string strSello = string.Empty;
            string strPathLlave = "C:/Users/Xochipilli/Desktop/CSDAAA010101AAA/CSD01_AAA010101AAA.key";
            string strLlavePwd = "12345678a";

            System.Security.SecureString passwordSeguro = new System.Security.SecureString();
            passwordSeguro.Clear();

            foreach (char c in strLlavePwd.ToCharArray())
                passwordSeguro.AppendChar(c);

            byte[] llavePrivadaBytes = System.IO.File.ReadAllBytes(strPathLlave);
            RSACryptoServiceProvider rsa = opensslkey.DecodeEncryptedPrivateKeyInfo(llavePrivadaBytes, passwordSeguro);

            SHA1Managed sha = new SHA1Managed();
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(CadenaOriginal);
            byte[] digest = sha.ComputeHash(bytes);

            RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(rsa);
            RSAFormatter.SetHashAlgorithm("SHA1");
            byte[] SignedHashValue = RSAFormatter.CreateSignature(digest);

            SHA1CryptoServiceProvider hasher = new SHA1CryptoServiceProvider();

            byte[] bytesFirmados = rsa.SignData(System.Text.Encoding.UTF8.GetBytes(CadenaOriginal), hasher);
            strSello = Convert.ToBase64String(bytesFirmados);  // Y aquí está el sello


            //**** XML ****//
            XmlWriter xmlWriter = XmlWriter.Create("Factura.xml");
            xmlWriter.WriteStartDocument();

            //** Comprobante **//
            xmlWriter.WriteStartElement("cfdi:Comrpobante");
            xmlWriter.WriteAttributeString("Version", SD.Version);
            xmlWriter.WriteAttributeString("Fecha", Factura.FechaExpedicion.ToString());
            xmlWriter.WriteAttributeString("Sello", strSello);
            xmlWriter.WriteAttributeString("FormaPago", Factura.Ventas.FormasPago.Clave.ToString());
            xmlWriter.WriteAttributeString("NoCertificado", Factura.NoCertificado);
            xmlWriter.WriteAttributeString("Certificado", Factura.Certificado);
            xmlWriter.WriteAttributeString("Certificado", Factura.Certificado);
            if (Factura.Ventas.CondicionesDePago != null)
            {
                xmlWriter.WriteAttributeString("CondicionesDePago", Factura.Ventas.CondicionesDePago);
            }
            xmlWriter.WriteAttributeString("SubTotal", Factura.Ventas.SubTotal.ToString());
            if (!Double.IsNaN(Factura.Ventas.DescuentoTotal))
            {
                xmlWriter.WriteAttributeString("Descuento", Factura.Ventas.DescuentoTotal.ToString());
            }
            xmlWriter.WriteAttributeString("Moneda", Factura.Ventas.Moneda.Clave);
            if (!Double.IsNaN(Factura.Ventas.TipoCambio))
            {
                xmlWriter.WriteAttributeString("TipoCambio", Factura.Ventas.TipoCambio.ToString());
            }
            xmlWriter.WriteAttributeString("Total", Factura.Ventas.Total.ToString());
            xmlWriter.WriteAttributeString("TipoDeComprobante", SD.TipoDeComprobante);
            xmlWriter.WriteAttributeString("MetodoPago", Factura.Ventas.MetodosPago.Clave);
            xmlWriter.WriteAttributeString("LugarExpedicion", SD.LugarExpedicion);

            //** Emisor **//
            xmlWriter.WriteStartElement("cfdi:Emisor");
            xmlWriter.WriteAttributeString("Rfc", SD.RFCEmisor);
            xmlWriter.WriteAttributeString("Nombre", SD.NombreEmisor);
            xmlWriter.WriteAttributeString("RegimenFiscal", SD.RegimenFiscalEmisor);
            xmlWriter.WriteEndElement();

            //** Receptor **//
            xmlWriter.WriteStartElement("cfdi:Receptor");
            xmlWriter.WriteAttributeString("Rfc", Factura.Clientes.RFC);
            xmlWriter.WriteAttributeString("Nombre", Factura.Clientes.Nombre);
            xmlWriter.WriteAttributeString("UsoCFDI", SD.UsoCFDI);
            xmlWriter.WriteEndElement();

            //** Conceptos **//
            xmlWriter.WriteStartElement("cfdi:Conceptos");
            xmlWriter.WriteAttributeString("Rfc", Factura.Clientes.RFC);
            xmlWriter.WriteAttributeString("Nombre", Factura.Clientes.Nombre);
            xmlWriter.WriteAttributeString("UsoCFDI", SD.UsoCFDI);

            //** Concepto **//
            foreach (var item in transacciones)
            {
                xmlWriter.WriteStartElement("cfdi:Concepto");
                xmlWriter.WriteAttributeString("ClaveProdServ", item.Productos.ClaveProdServ.Clave.ToString());
                xmlWriter.WriteAttributeString("Cantidad", item.CantidadVendida.ToString());
                xmlWriter.WriteAttributeString("ClaveUnidad", item.Productos.ClaveUnidad.Clave);
                xmlWriter.WriteAttributeString("Unidad", item.Productos.ClaveUnidad.Descripcion);
                xmlWriter.WriteAttributeString("Descripcion", item.Productos.Descripcion);
                xmlWriter.WriteAttributeString("ValorUnitario", item.Productos.ValorUnitario.ToString());
                xmlWriter.WriteAttributeString("Importe", item.SubTotal.ToString());
                if (!Double.IsNaN(item.Descuento))
                {
                    xmlWriter.WriteAttributeString("Descuento", item.Descuento.ToString());
                }
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();

            //** Impuestos **//
            xmlWriter.WriteStartElement("cfdi:Impuestos");
            xmlWriter.WriteAttributeString("TotalImpuestosRetenidos", acumuladorImpuestos.ToString());
            //** Retenciones **//
            xmlWriter.WriteStartElement("cfdi:Retenciones");
            //** Retencion **//
            foreach (var item in transacciones)
            {
                xmlWriter.WriteStartElement("cfdi:Retencion");
                xmlWriter.WriteAttributeString("Impuesto", SD.Impuesto);
                xmlWriter.WriteAttributeString("Impuesto", SD.TipoFactor);
                xmlWriter.WriteAttributeString("TasaOCuota", SD.TasaOCuota);
                xmlWriter.WriteAttributeString("Importe", item.ImpuestosRetenidos.ToString());
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();

            return View(Factura);
        }
    }
}
 