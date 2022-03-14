using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class OrderController : Controller
    {
        private FPTBookEntities3 db = new FPTBookEntities3();
        // GET: Order
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.orders.ToList());
        }

        public ActionResult Details(int? id)
        {
           
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                order order = db.orders.Find(id);
                if (order == null)
                {
                    return HttpNotFound();
                }
                return View(order);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(order order)
        {
            if (ModelState.IsValid)
            {
                db.orders.Attach(order);
                db.Entry(order).Property(e => e.order_id).IsModified = true;

                db.SaveChanges();
                if (Session["admin"] != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("OrderHistory", "ShoppingCart", new { id = Session["Username"] });
                }
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order order = db.orders.Find(id);

            foreach (var item in db.order_detail)
            {
                if (item.order_id == id)
                {
                    db.order_detail.Remove(item);
                }
            }
            
                       
            db.orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}