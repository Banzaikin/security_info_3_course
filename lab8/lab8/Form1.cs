using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputText = textBox1.Text;
            // Ключ шифрования
            string key = "SecretkeySecretkeySecretkeySecretkey";
            Gamming gamming = new Gamming();
            textBox2.Text = gamming.Encrypt(inputText, key);
        }
    }
}
