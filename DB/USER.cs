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
namespace BD1
{
    public partial class USER : Form
    {
        public USER()
        {
            InitializeComponent();
        }
       // private OracleConnection conn = new OracleConnection();
        private void button1_Click(object sender, EventArgs e)
        {
            bool connected = true;
            string userid = textBox1.Text;
            string password = textBox2.Text;
            //OracleConnection conn = new OracleConnection("Data Source = localhost; User ID =  elizabeth ;Password=12345");
            conn_string.Value = "Data Source = localhost;" + "User ID = " + userid + ";Password=" + password;
            OracleConnection conn = new OracleConnection(conn_string.Value);
            try
            {
                conn.Open();
            }
            catch(OracleException ex)
            {
                connected = false;
                switch(ex.Number)
                {
                    case 1:
                        MessageBox.Show("Error attempting to insert duplicate data.");
                        break;
                    case 12560:
                        MessageBox.Show("The database is unavailable.");
                        break;
                    default:
                        MessageBox.Show("Database error: " + ex.Message.ToString());
                        break; 
                }
            }
            catch(Exception ex)
            {
                connected = false;
                MessageBox.Show(ex.Message.ToString()); 
            }
            if (connected)
            {
                this.Hide();
                main_menu m = new main_menu();
                m.Show();
                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OracleConnection conn = new OracleConnection(conn_string.Value);
            conn.Open();
            this.Hide();
        }
    }
}
