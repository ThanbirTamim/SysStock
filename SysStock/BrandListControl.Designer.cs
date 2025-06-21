namespace SysStock
{
    partial class BrandListControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAddBrand = new System.Windows.Forms.Button();
            this.dataGridViewBrands = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBrands)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddBrand
            // 
            this.btnAddBrand.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddBrand.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnAddBrand.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddBrand.FlatAppearance.BorderSize = 0;
            this.btnAddBrand.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddBrand.Location = new System.Drawing.Point(837, 18);
            this.btnAddBrand.Name = "btnAddBrand";
            this.btnAddBrand.Size = new System.Drawing.Size(120, 46);
            this.btnAddBrand.TabIndex = 5;
            this.btnAddBrand.Text = "Add Brand";
            this.btnAddBrand.UseVisualStyleBackColor = false;
            this.btnAddBrand.Click += new System.EventHandler(this.btnAddBrand_Click);
            // 
            // dataGridViewBrands
            // 
            this.dataGridViewBrands.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBrands.Location = new System.Drawing.Point(0, 85);
            this.dataGridViewBrands.Name = "dataGridViewBrands";
            this.dataGridViewBrands.RowHeadersWidth = 62;
            this.dataGridViewBrands.RowTemplate.Height = 28;
            this.dataGridViewBrands.Size = new System.Drawing.Size(994, 425);
            this.dataGridViewBrands.TabIndex = 4;
            // 
            // BrandListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddBrand);
            this.Controls.Add(this.dataGridViewBrands);
            this.Name = "BrandListControl";
            this.Size = new System.Drawing.Size(994, 511);
            this.Load += new System.EventHandler(this.BrandListControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBrands)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddBrand;
        private System.Windows.Forms.DataGridView dataGridViewBrands;
    }
}
