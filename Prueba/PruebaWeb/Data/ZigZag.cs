using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace PruebaWeb.Data
{
    public class ZigZag
    {
        private static ZigZag _instance = null;
        public static ZigZag Instance
        {
            get
            {
                if (_instance == null) _instance = new ZigZag();
                return _instance;
            }
        }
        public static string MensajeEncriptadorZiZag = "";
        public static string MensajeAEncriptarZigZag = "";
        public static string MensajeDesencriptadoZigZag = "";
        public static double ClaveBuenaZigZag;
        public void EncriptarZigZag(HttpPostedFileBase postedFile, string ruta, int clave)
        {
            using (var stream = new FileStream(postedFile.FileName, FileMode.Open))
            {
                using (var reader = new StreamReader(stream, Encoding.Default))
                {
                    using (var writeStream = new FileStream(ruta+"\\EncriptarZigZag.cif", FileMode.OpenOrCreate))
                    {
                        using (var writer = new StreamWriter(writeStream))
                        {
                            var StringBuffer = reader.ReadLine();
                            while (reader.BaseStream.Position != reader.BaseStream.Length)
                            {
                                
                                ClaveBuenaZigZag = Convert.ToDouble(clave);
                                //Console.WriteLine("Ingrese mensaje a encryptar");
                                //MensajeAEncriptarZigZag = Console.ReadLine();
                                string[] niveles = new string[Convert.ToInt32(ClaveBuenaZigZag)];
                                int superior = Convert.ToInt32(Math.Ceiling(((2 * ClaveBuenaZigZag) + StringBuffer.Length - 3) / ((2 * ClaveBuenaZigZag) - 2)));
                                int intermedio = 2 * (superior - 1);
                                int inferior = superior - 1;
                                string Encriptado = "";

                                int i = 0;
                                //MensajeAEncriptarZigZag.ToCharArray();

                                while (i < StringBuffer.Length)
                                {
                                    for (int m = 0; m < ClaveBuenaZigZag; m++)
                                    {
                                        if (i == StringBuffer.Length)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            niveles[m] += StringBuffer[i];
                                            i++;
                                        }

                                    }
                                    for (int k = Convert.ToInt32(ClaveBuenaZigZag - 2); k > 0; k--)
                                    {

                                        if (i == StringBuffer.Length)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            niveles[k] += StringBuffer[i];
                                            i++;
                                        }

                                    }
                                }
                                while (niveles[0].Length < superior)
                                {
                                    niveles[0] += "$";
                                }
                                for (int z = 1; z < Convert.ToInt32(ClaveBuenaZigZag - 1); z++)
                                {
                                    while (niveles[z].Length < intermedio)
                                    {
                                        niveles[z] = niveles[z] + "$";
                                    }
                                }
                                while (niveles[Convert.ToInt32(ClaveBuenaZigZag - 1)].Length < inferior)
                                {
                                    niveles[Convert.ToInt32(ClaveBuenaZigZag - 1)] += "$";
                                }
                                for (int p = 0; p < ClaveBuenaZigZag; p++)
                                {
                                    MensajeEncriptadorZiZag += niveles[p];
                                }
                                writer.Write(MensajeEncriptadorZiZag);
                            }
                        }
                    }
                }
            }
        }

        public void DesencriptarZigZag(HttpPostedFileBase postedFile, string ruta, double clave)
        {
            using (var stream = new FileStream(postedFile.FileName, FileMode.Open))
            {
                using (var reader = new StreamReader(stream, Encoding.Default))
                {
                    using (var writeStream = new FileStream(ruta + "\\DesencriptadoZigZag.txt", FileMode.OpenOrCreate))
                    {
                        using (var writer = new StreamWriter(writeStream))
                        {
                            var StringBuffer = reader.ReadLine();
                            while (reader.BaseStream.Position != reader.BaseStream.Length)
                            {
                                
                                int superior = Convert.ToInt32(Math.Ceiling(((2 * clave) + MensajeAEncriptarZigZag.Length - 3) / ((2 * clave) - 2)));
                                int intermedio = 2 * (superior - 1);
                                int inferior = superior - 1;
                                int cont = 0;
                                string[] nivelesDesencriptar = new string[Convert.ToInt32(clave)];
                                if (clave == ClaveBuenaZigZag)
                                {
                                    //mensajeEncriptadorZiZag.ToCharArray();
                                    for (int i = 0; i < superior; i++)
                                    {
                                        nivelesDesencriptar[0] += StringBuffer[i].ToString();
                                    }
                                    for (int j = (StringBuffer.Length) - inferior; j < StringBuffer.Length; j++)
                                    {
                                        nivelesDesencriptar[Convert.ToInt32(clave - 1)] += StringBuffer[j].ToString();
                                    }

                                    int posicionFinal = StringBuffer.Length - (inferior + superior);
                                    string intermedios = StringBuffer.Substring(superior, posicionFinal);
                                    int contInt = 0;
                                    int posAct = 0;
                                    int q = 0;
                                    for (int m = 1; m < nivelesDesencriptar.Length; m++)
                                    {
                                        int agarrarCaracteresDelMedio = intermedios.Length / (nivelesDesencriptar.Length - 2);
                                        if (intermedios.Length <= posAct)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            nivelesDesencriptar[m] = intermedios.Substring(posAct, agarrarCaracteresDelMedio);
                                            posAct += agarrarCaracteresDelMedio;
                                        }
                                    }
                                    for (int p = 0; p < clave; p++)
                                    {
                                        MensajeDesencriptadoZigZag += nivelesDesencriptar[p];
                                    }
                                    writer.Write(MensajeDesencriptadoZigZag);
                                }
                                else
                                {
                                    var totalCaracteres = StringBuffer.Length;
                                    double m = (totalCaracteres + (2 * (clave - 2)+1))/(2+2*(clave-2));
                                    var mInt = Convert.ToInt32(Math.Round(m));
                                    if (mInt > m)
                                    {
                                        superior = Convert.ToInt32(Math.Floor(m));
                                        superior = inferior;
                                    }

                                }

                            }
                        }
                    }
                }
            }
        }
    }
}