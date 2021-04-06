using System;
using System.Numerics;
using System.Text;

namespace RSALab
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Write("Введите одну из команд (encode/decode/exit): ");
                var inputStr = Console.ReadLine().ToLower();

                if (inputStr == "exit")
                    break;

                if (inputStr == "encode")
                {
                    Console.WriteLine($"Введите p и q ({GetPrimesNumbersString(200)}):");
                    BigInteger p, q;
                    while (true)
                    {
                        try
                        {
                            Console.Write("p = ");
                            p = BigInteger.Parse(Console.ReadLine());
                            Console.Write("q = ");
                            q = BigInteger.Parse(Console.ReadLine());

                            if (!BigIntegerUtils.IsPrime(p) || !BigIntegerUtils.IsPrime(q))
                                throw new Exception();
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Значение p или q неверно");
                            continue;
                        }
                    }

                    RSA.Encode(p, q,
                        @"..\..\..\input.txt",
                        @"..\..\..\output.txt");
                }
                    

                if (inputStr == "decode")
                {
                    Console.WriteLine("Введите открытый ключ (d, n): ");
                    BigInteger d, n;
                    while (true)
                    {
                        try
                        {
                            Console.Write("d = ");
                            d = BigInteger.Parse(Console.ReadLine());
                            Console.Write("n = ");
                            n = BigInteger.Parse(Console.ReadLine());
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Значение d или n неверно");
                            continue;
                        }
                    }

                    RSA.Decode(d, n,
                        @"..\..\..\input.txt",
                        @"..\..\..\output.txt");
                }
            }
        }

        // Получение строки с простыми числами
        private static string GetPrimesNumbersString(int limit)
        {
            var stringBuilder = new StringBuilder();
            var count = 0;

            for (var number = 2; count <= limit; number++)
            {
                if (BigIntegerUtils.IsPrime(number))
                {
                    if (count == limit)
                        stringBuilder.Append(number);
                    else
                        stringBuilder.Append($"{number}, ");

                    count++;
                }
            }

            return stringBuilder.ToString();
        }
    }
}
