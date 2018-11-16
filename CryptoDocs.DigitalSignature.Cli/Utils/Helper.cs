using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Utils;

namespace Utils
{
    public static class Helper
    {
        private static ECPoint Q = new ECPoint();
        private static BigInteger d = new BigInteger();
        private static BigInteger Key = GeKey();
        
        public static BigInteger GeKey()
        {
            BigInteger p = new BigInteger("6277101735386680763835789423207666416083908700390324961279", 10);
            BigInteger a = new BigInteger("-3", 10);
            BigInteger b = new BigInteger("64210519e59c80e70fa7e9ab72243049feb8deecc146b9b1", 16);
            byte[] xG = FromHexStringToByte("03188da80eb03090f67cbf20eb43a18800f4ff0afd82ff1012");
            BigInteger n = new BigInteger("ffffffffffffffffffffffff99def836146bc9b1b4d22831", 16);
            DSGost DS = new DSGost(p, a, b, n, xG);
            
            return DS.GenPrivateKey(192);
        }

        public static string Sign(string text, BigInteger key)
        {
            BigInteger p = new BigInteger("6277101735386680763835789423207666416083908700390324961279", 10);
            BigInteger a = new BigInteger("-3", 10);
            BigInteger b = new BigInteger("64210519e59c80e70fa7e9ab72243049feb8deecc146b9b1", 16);
            byte[] xG = FromHexStringToByte("03188da80eb03090f67cbf20eb43a18800f4ff0afd82ff1012");
            BigInteger n = new BigInteger("ffffffffffffffffffffffff99def836146bc9b1b4d22831", 16);           
            DSGost DS = new DSGost(p, a, b, n, xG);
            d = key;//DS.GenPrivateKey(192);
            Q = DS.GenPublicKey(d);            
            GOST hash = new GOST(256);
            byte[] H = hash.GetHash(Encoding.Default.GetBytes(text));
            string sign  = DS.SingGen(H, d);
            bool result = DS.SingVer(H, sign, Q);

            return sign;
        }

        private static  byte[] FromHexStringToByte(string input)
        {
            byte[] data = new byte[input.Length / 2];
            string HexByte = "";
            for (int i = 0; i < data.Length; i++)
            {
                HexByte = input.Substring(i * 2, 2);
                data[i] = Convert.ToByte(HexByte, 16);
            }
            return data;
        }

       public static bool Check(string text, string sign, BigInteger key)
        {
            BigInteger p = new BigInteger("6277101735386680763835789423207666416083908700390324961279", 10);
            BigInteger a = new BigInteger("-3", 10);
            BigInteger b = new BigInteger("64210519e59c80e70fa7e9ab72243049feb8deecc146b9b1", 16);
            byte[] xG = FromHexStringToByte("03188da80eb03090f67cbf20eb43a18800f4ff0afd82ff1012");
            BigInteger n = new BigInteger("ffffffffffffffffffffffff99def836146bc9b1b4d22831", 16);
            DSGost DS = new DSGost(p, a, b, n, xG);
            GOST hash = new GOST(256);
            long start = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
            byte[] H = hash.GetHash(Encoding.Default.GetBytes(text));
            bool result=DS.SingVer(H, sign, Q);
            long finish = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
            long time = finish - start;

            return result;
        }
    }
}
