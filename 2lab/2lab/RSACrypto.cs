using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;

namespace _2lab
{
    public class RSACrypto
    {
        public int p;
        public int q;
        public BigInteger n;
        public BigInteger phi;
        public BigInteger e;
        public BigInteger d;
        public RSACrypto()
        {
            GenerateKeys();
        }

        private void GenerateKeys()
        {
            // Генерация случайных простых чисел p и q
            int num = 0;
            p = GeneratePrimeNumber(12, num);
            q = GeneratePrimeNumber(12, p);

            n = p * q;
            phi = (p - 1) * (q - 1);

            //Выбираeм такое е, что е < phi(n) и взаимно простое с phi.
            
            do
            {
                e = GeneratePrimeE(12, p, q);
            } while (e <= n && GCD(e, phi) != 1);
            d = ModInverse(e, phi); // закрытая экспонента
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
        static int GeneratePrimeE(int N, int num1, int num2)
        {
            Random rnd = new Random();
            int n;
            do
            {
                n = rnd.Next((int)Math.Pow(2, N), (int)Math.Pow(2, N + 1));
            } while (!IsPrime(n) || n == num1 || n == num2);
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
        //НОД между e и phi: (p-1)(q-1)
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

        // Возведение числа в степень по модулю
        static BigInteger ModPow(BigInteger value, BigInteger exponent, BigInteger modulus)
        {
            return BigInteger.ModPow(value, exponent, modulus);
        }

        // Шифрование текста
        public BigInteger Encrypt(string message, BigInteger e, BigInteger n)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            BigInteger m = new BigInteger(bytes);
            return ModPow(m, e, n);
        }

        // Расшифрование текста
        public string Decrypt(BigInteger encryptedMessage, BigInteger d, BigInteger n)
        {
            BigInteger decrypted = ModPow(encryptedMessage, d, n);
            byte[] bytes = decrypted.ToByteArray();
            return Encoding.UTF8.GetString(bytes);
        }

        // Нахождение обратного элемента
        static BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            BigInteger m0 = m;
            BigInteger y = 0, x = 1;

            if (m == 1)
                return 0;

            while (a > 1)
            {
                // q - частное
                BigInteger q = a / m;

                BigInteger t = m;

                // m - остаток
                m = a % m;

                a = t;
                t = y;

                y = x - q * y;
                x = t;
            }

            // x - это инверсия a и m
            if (x < 0)
                x += m0;

            return x;
        }
    }
}
