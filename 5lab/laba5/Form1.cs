using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba5
{
    public partial class Form1 : Form
    {
        CyclicCode cyclicCode = new CyclicCode();
        string encodedMessage;

        public Form1()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputText = textBox1.Text;
            encodedMessage = cyclicCode.Encode(inputText);
            // Введение ошибки в кодовое слово
            // (Для примера, вносим ошибку в шестой бит сообщения)
            //encodedMessage = FlipBit(encodedMessage, 6);
            textBox4.Text = encodedMessage;   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Декодирование сообщения с исправлением ошибки
            string decodedMessage = cyclicCode.Decode(encodedMessage);
            textBox2.Text = decodedMessage;
            int position = cyclicCode.errorPosition;
            textBox3.Text = "Ошибка в позиции: " + position.ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            encodedMessage = textBox4.Text;
        }
    }
}
