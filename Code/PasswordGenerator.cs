using static System.Convert;
using System.Security.Cryptography;
public class PasswordGenerator
{
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("Hex or Base64? H /// B\n---------------------------------------------------------------");
            string? a = Console.ReadLine();
            bool mode = false;
            switch (a.ToLower())
            {
                case "h":
                    mode = true;
                    break;
                case "b":
                    mode = false;
                    break;
                default:
                    Console.WriteLine("---------------------------------------------------------------\nInvalid Mode\n---------------------------------------------------------------");
                    Main();
                    break;
            }
            if (mode)
            {
                Console.WriteLine("---------------------------------------------------------------\nHow many digits of hex do you want?(Recommended 62 digits for password)\n---------------------------------------------------------------");
                string length = Console.ReadLine();
                try
                {
                    if (int.Parse(length) % 2 == 1)
                    {
                        Console.WriteLine("---------------------------------------------------------------\nPlease enter an even number\n---------------------------------------------------------------");
                    }
                    else
                    {
                        byte[] b = new byte[int.Parse(length) / 2];
                        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                        {
                            rng.GetBytes(b);
                            Console.WriteLine("---------------------------------------------------------------\n" + ToHexString(b) + "\n---------------------------------------------------------------\n" + (int.Parse(length) / 2) + " Byte\n---------------------------------------------------------------");
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid Input");
                }
            }
            if (!mode)
            {
                Console.WriteLine("---------------------------------------------------------------\nHow many digits of base64 do you want?(Recommended 36 digits for password)\n---------------------------------------------------------------");
                string length = Console.ReadLine();
                try
                {
                    if (int.Parse(length) % 4 != 0)
                    {
                        Console.WriteLine("---------------------------------------------------------------\nPlease enter 4 multiples number\n---------------------------------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("---------------------------------------------------------------\nHow many of byte do you want? 0 OR 1 OR 2\n---------------------------------------------------------------");
                        string bytev = Console.ReadLine();
                        if (int.Parse(bytev) == 0)
                        {
                            byte[] b = new byte[1 + (int.Parse(length) / 4 - 1) * 3];
                            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                            {
                                rng.GetBytes(b);
                                Console.WriteLine("---------------------------------------------------------------\n" + ToBase64String(b) + "\n---------------------------------------------------------------\n" + (1 + (int.Parse(length) / 4 - 1) * 3) + " Byte\n---------------------------------------------------------------");
                            }
                        }
                        else if (int.Parse(bytev) == 1)
                        {
                            byte[] b = new byte[2 + (int.Parse(length) / 4 - 1) * 3];
                            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                            {
                                rng.GetBytes(b);
                                Console.WriteLine("---------------------------------------------------------------\n" + ToBase64String(b) + "\n---------------------------------------------------------------\n" + (2 + (int.Parse(length) / 4 - 1) * 3) + " Byte\n---------------------------------------------------------------");
                            }
                        }
                        else if (int.Parse(bytev) == 2)
                        {
                            byte[] b = new byte[(int.Parse(length) / 4) * 3];
                            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                            {
                                rng.GetBytes(b);
                                Console.WriteLine("---------------------------------------------------------------\n" + ToBase64String(b) + "\n---------------------------------------------------------------\n" + ((int.Parse(length) / 4) * 3) + " Byte\n---------------------------------------------------------------");
                            }
                        }
                        else
                        {
                            Console.WriteLine("---------------------------------------------------------------\nPlease enter the number that is 0 OR 1 OR 2");
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("---------------------------------------------------------------\nInvalid Input\n---------------------------------------------------------------");
                }
            }
        }
    }
}
