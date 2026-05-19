using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Przychodnia
{
    public static class Szyfrator
    {
        private static readonly string Klucz = "A1b2C3d4E5f6G7h8I9j0K1l2M3n4O5p6";

        public static string Szyfruj(string jawnyTekst)
        {
            if (string.IsNullOrEmpty(jawnyTekst)) return jawnyTekst;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Klucz);
                aesAlg.IV = new byte[16];

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(jawnyTekst);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Odszyfruj(string zaszyfrowanyTekst)
        {
            if (string.IsNullOrEmpty(zaszyfrowanyTekst)) return zaszyfrowanyTekst;

            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Encoding.UTF8.GetBytes(Klucz);
                    aesAlg.IV = new byte[16];

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(zaszyfrowanyTekst)))
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
            catch
            {
                return zaszyfrowanyTekst;
            }
        }
    }
}