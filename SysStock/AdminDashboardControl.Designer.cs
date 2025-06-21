namespace SysStock
{
    partial class AdminDashboardControl
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
            this.lblHero = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblHero
            // 
            this.lblHero.AutoSize = true;
            this.lblHero.Location = new System.Drawing.Point(10, 11);
            this.lblHero.Name = "lblHero";
            this.lblHero.Size = new System.Drawing.Size(35, 16);
            this.lblHero.TabIndex = 0;
            this.lblHero.Text = "____";
            // 
            // AdminDashboardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblHero);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AdminDashboardControl";
            this.Size = new System.Drawing.Size(884, 409);
            this.Load += new System.EventHandler(this.AdminDashboardControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHero;
    }
}
