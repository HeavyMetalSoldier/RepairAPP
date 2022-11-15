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
    public partial class Document : Form
    {
        DataBase dataBase = new DataBase();
        public Document()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

            var ClientID = textBox_ClientID.Text;
            var ClientName = textBox_ClientName.Text;
            var OrderID = textBox_OrderID.Text;
            var Total = textBox_Total.Text;

            if(ClientID.Equals("")&&
               ClientName.Equals("")&&
               OrderID.Equals("")&&
               Total.Equals(""))
            {
                MessageBox.Show("Запись не может быть сохранена, т.к. отсутствуют значения в некоторых полях",
                   "ОШИБКА!",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);

                this.Close();
            }
            else
            {
                string InsertQuery = $"insert into Document(ClientID, ClientName, OrderID, Total)" +
                                     $"values('{ClientID}', '{ClientName}', '{OrderID}', '{Total}')";

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
            textBox_ClientID.Text = "";
            textBox_ClientName.Text = "";
            textBox_OrderID.Text = "";
            textBox_Total.Text = "";
        }
    }
}
