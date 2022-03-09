using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Models
{
    public class CartItem
    {
        public book _shopping_product { get; set; }
        public int _shopping_quantity { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add(book _pro, int _quantity = 1)
        {
            var item = items.FirstOrDefault(s => s._shopping_product.book_id == _pro.book_id);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _shopping_product = _pro,
                    _shopping_quantity = _quantity
                });
            }
            else
            {
                item._shopping_quantity += _quantity;
            }

        }
        public void Update_Quantity(string id, int quantity)
        {
            var item = items.Find(s => s._shopping_product.book_id == id);
            if (item != null)
            {
                item._shopping_quantity = quantity;
            }
        }
        public double Total_Money()
        {
            var total = items.Sum(s => s._shopping_product.book_price * s._shopping_quantity);
            return total;
        }
        public void DeleteProduct(string id)
        {
            items.RemoveAll(s => s._shopping_product.book_id == id);
        }
        public void ClearCart()
        {
            items.Clear();
        }

    }
}