using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WasifProject.Models;

namespace WasifProject.Controllers
{
    public class AdminController : Controller
    {
        //private CodeDBEntities db = new CodeDBEntities();

        private IAdminRepos myRepos=null;

        public AdminController(IAdminRepos repo) 
        {
            myRepos = repo;
        }

        //public ActionResult Edit(int id = 0)
        //{
        //    Admin admin = db.Admins.Find(id);
        //    if (admin == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(admin);
        //}

        ////
        //// POST: /Admin/Edit/5

        //[HttpPost]
        //public ActionResult Edit(Admin admin)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(admin).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(admin);
        //}

        

        protected override void Dispose(bool disposing)
        {
            myRepos.Dispose();   
            base.Dispose(disposing);
        }

        public ActionResult Login() 
        {
            return View();
        }

        public ActionResult ValidateAndLogin(Admin admin)
        {

            Admin obj = myRepos.LoginAdmin(admin);
            if (obj != null)
            {
                Session["Admin"] = obj;
                return RedirectToAction("../Admin/Menu");
            }
            else
            {
                ViewBag.ErrorMassge = "The userName or Password is incorrect try again.";
                return RedirectToAction("../Admin/Login");
            }
            
            
        }

        public ActionResult Menu() 
        {
            Admin obj = Session["Admin"] as Admin;
            if (obj == null)
                return RedirectToAction("../Admin/Login");
            return View(myRepos.GetFiles());
        }
    }
}