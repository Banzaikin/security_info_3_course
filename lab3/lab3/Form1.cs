using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace lab3
{
    public partial class Form1 : Form
    {
        string inputText;
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ElGamal elGamal = new ElGamal();
            inputText = textBox1.Text;
            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(inputText);
            string text = elGamal.ByteArrayToString(tmpSource);
            elGamal.M = BigInteger.Parse(text, NumberStyles.HexNumber);
            textBox2.Text = elGamal.Encrypt();
            textBox3.Text = elGamal.decryptText;
            textBox3.Text = inputText;
            label1.Text = "p = " + elGamal.p.ToString();
            label2.Text = "g = " + elGamal.g.ToString();
            label3.Text = "x = " + elGamal.x.ToString();
            label4.Text = "y = " + elGamal.y.ToString();
            label5.Text = "k = " + elGamal.k.ToString();
            label6.Text = "a = " + elGamal.a.ToString();
            label7.Text = "b = " + elGamal.b.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            inputText = "Hello world, my name is Kirill Bayandin. I'm from Russia. I'm like bananas." +
                "dfdsf dsfn jsdnfjndsknf kndkjnsdfijxichvuh xcuhvuhdus ufh dbhzbdbfj bjcjz kdkj nd dk" +
                "nfjbdsjbf b hbxhbvhbbsiudfi hdsihf i ncxjnvndj vbjsdb jbj hv xchv ndjs vjb djsbfhfihds";
            textBox1.Text = inputText;
        }
    }
}
