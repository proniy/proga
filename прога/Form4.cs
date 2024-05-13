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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace прога
{
    public partial class Form4 : Form
    {
        private const string connectionString = "Data Source=DESKTOP-OBNESAI\\SQLEXPRESS;Initial Catalog=управление недвижимостью;Integrated Security=True";
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "управление_недвижимостьюDataSet9.Report". При необходимости она может быть перемещена или удалена.
            this.reportTableAdapter1.Fill(this.управление_недвижимостьюDataSet9.Report);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "управление_недвижимостьюDataSet7.Orders". При необходимости она может быть перемещена или удалена.
            this.ordersTableAdapter2.Fill(this.управление_недвижимостьюDataSet7.Orders);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "управление_недвижимостьюDataSet1.Property". При необходимости она может быть перемещена или удалена.
            this.propertyTableAdapter.Fill(this.управление_недвижимостьюDataSet1.Property);

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все три текстовых поля.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT MAX(Id) FROM Property", connection);
                int lastId = Convert.ToInt32(command.ExecuteScalar());

                command = new SqlCommand("INSERT INTO Property (Id, Name, Adress, Value) VALUES (@Id, @Name, @Adress, @Value)", connection);
                command.Parameters.AddWithValue("@Id", lastId + 1);
                command.Parameters.AddWithValue("@Name", textBox1.Text);
                command.Parameters.AddWithValue("@Adress", textBox2.Text);
                command.Parameters.AddWithValue("@Value", int.Parse(textBox3.Text));

                command.ExecuteNonQuery();
                MessageBox.Show("Новая строка добавлена в базу данных.");
            }
            this.propertyTableAdapter.Fill(this.управление_недвижимостьюDataSet1.Property);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все текстовые поля.");
                return;
            }

            int id = Convert.ToInt32(textBox4.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Property SET Name = @Name, Adress = @Adress, Value = @Value WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Name", textBox1.Text);
                command.Parameters.AddWithValue("@Adress", textBox2.Text);
                command.Parameters.AddWithValue("@Value", int.Parse(textBox3.Text));
                command.Parameters.AddWithValue("@Id", id);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Строка успешно обновлена в базе данных.");
                }
                else
                {
                    MessageBox.Show("Не удалось найти строку с указанным Id.");
                }
            }
            this.propertyTableAdapter.Fill(this.управление_недвижимостьюDataSet1.Property);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Пожалуйста, введите Id строки для удаления.");
                return;
            }

            int id = Convert.ToInt32(textBox4.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Property WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Строка успешно удалена из базы данных.");
                }
                else
                {
                    MessageBox.Show("Не удалось найти строку с указанным Id.");
                }
            }
            this.propertyTableAdapter.Fill(this.управление_недвижимостьюDataSet1.Property);
        }

        private void AddButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text) || string.IsNullOrWhiteSpace(textBox6.Text) || string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все три текстовых поля.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand("SELECT MAX(ID) FROM Orders", connection);
                int lastId = Convert.ToInt32(command1.ExecuteScalar());

                command1 = new SqlCommand("INSERT INTO Orders (ID, ID_Employee, ID_Property, ID_Client,Open_date,Closing_date) VALUES (@ID, @ID_Employee, @ID_Property, @ID_Client,@Open_date,@Closing_date)", connection);
                command1.Parameters.AddWithValue("@ID", lastId + 1);
                command1.Parameters.AddWithValue("@ID_Employee", textBox5.Text);
                command1.Parameters.AddWithValue("@ID_Property", textBox6.Text);
                command1.Parameters.AddWithValue("@ID_Client", textBox7.Text);
                command1.Parameters.AddWithValue("@Open_date", dateTimePicker1.Value);
                command1.Parameters.AddWithValue("@Closing_date", dateTimePicker2.Value);

                command1.ExecuteNonQuery();
                MessageBox.Show("Новая строка добавлена в базу данных.");
            }
            this.ordersTableAdapter2.Fill(this.управление_недвижимостьюDataSet7.Orders);
        }

        private void UpdateButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox8.Text) || string.IsNullOrWhiteSpace(textBox5.Text) || string.IsNullOrWhiteSpace(textBox6.Text) || string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все текстовые поля.");
                return;
            }
            int id = Convert.ToInt32(textBox8.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand("UPDATE Orders SET ID_Employee = @ID_Employee, ID_Property = @ID_Property, ID_Client = @ID_Client, Open_date = @Open_date, Closing_date = @Closing_date WHERE Id = @Id", connection);
                command1.Parameters.AddWithValue("@ID_Employee", textBox5.Text);
                command1.Parameters.AddWithValue("@ID_Property", textBox6.Text);
                command1.Parameters.AddWithValue("@ID_Client", textBox7.Text);
                command1.Parameters.AddWithValue("@Open_date", dateTimePicker1.Value);
                command1.Parameters.AddWithValue("@Closing_date", dateTimePicker2.Value);
                command1.Parameters.AddWithValue("@Id", id);

                int rowsAffected = command1.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Строка успешно обновлена в базе данных.");
                }
                else
                {
                    MessageBox.Show("Не удалось найти строку с указанным Id.");
                }
            }
            this.ordersTableAdapter2.Fill(this.управление_недвижимостьюDataSet7.Orders);
        }

        private void DeleteButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox8.Text))
            {
                MessageBox.Show("Пожалуйста, введите Id строки для удаления.");
                return;
            }

            int id = Convert.ToInt32(textBox8.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand("DELETE FROM Orders WHERE Id = @Id", connection);
                command1.Parameters.AddWithValue("@Id", id);

                int rowsAffected = command1.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Строка успешно удалена из базы данных.");
                }
                else
                {
                    MessageBox.Show("Не удалось найти строку с указанным Id.");
                }
            }
            this.ordersTableAdapter2.Fill(this.управление_недвижимостьюDataSet7.Orders);
        }



        private void AddButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox9.Text) || string.IsNullOrWhiteSpace(textBox10.Text) || string.IsNullOrWhiteSpace(textBox11.Text) || string.IsNullOrWhiteSpace(textBox12.Text) || string.IsNullOrWhiteSpace(textBox13.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все три текстовых поля.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT MAX(Id) FROM Report", connection);
                int lastId = Convert.ToInt32(command.ExecuteScalar());

                command = new SqlCommand("INSERT INTO Report (Id, ID_Order, ID_Client, ID_Employee, ID_Property,Order_details) VALUES (@Id, @ID_Order, @ID_Client, @ID_Employee, @ID_Property,@Order_details)", connection);
                command.Parameters.AddWithValue("@Id", lastId + 1);
                command.Parameters.AddWithValue("@ID_Order", textBox9.Text);
                command.Parameters.AddWithValue("@ID_Client", textBox10.Text);
                command.Parameters.AddWithValue("@ID_Employee", textBox11.Text);
                command.Parameters.AddWithValue("@ID_Property", textBox12.Text);
                command.Parameters.AddWithValue("@Order_details", textBox13.Text);

                command.ExecuteNonQuery();
                MessageBox.Show("Новая строка добавлена в базу данных.");
            }
            this.reportTableAdapter1.Fill(this.управление_недвижимостьюDataSet9.Report);
        }

        private void UpdateButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox14.Text) || string.IsNullOrWhiteSpace(textBox9.Text) || string.IsNullOrWhiteSpace(textBox10.Text) || string.IsNullOrWhiteSpace(textBox11.Text) || string.IsNullOrWhiteSpace(textBox12.Text) || string.IsNullOrWhiteSpace(textBox13.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все текстовые поля.");
                return;
            }

            int id = Convert.ToInt32(textBox14.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Report SET ID_Order = @ID_Order, ID_Client = @ID_Client, ID_Employee = @ID_Employee, ID_Property = @ID_Property, Order_details = @Order_details WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@ID_Order", textBox9.Text);
                command.Parameters.AddWithValue("@ID_Client", textBox10.Text);
                command.Parameters.AddWithValue("@ID_Employee", textBox11.Text);
                command.Parameters.AddWithValue("@ID_Property", textBox12.Text);
                command.Parameters.AddWithValue("@Order_details", textBox13.Text);
                command.Parameters.AddWithValue("@Id", id);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Строка успешно обновлена в базе данных.");
                }
                else
                {
                    MessageBox.Show("Не удалось найти строку с указанным Id.");
                }
            }
            this.reportTableAdapter1.Fill(this.управление_недвижимостьюDataSet9.Report);
        }

        private void DeleteButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox14.Text))
            {
                MessageBox.Show("Пожалуйста, введите Id строки для удаления.");
                return;
            }

            int id = Convert.ToInt32(textBox14.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Report WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Строка успешно удалена из базы данных.");
                }
                else
                {
                    MessageBox.Show("Не удалось найти строку с указанным Id.");
                }
            }
            this.reportTableAdapter1.Fill(this.управление_недвижимостьюDataSet9.Report);
        }


    } 
}
