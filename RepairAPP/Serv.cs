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
    public partial class Serv : Form
    {
        DataBase dataBase = new DataBase();
        public Serv()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

            var ServiceName = textBox_ServiceName.Text;
            var Price = textBox_Price.Text;

            if(ServiceName.Equals("") && Price.Equals(""))
            {
                MessageBox.Show("Запись не может быть сохранена, т.к. отсутствуют значения в некоторых полях",
                   "ОШИБКА!",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);

                this.Close();
            }
            else
            {
                string InsertQuery = $"insert into serv(ServiceName, Price)" +
                                     $"values('{ServiceName}', '{Price}')";


                SqlCommand command = new SqlCommand(InsertQuery, dataBase.getConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Запись создана успешно", "Сохранение",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                this.Close();
            }

            dataBase.closeConnection();
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            textBox_ServiceName.Text = "";
            textBox_Price.Text = "";
        }
    }
}
