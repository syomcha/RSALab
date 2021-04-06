using System;
using System.Numerics;

namespace RSALab
{
    public class BigIntegerUtils
    {
        // Проверка на простое число
        public static bool IsPrime(BigInteger x)
        {
            if (x == 1) return false;
            if (x == 2) return true;

            for (var i = 2; i < x; ++i)
                if (x % i == 0)
                    return false;

            return true;
        }

        // НОД двух чисел с помощтю алгоритма Евклида
        public static BigInteger GCD(BigInteger a, BigInteger b)
        {
            BigInteger x;
            while (b != 0)
            {
                x = a % b;
                a = b;
                b = x;
            }

            return a;
        }

        // Расширенный алгоритм Евклида
        public static BigInteger EGCD(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }

            var gcd = EGCD(b % a, a, out BigInteger x1, out BigInteger y1);
            x = y1 - (b / a) * x1;
            y = x1;

            return gcd;
        }

        // Функция для определения являются ли два числа взаимно простыми
        public static bool IsRelativelyPrime(BigInteger a, BigInteger b)
        {
            return GCD(a, b) == 1;
        }

        // Подсчёт обратного модуля
        public static BigInteger ModInverse(BigInteger value, BigInteger module)
        {
            if (EGCD(value, module, out BigInteger x, out BigInteger y) != 1)
                throw new Exception($"Неправильный модуль для ModInverse(value: {value}, module: {module})");

            if (x < 0)
                x += module;

            return x % module;
        }
    }
}
