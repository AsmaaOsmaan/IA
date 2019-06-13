using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication10.Models;


namespace WebApplication10.Controllers
{
    public class homeController : Controller
    {
        private testtestEntities4 db = new testtestEntities4();

        public ActionResult Create()
        {
            ViewBag.progressstatus = new SelectList(db.progress, "Id", "name_");
            ViewBag.customerid = new SelectList(db.users, "Id", "email");
            ViewBag.statusid = new SelectList(db.status, "Id", "name_");
            var projs = db.projects.Where(x => x.statusid == 2 && x.progressstatus == 1).ToList();
            ViewBag.projs = projs;
            return View();
        }

        // POST: homePage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,titel,description,customerid,time_,statusid,progressstatus")] projects projects)
        {
            //var project = db.projects.Where(x => x.titel == projects.titel && x.description == projects.description).FirstOrDefault();
           
                if (ModelState.IsValid)
                {
                    projects.progressstatus = 1;
                    projects.statusid = 1;
                var sessionId = Convert.ToInt32(Session["Uid"]);

                projects.customerid = sessionId;
                    db.projects.Add(projects);
                    db.SaveChanges();
                ///*return RedirectToAction("Index");
                //return View();
                return RedirectToAction("Create");

            }
            //ViewBag.progressstatus = new SelectList(db.progress, "Id", "name_", projects.progressstatus);
            //ViewBag.customerid = new SelectList(db.users, "Id", "email", projects.customerid);
            //ViewBag.statusid = new SelectList(db.status, "Id", "name_", projects.statusid);
            
            return View();
        }

        public PartialViewResult listprojects()
        {

            var projects = db.projects.Where(x => x.statusid == 2 && x.progressstatus == 1);
            return PartialView("_listprojects", projects.ToList());
        }

        public ActionResult xxxx(int id)
        {
            return RedirectToAction("sendrequest/"+id , "Director");
        }

    }
}