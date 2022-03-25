using System.Text;
using System.Security.Cryptography;
using static System.Convert;
using System;
using System.IO;

public sealed class AES2562
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
                    Console.WriteLine("------------------------------------------------------------\nInvalid mode");
                    Main();
                    break;
            }
            if (modeb)
            {
                Console.WriteLine("------------------------------------------------------------\nEnter some password\n------------------------------------------------------------");
                string? password = Console.ReadLine();
                if (password == "")
                {
                    Console.WriteLine("Password could not be empty");
                }
                else
                {
                    Console.WriteLine("------------------------------------------------------------\nEnter some salt Hex\n------------------------------------------------------------");
                    string? salt = Console.ReadLine();
                    if (salt.Length != 64)
                    {
                        Console.WriteLine("Invalid Salt");
                    }
                    else 
                    {
                        Console.WriteLine("------------------------------------------------------------\nEnter some IV Hex\n------------------------------------------------------------");
                        string? IV = Console.ReadLine();
                        if(IV.Length != 32)
                        {
                            Console.WriteLine("Invalid IV");
                        }
                        else
                        {
                            Console.WriteLine("------------------------------------------------------------\nEnter Plain Text" + "\n------------------------------------------------------------");
                            string? dataToEncrypt = Console.ReadLine();
                            Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(password, FromHexString(salt), 100000);
                            Console.WriteLine("\nSecret Key Hex : " + ToHexString(hash.GetBytes(32)) + "\n------------------------------------------------------------");
                            Aes aes = Aes.Create();
                            aes.Key = hash.GetBytes(32);
                            aes.IV = FromHexString(IV);
                            MemoryStream encryptionStream = new MemoryStream();
                            CryptoStream encrypt = new CryptoStream(encryptionStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                            encrypt.Write(Encoding.UTF8.GetBytes(dataToEncrypt), 0, Encoding.UTF8.GetBytes(dataToEncrypt).Length);
                            encrypt.FlushFinalBlock();
                            encrypt.Close();
                            hash.Reset();
                            Console.WriteLine("IV Hex : " + IV.ToUpper() + "\n------------------------------------------------------------");
                            Console.WriteLine("Salt Hex : " + salt.ToUpper() + "\n------------------------------------------------------------");
                            Console.WriteLine("Cipher Hex  : " + ToHexString(encryptionStream.ToArray()) + "\n------------------------------------------------------------" + "\nPlain Text : " + dataToEncrypt);
                        }
                    }
                   
                }
                Console.WriteLine("------------------------------------------------------------");
            }
            if (!modeb)
            {
                Console.WriteLine("------------------------------------------------------------\nEnter some password\n------------------------------------------------------------");
                string? password = Console.ReadLine();
                if (password == "")
                {
                    Console.WriteLine("Password could not be empty");
                }
                else
                {
                    Console.WriteLine("------------------------------------------------------------\nEnter some salt\n------------------------------------------------------------");
                    string? salt = Console.ReadLine();

                    if (salt.Length != 64)
                    {
                        Console.WriteLine("Invalid salt");
                    }
                    else
                    {
                        Console.WriteLine("------------------------------------------------------------\nEnter some IV\n------------------------------------------------------------");
                        string? IV = Console.ReadLine();
                        if (IV.Length != 32)
                        {
                            Console.WriteLine("Invalid IV");
                        }
                        else
                        {
                            Console.WriteLine("------------------------------------------------------------\nEnter Some Cipher Text\n------------------------------------------------------------");
                            string? data = Console.ReadLine();
                            try
                            {
                                Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(password, FromHexString(salt), 100000);
                                Console.WriteLine("------------------------------------------------------------\nSecret Key Hex: " + ToHexString(hash.GetBytes(32)) + "\n------------------------------------------------------------");
                                Aes decryptAES = Aes.Create();
                                decryptAES.Key = hash.GetBytes(32);
                                decryptAES.IV = FromHexString(IV);
                                MemoryStream decryptionStreamBacking = new MemoryStream();
                                CryptoStream decrypt = new CryptoStream(decryptionStreamBacking, decryptAES.CreateDecryptor(), CryptoStreamMode.Write);
                                decrypt.Write(FromHexString(data), 0, FromHexString(data).Length);
                                decrypt.Flush();
                                decrypt.Close();
                                hash.Reset();
                                Console.WriteLine("Cipher Text : " + data + "\n------------------------------------------------------------\nPlain Text : " + Encoding.UTF8.GetString(decryptionStreamBacking.ToArray()));
                            }
                            catch
                            {
                                Console.WriteLine("\n------------------------------------------------------------\nIncorrect password, salt, IV or data");
                            }
                        }
                    }
                }
                Console.WriteLine("------------------------------------------------------------");
            }
        }
    }
}
