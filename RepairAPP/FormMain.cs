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
    }
}
