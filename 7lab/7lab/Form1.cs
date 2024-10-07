using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7lab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputText = textBox1.Text;
            if (inputText.Length != 16)
            {
                label3.Text = $"Длина пароля должна быть 16 символов (сейчас длина пароля {inputText.Length})";
                inputText = textBox1.Text;
                textBox2.Text = "";
            }
            else
            {
                PasswordHash pswd = new PasswordHash();
                textBox2.Text = pswd.HashPassword(inputText);
                bool isPswdCorrect = pswd.VerifyPassword(textBox2.Text, inputText);
                label3.Text = isPswdCorrect ? "Пароль верен (дешифрованный пароль совпадает с начальным)" :
                    "Пароль не верен!!! Ошибка!!!";
            }   
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
