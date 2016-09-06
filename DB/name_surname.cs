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
using Oracle.DataAccess.Types;
using System.Data.SqlTypes;
using System.Data.OracleClient;

namespace BD1
{
    public partial class name_surname : Form
    {
        public name_surname()
        {
            InitializeComponent();
            
        }
         
        private void name_surname_Load(object sender, EventArgs e){}

        private void textBox4_TextChanged(object sender, EventArgs e){}

        private void textBox3_TextChanged(object sender, EventArgs e){}
        
        public void button1_Click(object sender, EventArgs e)
        {
            //string c = "Data Source = localhost;" + "User ID = " + "elizabeth" + ";Password=" + "12345";
            //  OracleConnection conn = new OracleConnection(c); 
            OracleConnection conn = new OracleConnection(conn_string.Value);
            try
            {
                conn.Open();
             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            OracleCommand cmd = new OracleCommand("elizabeth.ADD_PERSONAL", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (textBox3.Text == ""|| textBox4.Text == "" || textBox6.Text == "" || textBox2.Text=="")
            {
                MessageBox.Show("ЗАПОВНІТЬ ОБОВ'ЯЗКОВІ ПОЛЯ!");
            }
            else
            {
                OracleParameter NAME_OF_PERS1 = new OracleParameter("NAME_OF_PERS1", OracleType.VarChar, 150);
                // OracleParameter NAME_OF_PERS1 = new OracleParameter("NAME_OF_PERS1", OracleDbType.Varchar2, 150);
                NAME_OF_PERS1.Direction = ParameterDirection.Input;
                NAME_OF_PERS1.Value = this.textBox3.Text;
                cmd.Parameters.Add(NAME_OF_PERS1);

                OracleParameter SURNAME_OF_PERS1 = new OracleParameter("SURNAME_OF_PERS1", OracleType.VarChar, 150);
                //OracleParameter NAME_OF_PERS1 = new OracleParameter("SURNAME_OF_PERS1", OracleDbType.Varchar2, 150);
                SURNAME_OF_PERS1.Direction = ParameterDirection.Input;
                SURNAME_OF_PERS1.Value = this.textBox4.Text;
                cmd.Parameters.Add(SURNAME_OF_PERS1);

                OracleParameter ADDRESS_OF_PERS1 = new OracleParameter("ADDRESS_OF_PERS1", OracleType.VarChar, 150);
                //OracleParameter ADDRESS_OF_PERS1 = new OracleParameter("ADDRESS_OF_PERS1", OracleDbType.Varchar2, 150);
                ADDRESS_OF_PERS1.Direction = ParameterDirection.Input;
                ADDRESS_OF_PERS1.Value = this.textBox1.Text;
                cmd.Parameters.Add(ADDRESS_OF_PERS1);

                OracleParameter TELEPHONE1 = new OracleParameter("TELEPHONE1", OracleType.Int32, 150);
                // OracleParameter telephon = new OracleParameter("TELEPHONE1", OracleDbType.Int32,150);
                TELEPHONE1.Direction = ParameterDirection.Input;
                try
                {
                    TELEPHONE1.Value = this.textBox2.Text;
                }
                catch (Exception ex) { }
                cmd.Parameters.Add(TELEPHONE1);

                OracleParameter E_MAIL1 = new OracleParameter("E_MAIL1", OracleType.VarChar, 150);
                //OracleParameter email = new OracleParameter("email", OracleDbType.Varchar2,150);
                E_MAIL1.Direction = ParameterDirection.Input;
                E_MAIL1.Value = this.textBox5.Text;
                cmd.Parameters.Add(E_MAIL1);

                ADD_WORKER a = new ADD_WORKER();

                OracleParameter DATE_ACCEPT1 = new OracleParameter("DATE_ACCEPT1", OracleType.DateTime);
                //OracleParameter dateOfStart = new OracleParameter("DATE_ACCEPT1", OracleDbType.Date);
                DATE_ACCEPT1.Direction = ParameterDirection.Input;
                DATE_ACCEPT1.Value = dateStart.Value;
                cmd.Parameters.Add(DATE_ACCEPT1);

                OracleParameter id_timetable1 = new OracleParameter("id_timetable1", OracleType.Int32, 150);
                //OracleParameter id_timetable1 = new OracleParameter("id_timetable1", OracleDbType.Int32, 150);
                id_timetable1.Direction = ParameterDirection.Input;
                id_timetable1.Value = indexPosition.Value;
                cmd.Parameters.Add(id_timetable1);


                OracleParameter salary1 = new OracleParameter("salary1", OracleType.Int32, 150);
                //OracleParameter salary1 = new OracleParameter("salary1", OracleDbType.Int32, 150);
                salary1.Direction = ParameterDirection.Input;
                salary1.Value = salaryVal.Value;
                cmd.Parameters.Add(salary1);

                OracleParameter age1 = new OracleParameter("age1", OracleType.Int32, 150);
                //OracleParameter age1 = new OracleParameter("age1", OracleDbType.Int32, 150);
                age1.Direction = ParameterDirection.Input;
                try
                {
                    age1.Value = textBox6.Text;
                }
                catch (Exception ex) { }
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
        }
        private void textBox1_TextChanged(object sender, EventArgs e){}
        private void textBox2_TextChanged(object sender, EventArgs e){}
        private void textBox6_TextChanged(object sender, EventArgs e){}
        private void textBox5_TextChanged(object sender, EventArgs e){}

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(textBox2.Text, out n);
            if (isNumeric != true)
                MessageBox.Show("НЕКОРЕКТНИЙ ТЕЛЕФОН!");
        }
        private void textBox6_Validating(object sender, CancelEventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(textBox6.Text, out n);
            if (isNumeric != true)
                MessageBox.Show("НЕКОРЕКТНИЙ ВІК!");
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            
        }

    }
}