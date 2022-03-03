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
        private FPTBookEntities1 db = new FPTBookEntities1();
        // GET: Account
        public ActionResult Index()
        {
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
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var data = db.accounts.Where(e => e.acc_name.Equals(username) && e.password.Equals(password)).ToList();

                if (data.Count() > 0)
                {
                        Session["Username"] = data.FirstOrDefault().acc_name;
                        return RedirectToAction("Index", "Home");
                    
                }
                else
                {
                    Response.Write("<script>alert('Username or Password is wrong. Please enter again!')</script>");
                }
            }
            return View();
        }
    }
}