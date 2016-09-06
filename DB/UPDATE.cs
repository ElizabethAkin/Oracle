using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OracleClient;
using Oracle.DataAccess.Types;
//using Oracle.DataAccess.Client;
using System.Data.SqlTypes;

namespace BD1
{
    public partial class UPDATE : Form
    {
        public UPDATE()
        {
            InitializeComponent();
       //     FillCombo();
        }
     /*   public void FillCombo()
        {
            string query = "SELECT * FROM elizabeth.POSITION";
            OracleConnection conn = new OracleConnection("Data Source = localhost; User ID =  elizabeth ;Password=12345");
            OracleCommand cmd = new OracleCommand(query, conn);
            OracleDataReader myReader;
            try
            {
                conn.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string sname = myReader.GetString(myReader.GetOrdinal("NAME_POSITION"));
                    comboBox2.Items.Add(sname);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }*/
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           /* string query = "SELECT t.id_timetable,p.id_position FROM elizabeth.POSITION p,elizabeth.timetable t where name_position='" + 
                comboBox2.Text + "' and t.id_position=p.id_position";
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
                    string sid1 = myReader.GetInt32(myReader.GetOrdinal("id_timetable")).ToString();
                    idPo.Value = int.Parse(sid1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }*/
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            dovid p = new dovid();
            p.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OracleConnection conn = new OracleConnection(conn_string.Value);
            conn.Open();
            OracleCommand cmd = new OracleCommand("elizabeth.UPDATE_DATA", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            OracleParameter name_of_pers1 = new OracleParameter("name_of_pers1", OracleType.VarChar, 150);
            //       OracleParameter name1 = new OracleParameter("name_of_pers1", OracleDbType.Varchar2, 150);
            name_of_pers1.Direction = ParameterDirection.Input;
            name_of_pers1.Value = this.textBox1.Text;
            cmd.Parameters.Add(name_of_pers1);

            OracleParameter surname_of_pers1 = new OracleParameter("surname_of_pers1", OracleType.VarChar, 150);
            //        OracleParameter surname_of_pers1 = new OracleParameter("surname_of_pers1", OracleDbType.Varchar2, 150);
            surname_of_pers1.Direction = ParameterDirection.Input;
            surname_of_pers1.Value = this.textBox2.Text;
            cmd.Parameters.Add(surname_of_pers1);

            OracleParameter address_of_pers1 = new OracleParameter("address_of_pers1", OracleType.VarChar, 150);
            //      OracleParameter address_of_pers1 = new OracleParameter("address_of_pers1", OracleDbType.Varchar2, 150);
            address_of_pers1.Direction = ParameterDirection.Input;
            address_of_pers1.Value = this.textBox5.Text;
            cmd.Parameters.Add(address_of_pers1);

            OracleParameter telephone1 = new OracleParameter("telephone1", OracleType.Int32, 150);
            //      OracleParameter telephone1 = new OracleParameter("telephone1", OracleDbType.Int32, 150);
            telephone1.Direction = ParameterDirection.Input;
            telephone1.Value = this.textBox6.Text;
            cmd.Parameters.Add(telephone1);

            OracleParameter e_mail1 = new OracleParameter("e_mail1", OracleType.VarChar, 150);
            //      OracleParameter email = new OracleParameter("e_mail1", OracleDbType.Varchar2, 150);
            e_mail1.Direction = ParameterDirection.Input;
            e_mail1.Value = this.textBox7.Text;
            cmd.Parameters.Add(e_mail1);

            OracleParameter date_of_begin1 = new OracleParameter("DATE_ACCEPT1", OracleType.DateTime);
            date_of_begin1.Direction = ParameterDirection.Input;
            date_of_begin1.Value = dateTimePicker2.Value.Date;
            cmd.Parameters.Add(date_of_begin1);

        /*    OracleParameter id_position1 = new OracleParameter("id_timetable1", OracleType.Int32);
            id_position1.Direction = ParameterDirection.Input;
            id_position1.Value = idPo.Value;
            cmd.Parameters.Add(id_position1);*/

            OracleParameter salary1 = new OracleParameter("salary1", OracleType.Int32);
            salary1.Direction = ParameterDirection.Input;
            salary1.Value = Int32.Parse(textBox9.Text);
            cmd.Parameters.Add(salary1);

            OracleParameter age1 = new OracleParameter("age1", OracleType.Int32);
            age1.Direction = ParameterDirection.Input;
            age1.Value = Int32.Parse(textBox3.Text);
            cmd.Parameters.Add(age1);

            OracleParameter id_ka = new OracleParameter("id_ka", OracleType.Number, 150);
            //    OracleParameter id_ka = new OracleParameter("id_ka", OracleDbType.Int32, 150);
            id_ka.Direction = ParameterDirection.Input;//OracleType
            id_ka.Value = changeId.Value;
            cmd.Parameters.Add(id_ka);


            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Retrieving Oracle Sequence Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("ЗМІНЕНО.");
            this.Close();
            PERSONAL personal = new PERSONAL();
            personal.Show();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

       
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}