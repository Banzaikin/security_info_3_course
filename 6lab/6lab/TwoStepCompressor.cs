using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6lab
{
    // Класс для двухступенчатого сжатия
    public class TwoStepCompressor
    {
        private LZWCompressor lzwCompressor;
        private HuffmanCompressor huffmanCompressor;
        public string lzwEncoded;
        public string huffmanEncoded;

        public TwoStepCompressor()
        {
            lzwCompressor = new LZWCompressor();
            huffmanCompressor = new HuffmanCompressor();
        }

        public string Compress(string text)
        {
            lzwEncoded = lzwCompressor.Encode(text);
            huffmanEncoded = huffmanCompressor.Encode(lzwEncoded);
            return huffmanEncoded;
        }

        public string Decompress(string compressedText)
        {
            string lzwEncoded = huffmanCompressor.Decode(compressedText);
            string decodedText = lzwCompressor.Decode(lzwEncoded);
            return decodedText;
        }
    }
}
