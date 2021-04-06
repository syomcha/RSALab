using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace RSALab
{
    public class RSA
    {
        // Шифрование сообщения из файла
        public static void Encode(BigInteger p, BigInteger q, string inputFilePath, string outputFilePath)
        {
            var encodeTextList = GetTextListFromFile(inputFilePath);

            var n = p * q;
            var fi = (p - 1) * (q - 1);
            Console.WriteLine($"n = {n}, fi = {fi}\n");

            var eList = GetEList(fi);
            Console.WriteLine($"Выберете значение e ({NumbersListToString(eList)}):");
            BigInteger e;
            while (true)
            {
                Console.Write("e = ");
                e = BigInteger.Parse(Console.ReadLine());
                if (eList.Contains(e))
                    break;
                Console.WriteLine("Невeрное значение \"e\"");
            }
            Console.WriteLine($"Зыкрытый ключ = ({e}, {n})\n");

            var outputTextList = Encode(p, q, e, encodeTextList);

            SaveTextListToFile(outputTextList, outputFilePath);
        }

        // Шифрование сообщения
        public static List<string> Encode(BigInteger p, BigInteger q, BigInteger e, List<string> inputData)
        {
            var n = p * q;
            var fi = (p - 1) * (q - 1);

            BigInteger d = CalculateD(e, fi);
            Console.WriteLine($"d = {d}");
            Console.WriteLine($"Открытый ключ = ({d}, {n})\n");

            var outputData = new List<string>();
            foreach (var encodeText in inputData)
            {
                foreach (var letter in encodeText)
                {
                    var value = new BigInteger(letter);

                    value = BigInteger.Pow(value, (int)e) % n;

                    outputData.Add(value.ToString());
                }
            }

            return outputData;
        }

        // Деширование сообщения в файл
        public static void Decode(BigInteger d, BigInteger n, string inputFilePath, string outputFilePath)
        {
            var decodeTextList = GetTextListFromFile(inputFilePath);

            var outputData = Decode(d, n, decodeTextList);

            SaveTextListToFile(outputData, outputFilePath);
        }

        // Дешифрование сообщения
        public static List<string> Decode(BigInteger d, BigInteger n, List<string> inputData)
        {
            var outputList = new List<string>();
            var outputText = new StringBuilder();

            foreach (var text in inputData)
            {
                var value = BigInteger.Parse(text);

                value = BigInteger.Pow(value, (int)d) % n;

                outputText.Append(Convert.ToChar((int)value));
            }
            outputList.Add(outputText.ToString());

            return outputList;
        }

        // Получение данных из файла в виде списка трок
        private static List<string> GetTextListFromFile(string filePath)
        {
            var resultList = new List<string>();
            using var sr = new StreamReader(filePath, Encoding.UTF8);

            string line;
            while ((line = sr.ReadLine()) != null)
                resultList.Add(line);

            return resultList;
        }

        // Сохранение списка строк в файл
        private static void SaveTextListToFile(List<string> textList, string filePath)
        {
            using var sw = new StreamWriter(filePath, false, Encoding.UTF8);

            foreach (var text in textList)
                sw.WriteLine(text);

            Console.WriteLine("Текст записан в файл\n");
        }

        // Получение параметра е
        private static List<BigInteger> GetEList(BigInteger fi)
        {
            var eList = new List<BigInteger>();
            var count = 0;

            for (var number = fi - 1; number > 0; number--)
            {
                if (count == 200)
                    return eList;

                if (BigIntegerUtils.IsPrime(number) &&
                    BigIntegerUtils.IsRelativelyPrime(number, fi))
                {
                    eList.Add(number);
                    count++;
                }
            }

            if (eList.Count > 0)
                return eList;
            else
                throw new Exception("e - не найдено");
        }

        // Подсчет параметра d
        private static BigInteger CalculateD(BigInteger e, BigInteger n)
        {
            var d = BigIntegerUtils.ModInverse(e, n);
            return e * d % n == 1 ? d : -1;
        }

        // Преобразование списка чисел в строку
        private static string NumbersListToString(List<BigInteger> list)
        {
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                    stringBuilder.Append($"{list[i]}");
                else
                    stringBuilder.Append($"{list[i]}, ");
            }

            return stringBuilder.ToString();
        }
    }
}
