using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6lab
{
    // Класс для алгоритма Хаффмана
    public class HuffmanCompressor
    {
        public string Encode(string text)
        {
            Dictionary<char, int> frequencies = GetCharacterFrequencies(text);
            HuffmanNode root = BuildHuffmanTree(frequencies);
            Dictionary<char, string> codes = GenerateHuffmanCodes(root);
            string encodedText = "";

            foreach (char c in text)
            {
                encodedText += codes[c];
            }

            return encodedText;
        }

        public string Decode(string encodedText)
        {
            Dictionary<char, int> frequencies = GetCharacterFrequencies(encodedText);
            HuffmanNode root = BuildHuffmanTree(frequencies);
            string decodedText = "";
            HuffmanNode current = root;
            foreach (char bit in encodedText)
            {
                if (bit == '0')
                {
                    current = current.Left;
                }
                else if (bit == '1')
                {
                    current = current.Right;
                }
                if (current.Left == null && current.Right == null)
                {
                    decodedText += current.Symbol;
                    current = root;
                }
            }
            return decodedText;
        }

        private Dictionary<char, int> GetCharacterFrequencies(string text)
        {
            Dictionary<char, int> frequencies = new Dictionary<char, int>();
            foreach (char c in text)
            {
                if (frequencies.ContainsKey(c))
                {
                    frequencies[c]++;
                }
                else
                {
                    frequencies.Add(c, 1);
                }
            }
            return frequencies;
        }

        private HuffmanNode BuildHuffmanTree(Dictionary<char, int> frequencies)
        {
            List<HuffmanNode> nodes = frequencies.Select(f => new HuffmanNode(f.Key, f.Value)).ToList();
            while (nodes.Count > 1)
            {
                nodes = nodes.OrderBy(n => n.Frequency).ToList();
                HuffmanNode left = nodes[0];
                HuffmanNode right = nodes[1];
                nodes.RemoveRange(0, 2);
                HuffmanNode parent = new HuffmanNode('\0', left.Frequency + right.Frequency, left, right);
                nodes.Add(parent);
            }
            return nodes[0];
        }

        private Dictionary<char, string> GenerateHuffmanCodes(HuffmanNode root)
        {
            Dictionary<char, string> codes = new Dictionary<char, string>();
            GenerateHuffmanCodesRecursive(root, "", codes);
            return codes;
        }

        private void GenerateHuffmanCodesRecursive(HuffmanNode node, string code, Dictionary<char, string> codes)
        {
            if (node.Left == null && node.Right == null)
            {
                codes.Add(node.Symbol, code);
                return;
            }
            GenerateHuffmanCodesRecursive(node.Left, code + "0", codes);
            GenerateHuffmanCodesRecursive(node.Right, code + "1", codes);
        }

        private class HuffmanNode
        {
            public char Symbol { get; set; }
            public int Frequency { get; set; }
            public HuffmanNode Left { get; set; }
            public HuffmanNode Right { get; set; }

            public HuffmanNode(char symbol, int frequency)
            {
                Symbol = symbol;
                Frequency = frequency;
            }

            public HuffmanNode(char symbol, int frequency, HuffmanNode left, HuffmanNode right) : this(symbol, frequency)
            {
                Left = left;
                Right = right;
            }
        }
    }
}
