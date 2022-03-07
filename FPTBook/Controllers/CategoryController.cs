using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        private FPTBookEntities3 db = new FPTBookEntities3();
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.categories.ToList());
        }
        public ActionResult Create()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cate_id,cate_name,cate_description")] category category)
        {
            if (ModelState.IsValid)
            {
                db.categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

    }
}