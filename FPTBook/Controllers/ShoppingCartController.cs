using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class ShoppingCartController : Controller
    {
        private FPTBookEntities1 db = new FPTBookEntities1();
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddCart(string id)
        {
            var pro = db.books.SingleOrDefault(s => s.book_id == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("ShowCart", "ShoppingCart");
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult Update_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            string id_pro = form["Book_id"];
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity(id_pro, quantity);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult Delete(string id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.DeleteProduct(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult OrderProduct(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                order _order = new order();
                
                _order.order_date = DateTime.Now;
                _order.acc_id = int.Parse(form["AccID"]);
                _order.receiver_name = form["ReName"];
                _order.phone = form["Phone"];
                _order.delivery_address = form["DeAddress"];
                _order.total_price = int.Parse(form["TotalPrice"]);
                db.orders.Add(_order);

                foreach (var item in cart.Items)
                {
                    order_detail orderDetail = new order_detail();
                    orderDetail.order_id = _order.order_id;
                    orderDetail.book_id = item._shopping_product.book_id;
                    orderDetail.quantity = item._shopping_quantity;
                    orderDetail.price = item._shopping_product.book_price * item._shopping_quantity;

                    var pro = db.books.SingleOrDefault(s => s.book_id == orderDetail.book_id);

                    pro.book_quantity -= orderDetail.quantity;
                    db.books.Attach(pro);
                    db.Entry(pro).Property(a => a.book_quantity).IsModified = true;

                    db.order_detail.Add(orderDetail);
                }

                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("OrderSuccess", "ShoppingCart", new { id = _order.order_id });
            }
            catch
            {
                return Content("Error order!");
            }
        }
        public ActionResult OrderSuccess()
        {
            return View();
        }
    }
}