using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class HomeController : Controller
    {
        private FPTBookEntities3 db = new FPTBookEntities3();
        public ActionResult Index()
        {
            return View(db.books.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult BookDetail(string id)
        {
            var books = db.books.ToList().Find(a => a.book_id == id);
            return View(books);
        }
    }
}