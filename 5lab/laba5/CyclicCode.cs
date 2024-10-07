using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5
{
    internal class CyclicCode
    {
        private const int GeneratorPolynomialDegree = 6; // Степень порождающего полинома
        private const int DataBits = 8; // Количество информационных бит в блоке
        private const int CodewordBits = DataBits + GeneratorPolynomialDegree; // Количество бит в кодовом слове
        private readonly int[] _generatorPolynomial = { 1, 0, 1, 1, 0, 0, 1 }; // Порождающий полином 
        public int errorPosition;
        private readonly string[] _syndromDecode = { "010011", "100101", "111110", "011111",
                                                  "100011", "111101", "110010", "011001",
                                                  "100000", "010000", "001000", "000100",
                                                  "000010", "000001" };
        public bool nonError = true;
        public int posErr = 0;
        public string Encode(string message)
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            string encodedMessage = "";

            foreach (byte b in messageBytes)
            {
                string binaryString = Convert.ToString(b, 2).PadLeft(8, '0');
                encodedMessage += EncodeBlock(binaryString);
            }

            return encodedMessage;
        }

        public string Decode(string encodedMessage)
        {
            string decodedMessage = "";

            // Делим сообщение на блоки
            for (int i = 0; i < encodedMessage.Length; i += CodewordBits)
            {
                string block = encodedMessage.Substring(i, CodewordBits);
                string correctedBlock = CorrectError(block);
                decodedMessage += correctedBlock;
            }

            // Преобразуем двоичный код в строку
            byte[] bytes = new byte[decodedMessage.Length / 8];
            for (int i = 0; i < bytes.Length; i++)
            {
                string byteString = decodedMessage.Substring(i * 8, 8);
                bytes[i] = Convert.ToByte(byteString, 2);
            }
            return Encoding.ASCII.GetString(bytes);
        }

        private string EncodeBlock(string dataBlock)
        {
            // Добавляем к блоку нулей
            dataBlock += new string('0', GeneratorPolynomialDegree);
            // Выполняем деление по модулю 2 с остатком
            string remainder = Mod2Division(dataBlock, _generatorPolynomial);
            // Возвращаем кодовое слово
            return dataBlock.Substring(0, DataBits) + remainder;
        }

        private string CorrectError(string codeword)
        {
            string syndrome = Mod2Division(codeword, _generatorPolynomial);
            // Если синдром равен нулю, ошибки нет
            if (syndrome == new string('0', GeneratorPolynomialDegree))
            {
                if (nonError)
                {
                    posErr += 14;
                }
                return codeword.Substring(0, DataBits);
            }
            nonError = false;
            // Определение положения ошибки
            errorPosition = CalculateErrorPosition(syndrome);

            // Исправление ошибки
            if (errorPosition > 0)
            {
                codeword = FlipBit(codeword, errorPosition);
            }
            posErr += errorPosition;
            return codeword.Substring(0, DataBits);
        }

        private string Mod2Division(string dividend, int[] divisor)
        {
            // Преобразование делимого в двоичный массив
            int[] dividendArray = new int[dividend.Length];
            for (int i = 0; i < dividend.Length; i++)
            {
                dividendArray[i] = int.Parse(dividend[i].ToString());
            }

            // Выполнение деления по модулю 2
            int[] remainder = new int[dividendArray.Length];
            Array.Copy(dividendArray, remainder, dividendArray.Length);
            for (int i = 0; i <= dividendArray.Length - divisor.Length; i++)
            {
                if (remainder[i] == 1)
                {
                    for (int j = 0; j < divisor.Length; j++)
                    {
                        remainder[i + j] ^= divisor[j];
                    }
                }
            }
            // Преобразование остатка в двоичную строку
            return string.Join("", remainder.Skip(remainder.Length - GeneratorPolynomialDegree).Select(b => b.ToString()));
        }

        private int CalculateErrorPosition(string syndrome)
        {
            // Преобразование синдрома в двоичный массив
            /*int[] syndromeArray = new int[syndrome.Length];
            for (int i = 0; i < syndrome.Length; i++)
            {
                syndromeArray[i] = int.Parse(syndrome[i].ToString());
            }*/

            // Определение позиции ошибки
            // (Ошибка находится в позиции, соответствующей индексу 1 в синдроме)
            errorPosition = 0;
            for (int i = 0; i < _generatorPolynomial.Length; i++)
            {
                if (syndrome == _syndromDecode[i])
                {
                    errorPosition = i + 1; // Индекс начинается с 0, а позиция ошибки с 1
                    break;
                }
            }

            return errorPosition;
        }

        private string FlipBit(string codeword, int position)
        {
            // Инвертирование бита в кодовом слове
            char[] codewordArray = codeword.ToCharArray();
            codewordArray[position - 1] = codewordArray[position - 1] == '0' ? '1' : '0';
            return new string(codewordArray);
        }
    }
}
