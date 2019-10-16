using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace PruebaWeb.Data
{
    public class Caesar
    {
        private static Caesar _instance = null;
        public static Caesar Instance
        {
            get
            {
                if (_instance == null) _instance = new Caesar();
                return _instance;
            }
        }
        public static string mensajeEncriptado = "";
        public static string mensajeDesencriptado = "";
        public void ElChecha(HttpPostedFileBase postedFile, string ruta, string clave)
        {
            using (var stream = new FileStream(postedFile.FileName, FileMode.Open))
            {
                using (var reader = new StreamReader(stream, Encoding.Default))
                {
                    using (var writeStream = new FileStream(ruta + "\\EncriptarCesar.cif", FileMode.OpenOrCreate))
                    {
                        using (var writer = new StreamWriter(writeStream))
                        {
                            
                            while (reader.BaseStream.Position != reader.BaseStream.Length)
                            {
                                var stringBuffer = reader.ReadLine();
                                var DiccionarioChechaMayus = new Dictionary<char, char>();
                                var DiccionarioChechaMinus = new Dictionary<char, char>();

                                string Alfabeto = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ .,:;¿?¡!1234567890";
                                string AlfabetoMin = "abcdefghijklmnñopqrstuvwxyz .,:;¿?¡!1234567890";
                                string claveChecha = clave.ToUpper() + Alfabeto.ToUpper();
                                string claveChechaMin = clave + AlfabetoMin;
                                //_ =  new string ("");


                                string c = RemoverLetrasRepetidas(claveChecha);
                                string r = RemoverLetrasRepetidas(claveChechaMin);
                                //llenar diccionario Mayusculas
                                for (int i = 0; i < Alfabeto.Length; i++)
                                {
                                    DiccionarioChechaMayus.Add(Alfabeto[i], c[i]);
                                }
                                //llenar diccionario Minusculas
                                for (int k = 0; k < AlfabetoMin.Length; k++)
                                {
                                    DiccionarioChechaMinus.Add(AlfabetoMin[k], r[k]);
                                }
                                string Encriptado = "";
                                for (int j = 0; j < stringBuffer.Length; j++)
                                {
                                    if (char.IsUpper(stringBuffer[j]))
                                    {
                                        if (DiccionarioChechaMayus.ContainsKey(stringBuffer[j]))
                                        {
                                            //char joto = MensajeAEncriptar[j];
                                            Encriptado = DiccionarioChechaMayus[stringBuffer[j]].ToString();
                                            mensajeEncriptado += Encriptado;
                                        }
                                    }
                                    else
                                    {
                                        if (DiccionarioChechaMinus.ContainsKey(stringBuffer[j]))
                                        {
                                            //char joto = MensajeAEncriptar[j];
                                            Encriptado = DiccionarioChechaMinus[stringBuffer[j]].ToString();
                                            mensajeEncriptado += Encriptado;
                                        }
                                    }
                                }
                                writer.Write(mensajeEncriptado);
                            }
                        }
                    }
                }
            }
           


            //Buffer = lectura.ReadBytes(1000000);
            //string Encriptado = "";
            //MensajeAEncriptar.ToCharArray();
            
        }
        public void DesencriptarChecha(HttpPostedFileBase postedFile, string ruta, string clave)
        {
            using (var stream = new FileStream(postedFile.FileName, FileMode.Open))
            {
                using (var reader = new StreamReader(stream, Encoding.Default))
                {
                    using (var writeStream = new FileStream(ruta + "\\DesencriptarCesar.txt", FileMode.OpenOrCreate))
                    {
                        using (var writer = new StreamWriter(writeStream))
                        {
                            
                            while (reader.BaseStream.Position != reader.BaseStream.Length)
                            {
                                var stringBuffer = reader.ReadLine();
                                var DiccionarioChechaMayus = new Dictionary<char, char>();
                                var DiccionarioChechaMinus = new Dictionary<char, char>();
                                string Alfabeto = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ .,:;¿?¡!1234567890";
                                string AlfabetoMin = "abcdefghijklmnñopqrstuvwxyz .,:;¿?¡!1234567890";
                                string claveChecha = clave.ToUpper() + Alfabeto.ToUpper();
                                string claveChechaMin = clave + AlfabetoMin;
                                //_ =  new string ("");


                                string c = RemoverLetrasRepetidas(claveChecha);
                                string r = RemoverLetrasRepetidas(claveChechaMin);
                                //llenar diccionario Mayusculas
                                for (int i = 0; i < Alfabeto.Length; i++)
                                {
                                    DiccionarioChechaMayus.Add(c[i], Alfabeto[i]);
                                }
                                //llenar diccionario Minusculas
                                for (int k = 0; k < AlfabetoMin.Length; k++)
                                {
                                    DiccionarioChechaMinus.Add(r[k], AlfabetoMin[k]);
                                }

                                string Encriptado = "";
                                for (int j = 0; j < stringBuffer.Length; j++)
                                {
                                    if (char.IsUpper(stringBuffer[j]))
                                    {
                                        if (DiccionarioChechaMayus.ContainsKey(stringBuffer[j]))
                                        {
                                            //char joto = MensajeAEncriptar[j];
                                            Encriptado = DiccionarioChechaMayus[stringBuffer[j]].ToString();
                                            mensajeDesencriptado += Encriptado;
                                        }
                                    }
                                    else
                                    {
                                        if (DiccionarioChechaMinus.ContainsKey(stringBuffer[j]))
                                        {
                                            //char joto = MensajeAEncriptar[j];
                                            Encriptado = DiccionarioChechaMinus[stringBuffer[j]].ToString();
                                            mensajeDesencriptado += Encriptado;
                                        }
                                    }
                                }
                                writer.Write(mensajeDesencriptado);
                            }
                        }
                    }
                }
            }
        }
        public static string RemoverLetrasRepetidas(string input)
        {
            return new string(input.ToCharArray().Distinct().ToArray());
        }
    }
}