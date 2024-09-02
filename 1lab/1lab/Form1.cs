using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1lab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Выводим текст в метку
            label1.Text = Code();
        }
        private string Code()
        {
            // Получаем текст из текстового поля
            string inputText = textBox1.Text;
            string K = textBox2.Text;
            int k = int.Parse(K);
            char[] arr = new char[inputText.Length];
            for (int i = 0; i < inputText.Length; i++)
            {
                int index = (int)inputText[i];
                if (inputText[i] == ' ')
                    arr[i] = ' ';
                else
                {
                    index += k;
                    char sim = (char)index;
                    arr[i] = sim;
                    if (arr[i] == '[')
                        arr[i] = 'A';
                    if (arr[i] == '\\')
                        arr[i] = 'B';
                    if (arr[i] == ']')
                        arr[i] = 'C';
                    if (arr[i] == '{')
                        arr[i] = 'a';
                    if (arr[i] == '|')
                        arr[i] = 'b';
                    if (arr[i] == '}')
                        arr[i] = 'c';
                    if (arr[i] == 'а')
                        arr[i] = 'А';
                    if (arr[i] == 'б')
                        arr[i] = 'Б';
                    if (arr[i] == 'в')
                        arr[i] = 'В';
                }
            }
            string shifr = new string(arr);
            return shifr;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
