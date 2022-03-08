using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class BookController : Controller
    {
        private FPTBookEntities1 db = new FPTBookEntities1();
        // GET: Book
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.books.ToList());
        }
        public ActionResult Create()
        {
            if (Session["admin"] == null)
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
                    return View();
                }
            }
            return View(book);
        }

        public ActionResult Edit(string id)
        {
                if (Session["admin"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                book book = db.books.Find(id);
                Session["oldpic"] = "~/Content/images/" + book.book_picture;
                if (book == null)
                {
                    return HttpNotFound();
                }
                ViewBag.cate_id = new SelectList(db.categories, "cate_id", "cate_name", book.cate_id);
                return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase image, book book)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string pic = Path.GetFileName(image.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/images"), pic);
                    string oldPath = Request.MapPath(Session["oldpic"].ToString());
                    image.SaveAs(path);
                    
                    book.book_picture = pic;

                    db.Entry(book).State = EntityState.Modified;
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            return View(book);
        }
        public ActionResult Delete(string id)
        {
            if (Session["admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                book book = db.books.Find(id);
                Session["OldPath"] = "~/Content/images/" + book.book_picture;
                if (book == null)
                {
                    return HttpNotFound();
                }
                return View(book);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            string oldPath = Request.MapPath(Session["OldPath"].ToString());

            book book = db.books.Find(id);
            db.books.Remove(book);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}