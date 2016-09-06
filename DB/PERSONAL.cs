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
    public partial class PERSONAL : Form
    {
        public PERSONAL()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleConnection conn = new OracleConnection(conn_string.Value);
            conn.Open();
            ADD_WORKER a = new ADD_WORKER();
            a.Show();
            name_surname p = new name_surname();
           // p.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OracleConnection conn = new OracleConnection(conn_string.Value);
            conn.Open();
            main_menu m = new main_menu();
            this.Hide();
            m.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            changep n = new changep();
            n.button1.Enabled = true;
            n.Show();
         //   n.button1.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FIRE f = new FIRE();
            this.Hide();
            f.Show();
        }

        private void PERSONAL_Load(object sender, EventArgs e)
        {

        }
    }
}
