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
        //public static string k1;
        //public static string k2;
        
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
            //var k1 = string.Empty;
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
            //var k2 = string.Empty;
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
        
    }
}