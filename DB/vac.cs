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
    public partial class vac : Form
    {
        public vac()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void vac_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'vACA.PVACATION' table. You can move, or remove it, as needed.
            this.pVACATIONTableAdapter.Fill(this.vACA.PVACATION);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                textBox2.Text = row.Cells[0].Value.ToString();
                textBox3.Text = row.Cells[1].Value.ToString();
                textBox4.Text = row.Cells[2].Value.ToString();
                textBox5.Text = row.Cells[3].Value.ToString();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OracleConnection conn = new OracleConnection(conn_string.Value);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            OracleCommand cmd = new OracleCommand("elizabeth.add_vacation", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (textBox1.Text == "")
            {
                MessageBox.Show("ЗАПОВНІТЬ ОБОВ'ЯЗКОВІ ПОЛЯ!");
            }
            else
            {
                OracleParameter ID_K_A1 = new OracleParameter("ID_K_A1", OracleType.Int32, 150);
                // OracleParameter ID_K_A1 = new OracleParameter("ID_K_A1", OracleDbType.Varchar2, 150);
                ID_K_A1.Direction = ParameterDirection.Input;
                ID_K_A1.Value = this.textBox5.Text;
                cmd.Parameters.Add(ID_K_A1);

                OracleParameter DATE1 = new OracleParameter("DATE1", OracleType.DateTime);
                //OracleParameter DATE1 = new OracleParameter("DATE1", OracleDbType.Date);
                DATE1.Direction = ParameterDirection.Input;
                DATE1.Value = dateTimePicker1.Value;
                cmd.Parameters.Add(DATE1);

                OracleParameter DATE2 = new OracleParameter("DATE2", OracleType.DateTime);
                //OracleParameter DATE2 = new OracleParameter("DATE2", OracleDbType.Date);
                DATE2.Direction = ParameterDirection.Input;
                DATE2.Value = dateTimePicker2.Value;
                cmd.Parameters.Add(DATE2);

                OracleParameter REASON1 = new OracleParameter("REASON1", OracleType.VarChar, 150);
                //OracleParameter email = new OracleParameter("email", OracleDbType.Varchar2,150);
                REASON1.Direction = ParameterDirection.Input;
                REASON1.Value = this.textBox1.Text;
                cmd.Parameters.Add(REASON1);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Retrieving Oracle Sequence Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("ВИХІДНИЙ ДОДАНИЙ.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'vACA.PVACATION' table. You can move, or remove it, as needed.
            this.pVACATIONTableAdapter.Fill(this.vACA.PVACATION);
            this.Hide();
            main_menu m = new main_menu();
            m.Show();
        }
        private OracleDataAdapter da;
        private OracleCommandBuilder cb;
        private DataSet ds;
        DataTable dt;
        private void button1_Click_1(object sender, EventArgs e)
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

       
    }
}
