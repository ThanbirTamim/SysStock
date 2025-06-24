namespace SysStock.UI
{
    partial class MainPanel
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageDashboard;
        private System.Windows.Forms.TabPage tabPageProducts;
        private System.Windows.Forms.TabPage tabPageOrderItems;
        private System.Windows.Forms.TabPage tabPageOrders;
        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.Panel headerPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageDashboard = new System.Windows.Forms.TabPage();
            this.tabPageProducts = new System.Windows.Forms.TabPage();
            this.tabPageOrderItems = new System.Windows.Forms.TabPage();
            this.tabPageOrders = new System.Windows.Forms.TabPage();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.lblCurrentUser = new System.Windows.Forms.Label();

            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.RowCount = 2;
            this.mainLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.mainLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Padding = new System.Windows.Forms.Padding(10);

            // 
            // headerPanel
            // 
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.headerPanel.Controls.Add(this.lblCurrentTime);
            this.headerPanel.Controls.Add(this.lblCurrentUser);

            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.Location = new System.Drawing.Point(10, 10);
            this.lblCurrentTime.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);

            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.AutoSize = true;
            this.lblCurrentUser.Location = new System.Drawing.Point(400, 10);
            this.lblCurrentUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);

            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Controls.AddRange(new System.Windows.Forms.Control[] {
            this.tabPageDashboard,
            this.tabPageProducts,
            this.tabPageOrderItems,
            this.tabPageOrders
        });

            // 
            // tabPageDashboard
            // 
            this.tabPageDashboard.Text = "Dashboard";
            this.tabPageDashboard.Padding = new System.Windows.Forms.Padding(3);

            // 
            // tabPageProducts
            // 
            this.tabPageProducts.Text = "Products";
            this.tabPageProducts.Padding = new System.Windows.Forms.Padding(3);

            // 
            // tabPageOrderItems
            // 
            this.tabPageOrderItems.Text = "Order Items";
            this.tabPageOrderItems.Padding = new System.Windows.Forms.Padding(3);

            // 
            // tabPageOrders
            // 
            this.tabPageOrders.Text = "Orders";
            this.tabPageOrders.Padding = new System.Windows.Forms.Padding(3);

            // Add controls to layout
            this.mainLayout.Controls.Add(this.headerPanel, 0, 0);
            this.mainLayout.Controls.Add(this.tabControl, 0, 1);

            // 
            // MainPanel
            // 
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.mainLayout);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Panel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainPanel_FormClosing);
        }
    }
}