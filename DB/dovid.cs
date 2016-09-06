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
    public partial class dovid : Form
    {
        public dovid()
        {
            InitializeComponent();
            FillCombo();
        }
        public void FillCombo()
        {
            string query = "SELECT * FROM elizabeth.POSITION";
            OracleConnection conn = new OracleConnection(conn_string.Value);
            OracleCommand cmd = new OracleCommand(query, conn);
            OracleDataReader myReader;
            try
            {
                conn.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string sname = myReader.GetString(myReader.GetOrdinal("NAME_POSITION"));
                    comboBox1.Items.Add(sname);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void dovid_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet3.VIEW1' table. You can move, or remove it, as needed.
            this.vIEW1TableAdapter1.Fill(this.dataSet3.VIEW1);
            // TODO: This line of code loads data into the 'dataSet2.VAKANSII' table. You can move, or remove it, as needed.
            this.vAKANSIITableAdapter.Fill(this.dataSet2.VAKANSII);
            // TODO: This line of code loads data into the 'dataSet1.VIEW1' table. You can move, or remove it, as needed.
            this.vIEW1TableAdapter.Fill(this.dataSet1.VIEW1);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            main_menu m = new main_menu();
            m.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            changeId.Value = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            string sql = "select * from elizabeth.VIEW1 where id_k_a='"+changeId.Value+"'";
            OracleConnection conn = new OracleConnection(conn_string.Value);
            OracleCommand cmd = new OracleCommand(sql, conn);
            OracleDataReader myReader;
            
            UPDATE u = new UPDATE();

            try
            {
                conn.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string sname = myReader.GetString(myReader.GetOrdinal("NAME_OF_PERS"));
                    string ssurname = myReader.GetString(myReader.GetOrdinal("SURNAME_OF_PERS"));
                    string age = Convert.ToString(myReader.GetInt32(myReader.GetOrdinal("AGE")));
                    string sAddress = myReader.GetString(myReader.GetOrdinal("address_of_pers"));
                    string sTelephone = Convert.ToString(myReader.GetInt32(myReader.GetOrdinal("telephone")));
                    string sMail = myReader.GetString(myReader.GetOrdinal("e_mail"));
                    string sSalary = Convert.ToString(myReader.GetInt32(myReader.GetOrdinal("salary")));
                    string sDate = Convert.ToString(myReader.GetDateTime(myReader.GetOrdinal("date_accept")));
                    string spos = myReader.GetString(myReader.GetOrdinal("NAME_POSITION"));
                    DateTime d = myReader.GetDateTime(myReader.GetOrdinal("date_accept"));

                   // int idpos = int.Parse(myReader.GetString(myReader.GetOrdinal("id_position")));
                    {
                        string query = "SELECT * FROM elizabeth.POSITION where name_position='" + spos + "'";
                        OracleCommand cmd1 = new OracleCommand(query, conn);
                        //OracleDataReader myReader;
                        try
                        {
                         //   conn.Open();
                            myReader = cmd1.ExecuteReader();
                            while (myReader.Read())
                            {
                                string sid = myReader.GetInt32(myReader.GetOrdinal("ID_POSITION")).ToString();
                                indexPosition.Value = int.Parse(sid);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    u.textBox1.Text = sname;
                    u.textBox2.Text = ssurname;
                    u.textBox3.Text = age;
                    u.comboBox2.Text = spos;
                    u.textBox5.Text = sAddress;
                    u.textBox6.Text = sTelephone;
                    u.textBox7.Text = sMail;
                    u.textBox9.Text = sSalary;
                    u.dateTimePicker2.Value = d;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            u.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM elizabeth.POSITION where name_position='" + comboBox1.Text + "'";
            OracleConnection conn = new OracleConnection(conn_string.Value);
            //OracleConnection conn = new OracleConnection("Data Source = localhost; User ID =  elizabeth ;Password=12345");
            OracleCommand cmd = new OracleCommand(query, conn);
            OracleDataReader myReader;
            try
            {
                conn.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string sid = myReader.GetInt32(myReader.GetOrdinal("ID_POSITION")).ToString();
                    indexPosition.Value = int.Parse(sid);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private OracleDataAdapter da;
        private OracleCommandBuilder cb;
        private DataSet ds;
        DataTable dt;
        private void button2_Click(object sender, EventArgs e)
        {
            DATAIL D = new DATAIL();
            OracleConnection conn = new OracleConnection(conn_string.Value);
            conn.Open();

            string sql = "SELECT * FROM elizabeth.VIEW1 WHERE name_position = '" + comboBox1.Text + "'";

            OracleCommand cmd = new OracleCommand(sql, conn);
            da = new OracleDataAdapter(cmd);
            cb = new OracleCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds);
            DataTable ret = new DataTable();
            OracleDataReader dr = cmd.ExecuteReader();
            ret.Load(dr);
            dr.Close();
            D.dataGridView1.DataSource = ret;


            D.dataGridView1.Columns[0].Visible=false;
            D.dataGridView1.Columns[1].HeaderText = "ІМ'Я";
            D.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            D.dataGridView1.Columns[2].HeaderText = "ПРІЗВИЩЕ";
            D.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            D.dataGridView1.Columns[3].HeaderText = "ВІК";
            D.dataGridView1.Columns[4].HeaderText = "АДРЕСА";
            D.dataGridView1.Columns[5].HeaderText = "ТЕЛЕФОН";
            D.dataGridView1.Columns[6].HeaderText = "ПОШТА";
            D.dataGridView1.Columns[7].HeaderText = "ЗАРПЛАТНЯ";
            D.dataGridView1.Columns[8].Visible = false;
            D.dataGridView1.Columns[9].HeaderText = "ПОСАДА";
            D.dataGridView1.Columns[10].HeaderText = "ДАТА ПРИЙОМУ";
            D.dataGridView1.Columns[11].Visible = false;

            D.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DATAIL D = new DATAIL();
            OracleConnection conn = new OracleConnection(conn_string.Value);
            conn.Open();
            string sql = "SELECT * FROM elizabeth.VACATIONS";
            OracleCommand cmd = new OracleCommand(sql, conn);
            da = new OracleDataAdapter(cmd);
            cb = new OracleCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds);
            DataTable ret = new DataTable();
            OracleDataReader dr = cmd.ExecuteReader();
            ret.Load(dr);
            dr.Close();
            D.dataGridView1.DataSource = ret;
            D.dataGridView1.Columns[0].Visible = false;
            D.dataGridView1.Columns[1].Visible = false;

            D.dataGridView1.Columns[2].HeaderText = "ІМ'Я";
            D.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            D.dataGridView1.Columns[3].HeaderText = "ПРІЗВИЩЕ";
            D.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            D.dataGridView1.Columns[4].Visible = false;
            D.dataGridView1.Columns[5].Visible = false;

            D.dataGridView1.Columns[6].HeaderText = "ПОСАДА";
            D.dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            D.dataGridView1.Columns[7].HeaderText = "ПРИЧИНА ВИХІДНОГО";
            D.dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            D.dataGridView1.Columns[8].HeaderText = "ПОЧАТОК ВИХІДНОГО";
            D.dataGridView1.Columns[9].HeaderText = "КІНЕЦЬ ВИХІДНОГО";
            D.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DATAIL D = new DATAIL();
            OracleConnection conn = new OracleConnection(conn_string.Value);
            conn.Open();

            string d1 = dateTimePicker1.Value.ToString("dd.MMM.yyyy");
            string d2 = dateTimePicker2.Value.ToString("dd.MMM.yyyy");

            string sql = "SELECT * FROM elizabeth.EX_P where  date_ACCEPT >= '" + d1 + "' AND" +
                          "  (date_FIRE   <= '" + d2 + "' or (date_fire is null))";


            OracleCommand cmd = new OracleCommand(sql, conn);
            da = new OracleDataAdapter(cmd);
            cb = new OracleCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds);
            DataTable ret = new DataTable();
            OracleDataReader dr = cmd.ExecuteReader();
            ret.Load(dr);
            dr.Close();
            D.dataGridView1.DataSource = ret;
            D.dataGridView1.Columns[0].Visible=false;
            D.dataGridView1.Columns[1].HeaderText = "ІМ'Я";
            D.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            D.dataGridView1.Columns[2].HeaderText = "ПРІЗВИЩЕ";
            D.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            D.dataGridView1.Columns[3].HeaderText = "ВІК";
            D.dataGridView1.Columns[4].HeaderText = "АДРЕСА";
            D.dataGridView1.Columns[5].HeaderText = "ТЕЛЕФОН";
            D.dataGridView1.Columns[6].HeaderText = "ПОШТА";
            D.dataGridView1.Columns[7].HeaderText = "ЗАРПЛАТНЯ";
            D.dataGridView1.Columns[8].HeaderText = "КОЛИШНЯ ПОСАДА";
            D.dataGridView1.Columns[9].HeaderText = "ДАТА ПРИЙОМУ";
            D.dataGridView1.Columns[10].HeaderText = "ДАТА ЗВІЛЬНЕННЯ";
            D.dataGridView1.Columns[11].Visible=false;

            D.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Reporte r = new Reporte();
            r.Show();
        }

    }
}
