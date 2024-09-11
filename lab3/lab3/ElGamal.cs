using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;

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
        public BigInteger M;
        public string encryptText;
        public string decryptText;
        public ElGamal()
        {
            GenerateKeys();
        }
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
            //k = GenerateK(20, p - 1);
            do
            {
                k = GenerateK(12, p);
            } while (GCD(k, p-1) != 1);
            // Вычисляет a и b
            a = BigInteger.ModPow(g, k, p);
            BigInteger temp = Pow(y, k) * M;
            b = BigInteger.ModPow(temp, k, p);
            //шифровка
            encryptText = a.ToString() + b.ToString();
            //дешифровка
            decryptText = ((b / BigInteger.ModPow(a, p - 1 - x, p)) % p).ToString();
        }
        //возведение в степень BigInteger
        public BigInteger Pow(BigInteger value, BigInteger exponent)
        {
            BigInteger originalValue = value;
            while (exponent-- > 1)
                value = BigInteger.Multiply(value, originalValue);
            return value;
        }
        private static BigInteger ModPow(BigInteger baseNumber, BigInteger exponent, BigInteger modulus)
        {
            // Быстрое возведение в степень по модулю
            BigInteger result = 1;
            baseNumber %= modulus;
            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                {
                    result = (result ^ baseNumber) % modulus;
                }
                exponent >>= 1;
                baseNumber = (baseNumber ^ baseNumber) % modulus;
            }
            return result;
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
        static BigInteger GenerateK(int N, BigInteger p1)
        {
            Random rnd = new Random();
            BigInteger n;
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
        //НОД между k и n: (p-1)(q-1)
        private static BigInteger GCD(BigInteger a, BigInteger b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        public string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
        //
        public string Encrypt()
        {
            // Генерация цифровой подписи
            // Вычисляет a и b
            a = BigInteger.ModPow(g, k, p);
            BigInteger temp = Pow(y, k) * M;
            b = BigInteger.ModPow(temp, k, p);
            //шифровка
            encryptText = a.ToString() + b.ToString();
            return encryptText;
        }
        // Дешифрует сообщение
        public string Decrypt()
        {
            BigInteger m = (b / BigInteger.ModPow(a, x, p));
            // Преобразует BigInteger в байты
            byte[] messageBytes = m.ToByteArray();
            decryptText = messageBytes.ToString();
            // Преобразует байты в строку
            return Encoding.UTF8.GetString(messageBytes);
        }
    }
}
