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
    public partial class changep : Form
    {
        public changep()
        {
            InitializeComponent();
            FillList();
            FillCombo();
        }
        public void FillCombo()
        {
            string query = "select po.name_position, va.amount_position, va.kilkist_viln_posad " +
                              "from elizabeth.position po  " +
                              "inner join elizabeth.vakansii va " +
                              "on po.id_position = va.id_position " +
                              "order by po.name_position";
            OracleConnection conn = new OracleConnection(conn_string.Value);
            OracleCommand cmd = new OracleCommand(query, conn);
            OracleDataReader myReader;
            try
            {
                conn.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    if (myReader.GetInt32(myReader.GetOrdinal("kilkist_viln_posad")) != 0)
                    {
                        string sname = myReader.GetString(myReader.GetOrdinal("NAME_POSITION"));
                        comboBox2.Items.Add(sname);
                    }
                    //comboBox1.Items.Add(myReader.GetString(myReader.GetOrdinal("NAME_OF_POSITION")));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void FillList()
        {
            // string query = "SELECT name_of_pers,surname_of_pers,age,personal.id_k_a FROM elizabeth.personal,elizabeth.AGREEMENT_ON_JOB_PLACEMENT WHERE AGREEMENT_ON_JOB_PLACEMENT.DATE_OF_RELEASE is NULL and AGREEMENT_ON_JOB_PLACEMENT.id_k_a=personal.id_k_a";
            string query = "SELECT * FROM elizabeth.VIEW1";
            OracleConnection conn = new OracleConnection(conn_string.Value);
            conn.Open();
            OracleCommand cmd = new OracleCommand(query, conn);
            OracleDataReader myReader = null;
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
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM elizabeth.VIEW1 where surname_of_pers='" + listBox1.Text + "'";//  +
            //    "and person_post.id_timetable=timetable.id_timetable and " +
            //    "person_post.id_k_a=personal.id_k_a and personal.id_k_a=k_a.id_k_a"+
           //     " and position.id_position=timatable.id_position";
            OracleConnection conn = new OracleConnection(conn_string.Value);
            OracleCommand cmd = new OracleCommand(query, conn);
            OracleDataReader myReader;
            try
            {
                conn.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                    //int idpos = myReader.GetInt32(myReader.GetOrdinal("ID_POSITION"));

                    changeId.Value = myReader.GetInt32(myReader.GetOrdinal("ID_K_A"));
                    //idPos.Value = myReader.GetInt32(myReader.GetOrdinal("ID_POSITION"));
                    string sname = myReader.GetString(myReader.GetOrdinal("NAME_OF_PERS"));
                    string ssurname = myReader.GetString(myReader.GetOrdinal("SURNAME_OF_PERS"));
                    string age = Convert.ToString(myReader.GetInt32(myReader.GetOrdinal("AGE")));
                    string spos = myReader.GetString(myReader.GetOrdinal("NAME_POSITION"));
                    string sAddress = myReader.GetString(myReader.GetOrdinal("address_of_pers"));
                    string sTelephone = Convert.ToString(myReader.GetInt32(myReader.GetOrdinal("telephone")));
                    string sMail = myReader.GetString(myReader.GetOrdinal("e_mail"));
                    string sSalary = Convert.ToString(myReader.GetInt32(myReader.GetOrdinal("salary")));
                    string sDate = Convert.ToString(myReader.GetDateTime(myReader.GetOrdinal("date_accept")));
                    textBox1.Text = sname;
                    textBox2.Text = ssurname;
                    textBox3.Text = age;
                    textBox4.Text = spos;
                    textBox5.Text = sAddress;
                    textBox6.Text = sTelephone;
                    textBox7.Text = sMail;
                    textBox8.Text = sSalary;
                  //  textBox10.Text = sDate;
                    dateTimePicker2.Value = myReader.GetDateTime(myReader.GetOrdinal("date_accept"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            PERSONAL p = new PERSONAL();
            p.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OracleConnection conn = new OracleConnection(conn_string.Value);
            conn.Open();

            if (comboBox2.Text == "" || textBox9.Text == "")
            {
                MessageBox.Show("ЗАПОВНІТЬ ОБОВ'ЯЗКОВІ ПОЛЯ!");
            }
            else
            {
                OracleCommand cmd = new OracleCommand("elizabeth.CHANGEPOS", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter id_k = new OracleParameter("id_k", OracleType.Number, 150);
                //    OracleParameter id_k = new OracleParameter("id_k", OracleDbType.Int32, 150);
                id_k.Direction = ParameterDirection.Input;//OracleType
                id_k.Value = changeId.Value;
                cmd.Parameters.Add(id_k);

                //      OracleParameter date_release = new OracleParameter("date_release", OracleDbType.Date);
                OracleParameter date_release = new OracleParameter("date_release", OracleType.DateTime);
                date_release.Direction = ParameterDirection.Input;
                date_release.Value = dateTimePicker1.Value.Date;
                cmd.Parameters.Add(date_release);

                OracleParameter name1 = new OracleParameter("NAME_OF_PERS1", OracleType.VarChar, 150);
                //       OracleParameter name1 = new OracleParameter("name1", OracleDbType.Varchar2, 150);
                name1.Direction = ParameterDirection.Input;
                name1.Value = this.textBox1.Text;
                cmd.Parameters.Add(name1);

                OracleParameter surname1 = new OracleParameter("SURNAME_OF_PERS1", OracleType.VarChar, 150);
                //        OracleParameter surname1 = new OracleParameter("surname1", OracleDbType.Varchar2, 150);
                surname1.Direction = ParameterDirection.Input;
                surname1.Value = this.textBox2.Text;
                cmd.Parameters.Add(surname1);

                OracleParameter address = new OracleParameter("ADDRESS_OF_PERS1", OracleType.VarChar, 150);
                //      OracleParameter address = new OracleParameter("address", OracleDbType.Varchar2, 150);
                address.Direction = ParameterDirection.Input;
                address.Value = this.textBox5.Text;
                cmd.Parameters.Add(address);

                OracleParameter telephon = new OracleParameter("TELEPHONE1", OracleType.Int32, 150);
                //      OracleParameter telephon = new OracleParameter("telephon", OracleDbType.Int32, 150);
                telephon.Direction = ParameterDirection.Input;
                telephon.Value = this.textBox6.Text;
                cmd.Parameters.Add(telephon);

                OracleParameter email = new OracleParameter("E_MAIL1", OracleType.VarChar, 150);
                //      OracleParameter email = new OracleParameter("email", OracleDbType.Varchar2, 150);
                email.Direction = ParameterDirection.Input;
                email.Value = this.textBox7.Text;
                cmd.Parameters.Add(email);

                OracleParameter dateOfStart = new OracleParameter("DATE_ACCEPT1", OracleType.DateTime);
                dateOfStart.Direction = ParameterDirection.Input;
                dateOfStart.Value = dateTimePicker1.Value.Date;
                cmd.Parameters.Add(dateOfStart);

                OracleParameter ID_PERSPOST1 = new OracleParameter("id_timetable1", OracleType.Int32);
                ID_PERSPOST1.Direction = ParameterDirection.Input;
                ID_PERSPOST1.Value = indexPosition.Value;
                cmd.Parameters.Add(ID_PERSPOST1);


                OracleParameter name_position1 = new OracleParameter("name_position1", OracleType.VarChar);
                name_position1.Direction = ParameterDirection.Input;
                name_position1.Value = textBox4.Text;
                cmd.Parameters.Add(name_position1);

                OracleParameter salary1 = new OracleParameter("salary1", OracleType.Int32);
                salary1.Direction = ParameterDirection.Input;
                salary1.Value = Int32.Parse(textBox9.Text);
                cmd.Parameters.Add(salary1);

                OracleParameter age1 = new OracleParameter("age1", OracleType.Int32);
                age1.Direction = ParameterDirection.Input;
                age1.Value = Int32.Parse(textBox3.Text);
                cmd.Parameters.Add(age1);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Retrieving Oracle Sequence Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Переведено.");
                this.Close();
                PERSONAL personal = new PERSONAL();
                personal.Show();
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM elizabeth.position where name_position='" + comboBox2.Text + "'";
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

        private void button3_Click(object sender, EventArgs e)
        {

            OracleConnection conn = new OracleConnection(conn_string.Value);
            conn.Open();
            OracleCommand cmd = new OracleCommand("elizabeth.ADD_PERSONAL", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            OracleParameter name1 = new OracleParameter("name1", OracleType.VarChar, 150);
            // OracleParameter name1 = new OracleParameter("name1", OracleDbType.Varchar2, 150);
            name1.Direction = ParameterDirection.Input;
            name1.Value = this.textBox1.Text;
            cmd.Parameters.Add(name1);

            OracleParameter surname1 = new OracleParameter("surname1", OracleType.VarChar, 150);
            //OracleParameter surname1 = new OracleParameter("surname1", OracleDbType.Varchar2, 150);
            surname1.Direction = ParameterDirection.Input;
            surname1.Value = this.textBox2.Text;
            cmd.Parameters.Add(surname1);

            OracleParameter address = new OracleParameter("address", OracleType.VarChar, 150);
            //OracleParameter address = new OracleParameter("address", OracleDbType.Varchar2, 150);
            address.Direction = ParameterDirection.Input;
            address.Value = this.textBox5.Text;
            cmd.Parameters.Add(address);

            OracleParameter telephon = new OracleParameter("telephon", OracleType.Int32, 150);
            // OracleParameter telephon = new OracleParameter("telephon", OracleDbType.Int32,150);
            telephon.Direction = ParameterDirection.Input;
            telephon.Value = this.textBox6.Text;
            cmd.Parameters.Add(telephon);

            OracleParameter email = new OracleParameter("email", OracleType.VarChar, 150);
            //OracleParameter email = new OracleParameter("email", OracleDbType.Varchar2,150);
            email.Direction = ParameterDirection.Input;
            email.Value = this.textBox7.Text;
            cmd.Parameters.Add(email);

            ADD_WORKER a = new ADD_WORKER();

            OracleParameter dateOfStart = new OracleParameter("dateOfStart", OracleType.DateTime);
            //OracleParameter dateOfStart = new OracleParameter("dateOfStart", OracleDbType.Date);
            dateOfStart.Direction = ParameterDirection.Input;
            dateOfStart.Value = dateTimePicker1.Value.Date;
            //dateOfStart.Value = dateStart.Value;
            cmd.Parameters.Add(dateOfStart);

            OracleParameter id_position1 = new OracleParameter("id_position1", OracleType.Int32, 150);
            //OracleParameter id_position1 = new OracleParameter("id_position1", OracleDbType.Int32, 150);
            id_position1.Direction = ParameterDirection.Input;
            id_position1.Value = indexPosition.Value;
            cmd.Parameters.Add(id_position1);


            OracleParameter salary1 = new OracleParameter("salary1", OracleType.Int32, 150);
            //OracleParameter salary1 = new OracleParameter("salary1", OracleDbType.Int32, 150);
            salary1.Direction = ParameterDirection.Input;
            salary1.Value = Convert.ToString(textBox9.Text);
            //salary1.Value = salaryVal.Value;
            cmd.Parameters.Add(salary1);

            OracleParameter age1 = new OracleParameter("age1", OracleType.Int32, 150);
            //OracleParameter age1 = new OracleParameter("age1", OracleDbType.Int32, 150);
            age1.Direction = ParameterDirection.Input;
            age1.Value = textBox3.Text;
            cmd.Parameters.Add(age1);


            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Retrieving Oracle Sequence Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            main_menu n = new main_menu();
            this.Hide();
            n.Show();
        }

        private void textBox9_Validating(object sender, CancelEventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(textBox9.Text, out n);
            if (isNumeric != true)
                MessageBox.Show("НЕКОРЕКТНА ЗАРПЛАТНЯ!");
        }
    }
}