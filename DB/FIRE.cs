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
using System.Data.OracleClient;

namespace BD1
{
    
    public partial class FIRE : Form
    {
        public FIRE()
        {
            InitializeComponent();
            FillList();
        }

        void FillList()
        {
            string query ="SELECT * FROM elizabeth.VIEW1";
            //string query = "SELECT * FROM elizabeth.VIEW1";
            OracleConnection conn = new OracleConnection(conn_string.Value);
            //OracleConnection conn = new OracleConnection("Data Source = localhost; User ID =  elizabeth ;Password=12345");
            conn.Open();
            OracleCommand cmd = new OracleCommand(query,conn);
            OracleDataReader myReader=null;
            try
            {
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string sname = myReader.GetString(myReader.GetOrdinal("surname_of_pers"));
                    listBox1.Items.Add(sname);
                    //comboBox1.Items.Add(myReader.GetString(myReader.GetOrdinal("NAME_OF_POSITION")));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            PERSONAL p = new PERSONAL();
            this.Hide();
            p.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM elizabeth.VIEW1 where surname_of_pers='" + listBox1.Text + "'";
            OracleConnection conn = new OracleConnection("Data Source = localhost; User ID =  elizabeth ;Password=12345");
            OracleCommand cmd = new OracleCommand(query, conn);
            OracleDataReader myReader;
            try
            {
                conn.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                    delId.Value = myReader.GetInt32(myReader.GetOrdinal("ID_K_A"));
                    
                    string sname = myReader.GetString(myReader.GetOrdinal("NAME_OF_PERS"));
                    string ssurname = myReader.GetString(myReader.GetOrdinal("SURNAME_OF_PERS"));
                    string age = Convert.ToString(myReader.GetInt32(myReader.GetOrdinal("age")));
                    string name_position1 = myReader.GetString(myReader.GetOrdinal("NAME_POSITION"));
                    textBox1.Text = sname;
                    textBox2.Text = ssurname;
                    textBox3.Text = age;
                    textBox4.Text = name_position1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OracleConnection conn = new OracleConnection("Data Source = localhost; User ID = Elizabeth; Password= 12345");
            conn.Open();
            OracleCommand cmd = new OracleCommand("elizabeth.DEL_PERSONAL", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            OracleParameter id_k = new OracleParameter("id_k", OracleType.Number, 150);
            //OracleParameter id_k = new OracleParameter("id_k", OracleDbType.Int32, 150);
            id_k.Direction = ParameterDirection.Input;//OracleType
            id_k.Value = delId.Value;
            cmd.Parameters.Add(id_k);

            //OracleParameter date_release = new OracleParameter("date_release", OracleDbType.Date);
            OracleParameter date_release = new OracleParameter("date_release", OracleType.DateTime);
            date_release.Direction = ParameterDirection.Input;
            date_release.Value = dateTimePicker1.Value.Date;
            cmd.Parameters.Add(date_release);

            OracleParameter name_position1 = new OracleParameter("name_position1", OracleType.VarChar, 150);
            //OracleParameter name_position1 = new OracleParameter("name_position1", OracleDbType.Int32, 150);
            name_position1.Direction = ParameterDirection.Input;//OracleType
            name_position1.Value = textBox4.Text;
            cmd.Parameters.Add(name_position1);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Retrieving Oracle Sequence Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Звільнений.");
            this.Close();
            PERSONAL personal = new PERSONAL();
            personal.Show();
        }
    }
}
