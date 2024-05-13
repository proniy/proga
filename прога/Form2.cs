using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace прога
{
    public partial class Form2 : Form
    {
        private const string connectionString = "Data Source=DESKTOP-OBNESAI\\SQLEXPRESS;Initial Catalog=управление недвижимостью;Integrated Security=True";
        private SqlConnection connection;
        public Form2()
        {
            InitializeComponent();
            string connectionString = @"Data Source=DESKTOP-OBNESAI\\SQLEXPRESS;Initial Catalog=управление недвижимостью;Integrated Security=True";
            connection = new SqlConnection(connectionString);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Visible= false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != ""&& textBox6.Text != ""&& textBox7.Text != "")
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT TOP 1 Id FROM Client ORDER BY Id DESC", connection);
                    int lastId = Convert.ToInt32(command.ExecuteScalar()) + 1;

                    command = new SqlCommand("INSERT INTO Client (Id, Surname, Firstname, Lastname, telephonnumber, adress) VALUES (@Id, @Surname, @Firstname, @Lastname, @telephonnumber, @adress)", connection);
                    command.Parameters.AddWithValue("@Id", lastId);
                    command.Parameters.AddWithValue("@Surname", textBox1.Text);
                    command.Parameters.AddWithValue("@Firstname", textBox2.Text);
                    command.Parameters.AddWithValue("@Lastname", textBox3.Text);
                    command.Parameters.AddWithValue("@telephonnumber", textBox4.Text);
                    command.Parameters.AddWithValue("@adress", textBox5.Text);
                    command.ExecuteNonQuery();

                    connection.Close();
                    MessageBox.Show("Данные успешно добавлены в базу данных!");
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO LOGPASS (Login, Password, IDclient, idemployee) VALUES (@Login, @Password, @IDclient, @idemployee)";
                    SqlCommand command = new SqlCommand(insertQuery, connection);

                    command.Parameters.AddWithValue("@Login", textBox6.Text);
                    command.Parameters.AddWithValue("@Password", textBox7.Text);
                    command.Parameters.AddWithValue("@IDclient", 1);
                    command.Parameters.AddWithValue("@idemployee", 0);

                    command.ExecuteNonQuery();

                    connection.Close();
                    MessageBox.Show("Данные успешно добавлены в базу данных!");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все текстовые поля.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}

