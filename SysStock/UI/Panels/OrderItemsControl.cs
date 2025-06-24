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
    public partial class OrderItemsControl : UserControl
    {
        public OrderItemsControl()
        {
            InitializeComponent();
            LoadOrderItems();
        }
        public void RefreshData()
        {
            LoadOrderItems(); // Or whatever method loads/reloads the grid
        }

        private void LoadOrderItems()
        {
            try
            {
                var orderItemDal = new OrderItemDAL();
                var orderItems = orderItemDal.GetAll();
                dgvOrderItems.DataSource = orderItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order items: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
