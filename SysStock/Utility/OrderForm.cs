using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SysStock.Utility.DataAccess;
using SysStock.Utility.Models;

namespace SysStock.Utility
{
    public partial class OrderForm : Form
    {
        private readonly ShoppingCart _cart;
        private readonly OrderDAL _orderDal;
        private DataGridView dgvCart;
        private TextBox txtSubTotal;
        private TextBox txtVATPercentage;
        private TextBox txtVATAmount;
        private TextBox txtGrossDiscount;
        private TextBox txtTotalAmount;
        private TextBox txtAmountPaid;
        private TextBox txtChangeGiven;
        private ComboBox cboPaymentMethod;
        private TextBox txtRemarks;
        private Button btnProcessOrder;
        private Button btnCancel;
        private Label lblCurrentTime;
        private Label lblCurrentUser;

        public OrderForm()
        {
            _cart = ShoppingCart.Instance;
            _orderDal = new OrderDAL();
            SetupForm();
            LoadCartItems();
            StartTimer();
        }

        private void SetupForm()
        {
            this.MinimumSize = new Size(800, 600);
            this.Size = new Size(1024, 768);
            this.Text = "Process Order";

            // Main TableLayoutPanel
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(10),
            };

            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); // Header
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));  // Cart
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));  // Details
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Buttons

            // Header Panel with improved formatting
            var headerPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0),
                BackColor = Color.FromArgb(240, 240, 240) // Light gray background
            };
            headerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            headerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));

            lblCurrentTime = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(64, 64, 64), // Dark gray text
                Padding = new Padding(10, 0, 0, 0)
            };

            lblCurrentUser = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(64, 64, 64), // Dark gray text
                Padding = new Padding(0, 0, 10, 0)
            };


            headerPanel.Controls.Add(lblCurrentTime, 0, 0);
            headerPanel.Controls.Add(lblCurrentUser, 1, 0);

            // Cart DataGridView
            dgvCart = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                MultiSelect = false
            };

            dgvCart.Columns.AddRange(new DataGridViewColumn[]
            {
            new DataGridViewTextBoxColumn { Name = "ProductId", HeaderText = "ID", FillWeight = 50 },
            new DataGridViewTextBoxColumn { Name = "ProductName", HeaderText = "Product", FillWeight = 200 },
            new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Quantity", FillWeight = 80 },
            new DataGridViewTextBoxColumn { Name = "UnitPrice", HeaderText = "Unit Price", FillWeight = 100 },
            new DataGridViewTextBoxColumn { Name = "DiscountPercent", HeaderText = "Discount %", FillWeight = 80 },
            new DataGridViewTextBoxColumn { Name = "DiscountAmount", HeaderText = "Discount", FillWeight = 100 },
            new DataGridViewTextBoxColumn { Name = "LineTotal", HeaderText = "Total", FillWeight = 100 }
            });

            // Details Panel
            var detailsLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 9,
                Padding = new Padding(5)
            };

            detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

            for (int i = 0; i < 9; i++)
            {
                detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 11.11F));
            }

            // Add controls to details layout
            AddDetailRow("Sub Total:", out txtSubTotal, 0, true, detailsLayout);
            AddDetailRow("VAT %:", out txtVATPercentage, 1, false, detailsLayout);
            AddDetailRow("VAT Amount:", out txtVATAmount, 2, true, detailsLayout);
            AddDetailRow("Gross Discount:", out txtGrossDiscount, 3, false, detailsLayout);
            AddDetailRow("Total Amount:", out txtTotalAmount, 4, true, detailsLayout);
            AddDetailRow("Amount Paid:", out txtAmountPaid, 5, false, detailsLayout);
            AddDetailRow("Change:", out txtChangeGiven, 6, true, detailsLayout);

            // Payment Method
            var lblPaymentMethod = new Label
            {
                Text = "Payment Method:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight
            };

            cboPaymentMethod = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboPaymentMethod.Items.AddRange(new string[] { "Cash", "Card", "Mobile Banking" });
            cboPaymentMethod.SelectedIndex = 0;

            detailsLayout.Controls.Add(lblPaymentMethod, 0, 7);
            detailsLayout.Controls.Add(cboPaymentMethod, 1, 7);

            // Remarks
            var lblRemarks = new Label
            {
                Text = "Remarks:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight
            };

            txtRemarks = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true
            };

            detailsLayout.Controls.Add(lblRemarks, 0, 8);
            detailsLayout.Controls.Add(txtRemarks, 1, 8);

            // Buttons Panel with improved styling
            var buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 10, 0, 0)
            };

            btnCancel = new Button
            {
                Text = "Cancel",
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                BackColor = Color.FromArgb(234, 67, 53), // Red color
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(10, 0, 0, 0)
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.Close();

            btnProcessOrder = new Button
            {
                Text = "Process Order",
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                BackColor = Color.FromArgb(52, 168, 83), // Green color
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(10, 0, 0, 0)
            };
            btnProcessOrder.FlatAppearance.BorderSize = 0;
            btnProcessOrder.Click += BtnProcessOrder_Click;

            // Add hover effects
            btnCancel.MouseEnter += (s, e) => btnCancel.BackColor = Color.FromArgb(210, 60, 48);
            btnCancel.MouseLeave += (s, e) => btnCancel.BackColor = Color.FromArgb(234, 67, 53);

            btnProcessOrder.MouseEnter += (s, e) => btnProcessOrder.BackColor = Color.FromArgb(47, 151, 75);
            btnProcessOrder.MouseLeave += (s, e) => btnProcessOrder.BackColor = Color.FromArgb(52, 168, 83);

            buttonsPanel.Controls.Add(btnCancel);
            buttonsPanel.Controls.Add(btnProcessOrder);


            // Add all layouts to main layout
            mainLayout.Controls.Add(headerPanel, 0, 0);
            mainLayout.Controls.Add(dgvCart, 0, 1);
            mainLayout.Controls.Add(detailsLayout, 0, 2);
            mainLayout.Controls.Add(buttonsPanel, 0, 3);

            this.Controls.Add(mainLayout);

            // Set up event handlers
            txtVATPercentage.Text = "5.00";
            txtGrossDiscount.Text = "0.00";
            txtVATPercentage.TextChanged += CalculateTotals;
            txtGrossDiscount.TextChanged += CalculateTotals;
            txtAmountPaid.TextChanged += CalculateChange;
        }

        private void AddDetailRow(string labelText, out TextBox textBox, int row, bool readOnly, TableLayoutPanel layout)
        {
            var label = new Label
            {
                Text = labelText,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight
            };

            textBox = new TextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = readOnly
            };

            layout.Controls.Add(label, 0, row);
            layout.Controls.Add(textBox, 1, row);
        }

        private void StartTimer()
        {
            Timer timer = new Timer();
            timer.Interval = 1000; // Update every second
            timer.Tick += (s, e) =>
            {
                lblCurrentTime.Text = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            };
            timer.Start();
            lblCurrentTime.Text = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        }
        private void AddLabelAndTextBox(string labelText, string textBoxName, ref int y, int labelX, int textX, out TextBox textBox)
        {
            var label = new Label
            {
                Text = labelText,
                Location = new Point(labelX, y),
                AutoSize = true
            };

            textBox = new TextBox
            {
                Name = textBoxName,
                Location = new Point(textX, y),
                Size = new Size(200, 23)
            };

            this.Controls.Add(label);
            this.Controls.Add(textBox);

            y += 30;
        }

        private void LoadCartItems()
        {
            dgvCart.Rows.Clear();
            foreach (var item in _cart.Items)
            {
                dgvCart.Rows.Add(
                    item.ProductId,
                    item.ProductName,
                    item.Quantity,
                    item.UnitPrice.ToString("C2"),
                    item.DiscountPercent.ToString("N2"),
                    item.DiscountAmount.ToString("C2"),
                    item.LineTotal.ToString("C2")
                );
            }
            CalculateTotals(null, null);
        }

        private void CalculateTotals(object sender, EventArgs e)
        {
            var subTotal = _cart.GetSubTotal();
            txtSubTotal.Text = subTotal.ToString("C2");

            decimal vatPercentage;
            if (!decimal.TryParse(txtVATPercentage.Text, out vatPercentage))
                vatPercentage = 0;

            decimal grossDiscount;
            if (!decimal.TryParse(txtGrossDiscount.Text, out grossDiscount))
                grossDiscount = 0;

            var vatAmount = subTotal * (vatPercentage / 100);
            txtVATAmount.Text = vatAmount.ToString("C2");

            var totalAmount = subTotal + vatAmount - grossDiscount;
            txtTotalAmount.Text = totalAmount.ToString("C2");

            CalculateChange(null, null);
        }

        private void CalculateChange(object sender, EventArgs e)
        {
            decimal totalAmount;
            decimal.TryParse(txtTotalAmount.Text.TrimStart('$'), out totalAmount);

            decimal amountPaid;
            if (decimal.TryParse(txtAmountPaid.Text, out amountPaid))
            {
                var change = amountPaid - totalAmount;
                txtChangeGiven.Text = change.ToString("C2");
                btnProcessOrder.Enabled = amountPaid >= totalAmount;
            }
            else
            {
                txtChangeGiven.Text = "0.00";
                btnProcessOrder.Enabled = false;
            }
        }

        private void BtnProcessOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (_cart.Items.Count == 0)
                {
                    MessageBox.Show("Cart is empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal amountPaid;
                if (!decimal.TryParse(txtAmountPaid.Text, out amountPaid))
                {
                    MessageBox.Show("Please enter a valid amount paid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var order = new Order
                {
                    UserId = CurrentUserSession.UserId,
                    OrderDate = DateTime.UtcNow,
                    SubTotal = _cart.GetSubTotal(),
                    GrossDiscount = decimal.Parse(txtGrossDiscount.Text),
                    VATPercentage = decimal.Parse(txtVATPercentage.Text),
                    VATAmount = decimal.Parse(txtVATAmount.Text.TrimStart('$')),
                    TotalAmount = decimal.Parse(txtTotalAmount.Text.TrimStart('$')),
                    AmountPaid = amountPaid,
                    ChangeGiven = decimal.Parse(txtChangeGiven.Text.TrimStart('$')),
                    PaymentMethod = cboPaymentMethod.SelectedItem.ToString(),
                    Remarks = txtRemarks.Text,
                    OrderItems = _cart.Items
                };

                var orderId = _orderDal.CreateOrder(order);
                MessageBox.Show($"Order #{orderId} processed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _cart.Clear();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
