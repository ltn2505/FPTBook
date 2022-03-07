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
            if(Session["Admin"] == null)
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
        public ActionResult Create([Bind(Include = "acc_name,password,full_name,gender,email,address")] account account)
        {
            if (ModelState.IsValid)
            {
                db.accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
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

                if (data!=null)
                {
                    Session["username"] = acc_name;
                    if (data.FirstOrDefault().state!=null)
                    {
                        Session["admin"] = "admin";
                        return RedirectToAction("Index", "Account");
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
        public ActionResult Edit()
        {
            account objAccount = db.accounts.ToList().Find(a => a.acc_name.Equals(Session["username"]));
            if (objAccount == null)
            {
                return HttpNotFound();
            }
            return View(objAccount);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "acc_id,acc_name,password,full_name,gender,email,address")] account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(account);
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Home");
        }
    }
}