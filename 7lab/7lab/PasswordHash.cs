using System;
using System.Security.Cryptography;
using System.Text;

namespace _7lab
{
    internal class PasswordHash
    {
        private const int BlockSize = 8; // Размер блока в байтах (64 бита) - 4 вариант
        public string HashPassword(string password)
        {
            // Преобразование пароля в байтовый массив
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);

            // Генерация случайного ключа и вектора инициализации (IV)
            byte[] key = new byte[8]; // 64-битный ключ для DES
            byte[] iv = new byte[BlockSize];
            RandomNumberGenerator.Create().GetBytes(key);
            RandomNumberGenerator.Create().GetBytes(iv);

            // Создание объекта DES
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            // Создание объекта шифрования
            ICryptoTransform encryptor = des.CreateEncryptor(key, iv);

            // Шифрование пароля
            byte[] encryptedPassword = encryptor.TransformFinalBlock(passwordBytes, 0, passwordBytes.Length);

            // Объединение ключа, IV и зашифрованного пароля в один байтовый массив
            byte[] hashedPassword = new byte[key.Length + iv.Length + encryptedPassword.Length];
            Array.Copy(key, 0, hashedPassword, 0, key.Length);
            Array.Copy(iv, 0, hashedPassword, key.Length, iv.Length);
            Array.Copy(encryptedPassword, 0, hashedPassword, key.Length + iv.Length, encryptedPassword.Length);

            // Преобразование байтового массива в строку Base64
            return Convert.ToBase64String(hashedPassword);
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            // Разделение хешированного пароля на ключ, IV и зашифрованный пароль
            byte[] hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            byte[] key = new byte[8]; // 64-битный ключ для DES
            Array.Copy(hashedPasswordBytes, 0, key, 0, 8);
            byte[] iv = new byte[BlockSize];
            Array.Copy(hashedPasswordBytes, 8, iv, 0, BlockSize);
            byte[] encryptedPassword = new byte[hashedPasswordBytes.Length - 8 - BlockSize];
            Array.Copy(hashedPasswordBytes, 8 + BlockSize, encryptedPassword, 0, encryptedPassword.Length);

            // Создание объекта DES
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            // Создание объекта дешифрования
            ICryptoTransform decryptor = des.CreateDecryptor(key, iv);

            try
            {
                // Дешифрование пароля
                byte[] decryptedPassword = decryptor.TransformFinalBlock(encryptedPassword, 0, encryptedPassword.Length);
                return Encoding.ASCII.GetString(decryptedPassword) == password;
            }
            catch (CryptographicException)
            {
                return false;
            }
        }
    }
}
