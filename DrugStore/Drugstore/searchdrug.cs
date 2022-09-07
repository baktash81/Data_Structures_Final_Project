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
    public partial class searchdrug : Form
    {
        public searchdrug(Store data)
        {
            this.data = data;
            InitializeComponent();
        }
        public Store data;
        private void searchdrug_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void drugname_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                string dname = drugname.Text;
                double price = data.GetDrugInfo(dname).Price;
                List<string> pall = data.GetDrugInfo(dname).PAllergies;
                List<string> nall = data.GetDrugInfo(dname).NAllergies;
                List<(string drug, string effect)> effects = data.GetDrugInfo(dname).Effects;
                watch.Stop();
                textBox6.Text = $"Execution Time: {watch.ElapsedTicks / 10} micro-seccond";
                textBox1.Text = dname;
                textBox2.Text = Convert.ToString(price);
                if (pall.Count == 0)
                    textBox3.Text = "0";
                if (nall.Count == 0)
                    textBox5.Text = "0";
                if (effects.Count == 0)
                    textBox4.Text = "0";
                for (int i = 0; i < pall.Count; i++)
                {
                    textBox3.AppendText(pall[i] + '\n');
                }
                for (int i = 0; i < nall.Count; i++)
                {
                    textBox5.AppendText(nall[i] + '\n');
                }
                for (int i = 0; i < effects.Count; i++)
                {
                    string tmp = effects[i].drug + effects[i].effect;
                    textBox4.AppendText(tmp + '\n');
                }
            }
            catch
            {
                textBox1.Text = "Not Found!";
                textBox2.Text = "Not Found!";
                textBox3.Text = "Not Found!";
                textBox4.Text = "Not Found!";
                textBox5.Text = "Not Found!";
                textBox6.Text = "Not Found!";

            }
            drugname.Clear();

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                data.DeleteDrug(textBox1.Text);
                button3_Click(sender, e);
            }
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }
    }
}
