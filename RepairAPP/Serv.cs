﻿using System;
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
    }
}