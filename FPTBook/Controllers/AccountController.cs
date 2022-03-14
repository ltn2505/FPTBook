using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FPTBook.Models;

namespace FPTBook.Controllers
{
    public class AccountController : Controller
    {
        private FPTBookEntities3 db = new FPTBookEntities3();
        // GET: Account
        public ActionResult Index()
        {
            if(Session["admin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.accounts.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(account account)
        {
            if (ModelState.IsValid)
            {
                db.accounts.Add(account);
                db.SaveChanges();
                Response.Write("<script>alert('Register new account success!')</script>");
                return RedirectToAction("Login","Account");
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string acc_name, string password)
        {
            if (ModelState.IsValid)
            {
                var data = db.accounts.Where(e => e.acc_name.Equals(acc_name) && e.password.Equals(password)).ToList();

                if (data.Count()>0)
                {
                    Session["username"] = acc_name;
                    if (data.FirstOrDefault().state!=null)
                    {
                        Session["admin"] = "admin";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Username or Password is wrong. Please enter again!')</script>");
                }
            }
            return View();
        }
        public ActionResult Edit(string id)
        {
            id = (string)Session["username"];
            account objAccount = db.accounts.ToList().Find(a => a.acc_name.Equals(id));
            if (objAccount == null)
            {
                return HttpNotFound();
            }
            return View(objAccount);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "acc_name,password,confirmpass,full_name,gender,email,address,state")] account account)
        {
            if (ModelState.IsValid)
            {
                db.accounts.Attach(account);

                db.Entry(account).Property(a => a.full_name).IsModified = true;
                db.Entry(account).Property(a => a.gender).IsModified = true;
                db.Entry(account).Property(a => a.email).IsModified = true;
                db.Entry(account).Property(a => a.address).IsModified = true;

                db.SaveChanges();

                Response.Write("<script>alert('Update information success!');window.location='/';</script>");
            }
            return View(account);
        }

        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Delete(string id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            account account = db.accounts.Find(id);
            db.accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}