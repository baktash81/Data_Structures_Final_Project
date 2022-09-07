using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Drugstore
{
    public partial class Drugstore : Form
    {
        public Drugstore()
        {
            InitializeComponent();
        }
        public string path;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                var data = new Store(path);
                data.ReadFromDataset();
                watch.Stop();
                var newform = new Form1(data);
                var newform1 = new sabadkharid(data);
                newform.Closed += (s, args) => this.Close();
                newform.Show();
                newform1.Show();
                string message = $"Execution Time: {watch.ElapsedTicks / 10} micro-seccond";
                string title = "duration";
                MessageBox.Show(message, title);
            }
            catch
            {
                MessageBox.Show("Choose database path!", "Error");
                textBox2.Clear();
                this.Show();
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var opnDlg = new FolderBrowserDialog()) 
            {
                if (opnDlg.ShowDialog() == DialogResult.OK)
                {
                    path = opnDlg.SelectedPath;
                    textBox2.Text = path;
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }

}
