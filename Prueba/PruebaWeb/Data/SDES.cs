using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Web;

namespace PruebaWeb.Data
{
    public class SDES
    {
        private static SDES _instance = null;
        public static SDES Instance
        {
            get
            {
                if (_instance == null) _instance = new SDES();
                return _instance;
            }
        }
        public static string p10;
        public static string p8;
        public static string p4;
        public static string ep;
        public static string ip;
        public static string ipInversa;
        public static string[,] SBOX0 = {
                                            {"01","00","11","10"},
                                            {"11","10","01","00" },
                                            {"00","10","01","11" },
                                            {"11","01","11","10" }
        };
        public static string[,] SBOX1 = {
                                            {"00","01","10","11"},
                                            {"10","00","01","11" },
                                            {"11","00","01","00" },
                                            {"10","01","00","11" }
        };

        public static void obtenerPermutaciones()
        {
            using (var lector = new StreamReader("Permutaciones.txt", Encoding.Default))
            {
                string a = File.ReadAllText("Permutaciones.txt");
                string[] x = a.Split('.');
                p10 = x[0];
                p8 = x[1];
                p4 = x[2];
                ep = x[3];
                ip = x[4];
                ipInversa = x[5];
            }
        }
        public static string generarK1(string k)
        {

            string k1 = "";
            var claveEnBits = Convert.ToString(Convert.ToInt32(k), 2);
            
            if (claveEnBits.Length < 10)
            {
                claveEnBits = claveEnBits.PadLeft(10, '0');
            }
            string kP10 = "";
            for (int i = 0; i < 10; i++)
            {
                var index = Convert.ToInt32(p10[i].ToString());
                kP10 += claveEnBits[index];

            }
            string ls1 = LeftShift(kP10);
            for (int j = 0; j < 8; j++)
            {
                var index = Convert.ToInt32(p8[j].ToString());
                k1 += ls1[index];
            }
            
            return k1;
        }
        public static string GenerarK2(string k)
        {
            string k2 = "";
            var claveEnBits = Convert.ToString(Convert.ToInt32(k), 2);

            if (claveEnBits.Length < 10)
            {
                claveEnBits = claveEnBits.PadLeft(10, '0');
            }
            string kP10 = "";
            for (int i = 0; i < 10; i++)
            {
                var index = Convert.ToInt32(p10[i].ToString());
                kP10 += claveEnBits[index];

            }
            string ls1 = LeftShift(kP10);
            string primeravez = LeftShift(ls1);
            string ls2 = LeftShift(primeravez);
            for (int j = 0; j < 8; j++)
            {
                var index = Convert.ToInt32(p8[j].ToString());
                k2 += ls2[index];
            }
            return k2;
        }
        public static string LeftShift(string k)
        {
            string parte1, parte2;
            parte1 = k.Substring(0, 5);
            parte2 = k.Substring(5, 5);
            string parte1LS;
            string parte2LS;
            parte1LS = parte1[1].ToString() + parte1[2].ToString() + parte1[3].ToString() + parte1[4].ToString() + parte1[0].ToString();
            parte2LS = parte2[1].ToString() + parte2[2].ToString() + parte2[3].ToString() + parte2[4].ToString() + parte2[0].ToString();
            return parte1LS + parte2LS;
        }
        public static string XOR(string i, string b, int largoDeString)
        {
            int Xor1, Xor2, codificarXor;
            var codificarXor1 = string.Empty;
            Xor1 = Convert.ToInt32(i, 2);
            Xor2 = Convert.ToInt32(b, 2);
            codificarXor = Xor1 ^ Xor2;
            codificarXor1 = Convert.ToString(codificarXor, 2);
            if (codificarXor1.Length < largoDeString)
            {
                codificarXor1 = codificarXor1.PadLeft(largoDeString, '0');
            }
            return codificarXor1;
        }
        public static string Swap(string a)
        {
            var intercambio = a[4].ToString() + a[5].ToString() + a[6].ToString() + a[7].ToString() + a[0].ToString() + a[1].ToString() + a[2].ToString() + a[3].ToString();
            return intercambio;
        }
        public void CodificarSDES(HttpPostedFileBase postedFile, string ruta, string clave)
        {
            int bufferLength = 8;
            using (var stream = new FileStream(postedFile.FileName, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream))
                {
                    using (var writeStream = new FileStream(ruta + "\\Codificado.cif", FileMode.OpenOrCreate))
                    {
                        using (var writer = new BinaryWriter(writeStream))
                        {
                            var byteBuffer = new byte[bufferLength];

                            while (reader.BaseStream.Position != reader.BaseStream.Length)
                            {
                                var codificarFinal = new byte[bufferLength];
                                var contadorPosiconByte = 0;
                                byteBuffer = reader.ReadBytes(bufferLength);
                                foreach (var a in byteBuffer)
                                {
                                    string k1 = generarK1(clave);
                                    string k2 = GenerarK2(clave);
                                    string codificarIP = "";
                                    string codificar = Convert.ToString(a, 2);
                                    if (codificar.Length < 8)
                                    {
                                        codificar = codificar.PadLeft(8, '0');
                                    }
                                    for (int j = 0; j < 8; j++)
                                    {
                                        var index = Convert.ToInt32(ip[j].ToString());

                                        codificarIP += codificar[index];
                                    }
                                    string hacerEP = codificarIP.Substring(4, 4);
                                    string codificarEP = "";
                                    for (int i = 0; i < 8; i++)
                                    {
                                        var index = Convert.ToInt32(ep[i].ToString());

                                        codificarEP += hacerEP[index];
                                    }
                                    var codificarXor1 = string.Empty;

                                    codificarXor1 = XOR(codificarEP, k1, 8);


                                    var S0 = string.Empty;
                                    var S1 = string.Empty;
                                    S0 = codificarXor1.Substring(0, 4);
                                    S1 = codificarXor1.Substring(4, 4);
                                    string f0 = "", c0 = "", f1 = "", c1 = "";

                                    f0 = S0[0].ToString() + S0[3].ToString();
                                    c0 = S0[1].ToString() + S0[2].ToString();
                                    f1 = S1[0].ToString() + S1[3].ToString();
                                    c1 = S1[1].ToString() + S1[2].ToString();

                                    int F0 = 0, C0 = 0, F1 = 0, C1 = 0;
                                    F0 = Convert.ToInt32(f0, 2);
                                    C0 = Convert.ToInt32(c0, 2);
                                    F1 = Convert.ToInt32(f1, 2);
                                    C1 = Convert.ToInt32(c1, 2);

                                    var codificarSBox = string.Empty;
                                    codificarSBox = SBOX0[F0, C0] + SBOX1[F1, C1];
                                    var codificarP4 = string.Empty;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        var index = Convert.ToInt32(p4[i].ToString());

                                        codificarP4 += codificarSBox[index];
                                    }
                                    var codificarXorConResto = string.Empty;
                                    var primerapartedeIP = codificarIP.Substring(0, 4);
                                    var segundaParteIP = codificarIP.Substring(4, 4);
                                    codificarXorConResto = XOR(codificarP4, primerapartedeIP, 4);
                                    var union = codificarXorConResto + segundaParteIP;
                                    var codificarSwap = string.Empty;
                                    codificarSwap = Swap(union);
                                    var codificarSwapSegundaParte = codificarSwap.Substring(4, 4);
                                    var codificarSwapEP = string.Empty;
                                    for (int i = 0; i < 8; i++)
                                    {
                                        var index = Convert.ToInt32(ep[i].ToString());

                                        codificarSwapEP += codificarSwapSegundaParte[index];
                                    }
                                    var codificarSwapEPXor = string.Empty;
                                    codificarSwapEPXor = XOR(codificarSwapEP, k2, 8);
                                    var S02 = string.Empty;
                                    var S12 = string.Empty;
                                    S02 = codificarSwapEPXor.Substring(0, 4);
                                    S12 = codificarSwapEPXor.Substring(4, 4);
                                    string f02 = "", c02 = "", f12 = "", c12 = "";

                                    f02 = S02[0].ToString() + S02[3].ToString();
                                    c02 = S02[1].ToString() + S02[2].ToString();
                                    f12 = S12[0].ToString() + S12[3].ToString();
                                    c12 = S12[1].ToString() + S12[2].ToString();

                                    int F02 = 0, C02 = 0, F12 = 0, C12 = 0;
                                    F02 = Convert.ToInt32(f02, 2);
                                    C02 = Convert.ToInt32(c02, 2);
                                    F12 = Convert.ToInt32(f12, 2);
                                    C12 = Convert.ToInt32(c12, 2);

                                    var codificarSBox2 = SBOX0[F02, C02] + SBOX1[F12, C12];
                                    var codificarSBox2P4 = string.Empty;
                                    var codificarSwapPrimeraParte = codificarSwap.Substring(0, 4);
                                    for (int i = 0; i < 4; i++)
                                    {
                                        var index = Convert.ToInt32(p4[i].ToString());

                                        codificarSBox2P4 += codificarSBox2[index];
                                    }
                                    var codificarXorRestoSwap = string.Empty;
                                    codificarXorRestoSwap = XOR(codificarSBox2P4, codificarSwapPrimeraParte, 4);
                                    var union2 = codificarXorRestoSwap + codificarSwapSegundaParte;
                                    var codificarIpInversa = string.Empty;
                                    for (int i = 0; i < 8; i++)
                                    {
                                        var index = Convert.ToInt32(ipInversa[i].ToString());

                                        codificarIpInversa += union2[index];
                                    }

                                    var convertirInt = 0;
                                    convertirInt = Convert.ToInt32(codificarIpInversa, 2);
                                    codificarFinal[contadorPosiconByte] = Convert.ToByte(convertirInt);
                                    contadorPosiconByte++;


                                }
                                writer.Write(codificarFinal);
                            }
                        }
                    }
                }
            }
        }
    }
}