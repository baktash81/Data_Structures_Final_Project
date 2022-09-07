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
    public partial class adddisease : Form
    {
        public adddisease(Store data)
        {
            this.data = data;
            InitializeComponent();
        }
        public Store data;
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void adddisease_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            try
            {
                
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                data.AddDisease(textBox1.Text);
                watch.Stop();
                textBox2.Text = $"Execution Time: {watch.ElapsedTicks / 10} micro-sec";
                string message = "اضافه شد";
                string title = "addisease";
                MessageBox.Show(message, title);
            }
            catch
            {
                string message = "Already Exist!";
                string title = "addisease";
                MessageBox.Show(message, title);
            }
            textBox1.Clear();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
