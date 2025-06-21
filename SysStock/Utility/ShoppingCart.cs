using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysStock.Utility.Models;

namespace SysStock.Utility
{
    public class ShoppingCart
    {
        private static ShoppingCart _instance;
        private List<OrderItem> _items;

        private ShoppingCart()
        {
            _items = new List<OrderItem>();
        }

        public static ShoppingCart Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ShoppingCart();
                return _instance;
            }
        }

        public List<OrderItem> Items => _items;

        public void AddItem(Product product, int quantity)
        {
            var existingItem = _items.FirstOrDefault(i => i.ProductId == product.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.LineTotal = existingItem.Quantity * (existingItem.UnitPrice * (1 - existingItem.DiscountPercent / 100));
            }
            else
            {
                var newItem = new OrderItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.Name,
                    Quantity = quantity,
                    UnitPrice = product.UnitPrice,
                    DiscountPercent = product.DiscountPercent,
                    DiscountAmount = product.UnitPrice * (product.DiscountPercent / 100) * quantity,
                    LineTotal = quantity * (product.UnitPrice * (1 - product.DiscountPercent / 100))
                };
                _items.Add(newItem);
            }
        }

        public void RemoveItem(int productId)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
                _items.Remove(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public decimal GetSubTotal()
        {
            return _items.Sum(i => i.LineTotal);
        }
    }
}
