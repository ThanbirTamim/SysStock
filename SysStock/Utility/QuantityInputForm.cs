using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysStock.Utility
{
    public partial class QuantityInputForm : Form
    {
        public int Quantity { get; private set; }
        private readonly int _maxQuantity;

        public QuantityInputForm(int maxQuantity)
        {
            //InitializeComponent();
            _maxQuantity = maxQuantity;

            // Create and setup controls
            var numericUpDown = new NumericUpDown
            {
                Minimum = 1,
                Maximum = maxQuantity,
                Value = 1,
                Location = new Point(12, 12),
                Width = 120
            };

            var okButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new Point(12, 40)
            };

            var cancelButton = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new Point(93, 40)
            };

            okButton.Click += (s, e) =>
            {
                Quantity = (int)numericUpDown.Value;
                this.Close();
            };

            // Add controls to form
            this.Controls.AddRange(new Control[] { numericUpDown, okButton, cancelButton });

            // Form properties
            this.Text = "Enter Quantity";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.AcceptButton = okButton;
            this.CancelButton = cancelButton;
            this.Size = new Size(200, 120);
        }
    }
}
