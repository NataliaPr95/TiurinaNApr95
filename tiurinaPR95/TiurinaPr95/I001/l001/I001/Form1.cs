using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace I001
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        public void GetTable()
        {
            string constr = @"Data Source=.\SQLEXPRESS;Initial Catalog=I001;User ID=student;Password=Passw0rd";
            SqlConnection conn = new SqlConnection(constr);
            SqlCommand com = new SqlCommand("SELECT * FROM Users", conn);
            DataTable dt = new DataTable();

            conn.Open();
            SqlDataReader sdr = com.ExecuteReader();
            dt.Load(sdr);
            conn.Close();

            DG.DataSource = dt;
        }
        private void select_Click(object sender, EventArgs e)
        {
            GetTable();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (id.Text != "" && Nam.Text != "" && login.Text != "" && pass.Text != "")
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=I001;User ID=student;Password=Passw0rd"))//строка подключения
                {
                    try
                    {
                        con.Open();
                        SqlCommand com = con.CreateCommand();
                        com.CommandText = $"INSERT INTO Users (id, name, login, password, isadmin) VALUES('" + id.Text + "','" + Nam.Text + "','" + login.Text + "','" + pass.Text + "', @adm)";
                        if (checkBox1.Checked == true)
                        {
                            com.Parameters.AddWithValue("@adm", checkBox1.Checked == true);

                        }
                        else
                        {
                            com.Parameters.AddWithValue("@adm", checkBox1.Checked == false);
                        }
                        com.ExecuteNonQuery();
                        GetTable();//обновление таблицы
                        MessageBox.Show("Добавить удалось!");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        MessageBox.Show("Добавить не удалось!");

                    }
                    con.Close();
                }
            }
            else
                MessageBox.Show("Введите данные!!!");
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите удалить?", "Удалить", MessageBoxButtons.YesNo);//сообщение для подтверждения удаления
            if (dialogResult == DialogResult.Yes)
            {
                if (id.Text != "")
                    using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=I001;User ID=student;Password=Passw0rd"))//строка подключения
                    {
                        try
                        {
                            con.Open();
                            SqlCommand com = con.CreateCommand();
                            com.CommandText = $"DELETE FROM Users WHERE id = @id";//запрос удаления 
                            com.Parameters.AddWithValue(@"id", id.Text);
                            com.ExecuteNonQuery();
                            GetTable();//обновление таблицы
                            MessageBox.Show("Запись удалена");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        con.Close();
                    }
                else
                    MessageBox.Show("Введите данные!!");
                clear(); 
            }
            else 
            if (dialogResult == DialogResult.No)
            {
                clear();
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (id.Text != "" && Nam.Text != "" && login.Text != "" && pass.Text != "")
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=I001;User ID=student;Password=Passw0rd"))//строка подключения
                {
                    try
                    {
                        con.Open();
                        SqlCommand com = con.CreateCommand();
                        //Обновление информации
                        com.CommandText = $"Update Users SET id='{id.Text}', name='{Nam.Text}', login='{login.Text}', password='{pass.Text}', isadmin=@adm Where id='{id.Text}'";
                        if (checkBox1.Checked == true)
                        {
                            com.Parameters.AddWithValue("@adm", checkBox1.Checked == true);

                        }
                        else
                        {
                            com.Parameters.AddWithValue("@adm", checkBox1.Checked == false);
                        }
                        com.ExecuteNonQuery();
                        GetTable();//обновление таблицы
                        MessageBox.Show("Запись обновлена");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        MessageBox.Show("Обновить запись не удалось!");
                    }
                    
                    con.Close();
                }
            }
            else
                MessageBox.Show("Введите данные!!!");
        }

        private void DG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id.Text = DG.CurrentRow.Cells[0].Value.ToString();
            Nam.Text = DG.CurrentRow.Cells[1].Value.ToString();
            login.Text = DG.CurrentRow.Cells[2].Value.ToString();
            pass.Text = DG.CurrentRow.Cells[3].Value.ToString();
            if (DG.CurrentRow.Cells[4].Value.ToString() == "" && DG.CurrentRow.Cells[3].Value.ToString()=="" && DG.CurrentRow.Cells[2].Value.ToString()=="" && DG.CurrentRow.Cells[1].Value.ToString() == "" && DG.CurrentRow.Cells[0].Value.ToString() == "") {
                id.Clear();
                Nam.Clear();
                login.Clear();
                pass.Clear();
                checkBox1.Checked = false;
            }
            else
            if((bool)(DG.CurrentRow.Cells[4].Value) == true){
                checkBox1.Checked = true;
            }
            else
                checkBox1.Checked = false;
        }

        public void clear()
        {
            id.Clear();
            Nam.Clear();
            login.Clear();
            pass.Clear();
            checkBox1.Checked = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
    }

