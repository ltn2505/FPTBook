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
        private FPTBookEntities2 db = new FPTBookEntities2();
        // GET: Account
        public ActionResult Index()
        {
            if(Session["Admin"] != null)
            {
                return View(db.accounts.ToList());
            }
            else
            {
                Response.Write("<script>alert('you are not admin!')</script>");
            }
            return RedirectToAction("Index", "Home");
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
                        Session["admin"] = acc_name;
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
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Home");
        }
    }
}