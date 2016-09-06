using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Oracle.DataAccess.Client;
//using Oracle.DataAccess.Types;
using System.Data.OracleClient;

namespace BD1
{
    public partial class ADD_WORKER : Form
    {
        
        public ADD_WORKER()
        {
            InitializeComponent();
                FillCombo();
        }
        public void FillCombo()
        {
            string sq = "select po.name_position, va.amount_position, va.kilkist_viln_posad " +
                              "from elizabeth.position po  "+
                              "inner join elizabeth.vakansii va " +
                              "on po.id_position = va.id_position " +
                              "order by po.name_position";
            string query = "SELECT * FROM elizabeth.POSITION";
            OracleConnection conn = new OracleConnection(conn_string.Value);
            OracleCommand cmd = new OracleCommand(sq, conn);
            OracleDataReader myReader;
            try
            {
                conn.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    if (myReader.GetInt32(myReader.GetOrdinal("kilkist_viln_posad")) != 0)
                    {
                        string sname = myReader.GetString(myReader.GetOrdinal("name_position"));
                        comboBox1.Items.Add(sname);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

       
        public void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            dateStart.Value = dateTimePicker1.Value.Date;
        }
        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM elizabeth.position where name_position='" + comboBox1.Text + "'";
            OracleConnection conn = new OracleConnection(conn_string.Value);
            OracleCommand cmd = new OracleCommand(query, conn);
            OracleDataReader myReader;
            try
            {
                conn.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string sid = myReader.GetInt32(myReader.GetOrdinal("id_position")).ToString();
                        indexPosition.Value = int.Parse(sid); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void button2_Click(object sender, EventArgs e){}
        public void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            main_menu m = new main_menu();
            m.Show();
        }
        public void ADD_WORKER_Load(object sender, EventArgs e) { }
        public void button2_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || salaryBox.Text == "")
            {
                MessageBox.Show("ЗАПОВНІТЬ ОБОВ'ЯЗКОВІ ПОЛЯ");
            }
            else
            {
                OracleConnection conn = new OracleConnection(conn_string.Value);
                conn.Open();
                name_surname n = new name_surname();
                n.Show();
                this.Hide();
            }
        }
        public void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                salaryVal.Value = Int32.Parse(salaryBox.Text);
            }
            catch (Exception ex)
            {  }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            changep c = new changep();
            c.Show();
            c.button3.Enabled = true;
            this.Hide();
        }

        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {
                
        }

        private void dateTimePicker1_Validating(object sender, CancelEventArgs e) {
        
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void salaryBox_Validating(object sender, CancelEventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(salaryBox.Text, out n);
            if (isNumeric!=true)
                MessageBox.Show("некоректна зарплатня!");
        }
    }
}