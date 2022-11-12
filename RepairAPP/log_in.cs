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
using RepairAPP;

namespace RepairAPP
{
    public partial class log_in : Form
    {
        DataBase dataBase = new DataBase();
        public log_in()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void log_in_Load(object sender, EventArgs e)
        {
            textBox_login.MaxLength = 50;
            textBox_password.MaxLength = 50;
            textBox_password.PasswordChar = '*';
        }

      

        private void signuplink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sign_up formsign = new sign_up();
            formsign.Show();
            this.Hide();
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            var UserLogin = textBox_login.Text;
            var UserPassword = textBox_password.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            string loginQuery = $"select UserID, UserLogin, UserPassword from Register where UserLogin = '{UserLogin}' and UserPassword = '{UserPassword}'";
            SqlCommand command = new SqlCommand(loginQuery, dataBase.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count == 1)
            {
                MessageBox.Show("Вы успешно вошли");
                FormMain formMain = new FormMain();
                this.Hide();
                formMain.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Введен неправильный логин или пароль");
            }
        }
    }
}