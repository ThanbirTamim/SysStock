namespace SysStock
{
    partial class CatagoryCreateControl
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
            this.txtCatagoryName = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblCatagoryName = new System.Windows.Forms.Label();
            this.lblAddCatagory = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtbDescription = new System.Windows.Forms.RichTextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCatagoryName
            // 
            this.txtCatagoryName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCatagoryName.Location = new System.Drawing.Point(155, 14);
            this.txtCatagoryName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCatagoryName.Name = "txtCatagoryName";
            this.txtCatagoryName.Size = new System.Drawing.Size(234, 26);
            this.txtCatagoryName.TabIndex = 29;
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(311, 195);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(108, 25);
            this.lblDescription.TabIndex = 28;
            this.lblDescription.Text = "Description";
            // 
            // lblCatagoryName
            // 
            this.lblCatagoryName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCatagoryName.AutoSize = true;
            this.lblCatagoryName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCatagoryName.Location = new System.Drawing.Point(311, 144);
            this.lblCatagoryName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCatagoryName.Name = "lblCatagoryName";
            this.lblCatagoryName.Size = new System.Drawing.Size(141, 25);
            this.lblCatagoryName.TabIndex = 27;
            this.lblCatagoryName.Text = "Catagory Name";
            // 
            // lblAddCatagory
            // 
            this.lblAddCatagory.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblAddCatagory.AutoSize = true;
            this.lblAddCatagory.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddCatagory.Location = new System.Drawing.Point(407, 33);
            this.lblAddCatagory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAddCatagory.Name = "lblAddCatagory";
            this.lblAddCatagory.Size = new System.Drawing.Size(207, 40);
            this.lblAddCatagory.TabIndex = 26;
            this.lblAddCatagory.Text = "Add Catagory";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.txtCatagoryName);
            this.panel1.Controls.Add(this.rtbDescription);
            this.panel1.Controls.Add(this.btnRegister);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Location = new System.Drawing.Point(301, 129);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 348);
            this.panel1.TabIndex = 30;
            // 
            // rtbDescription
            // 
            this.rtbDescription.Location = new System.Drawing.Point(155, 66);
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.Size = new System.Drawing.Size(234, 96);
            this.rtbDescription.TabIndex = 11;
            this.rtbDescription.Text = "";
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegister.ForeColor = System.Drawing.SystemColors.Control;
            this.btnRegister.Location = new System.Drawing.Point(266, 211);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(110, 35);
            this.btnRegister.TabIndex = 10;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(140, 211);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(110, 35);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // CatagoryCreateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblCatagoryName);
            this.Controls.Add(this.lblAddCatagory);
            this.Controls.Add(this.panel1);
            this.Name = "CatagoryCreateControl";
            this.Size = new System.Drawing.Size(994, 511);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCatagoryName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblCatagoryName;
        private System.Windows.Forms.Label lblAddCatagory;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnClear;
    }
}
