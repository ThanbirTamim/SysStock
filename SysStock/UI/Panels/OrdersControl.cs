using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SysStock.Utility.DataAccess;

namespace SysStock.UI.Panels
{
    public partial class OrdersControl : UserControl
    {
        public OrdersControl()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            try
            {
                var orderDal = new OrderDAL();
                var orders = orderDal.GetAll();
                dgvOrders.DataSource = orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshData()
        {
            LoadOrders(); // Or whatever method loads/reloads the grid
        }
    }
}
