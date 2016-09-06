using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
//using System.Data.OracleClient;

namespace BD1
{
    public partial class main_menu : Form
    {
        public main_menu()
        {
            InitializeComponent();
            if (conn_string.Value == ("Data Source = localhost;" + "User ID = " + "human_res" + ";Password=" +"12345"))
            {
                button1.Enabled = true;
                button4.Enabled = true;
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            PERSONAL p = new PERSONAL();
            this.Hide();
            p.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dovid d = new dovid();
            this.Hide();
            d.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void main_menu_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            vac v = new vac();
            this.Hide();
            v.Show();
        }
    }
}
