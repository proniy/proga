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
    public partial class Form1 : Form
    {
        private const string connectionString = "Data Source=DESKTOP-OBNESAI\\SQLEXPRESS;Initial Catalog=управление недвижимостью;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Visible = false;
            var from = new Form2();
            from.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            bool isAuthenticated = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT idemployee FROM LOGPASS WHERE Login = @Login AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Password", password);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        isAuthenticated = true;
                        int employeeId = reader.GetInt32(0);

                        this.Hide();
                        if (employeeId == 1) 
                        {
                            Form4 form4 = new Form4();
                            form4.Show();
                        }
                        else
                        {
                            Form3 form3 = new Form3();
                            form3.Show();
                        }
                        break;
                    }
                }

                if (!isAuthenticated)
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
            }
        }
    }
}
