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
        private FPTBookEntities3 db = new FPTBookEntities3();
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"]=cart;
            }
            return cart;
        }
        public ActionResult AddCart(string id)
        {
            var pro=db.books.SingleOrDefault(s=>s.book_id==id);
            if(pro != null)
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
    }
}