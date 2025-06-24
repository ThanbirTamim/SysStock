namespace SysStock.UI.Panels
{
    partial class ProductListControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewProducts;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnCartView;
        private System.Windows.Forms.TableLayoutPanel topLayoutPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.dataGridViewProducts = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnCartView = new System.Windows.Forms.Button();
            this.topLayoutPanel = new System.Windows.Forms.TableLayoutPanel();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.topLayoutPanel.SuspendLayout();
            this.SuspendLayout();

            // 
            // dataGridViewProducts
            // 
            this.dataGridViewProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewProducts.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewProducts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewProducts.Name = "dataGridViewProducts";
            this.dataGridViewProducts.RowHeadersWidth = 62;
            this.dataGridViewProducts.RowTemplate.Height = 28;
            this.dataGridViewProducts.Size = new System.Drawing.Size(884, 360);
            this.dataGridViewProducts.TabIndex = 2;
            this.dataGridViewProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewProducts.MinimumSize = new System.Drawing.Size(300, 100);

            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;

            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.topLayoutPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewProducts);
            this.splitContainer1.Size = new System.Drawing.Size(884, 409);
            this.splitContainer1.SplitterDistance = 45;
            this.splitContainer1.TabIndex = 4;

            // 
            // topLayoutPanel
            // 
            this.topLayoutPanel.ColumnCount = 2;
            this.topLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.topLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.topLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.topLayoutPanel.Name = "topLayoutPanel";
            this.topLayoutPanel.RowCount = 1;
            this.topLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topLayoutPanel.Size = new System.Drawing.Size(884, 45);
            this.topLayoutPanel.TabIndex = 0;

            // 
            // btnCartView
            // 
            this.btnCartView.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnCartView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCartView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCartView.FlatAppearance.BorderSize = 0;
            this.btnCartView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCartView.Location = new System.Drawing.Point(710, 3);
            this.btnCartView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCartView.Name = "btnCartView";
            this.btnCartView.Size = new System.Drawing.Size(171, 39);
            this.btnCartView.TabIndex = 4;
            this.btnCartView.Text = "View Cart";
            this.btnCartView.UseVisualStyleBackColor = false;
            this.btnCartView.Click += new System.EventHandler(this.btnCartView_Click);

            // Add controls to topLayoutPanel
            this.topLayoutPanel.Controls.Add(new System.Windows.Forms.Label()
            {
                Text = "Product List",
                AutoSize = true,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Dock = System.Windows.Forms.DockStyle.Left,
                Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)))
            }, 0, 0);
            this.topLayoutPanel.Controls.Add(this.btnCartView, 1, 0);

            // 
            // ProductListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ProductListControl";
            this.Size = new System.Drawing.Size(884, 409);
            this.Load += new System.EventHandler(this.ProductListControl_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.topLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}