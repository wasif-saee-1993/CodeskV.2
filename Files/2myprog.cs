using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WasifProject.Models;

namespace WasifProject.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SearchData( )
        {
            var db = new CodeDBEntities();
            string name = Request["Name"];
            
              var objs  =  from val in db.FileDatas  
                           where val.fileName.Contains(name) 
                           select val;
              
            if (objs == null)
            {
                return this.Json(null, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(objs, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
