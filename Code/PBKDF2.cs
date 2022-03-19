using System.Text;
using System.Security.Cryptography;
using static System.Convert;
using System;

public sealed class Class1
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
                Console.WriteLine("Enter some password" + "\n------------------------------------------------------------");
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
                    Console.WriteLine("Enter Plain Text" + "\n------------------------------------------------------------");
                    string? dataToEncrypt = Console.ReadLine();
                    Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(password, salt, 100000);
                    Console.WriteLine("\nSecret Key Base64 : " + ToBase64String(hash.GetBytes(32)) + "\n------------------------------------------------------------");
                    Aes aes = Aes.Create();
                    aes.Key = hash.GetBytes(32);
                    MemoryStream encryptionStream = new MemoryStream();
                    CryptoStream encrypt = new CryptoStream(encryptionStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    encrypt.Write(Encoding.UTF8.GetBytes(dataToEncrypt), 0, Encoding.UTF8.GetBytes(dataToEncrypt).Length);
                    encrypt.FlushFinalBlock();
                    encrypt.Close();
                    hash.Reset();
                    Console.WriteLine("IV Base64 : " + ToBase64String(aes.IV) + "\n------------------------------------------------------------");
                    Console.WriteLine("Salt Base64 : " + ToBase64String(salt) + "\n------------------------------------------------------------");
                    Console.WriteLine("Cipher Base64  : " + ToBase64String(encryptionStream.ToArray()) + "\n------------------------------------------------------------" + "\nPlain Text : " + dataToEncrypt);
                }
                Console.WriteLine("------------------------------------------------------------");
            }
            if (!modeb)
            {
                Console.WriteLine("Enter some password\n------------------------------------------------------------");
                string? password = Console.ReadLine();
                if (password == "")
                {
                    Console.WriteLine("Password could not be empty");
                }
                else
                {
                    Console.WriteLine("Enter some salt\n------------------------------------------------------------");
                    string? salt = Console.ReadLine();
                    if (salt.Length != 44)
                    {
                        Console.WriteLine("Invalid salt");
                    }
                    else
                    {
                        Console.WriteLine("Enter some IV\n------------------------------------------------------------");
                        string? IV = Console.ReadLine();
                        if (IV.Length != 24)
                        {
                            Console.WriteLine("Invalid IV");
                        }
                        else
                        {
                            Console.WriteLine("Enter Some Cipher Text\n------------------------------------------------------------");
                            string? data = Console.ReadLine();
                                try
                                {
                                    Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(password, FromBase64String(salt), 100000);
                                    Console.WriteLine("Secret Key Base64: " + ToBase64String(hash.GetBytes(32)) + "\n------------------------------------------------------------");
                                    Aes decryptAES = Aes.Create();
                                    decryptAES.Key = hash.GetBytes(32);
                                    decryptAES.IV = FromBase64String(IV);
                                    MemoryStream decryptionStreamBacking = new MemoryStream();
                                    CryptoStream decrypt = new CryptoStream(decryptionStreamBacking, decryptAES.CreateDecryptor(), CryptoStreamMode.Write);
                                    decrypt.Write(FromBase64String(data), 0, FromBase64String(data).Length);
                                    decrypt.Flush();
                                    decrypt.Close();
                                    hash.Reset();
                                    Console.WriteLine("\nCipher Text : " + data + "\n------------------------------------------------------------\nPlain Text : " + Encoding.UTF8.GetString(decryptionStreamBacking.ToArray()));
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
