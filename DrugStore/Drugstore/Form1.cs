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
    public partial class Form1 : Form
    {
        public Form1(Store data)
        {
            this.data = data;
            InitializeComponent();
        }
        public Store data;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var newform = new searchdrug(data);
            newform.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var newform = new adddrug(data);
            newform.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            var newform = new searchdisease(data);
            newform.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var newform = new adddisease(data);
            newform.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            data.WriteToDataset();
            watch.Stop();
            string message = "ذخیره شد";
            string title = "save";
            string message2 = $"Execution Time: {watch.ElapsedTicks / 10} micro-seccond";
            MessageBox.Show(message, title);
            MessageBox.Show(message2, "duration");
        }
    }
}
