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
    public partial class Orders : Form
    {
        DataBase dataBase = new DataBase();
        public Orders()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

            var ClientID = textBox_ClientID.Text;
            var ServiceName = textBox_ServiceName.Text;
            var Description = textBox_Description.Text;
            var Execution = textBox_Execution.Text;
            var Progress = textBox_Progress.Text;

            if(ClientID.Equals("")&&
               ServiceName.Equals("")&&
               Description.Equals("")&&
               Execution.Equals("")&&
               Progress.Equals(""))
            {
                MessageBox.Show("Запись не может быть сохранена, т.к. отсутствуют значения в некоторых полях",
                    "ОШИБКА!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                
                this.Close();
            }
            else
            {
                string InsertQuery = $"insert into Orders (ClientID, ServiceName, Descript, Execution, Progress)" +
                                     $"values ('{ClientID}', '{ServiceName}', '{Description}', '{Execution}', '{Progress}')";
                
                SqlCommand command = new SqlCommand(InsertQuery, dataBase.getConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Запись создана успешно", "Сохранение",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                this.Close();
            }
                

            
        }
    }
}
