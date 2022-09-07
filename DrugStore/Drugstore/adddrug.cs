using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drugstore
{
    public partial class adddrug : Form
    {
        public adddrug(Store data)
        {
            this.data = data;
            InitializeComponent();
        }
        public Store data;
        private void adddrug_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            try
            {
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                data.AddDrug(textBox1.Text,Convert.ToInt32(textBox2.Text));
                watch.Stop();
                textBox3.Text = $"Execution Time: {watch.ElapsedTicks / 10} micro-seccond";
                string message = "اضافه شد";
                string title = "addrug";
                MessageBox.Show(message, title);
            }
            catch
            {
                string message = "Already Exist!";
                string title = "addrug";
                MessageBox.Show(message, title);
            }
            textBox1.Clear();
            textBox2.Clear();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
