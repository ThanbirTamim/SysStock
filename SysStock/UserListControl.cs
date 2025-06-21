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

namespace SysStock
{
    public partial class UserListControl: UserControl
    {
        private MainForm mainForm;
        public UserListControl(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void UserListControl_Load(object sender, EventArgs e)
        {
            //string connectionString = @"Server=RUHI-S-HP\SQLEXPRESS;Database=SysStockDB;Trusted_Connection=True;";
            //using (SqlConnection con = new SqlConnection(connectionString))
            //{
            //    try
            //    {
            //        string query = "SELECT UserId, Username, FullName, Email, Role, IsActive, CreatedAt FROM Users";
            //        SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            //        DataTable dt = new DataTable();
            //        adapter.Fill(dt);

            //        dataGridViewUsers.DataSource = dt;

            //        // Optional Styling
            //        dataGridViewUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //        dataGridViewUsers.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            //        dataGridViewUsers.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10);
            //        dataGridViewUsers.ReadOnly = true;
            //        dataGridViewUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Error loading users: " + ex.Message);
            //    }
            //}

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            mainForm.LoadControl(new UserRegisterControl());
        }
    }
}
