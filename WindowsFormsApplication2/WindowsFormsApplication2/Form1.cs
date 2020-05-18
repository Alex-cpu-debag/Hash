using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using hashing;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(@"C:\" + textBox2.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }

            string path = @"C:\" + textBox2.Text + @"\MD5.txt";
            string path1 = @"C:\" + textBox2.Text + @"\SHA256.txt";
            string path2 = @"C:\" + textBox2.Text + @"\SHA384.txt";
            string path3 = @"C:\" + textBox2.Text + @"\SHA512.txt";
            string path4 = @"C:\" + textBox2.Text + @"\SHA256S.txt";
            string path5 = @"C:\" + textBox2.Text + @"\SHA384S.txt";
            string path6 = @"C:\" + textBox2.Text + @"\SHA512S.txt";

            using (StreamWriter stream = new StreamWriter(path, true))
            {
                stream.WriteLine("" + textBox10.Text);
            }
            using (StreamWriter stream = new StreamWriter(path1, true))
            {
                stream.WriteLine("" + textBox9.Text);
            }
            using (StreamWriter stream = new StreamWriter(path2, true))
            {
                stream.WriteLine("" + textBox8.Text);
            }
            using (StreamWriter stream = new StreamWriter(path3, true))
            {
                stream.WriteLine("" + textBox7.Text);
            }
            using (StreamWriter stream = new StreamWriter(path4, true))
            {
                stream.WriteLine("" + textBox6.Text);
            }
            using (StreamWriter stream = new StreamWriter(path5, true))
            {
                stream.WriteLine("" + textBox5.Text);
            }
            using (StreamWriter stream = new StreamWriter(path6, true))
            {
                stream.WriteLine("" + textBox4.Text);
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox6.Text = Hashing.ComputeHash(textBox2.Text, Supported_HA.SHA256, null);
            textBox5.Text = Hashing.ComputeHash(textBox2.Text, Supported_HA.SHA384, null);
            textBox4.Text = Hashing.ComputeHash(textBox2.Text, Supported_HA.SHA512, null);
            textBox9.Text = Hashing.ComputeHashNoSalt(textBox2.Text, Supported_HA.SHA256);
            textBox8.Text = Hashing.ComputeHashNoSalt(textBox2.Text, Supported_HA.SHA384);
            textBox7.Text = Hashing.ComputeHashNoSalt(textBox2.Text, Supported_HA.SHA512);
            textBox10.Text = Hashing.CalculateMD5Hash(textBox2.Text, Supported_HA.MD5);

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

         

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
          
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
        }

        private void выбратьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())// создание нового OpenFileDialog
            {
                dialog.Filter = "Текстовые файлы|*.txt"; // отображение файлов с расширением .txt
                if (dialog.ShowDialog() == DialogResult.OK) // если выбираем файл и нажимаем кнопку ОК
                {
                    textBox3.Text = ("SHA256: ");
                    textBox3.Text = Hashing.ComputeHashNoSalt(File.ReadAllText(dialog.FileName), Supported_HA.SHA256);
                    textBox3.Text = Hashing.ComputeHashNoSalt(File.ReadAllText(dialog.FileName), Supported_HA.SHA384);
                     // вывод хеша в текст бокс файла, 
                    // предварительно считав с него текст                    
                }
            }
        }
    }
}
