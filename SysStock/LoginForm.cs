using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SysStock.Utility;
using SysStock.Utility.DataAccess;
using SysStock.Utility.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SysStock
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }



        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }
        private void Loginbtn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UserDAL userDAL = new UserDAL();
            var user = userDAL.Authenticate(username, password);
            if (user != null)
            {
                CurrentUserSession.SetCurrentUser(user);


                MessageBox.Show($"Welcome {user.FullName} ({user.Role})", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MainForm main = new MainForm(user.FullName, user);
                this.Hide();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid credentials or user is inactive.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();
            }


            #region Old code
            /*//Use systemutility=>connectionstring
            //string connectionString = @"Server=RUHI-S-HP\SQLEXPRESS;Database=SysStockDB;Trusted_Connection=True;";
            string connectionString = SystemUtility.connectionString;
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password AND IsActive = 1";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    adapter.SelectCommand.Parameters.AddWithValue("@username", username);
                    adapter.SelectCommand.Parameters.AddWithValue("@password", password);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        string fullName = dt.Rows[0]["FullName"].ToString();
                        string role = dt.Rows[0]["Role"].ToString();

                        UserDAL userDAL = new UserDAL();
                        userDAL.Authenticate(username, password);
                        CurrentUserSession.SetCurrentUser(user); // Set the current user info

                        MessageBox.Show($"Welcome {fullName} ({role})", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        MainForm main = new MainForm(fullName);
                        this.Hide();
                        main.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid credentials or user is inactive.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsername.Clear();
                        txtPassword.Clear();
                        txtUsername.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }*/
            #endregion
        }


        private void Clearbtn_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        private void ShowPassCB_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowPassCB.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }
    }
}
