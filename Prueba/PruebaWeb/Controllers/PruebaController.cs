using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Runtime.InteropServices;

namespace PruebaWeb.Controllers
{
    public class PruebaController : Controller
    {
        
        public ActionResult CaesarEncriptar(string clave, HttpPostedFileBase postedFile)
        {
            string rutaArchivo = string.Empty;
            //el siguiente if permite seleccionar un archivo en específico
            if (postedFile != null)
            {
                string ruta = Server.MapPath("");
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                rutaArchivo = ruta + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);

               
                Data.Caesar.Instance.ElChecha(postedFile, ruta, clave);
                byte[] ByteArchivos = new byte[postedFile.ContentLength];
                postedFile.InputStream.Read(ByteArchivos, 0, ByteArchivos.Length);
                return File(ByteArchivos, System.Net.Mime.MediaTypeNames.Application.Octet, postedFile.FileName);
            }
            return View();
        }
        public ActionResult CaesarDesEncriptar(HttpPostedFileBase postedFile, string clave)
        {
            string rutaArchivo = string.Empty;
            //el siguiente if permite seleccionar un archivo en específico
            if (postedFile != null)
            {
                string ruta = Server.MapPath("");
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                rutaArchivo = ruta + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);

                
                Data.Caesar.Instance.DesencriptarChecha(postedFile, ruta, clave);
                byte[] ByteArchivos = new byte[postedFile.ContentLength];
                postedFile.InputStream.Read(ByteArchivos, 0, ByteArchivos.Length);
                return File(ByteArchivos, System.Net.Mime.MediaTypeNames.Application.Octet, postedFile.FileName);
            }
            return View();
        }
        public ActionResult ZigZagEncriptar(HttpPostedFileBase postedFile, double? clave)
        {
            string rutaArchivo = string.Empty;
            //el siguiente if permite seleccionar un archivo en específico
            if (postedFile != null)
            {
                string ruta = Server.MapPath("");
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                rutaArchivo = ruta + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);

                
                Data.ZigZag.Instance.EncriptarZigZag(postedFile, ruta, clave);
                byte[] ByteArchivos = new byte[postedFile.ContentLength];
                postedFile.InputStream.Read(ByteArchivos, 0, ByteArchivos.Length);
                return File(ByteArchivos, System.Net.Mime.MediaTypeNames.Application.Octet, postedFile.FileName);
            }
            return View();
        }

        public ActionResult DesencriptarZigZag(HttpPostedFileBase postedFile, double? clave1 )
        {
            string rutaArchivo = string.Empty;
            //el siguiente if permite seleccionar un archivo en específico
            if (postedFile != null)
            {
                string ruta = Server.MapPath("");
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                rutaArchivo = ruta + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);


                Data.ZigZag.Instance.DesencriptarZigZag(postedFile, ruta, clave1);
                byte[] ByteArchivos = new byte[postedFile.ContentLength];
                postedFile.InputStream.Read(ByteArchivos, 0, ByteArchivos.Length);
                return File(ByteArchivos, System.Net.Mime.MediaTypeNames.Application.Octet, postedFile.FileName);
            }
            return View();
        }
        public ActionResult SDESEncriptar(string clave, HttpPostedFileBase postedFile)
        {
            string rutaArchivo = string.Empty;
            //el siguiente if permite seleccionar un archivo en específico
            if (postedFile != null)
            {
                string ruta = Server.MapPath("");
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                rutaArchivo = ruta + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);


                Data.SDES.Instance.CodificarSDES(postedFile, ruta, clave);
                byte[] ByteArchivos = new byte[postedFile.ContentLength];
                postedFile.InputStream.Read(ByteArchivos, 0, ByteArchivos.Length);
                return File(ByteArchivos, System.Net.Mime.MediaTypeNames.Application.Octet, postedFile.FileName);
            }
            return View();
        }
        public ActionResult SDESDesencriptar(HttpPostedFileBase postedFile, string clave)
        {
            string rutaArchivo = string.Empty;
            //el siguiente if permite seleccionar un archivo en específico
            if (postedFile != null)
            {
                string ruta = Server.MapPath("");
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                rutaArchivo = ruta + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);


                Data.SDES.Instance.DecodificarSDES(postedFile, ruta, clave);
                byte[] ByteArchivos = new byte[postedFile.ContentLength];
                postedFile.InputStream.Read(ByteArchivos, 0, ByteArchivos.Length);
                return File(ByteArchivos, System.Net.Mime.MediaTypeNames.Application.Octet, postedFile.FileName);
            }
            return View();
        }
    }
}
