using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6lab
{
    // Класс для алгоритма Лемпеля-Зива
    public class LZWCompressor
    {
        public string Encode(string text)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            for (int i = 0; i < 256; i++)
            {
                dictionary.Add(Convert.ToChar(i).ToString(), i);
            }
            int nextCode = 256;
            string currentString = "";
            string encodedString = "";

            foreach (char c in text)
            {
                string nextString = currentString + c;
                if (dictionary.ContainsKey(nextString))
                {
                    currentString = nextString;
                }
                else
                {
                    encodedString += dictionary[currentString] + " ";
                    dictionary.Add(nextString, nextCode++);
                    currentString = c.ToString();
                }
            }

            if (currentString != "")
            {
                encodedString += dictionary[currentString] + " ";
            }

            return encodedString.Trim();
        }

        public string Decode(string encodedString)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for (int i = 0; i < 256; i++)
            {
                dictionary.Add(i, Convert.ToChar(i).ToString());
            }
            int nextCode = 256;
            string previousString = "";
            string decodedString = "";
            string[] codes = encodedString.Split(' ');

            foreach (string code in codes)
            {
                int codeValue = int.Parse(code);
                if (codeValue < 256)
                {
                    decodedString += Convert.ToChar(codeValue);
                    previousString = Convert.ToChar(codeValue).ToString();
                }
                else
                {
                    string currentString = dictionary[codeValue - 1];
                    decodedString += currentString;
                    dictionary.Add(nextCode++, previousString + currentString[0]);
                    previousString = currentString;
                }
            }

            return decodedString;
        }
    }
}
