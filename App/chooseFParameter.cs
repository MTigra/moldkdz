using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace App
{

    public partial class chooseFParameter : Form
    {
        List<string> availibleParameters = new List<string>();
        Form1 form1;
        public chooseFParameter(string a,Form1 _form1)
        {
            this.Location = _form1.Location;
            form1 = _form1;
            InitializeComponent();
            this.Text = a;
            
        }
        public  string FPatameter
        {
             get
            {
                return this.comboBox1.Text;
            }
        }

        private void chooseFParameter_Load(object sender, EventArgs e)
        {
            if (this.Text == "Отфильтровать по american_express")
            {
                comboBox1.DataSource = form1.GetAEList();
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.DataSource = form1.GetAccessList();
                comboBox1.SelectedIndex = 0;
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Hide();
        }
    }
}
