using System;
using System.Windows.Forms;
using SysStock.Utility;
using SysStock.Utility.DataAccess;
using SysStock.Utility.Models;

namespace SysStock
{
    public partial class ProductListControl : UserControl
    {
        private DataGridView dgvProducts;
        private ProductDAL productDal;
        private readonly string userRole;
        private MainForm mainForm;
        public ProductListControl(MainForm mainForm, string role)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.userRole = role;
            productDal = new ProductDAL();
            SetupDataGridView();
            LoadProducts();
        }

        private void ProductListControl_Load(object sender, EventArgs e)
        {

        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            mainForm.LoadControl(new ProductCreateControl());
        }

        private void SetupDataGridView()
        {
            dgvProducts = dataGridViewProducts;
            dgvProducts.AutoGenerateColumns = false; // Add this line to prevent auto-generation of columns
            dgvProducts.CellContentClick += DgvProducts_CellContentClick;

            // Add columns
            dgvProducts.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    Name = "ProductId",
                    HeaderText = "ID",
                    DataPropertyName = "ProductId",
                    Width = 50
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Name",
                    HeaderText = "Product Name",
                    DataPropertyName = "Name",
                    Width = 200
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "CategoryId",
                    HeaderText = "Category ID",
                    DataPropertyName = "CategoryId",
                    Width = 80
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "BrandId",
                    HeaderText = "Brand ID",
                    DataPropertyName = "BrandId",
                    Width = 80
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "UnitPrice",
                    HeaderText = "Price",
                    DataPropertyName = "UnitPrice",
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                    Width = 80
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "DiscountPercent",
                    HeaderText = "Discount %",
                    DataPropertyName = "DiscountPercent",
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" },
                    Width = 80
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "QuantityInStock",
                    HeaderText = "Stock",
                    DataPropertyName = "QuantityInStock",
                    Width = 60
                }
            });

            // Add action buttons based on role
            if (userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                var deleteColumn = new DataGridViewImageColumn
                {
                    Name = "Delete",
                    HeaderText = "Delete",
                    Image = Properties.Resources.delete_icon,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 60
                };
                dgvProducts.Columns.Add(deleteColumn);
            }
            else if (userRole.Equals("Staff", StringComparison.OrdinalIgnoreCase))
            {
                var addToCartColumn = new DataGridViewImageColumn
                {
                    Name = "AddToCart",
                    HeaderText = "Add to Cart",
                    Image = Properties.Resources.add_to_cart_icon,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 60
                };
                var removeFromCartColumn = new DataGridViewImageColumn
                {
                    Name = "RemoveFromCart",
                    HeaderText = "Remove",
                    Image = Properties.Resources.remove_from_cart_icon,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 60
                };
                dgvProducts.Columns.AddRange(new DataGridViewColumn[] { addToCartColumn, removeFromCartColumn });
            }
        }

        private void LoadProducts()
        {
            try
            {
                var products = productDal.GetAll();
                var currentDateTime = DateTime.UtcNow;
                var currentUser = CurrentUserSession.Username;

                dgvProducts.DataSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var productId = Convert.ToInt32(dgvProducts.Rows[e.RowIndex].Cells["ProductId"].Value);

            if (userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                if (e.ColumnIndex == dgvProducts.Columns["Delete"].Index)
                {
                    HandleDelete(productId);
                }
            }
            else if (userRole.Equals("Staff", StringComparison.OrdinalIgnoreCase))
            {
                if (e.ColumnIndex == dgvProducts.Columns["AddToCart"].Index)
                {
                    HandleAddToCart(productId);
                }
                else if (e.ColumnIndex == dgvProducts.Columns["RemoveFromCart"].Index)
                {
                    HandleRemoveFromCart(productId);
                }
            }
        }

        private void HandleDelete(int productId)
        {
            try
            {
                var result = MessageBox.Show("Are you sure you want to delete this product?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (productDal.Delete(productId))
                    {
                        MessageBox.Show("Product deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProducts(); // Refresh the grid
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting product: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleAddToCart(int productId)
        {
            try
            {
                var product = productDal.GetById(productId);
                if (product == null)
                {
                    MessageBox.Show("Product not found!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var quantityForm = new QuantityInputForm(product.QuantityInStock))
                {
                    if (quantityForm.ShowDialog() == DialogResult.OK)
                    {
                        int quantity = quantityForm.Quantity;
                        ShoppingCart.Instance.AddItem(product, quantity);
                        MessageBox.Show($"Added {quantity} items to cart", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding to cart: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleRemoveFromCart(int productId)
        {
            try
            {
                // Implement remove from cart logic here
                // You'll need to implement this based on your cart management system
                ShoppingCart.Instance.RemoveItem(productId);
                MessageBox.Show("Item removed from cart", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing from cart: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCartView_Click(object sender, EventArgs e)
        {
            using (var orderForm = new OrderForm())
            {
                orderForm.ShowDialog();
            }
        }
    }
}
