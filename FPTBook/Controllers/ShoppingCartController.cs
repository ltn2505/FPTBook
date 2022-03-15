using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class ShoppingCartController : Controller
    {
        private FPTBookEntities3 db = new FPTBookEntities3();
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
                if (pro.book_quantity <= 0)
                {
                    return Content("<script>alert('This product is currently out of stock');window.location.replace('/');</script>");
                }
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
            {
                return View("ShowCart", "ShoppingCart");
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult Update_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            string id_pro = form["Book_id"];
            book quanKho = db.books.FirstOrDefault(a => a.book_id == id_pro);
            int quantity = int.Parse(form["Quantity"]);
            if (quantity <=0)
            {
                return Content("<script>alert('Can not buy quantity less than 1');window.location.replace('/');</script>");
            }
            if (quantity > 5)
            {
                return Content("<script>alert('Can not buy quantity larger than 5');window.location.replace('/');</script>");
            }
            if(quantity > quanKho.book_quantity)
            {
                return Content("<script>alert('This product is currently out of stock');window.location.replace('/');</script>");
            }
            
            cart.Update_Quantity(id_pro, quantity);
             
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult Delete(string id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.DeleteProduct(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult OrderProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OrderProduct(/*FormCollection form,*/ order order )
        {
            if (Session["username"] == null)
            {
                return Content("<script>alert('You must login');window.location.replace('/');</script>");
            }
            try
            {
                Cart cart = Session["Cart"] as Cart;
                order.order_date=DateTime.Now;
                db.orders.Add(order);
                db.SaveChanges();
                //order _order = new order();

                //_order.order_date = DateTime.Now;
                //_order.acc_name = form["AccName"];
                //_order.receiver_name = form["ReName"];
                //_order.phone = form["Phone"];
                //_order.delivery_address = form["DeAddress"];
                //_order.total_price = int.Parse(form["TotalPrice"]);
                //db.orders.Add(_order);

                foreach (var item in cart.Items)
                {
                    order_detail orderDetail = new order_detail();
                    orderDetail.order_id = order.order_id;
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
                return RedirectToAction("OrderSuccess", "ShoppingCart", new { id = order.order_id });
            }
            catch
            {
                return Content("Error order!");
            }
        }
        public PartialViewResult BagCart()
        {
            int total_item = 0;

            Cart cart = Session["Cart"] as Cart;

            if (cart != null)
                total_item = cart.TotalQuantity();
            ViewBag.TotalItems = total_item;

            return PartialView("BagCart");
        }
        public ActionResult OrderSuccess()
        {
            return View();
        }
        public ActionResult OrderHistory(string id)
        {
            id= (string)Session["username"];
            if (Session["username"] != null)
            {
                var orderHis = db.orders.ToList().Where(s => s.acc_name == id);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (orderHis == null)
                {
                    return HttpNotFound();
                }
                return View(orderHis.ToList().OrderByDescending(o => o.order_date));
            }
            return Content("<script>alert('You must login');window.location.replace('/');</script>");
        }
    }
}