using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace _2lab
{
    public partial class Form1 : Form
    {
        BigInteger p;
        BigInteger q;
        BigInteger n;
        BigInteger phi;
        BigInteger exp;
        BigInteger d;
        string dis;
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
            label1.Text = RSA();
            label2.Text = "P = " + p.ToString();
            label3.Text = "q = " + q.ToString();
            label4.Text = "n = " + n.ToString();
            label5.Text = "НОД: " + phi.ToString();
            label6.Text = "exp = " + exp.ToString();
            label7.Text = "d = " + d.ToString();
            label9.Text = "деш = " + dis;

        }
        public string RSA()
        {
            RSACrypto rsa = new RSACrypto();
            string inputText = textBox1.Text;
            p = rsa.p;
            q = rsa.q;
            n = rsa.n;
            phi = (p - 1) * (q - 1);
            exp = 7; // открытая экспонента
            d = rsa.d; // закрытая экспонента
            BigInteger res = rsa.Encrypt(inputText, exp, n);
            dis = rsa.Decrypt(res, d, n);
            string str = res.ToString();
            return str;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
