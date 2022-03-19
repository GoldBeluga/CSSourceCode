using System.Text;
using System.Security.Cryptography;
using static System.Convert;
using System;

public sealed class PBKDF2
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Encrypt Or Dencrypt? E /// D");
            string? mode = Console.ReadLine();
            bool modeb = false;
            switch (mode.ToLower())
            {
                case "e":
                    modeb = true;
                    break;
                case "d":
                    modeb = false;
                    break;
                default:
                    Console.WriteLine("Invalid mode");
                    Main();
                    break;
            }
            if (modeb)
            {
                Console.WriteLine("Enter some password");
                string? password = Console.ReadLine();
                if (password == "")
                {
                    Console.WriteLine("Password could not be empty");
                }
                else
                {
                    byte[] salt = new byte[32];
                    using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                    {
                        rng.GetBytes(salt);
                    }
                    Console.WriteLine("Enter some data");
                    string? dataToEncrypt = Console.ReadLine();
                    Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(password, salt, 100000);
                    Console.WriteLine("Secret Key Hex : " + ToHexString(hash.GetBytes(32)));
                    Aes aes = Aes.Create();
                    aes.Key = hash.GetBytes(32);
                    MemoryStream encryptionStream = new MemoryStream();
                    CryptoStream encrypt = new CryptoStream(encryptionStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    encrypt.Write(Encoding.UTF8.GetBytes(dataToEncrypt), 0, Encoding.UTF8.GetBytes(dataToEncrypt).Length);
                    encrypt.FlushFinalBlock();
                    encrypt.Close();
                    hash.Reset();
                    Console.WriteLine("IV Hex: " + ToHexString(aes.IV));
                    Console.WriteLine("Salt Hex : " + ToHexString(salt));
                    Console.WriteLine("Data before encrypt : " + dataToEncrypt + "\nData after encrypt : " + ToHexString(encryptionStream.ToArray()));
                }
                Console.WriteLine("------------------------------------------------------------");
            }
            if (!modeb)
            {
                Console.WriteLine("Enter some password");
                string? password = Console.ReadLine();
                if (password == "")
                {
                    Console.WriteLine("Password could not be empty");
                }
                else
                {
                    Console.WriteLine("Enter some salt");
                    string? salt = Console.ReadLine();
                    if (salt.Length != 64)
                    {
                        Console.WriteLine("Invalid salt");
                    }
                    else
                    {
                        Console.WriteLine("Enter some IV");
                        string? IV = Console.ReadLine();
                        if (IV.Length != 32)
                        {
                            Console.WriteLine("Invalid IV");
                        }
                        else
                        {
                            Console.WriteLine("Enter some data");
                            string? data = Console.ReadLine();
                                try
                                {
                                    Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(password, FromHexString(salt), 100000);
                                    Console.WriteLine("Secret Key Hex: " + ToHexString(hash.GetBytes(32)));
                                    Aes decryptAES = Aes.Create();
                                    decryptAES.Key = hash.GetBytes(32);
                                    decryptAES.IV = FromHexString(IV);
                                    MemoryStream decryptionStreamBacking = new MemoryStream();
                                    CryptoStream decrypt = new CryptoStream(decryptionStreamBacking, decryptAES.CreateDecryptor(), CryptoStreamMode.Write);
                                    decrypt.Write(FromHexString(data), 0, FromHexString(data).Length);
                                    decrypt.Flush();
                                    decrypt.Close();
                                    hash.Reset();
                                    Console.WriteLine("Encrypt Data : " + data + "\nDecrypt Data : " + Encoding.UTF8.GetString(decryptionStreamBacking.ToArray()));
                                }
                                catch
                                {
                                    Console.WriteLine("Incorrect password, salt, IV or data");
                                }
                        }
                    }
                }
                Console.WriteLine("------------------------------------------------------------");
            }
        }
    }
}
