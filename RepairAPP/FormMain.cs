using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RepairAPP
{
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }

    public partial class FormMain : Form
    {
        DataBase dataBase = new DataBase();
        int SelectedRow;
        public FormMain()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void CreateColumns()
        {
            Orders_dataGridView.Columns.Add("ID", "Номер заказа");
            Orders_dataGridView.Columns.Add("ClientID", "ID клиента");
            Orders_dataGridView.Columns.Add("ServiceName", "Название услуги");
            Orders_dataGridView.Columns.Add("Descript", "Описание");
            Orders_dataGridView.Columns.Add("OrderDate", "Дата заказа");
            Orders_dataGridView.Columns.Add("Execution", "Срок выполнения");
            Orders_dataGridView.Columns.Add("Progress", "Прогресс");
            Orders_dataGridView.Columns.Add("IsNew", String.Empty);

            Client_dataGridView.Columns.Add("ID", "ID клиента");
            Client_dataGridView.Columns.Add("FullName", "ФИО");
            Client_dataGridView.Columns.Add("Adress", "Адрес");
            Client_dataGridView.Columns.Add("Telephone", "Номер телефона");
            Client_dataGridView.Columns.Add("IsNew", String.Empty);

            Serv_dataGridView.Columns.Add("ServiceName", "Название услуги");
            Serv_dataGridView.Columns.Add("Price", "Цена");
            Serv_dataGridView.Columns.Add("ID", "ID");
            Serv_dataGridView.Columns.Add("IsNew", String.Empty);

            Document_dataGridView.Columns.Add("ID", "Номер договора");
            Document_dataGridView.Columns.Add("ClientID", "ID клиента");
            Document_dataGridView.Columns.Add("ClientName", "ФИО клиента");
            Document_dataGridView.Columns.Add("OrderID", "Номер заказа");
            Document_dataGridView.Columns.Add("Total", "Сумма итого");
            Document_dataGridView.Columns.Add("DocumentDate", "Дата");
            Document_dataGridView.Columns.Add("IsNew", String.Empty);
        }

        private void ReadSingleRow(DataGridView dataGrid, IDataRecord record, string tablename)
        {
            switch (tablename)
            {
                case "Orders":
                    dataGrid.Rows.Add(record.GetInt32(0),
                                      record.GetInt32(1),
                                      record.GetString(2),
                                      record.GetString(3),
                                      record.GetDateTime(4),
                                      record.GetDateTime(5),
                                      record.GetString(6),
                                      RowState.ModifiedNew);
                    break;

                case "Client":
                    dataGrid.Rows.Add(record.GetInt32(0),
                                     record.GetString(1),
                                     record.GetString(2),
                                     record.GetString(3),
                                     RowState.ModifiedNew);
                    break;

                case "Serv":
                    dataGrid.Rows.Add(record.GetString(0),
                                      record.GetDecimal(1),
                                      record.GetInt32(2),
                                      RowState.ModifiedNew);
                    break;

                case "Document":
                    dataGrid.Rows.Add(record.GetInt32(0),
                                      record.GetInt32(1),
                                      record.GetString(2),
                                      record.GetInt32(3),
                                      record.GetDecimal(4),
                                      record.GetDateTime(5),
                                      RowState.ModifiedNew);
                    break;
            }
        }

        string OrderQuery = $"select * from Orders";
        string ClientQuery = $"select * from Client";
        string ServQuery = $"select * from Serv";
        string DocumentQuery = $"select * from Document";
        string Orders = "Orders";
        string Client = "Client";
        string Serv = "Serv";
        string Document = "Document";


        private void RefreshDataGrid(DataGridView dataGrid, string QueryString, string tablename)
        {
            dataGrid.Rows.Clear();

            SqlCommand command = new SqlCommand(QueryString, dataBase.getConnection());
            dataBase.openConnection();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dataGrid, reader, tablename);
            }
            reader.Close();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(Orders_dataGridView, OrderQuery, Orders);
            RefreshDataGrid(Client_dataGridView, ClientQuery, Client);
            RefreshDataGrid(Serv_dataGridView, ServQuery, Serv);
            RefreshDataGrid(Document_dataGridView, DocumentQuery, Document);
        }

        private void Orders_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = e.RowIndex;

            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = Orders_dataGridView.Rows[SelectedRow];

                Order_textBox_ID.Text = row.Cells[0].Value.ToString();
                Order_textBox_ClientID.Text = row.Cells[1].Value.ToString();
                Order_textBox_ServiceName.Text = row.Cells[2].Value.ToString();
                Order_textBox_Descript.Text = row.Cells[3].Value.ToString();
                Order_textBox_OrderDate.Text = row.Cells[4].Value.ToString();
                Order_textBox_Execution.Text = row.Cells[5].Value.ToString();
                Order_textBox_Progress.Text = row.Cells[6].Value.ToString();
            }
        }

        private void Client_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = e.RowIndex;

            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = Client_dataGridView.Rows[SelectedRow];

                Client_textBox_ID.Text = row.Cells[0].Value.ToString();
                Client_textBox_FullName.Text = row.Cells[1].Value.ToString();
                Client_textBox_Adress.Text = row.Cells[2].Value.ToString();
                Client_textBox_Telephone.Text = row.Cells[3].Value.ToString();
            }
        }

        private void Serv_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = Serv_dataGridView.Rows[SelectedRow];

                Serv_textBox_ServiceName.Text = row.Cells[0].Value.ToString();
                Serv_textBox_Price.Text = row.Cells[1].Value.ToString();
                Serv_textBox_ID.Text = row.Cells[2].Value.ToString();
            }
        }

        private void Document_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = Document_dataGridView.Rows[SelectedRow];

                Document_textBox_ID.Text = row.Cells[0].Value.ToString();
                Document_textBox_ClientID.Text = row.Cells[1].Value.ToString();
                Document_textBox_ClientName.Text = row.Cells[2].Value.ToString();
                Document_textBox_OrderID.Text = row.Cells[3].Value.ToString();
                Document_textBox_Total.Text = row.Cells[4].Value.ToString();
                Document_textBox_DocumentDate.Text = row.Cells[5].Value.ToString();
            }
        }
        
        private void Order_button_Refresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(Orders_dataGridView, OrderQuery, Orders);
        }

        private void Client_button_Refresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(Client_dataGridView, ClientQuery, Client);
        }

        private void Serv_button_Refresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(Serv_dataGridView, ServQuery, Serv);
        }

        private void Document_button_Refresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(Document_dataGridView, DocumentQuery, Document);
        }

        private void Order_button_Clear_Click(object sender, EventArgs e)
        {
            Order_textBox_ID.Text = "";
            Order_textBox_ClientID.Text = "";
            Order_textBox_ServiceName.Text = "";
            Order_textBox_Descript.Text = "";
            Order_textBox_OrderDate.Text = "";
            Order_textBox_Execution.Text = "";
            Order_textBox_Progress.Text = "";
        }

        private void Client_button_Clear_Click(object sender, EventArgs e)
        {
            Client_textBox_ID.Text = "";
            Client_textBox_FullName.Text = "";
            Client_textBox_Adress.Text = "";
            Client_textBox_Telephone.Text = "";
        }

        private void Serv_button_Clear_Click(object sender, EventArgs e)
        {
            Serv_textBox_ServiceName.Text = "";
            Serv_textBox_Price.Text = "";
            Serv_textBox_ID.Text = "";
        }

        private void Document_button_Clear_Click(object sender, EventArgs e)
        {
            Document_textBox_ID.Text = "";
            Document_textBox_ClientID.Text = "";
            Document_textBox_ClientName.Text = "";
            Document_textBox_OrderID.Text = "";
            Document_textBox_Total.Text = "";
            Document_textBox_DocumentDate.Text = "";
        }

        private void Order_button_New_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            orders.Show();
        }

        private void Client_button_New_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            client.Show();
        }

        private void Serv_button_New_Click(object sender, EventArgs e)
        {
            Serv serv = new Serv();
            serv.Show();
        }

        private void Document_button_New_Click(object sender, EventArgs e)
        {
            Document document = new Document();
            document.Show();
        }

        private void Search(DataGridView dataGrid, string SearchQuery, string tablename)
        {
            dataGrid.Rows.Clear();  

            SqlCommand command = new SqlCommand(SearchQuery, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader read = command.ExecuteReader();

            while(read.Read())
            {
                ReadSingleRow(dataGrid, read, tablename);
            }

            read.Close();
        }

        private void Order_textBox_Search_TextChanged(object sender, EventArgs e)
        {
            string OrderSearch = $"select * from Orders where concat" +
                             $"(ID, ClientID, ServiceName, Descript, OrderDate, Execution, Progress)" +
                             $"like '%" + Order_textBox_Search.Text + "%'";

            Search(Orders_dataGridView, OrderSearch, Orders);
        }

        private void Client_textBox_Search_TextChanged(object sender, EventArgs e)
        {
            string ClientSearch = $"select * from Client where concat" +
                                  $"(ID, FullName, Adress, Telephone)" +
                                  $"like '%" + Client_textBox_Search.Text + "%'";

            Search(Client_dataGridView, ClientSearch, Client);
        }

        private void Serv_textBox_Search_TextChanged(object sender, EventArgs e)
        {
            string ServSearch = $"select * from Serv where concat" +
                                $"(ServiceName, Price, ID) like '%" + Serv_textBox_Search.Text + "%'";

            Search(Serv_dataGridView, ServSearch, Serv);
        }

        private void Document_textBox_Search_TextChanged(object sender, EventArgs e)
        {
            string DocumentSearch = $"select * from Document where concat" +
                                    $"(ID, ClientID, ClientName, OrderID, Total, DocumentDate)" +
                                    $"like '%" + Document_textBox_Search.Text + "%'";

            Search(Document_dataGridView, DocumentSearch, Document);
        }

        int OrderIndex = 7;
        int ClientIndex = 4;
        int ServIndex = 3;
        int DocumentIndex = 6;

        string OrderDelete = $"delete from Orders ";
        string ClientDelete = $"delete from Client ";
        string ServDelete = $"delete from Serv ";
        string DocumentDelete = $"delete from Document ";

        private void OrderAlterIndex(int index)
        { 
            var ID = Orders_dataGridView.Rows[index].Cells[0].Value.ToString();
            var ClientID = Orders_dataGridView.Rows[index].Cells[1].Value.ToString();
            var ServiceName = Orders_dataGridView.Rows[index].Cells[2].Value.ToString();
            var Descript = Orders_dataGridView.Rows[index].Cells[3].Value.ToString();
            var OrderDate = Orders_dataGridView.Rows[index].Cells[4].Value.ToString();
            var Execution = Orders_dataGridView.Rows[index].Cells[5].Value.ToString();
            var Progress = Orders_dataGridView.Rows[index].Cells[6].Value.ToString();

            var OrderAlterQuery = $"update Orders set " +
                                  $"ClientID = '{ClientID}'," +
                                  $"ServiceName = '{ServiceName}'," +
                                  $"Descript = '{Descript}'," +
                                  $"Execution = '{Execution}'," +
                                  $"Progress = '{Progress}' where ID = '{ID}'";

            SqlCommand command = new SqlCommand(OrderAlterQuery, dataBase.getConnection());
            command.ExecuteNonQuery();
        }

        private void ClientAlterIndex(int index)
        {
            var ID = Client_dataGridView.Rows[index].Cells[0].Value.ToString();
            var FullName = Client_dataGridView.Rows[index].Cells[1].Value.ToString();
            var Adress = Client_dataGridView.Rows[index].Cells[2].Value.ToString();
            var Telephone = Client_dataGridView.Rows[index].Cells[3].Value.ToString();

            var ClientAlterQuery = $"update Client set " +
                                   $"FullName = '{FullName}'," +
                                   $"Adress = '{Adress}'," +
                                   $"Telephone = '{Telephone}' where ID = '{ID}'";
            
            SqlCommand command = new SqlCommand(ClientAlterQuery, dataBase.getConnection());
            command.ExecuteNonQuery();
        }

        private void ServAlterIndex(int index)
        {
            var ServiceName = Serv_dataGridView.Rows[index].Cells[0].Value.ToString();
            var Price = Serv_dataGridView.Rows[index].Cells[1].Value.ToString();
            var ID = Serv_dataGridView.Rows[index].Cells[2].Value.ToString();

            var ServAlterQuery = $"update Serv set " +
                                 $"ServiceName = '{ServiceName}'," +
                                 $"Price = '{Price}' where ID = '{ID}'";

            SqlCommand command = new SqlCommand(ServAlterQuery, dataBase.getConnection());
            command.ExecuteNonQuery();
        }

        private void DocumentAlterIndex(int index)
        {
            var ID = Document_dataGridView.Rows[index].Cells[0].Value.ToString();
            var ClientID = Document_dataGridView.Rows[index].Cells[1].Value.ToString();
            var ClientName = Document_dataGridView.Rows[index].Cells[2].Value.ToString();
            var OrderID = Document_dataGridView.Rows[index].Cells[3].Value.ToString();
            var Total = Document_dataGridView.Rows[index].Cells[4].Value.ToString();
            var DocumentDate = Document_dataGridView.Rows[index].Cells[5].Value.ToString();

            var DocumentAlterQuery = $"update Document set " +
                                     $"ClientID = '{ClientID}'," +
                                     $"ClientName = '{ClientName}'," +
                                     $"OrderID = '{OrderID}'," +
                                     $"Total = '{Total}'" +
                                     $"where ID = '{ID}'";

            SqlCommand command = new SqlCommand(DocumentAlterQuery, dataBase.getConnection());
            command.ExecuteNonQuery();
        }

        private void Update(DataGridView dataGrid, int tableindex, string deleteQuery, string table)
        {
            dataBase.openConnection();

            for (int index = 0; index < dataGrid.Rows.Count; index++)
            {
                var rowState = (RowState)dataGrid.Rows[index].Cells[tableindex].Value;

                if (rowState == RowState.Existed) continue;

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGrid.Rows[index].Cells[0].Value);
                    string query = deleteQuery + $"where ID = {id}";

                    SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if(rowState == RowState.Modified)
                {
                    switch (table)
                    {
                        case "Orders":
                            OrderAlterIndex(index);
                            break;
                        case "Client":
                            ClientAlterIndex(index);
                            break;
                        case "Serv":
                            ServAlterIndex(index);
                            break;
                        case "Document":
                            DocumentAlterIndex(index);
                            break;
                        
                    }
                }
                
                
            }

            dataBase.closeConnection();
        }

        private void DeleteRow(DataGridView dataGrid, int tableindex)
        {
            var index = dataGrid.CurrentCell.RowIndex;

            dataGrid.Rows[index].Visible= false;

            if (dataGrid.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGrid.Rows[index].Cells[tableindex].Value = RowState.Deleted;
                return;
            }

            dataGrid.Rows[index].Cells[tableindex].Value = RowState.Deleted;
        }

        private void Order_button_Delete_Click(object sender, EventArgs e)
        {
            DeleteRow(Orders_dataGridView, OrderIndex);
        }

        private void Order_button_Save_Click(object sender, EventArgs e)
        {
            Update(Orders_dataGridView, OrderIndex, OrderDelete, Orders);
        }

        private void Client_button_Delete_Click(object sender, EventArgs e)
        {
            DeleteRow(Client_dataGridView, ClientIndex);
        }

        private void Client_button_Save_Click(object sender, EventArgs e)
        {
            Update(Client_dataGridView, ClientIndex, ClientDelete, Client);
        }

        private void Serv_button_Delete_Click(object sender, EventArgs e)
        {
            DeleteRow(Serv_dataGridView, ServIndex);
        }

        private void Serv_button_Save_Click(object sender, EventArgs e)
        {
            Update(Serv_dataGridView, ServIndex, ServDelete, Serv);
        }

        private void Document_button_Delete_Click(object sender, EventArgs e)
        {
            DeleteRow(Document_dataGridView, DocumentIndex);
        }

        private void Document_button_Save_Click(object sender, EventArgs e)
        {
            Update(Document_dataGridView, DocumentIndex, DocumentDelete, Document);
        }

        private void Order_button_Alter_Click(object sender, EventArgs e)
        {
            var selectedRowIndex = Orders_dataGridView.CurrentCell.RowIndex;

            var ID = Order_textBox_ID.Text;
            var ClientID = Order_textBox_ClientID.Text;
            var ServiceName = Order_textBox_ServiceName.Text;
            var Descript = Order_textBox_Descript.Text;
            var OrderDate = Order_textBox_OrderDate.Text;
            var Execution = Order_textBox_Execution.Text;
            var Progress = Order_textBox_Progress.Text;

            if (Orders_dataGridView.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if(ClientID.Equals("") &&
               ServiceName.Equals("") &&
               Descript.Equals("") &&
               Execution.Equals("") &&
               Progress.Equals(""))
                {
                    MessageBox.Show("Запись не может быть сохранена, т.к. отсутствуют значения в некоторых полях",
                    "ОШИБКА!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                }
                else
                {
                    Orders_dataGridView.Rows[selectedRowIndex].SetValues(ID, ClientID, ServiceName, Descript, OrderDate, Execution, Progress);
                    Orders_dataGridView.Rows[selectedRowIndex].Cells[7].Value = RowState.Modified;
                }
            }
        }

        private void Client_button_Alter_Click(object sender, EventArgs e)
        {
            var selectedRowIndex = Client_dataGridView.CurrentCell.RowIndex;

            var ID = Client_textBox_ID.Text;
            var FullName = Client_textBox_FullName.Text;
            var Adress = Client_textBox_Adress.Text;
            var Telephone = Client_textBox_Telephone.Text;

            if (Client_dataGridView.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (ID.Equals("") &&
               FullName.Equals("") &&
               Adress.Equals("") &&
               Telephone.Equals(""))
                {
                    MessageBox.Show("Запись не может быть сохранена, т.к. отсутствуют значения в некоторых полях",
                    "ОШИБКА!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                }
                else
                {
                    Client_dataGridView.Rows[selectedRowIndex].SetValues(ID, FullName, Adress, Telephone);
                    Client_dataGridView.Rows[selectedRowIndex].Cells[4].Value = RowState.Modified;
                }
            }
        }

        private void Serv_button_Alter_Click(object sender, EventArgs e)
        {
            var selectedRowIndex = Serv_dataGridView.CurrentCell.RowIndex;

            var ServiceName = Serv_textBox_ServiceName.Text;
            var Price = Serv_textBox_Price.Text;
            var ID = Serv_textBox_ID.Text;

            if (Serv_dataGridView.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (ServiceName.Equals("") &&
                    Price.Equals("") &&
                    ID.Equals(""))
                {
                    MessageBox.Show("Запись не может быть сохранена, т.к. отсутствуют значения в некоторых полях",
                    "ОШИБКА!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                }
                else
                {
                    Serv_dataGridView.Rows[selectedRowIndex].SetValues(ServiceName, Price, ID);
                    Serv_dataGridView.Rows[selectedRowIndex].Cells[3].Value = RowState.Modified;
                }
            }
        }

        private void Document_button_Alter_Click(object sender, EventArgs e)
        {
            var selectedRowIndex = Document_dataGridView.CurrentCell.RowIndex;

            var ID = Document_textBox_ID.Text;
            var ClientID = Document_textBox_ClientID.Text;
            var ClientName = Document_textBox_ClientName.Text;
            var OrderID = Document_textBox_OrderID.Text;
            var Total = Document_textBox_Total.Text;
            var DocumentDate = Document_textBox_DocumentDate.Text;

            if (Document_dataGridView.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (ID.Equals("")&&
                    ClientID.Equals("") &&
                    ClientName.Equals("")&&
                    OrderID.Equals("")&&
                    Total.Equals("")&&
                    DocumentDate.Equals(""))
                {
                    MessageBox.Show("Запись не может быть сохранена, т.к. отсутствуют значения в некоторых полях",
                    "ОШИБКА!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                }
                else
                {
                    Document_dataGridView.Rows[selectedRowIndex].SetValues(ID, ClientID, ClientName, OrderID, Total, DocumentDate);
                    Document_dataGridView.Rows[selectedRowIndex].Cells[6].Value = RowState.Modified;
                }
            }
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти? Несохраненные данные будут потеряны.", "Выход", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
            else
            {
                this.Show();
            }
        }
    }
}
