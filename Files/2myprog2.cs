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
    public class UserController : Controller
    {


        //
        // GET: /User/
        private IuserRepository myRepository = null;

        public UserController(IuserRepository repo)
        {
            myRepository = repo;
        }

        public ActionResult Index()
        {
            UserTable user = Session["Key"] as WasifProject.Models.UserTable;
            if (user != null)
            {
                UserTable obj = myRepository.GetObj(user.Id);
               
                return View(obj);
            }
            return RedirectToAction("../User/Login");
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id = 0)
        {
            UserTable usertable = myRepository.GetObj(id);
            if (usertable == null)
            {
                return HttpNotFound();
            }
            return View(usertable);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserTable usertable)
        {
            if (ModelState.IsValid)
            {
                myRepository.Register(usertable);
                return RedirectToAction("Index");
            }

            return View(usertable);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserTable usertable = myRepository.GetObj(id);
            if (usertable == null)
            {
                return HttpNotFound();
            }
            return View(usertable);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserTable usertable)
        {
            if (ModelState.IsValid)
            {
                myRepository.SaveEdite(usertable);
                return RedirectToAction("Index");
            }
            return View(usertable);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UserTable usertable = myRepository.GetObj(id);
            if (usertable == null)
            {
                return HttpNotFound();
            }
            return View(usertable);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserTable usertable = myRepository.GetObj(id);
            myRepository.RemoveObj(usertable);
            Session["Key"] = null;
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            myRepository.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            Session["key"] = null;
            return View();
        }
        public ActionResult ValidateAndLogin(userLogin ut)
        {

            var obj = myRepository.Login(ut);
            if (obj != null)
            {
                Session["Key"] = obj;
                return RedirectToAction("../Home/Account");
            }
            else
            {
                return RedirectToAction("../User/Login");
            }
        }

        [HttpPost]
        public ActionResult ValidateAndAdd(UserTable us)
        {

            if (myRepository.Register(us))
            {
                var user = us;
                Session["Key"] = user; //Put value into session
                return RedirectToAction("../Home/Gallery/");
            }
            else
            {
                return RedirectToAction("../User/Register");
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        public JsonResult CheckUserName()
        {
            string userName = Request["userName"];
            return this.Json(myRepository.CheckUserName(userName), JsonRequestBehavior.AllowGet);
        }
    }
}