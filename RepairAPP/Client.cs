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
    public partial class Client : Form
    {
        DataBase dataBase = new DataBase();
        public Client()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

            var FullName = textBox_FullName.Text;
            var Adress = textBox_Adress.Text;
            var Telephone = textBox_Telephone.Text;

            if(FullName.Equals("")&&
               Adress.Equals("")&&
               Telephone.Equals(""))
            {
                MessageBox.Show("Запись не может быть сохранена, т.к. отсутствуют значения в некоторых полях",
                   "ОШИБКА!",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);

                this.Close();
            }
            else
            {
                string InsertQuery = $"insert into Client(FullName, Adress, Telephone)" +
                                     $"values('{FullName}', '{Adress}', '{Telephone}')";

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
            textBox_FullName.Text = "";
            textBox_Adress.Text = "";
            textBox_Telephone.Text = "";
        }
    }
}
