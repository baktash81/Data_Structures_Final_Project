using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Drugstore
{
    public partial class sabadkharid : Form
    {
        public sabadkharid(Store data)
        {
            this.data = data;
            InitializeComponent();
        }
        public Store data;
        public int price = 0;
        public int tvrom = 0;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void sabadkharid_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                textBox3.Clear();
                var watch = new System.Diagnostics.Stopwatch();
                if (textBox1.Text != "")
                {
                    tvrom = Convert.ToInt32(textBox1.Text);

                }
                watch.Start();
                price = price+data.GetDrugInfo(drugname.Text).Price + (data.GetDrugInfo(drugname.Text).Price)*tvrom/100;
                watch.Stop();
                textBox3.Text = $"Execution Time: {watch.ElapsedTicks / 10} micro-sec";

                textBox4.AppendText(drugname.Text + data.GetDrugInfo(drugname.Text).Price + "\n");
                textBox2.Text = Convert.ToString(price);
            }
            catch
            {
                string message = "Not Found!";
                string title = "addisease";
                MessageBox.Show(message, title);
            }
            textBox1.Clear();
            drugname.Clear();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
