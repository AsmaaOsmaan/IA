using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class CustomerController : Controller
    {
        private testtestEntities4 db = new testtestEntities4();

        // GET: Customer
        public ActionResult Index()
        {
            // var usreid = 2;
            var sessionId = Convert.ToInt32(Session["Uid"]);
            var projects = db.projects.Include(p => p.progress).Include(p => p.users).Include(p => p.status).Where(x=>x.customerid == sessionId);
            return View(projects.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            projects projects = db.projects.Find(id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            return View(projects);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
           // ViewBag.progressstatus = new SelectList(db.progress, "Id", "name_");
          //  ViewBag.customerid = new SelectList(db.users, "Id", "email");
           // ViewBag.statusid = new SelectList(db.status, "Id", "name_");
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,titel,description,customerid,time_,statusid,progressstatus")] projects projects)
        {
            if (ModelState.IsValid)
            {
                var sessionId = Convert.ToInt32(Session["Uid"]);

               // var sesstion = 2;
                projects.customerid = sessionId;
                projects.progressstatus = 1;
                projects.statusid = 1;
                db.projects.Add(projects);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.progressstatus = new SelectList(db.progress, "Id", "name_", projects.progressstatus);
            ViewBag.customerid = new SelectList(db.users, "Id", "email", projects.customerid);
            ViewBag.statusid = new SelectList(db.status, "Id", "name_", projects.statusid);
            return View(projects);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            projects projects = db.projects.Find(id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            ViewBag.progressstatus = new SelectList(db.progress, "Id", "name_", projects.progressstatus);
            ViewBag.customerid = new SelectList(db.users, "Id", "email", projects.customerid);
            ViewBag.statusid = new SelectList(db.status, "Id", "name_", projects.statusid);
            return View(projects);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,titel,description,customerid,time_,statusid,progressstatus")] projects projects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.progressstatus = new SelectList(db.progress, "Id", "name_", projects.progressstatus);
            ViewBag.customerid = new SelectList(db.users, "Id", "email", projects.customerid);
            ViewBag.statusid = new SelectList(db.status, "Id", "name_", projects.statusid);
            return View(projects);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            projects projects = db.projects.Find(id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            return View(projects);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            projects projects = db.projects.Find(id);
            db.projects.Remove(projects);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // to list requests form director to customer
        public ActionResult listrequests()
        {
            // instead of sesstion [id]
            // var userid = 1;
            var sessionId = Convert.ToInt32(Session["Uid"]);
            var project = db.projectRequest.Where(x => x.projects.customerid == sessionId && x.statusid == 1);
            return View(project.ToList());
        }


// to accept requests
        public ActionResult AcceptRequest(int id)
        {
            var project = db.projectRequest.Where(x => x.Id == id).FirstOrDefault();
            if (project != null) {
                if (project.statusid != 3)
                {
                    project.statusid = 2;
                    project.projects.progressstatus = 2;
                    projectteams team = new projectteams();
                    team.projectid = project.projects.id;
                    team.userid = project.managerid;
                    db.projectteams.Add(team);
                    db.SaveChanges();
                    return RedirectToAction("listrequests");
                }
            }
            
            return RedirectToAction("listrequests");
        }
        // to reject requests
        public ActionResult RejectRequest(int id)
        {
            var project = db.projectRequest.Where(x => x.Id == id).FirstOrDefault();
            if (project != null)
            {
                if (project.statusid != 2)
                {
                    project.statusid = 3;
                   
                    db.SaveChanges();
                    return RedirectToAction("listrequests");
                }
            }

            return RedirectToAction("listrequests");
        }

        // to list personal indormation 
        public ActionResult listInformation()
        {
            var sessionId = Convert.ToInt32(Session["Uid"]);
          //  var userid = 1;
            var list = db.users.Where(x => x.Id == sessionId);
           
            return View(list.ToList());
        }
      public PartialViewResult notdliverd()
        {
            var sessionId = Convert.ToInt32(Session["Uid"]);
           // var userid = 1;
            var current = db.projects.Where(x => x.customerid == sessionId && x.progressstatus == 2).ToList();
            return PartialView("_notdliverd", current);
        }
        public PartialViewResult dliverd()
        {
            var sessionId = Convert.ToInt32(Session["Uid"]);

            //var userid = 1;
            var previous = db.projects.Where(x => x.customerid == sessionId && x.progressstatus == 3).ToList();
            return PartialView("_dliverd", previous);
        }






        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
