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
    public partial class searchdisease : Form
    {
        public searchdisease(Store data)
        {
            this.data = data;
            InitializeComponent();
        }
        public Store data;
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                data.DeleteDisease(textBox1.Text);
                textBox1.Clear();
                textBox3.Clear();
                diseasename.Clear();
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                string diseasen = diseasename.Text;
                List<string> pall = data.GetDiseaseInfo(diseasen).PAllergies;
                textBox1.Text = diseasen;
                watch.Stop();
                textBox2.Text = $"Execution Time: {watch.ElapsedTicks / 10} micro-sec";
                for (int i = 0; i < pall.Count; i++)
                {
                    textBox3.AppendText(pall[i] + '\n');
                }
            }
            catch
            {
                textBox1.Text = "Not Found!";
                textBox3.Text = "Not Found!";
                textBox2.Text = "Not Found!";
            }
            diseasename.Clear();
        }

        private void searchdisease_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox3.Clear();
            textBox2.Clear();
            diseasename.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
