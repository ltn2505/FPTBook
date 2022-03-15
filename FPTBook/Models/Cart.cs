using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int TotalQuantity()
        {
            return items.Sum(s => s._shopping_quantity);
        }
        public void DeleteProduct(string id)
        {
            items.RemoveAll(s => s._shopping_product.book_id == id);
        }
        public void ClearCart()
        {
            items.Clear();
        }
        [Display(Name = "Order id")]
        public int order_id { get; set; }
        [Display(Name = "User name")]
        public string acc_name { get; set; }
        [Display(Name = "Receiver's name")]
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string receiver_name { get; set; }
        [Display(Name = "Number phone")]
        [Phone]
        [Required]
        [StringLength(11, MinimumLength = 9)]
        public string phone { get; set; }
        [Display(Name = "Order date")]
        public System.DateTime order_date { get; set; }
        [Display(Name = "Address")]
        [Required]
        [StringLength(100, MinimumLength = 5)]

        public string delivery_address { get; set; }
        [Display(Name = "Total price")]
        public int total_price { get; set; }

    }
}