using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Security;
using System.Windows.Forms;

namespace lab8
{
    public class Gamming
    {
        private const int BlockSize = 4; // Размер блока в байтах (32 бита)
        List<byte[]> messageBlocks; // сообщение, разделенное на блоки
        List<byte[]> keyBlocks; //ключ, разделенный на блоки
        public string Encrypt(string message, string key)
        {
            // Преобразование сообщения и ключа в байтовые массивы
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            byte[] paddedKeyBytes = messageBytes;
            // Проверка, что длина ключа равна или больше длины сообщения
            if (keyBytes.Length < messageBytes.Length)
            {
                //throw new ArgumentException("Ключ должен быть не короче сообщения.");
                // Дополнение ключа нулями, если он короче сообщения
                paddedKeyBytes = new byte[messageBytes.Length];
                Array.Copy(keyBytes, paddedKeyBytes, keyBytes.Length);
            }

            // Разделение сообщения и ключа на блоки
            messageBlocks = SplitToBlocks(messageBytes);
            keyBlocks = SplitToBlocks(paddedKeyBytes);

            // Шифрование сообщения блоками
            List<byte[]> encryptedBlocks = new List<byte[]>();
            for (int i = 0; i < messageBlocks.Count; i++)
            {
                encryptedBlocks.Add(MultiplyBlocksModulo2N(messageBlocks[i], keyBlocks[i], messageBlocks.Count));
            }

            // Объединение зашифрованных блоков в единый массив
            byte[] encryptedBytes = encryptedBlocks.SelectMany(b => b).ToArray();

            // Преобразование зашифрованного сообщения в строку Base64
            return Convert.ToBase64String(encryptedBytes);
        }

        public string Decrypt(string encryptedMessage, string key)
        {
            // Преобразование зашифрованного сообщения из Base64 в байтовый массив
            byte[] encryptedBytes = Convert.FromBase64String(encryptedMessage);
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);

            // Проверка, что длина ключа равна или больше длины сообщения
            if (keyBytes.Length < encryptedBytes.Length)
            {
                throw new ArgumentException("Ключ должен быть не короче сообщения.");
            }

            // Дополнение ключа нулями, если он короче сообщения
            byte[] paddedKeyBytes = new byte[encryptedBytes.Length];
            Array.Copy(keyBytes, paddedKeyBytes, keyBytes.Length);

            // Разделение зашифрованного сообщения и ключа на блоки
            List<byte[]> encryptedBlocks = SplitToBlocks(encryptedBytes);
            List<byte[]> keyBlocks = SplitToBlocks(paddedKeyBytes);

            // Дешифрование сообщения блоками
            List<byte[]> decryptedBlocks = new List<byte[]>();
            for (int i = 0; i < encryptedBlocks.Count; i++)
            {
                decryptedBlocks.Add(MultiplyBlocksModulo2N(encryptedBlocks[i], keyBlocks[i], messageBlocks.Count));
            }

            // Объединение расшифрованных блоков в единый массив
            byte[] decryptedBytes = decryptedBlocks.SelectMany(b => b).ToArray();

            // Преобразование расшифрованного сообщения в строку
            return Encoding.ASCII.GetString(decryptedBytes);
        }

        private List<byte[]> SplitToBlocks(byte[] bytes)
        {
            List<byte[]> blocks = new List<byte[]>();
            for (int i = 0; i < bytes.Length; i += BlockSize)
            {
                blocks.Add(bytes.Skip(i).Take(BlockSize).ToArray());
            }
            return blocks;
        }

        private byte[] MultiplyBlocksModulo2N(byte[] block1, byte[] block2, int N)
        {
            byte[] result = new byte[block1.Length];
            for (int i = 0; i < block1.Length; i++)
            {
                // Умножение по модулю 2^N
                result[i] = (byte)((block1[i] * block2[i]) % (1 << N));
            }
            return result;
        }
    }
}
