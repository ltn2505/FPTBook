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
            if (Session["Admin"] != null)
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
            return View("Error");
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
                if (Session["Admin"] != null)
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
    }
}