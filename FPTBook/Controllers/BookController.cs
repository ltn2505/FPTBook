using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class BookController : Controller
    {
        private FPTBookEntities3 db = new FPTBookEntities3();
        // GET: Book
        public ActionResult Index()
        {
            return View(db.books.ToList());
        }
        public ActionResult Create()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.cate_id = new SelectList(db.categories, "cate_id", "cate_name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase image, book book)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string pic = Path.GetFileName(image.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/images/"), pic);
                    image.SaveAs(path);

                    book.book_picture = pic;
                    db.books.Add(book);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "............";
                    return View();
                }
            }

            return View(book);
        }
    }
}