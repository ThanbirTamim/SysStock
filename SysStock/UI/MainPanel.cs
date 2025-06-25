using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SysStock.UI.Panels;
using SysStock.Utility;
using SysStock.Utility.Models;

namespace SysStock.UI
{
    public partial class MainPanel : Form
    {
        private DashboardPanel dashboardPanel;
        private ProductListControl productListControl;
        private OrderItemsControl orderItemsControl;
        private OrdersControl ordersControl;
        private Label lblCurrentTime;
        private Label lblCurrentUser;
        private Timer timer;
        private User User;
        public string CurrentUser { get; internal set; }

        public MainPanel()
        {
            InitializeComponent();
            InitializeControls();
            StartTimer();
        }

        public MainPanel(string username, User user) :this()
        {
            CurrentUser = username;
            this.User = user;
        }

        private void InitializeControls()
        {
            // Initialize user controls
            dashboardPanel = new DashboardPanel(this);
            productListControl = new ProductListControl(this, CurrentUserSession.Role);
            orderItemsControl = new OrderItemsControl();
            ordersControl = new OrdersControl();

            // Add controls to respective tab pages
            tabPageDashboard.Controls.Add(dashboardPanel);
            tabPageProducts.Controls.Add(productListControl);
            tabPageOrderItems.Controls.Add(orderItemsControl);
            tabPageOrders.Controls.Add(ordersControl);

            // Dock all controls
            dashboardPanel.Dock = DockStyle.Fill;
            productListControl.Dock = DockStyle.Fill;
            orderItemsControl.Dock = DockStyle.Fill;
            ordersControl.Dock = DockStyle.Fill;

            // Update header labels
            UpdateDateTime();
        }

        private void StartTimer()
        {
            timer = new Timer();
            timer.Interval = 1000; // Update every sec
            timer.Tick += (s, e) => UpdateDateTime();
            timer.Start();
        }

        private void UpdateDateTime()
        {
            lblCurrentTime.Text = $"Current Date and Time (UTC): {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}";
            lblCurrentUser.Text = $"Current User: {CurrentUserSession.Username}";
        }

        private void MainPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer?.Stop();
            timer?.Dispose();
            Application.Exit();
        }

        public void RefreshAllTabs()
        {
            productListControl?.RefreshData();
            orderItemsControl?.RefreshData();
            ordersControl?.RefreshData();
        }
    }
}
