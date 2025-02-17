using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class AESCipher
{
    // Default Key and IV (hex format converted to byte arrays)
    private static readonly byte[] globalKey = HexStringToByteArray("CF6D45D7803C7DF3A3F12AE5E1793733");
    private static readonly byte[] globalIV = HexStringToByteArray("F59FB77F2890F521AFA4C1376E02875D");

    private static readonly byte[] globalKey2 = HexStringToByteArray("C2959D0527C2E42DC65B19C3FF5DC985");
    private static readonly byte[] globalIV2 = HexStringToByteArray("DB5E88BCA58683389A04641A60E693CA");

    // Convert a byte array to hexadecimal string
    private static string ByteArrayToHex(byte[] bytes)
    {
        StringBuilder hex = new StringBuilder(bytes.Length * 2);
        foreach (byte b in bytes)
        {
            hex.AppendFormat("{0:X2}", b);
        }
        return hex.ToString();
    }

    // Convert a hexadecimal string to a byte array
    private static byte[] HexStringToByteArray(string hex)
    {
        int length = hex.Length;
        byte[] bytes = new byte[length / 2];
        for (int i = 0; i < length; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }
        return bytes;
    }

    // Encryption method using the default key and IV
    public static string Encrypt(string plainText, int number = 0)
    {
        using (Aes aesAlg = Aes.Create())
        {
            if (number == 0)
            {
                aesAlg.Key = globalKey;
                aesAlg.IV = globalIV;
            }
            else
            {
                aesAlg.Key = globalKey2;
                aesAlg.IV = globalIV2;
            }
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    // Decryption method using the default key and IV
    public static string Decrypt(string cipherText, int number = 0)
    {
        using (Aes aesAlg = Aes.Create())
        {
            if (number == 0)
            {
                aesAlg.Key = globalKey;
                aesAlg.IV = globalIV;
            }
            else
            {
                aesAlg.Key = globalKey2;
                aesAlg.IV = globalIV2;
            }

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }


}
