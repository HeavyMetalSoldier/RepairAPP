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


namespace RepairAPP
{
    public partial class sign_up : Form
    {
        DataBase dataBase = new DataBase();
        public sign_up()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        public void sign_up_Load(object sender, EventArgs e)
        {
            textBox_login2.MaxLength = 50;
            textBox_password2.MaxLength = 50;
        }

        private void buttonEnter2_Click(object sender, EventArgs e)
        {
            var Login = textBox_login2.Text;
            var Password = textBox_password2.Text;

            string signQuery = $"insert into Register(UserLogin, UserPassword) values('{Login}', '{Password}') ";
            SqlCommand command = new SqlCommand(signQuery, dataBase.getConnection());
            dataBase.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Вы успешно зарегистрировались");
                log_in loginform = new log_in();
                loginform.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("ОШИБКА");
            }

            dataBase.closeConnection();
        }

        private Boolean Check()
        {
            var login = textBox_login2.Text;
            var password = textBox_password2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            string signupQuery = $"select UserID, UserLogin, UserPassword from Register where UserLogin = '{login}' and UserPassword = '{password}'";

            SqlCommand command = new SqlCommand(signupQuery, dataBase.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            if(dataTable.Rows.Count > 0)
            {
                MessageBox.Show("Такой пользователь уже существует");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
