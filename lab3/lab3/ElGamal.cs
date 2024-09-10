using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace lab3
{
    internal class ElGamal
    {
        public int p;
        public int g;
        public int x;
        public BigInteger y;
        public BigInteger k;
        public BigInteger a;
        public BigInteger b;
        private void GenerateKeys()
        {
            int num = 0;
            //x - секретный ключ
            x = GeneratePrimeNumber(12, num);
            g = GeneratePrimeNumber(12, x);
            p = GeneratePrimeNumber(20, g);
            // Вычисляет открытый ключ y
            y = BigInteger.ModPow(g, x, p);
            // Выбирает случайное число k
            k = GenerateK(20, p - 1);

            // Вычисляет a и b
            a = BigInteger.ModPow(g, k, p);
            b = BigInteger.ModPow(y, k * m, p);
        }
        // Генерация случайного простого числа заданной разрядности
        static int GeneratePrimeNumber(int N, int num)
        {
            Random rnd = new Random();
            int n;
            do
            {
                n = rnd.Next((int)Math.Pow(2, N), (int)Math.Pow(2, N + 1));
            } while (!IsPrime(n) || n == num);
            return n;
        }
        static int GenerateK(int N, BigInteger p1)
        {
            Random rnd = new Random();
            int n;
            do
            {
                n = rnd.Next((int)Math.Pow(2, N), (int)Math.Pow(2, N + 1));
            } while (!IsPrime(n) || (n > p1));
            return n;
        }

        // Проверка числа на простоту
        static bool IsPrime(BigInteger n)
        {
            if (n <= 1)
            {
                return false;
            }
            if (n == 2)
            {
                return true;
            }
            if (n % 2 == 0)
            {
                return false;
            }

            for (BigInteger i = 3; i * i <= n; i += 2)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        // Вычисляет модульное обратное число
        private static BigInteger ModInverse(BigInteger a, BigInteger p)
        {
            return BigInteger.ModPow(a, p - 2, p);
        }

        // Шифрует сообщение
        public static (BigInteger, BigInteger) Encrypt(string message, BigInteger p, BigInteger g, BigInteger y)
        {
            // Преобразует сообщение в байты
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);

            // Преобразует байты в BigInteger
            BigInteger m = new BigInteger(messageBytes);

            return (m);
        }

        // Дешифрует сообщение
        public static string Decrypt(BigInteger c1, BigInteger c2, BigInteger p, BigInteger x)
        {
            // Вычисляет m
            BigInteger m = (c2  BigInteger.ModPow(c1, p - 1 - x, p)) % p;

            // Преобразует BigInteger в байты
            byte[] messageBytes = m.ToByteArray();

            // Преобразует байты в строку
            return System.Text.Encoding.UTF8.GetString(messageBytes);
        }
    }
}
