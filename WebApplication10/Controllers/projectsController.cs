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
    public class projectsController : Controller
    {
        private testtestEntities4 db = new testtestEntities4();

        // GET: projects
        public ActionResult Index()
        {
            var projects = db.projects.Include(p => p.progress).Include(p => p.users).Include(p => p.status);
            return View(projects.ToList());
        }

        // GET: projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            projects project = db.projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: projects/Create
        public ActionResult Create()
        {
            ViewBag.progressstatus = new SelectList(db.progress, "Id", "name_");
            ViewBag.customerid = new SelectList(db.users, "Id", "firstname");
            ViewBag.statusid = new SelectList(db.status, "Id", "name_");
            return View();
        }

        // POST: projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,titel,description,customerid,time_,statusid,progressstatus")] projects project)
        {
            if (ModelState.IsValid)
            {
                db.projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.progressstatus = new SelectList(db.progress, "Id", "name_", projects.progressstatus);
            //ViewBag.customerid = new SelectList(db.users, "Id", "firstname", project.customerid);
            //ViewBag.statusid = new SelectList(db.status, "Id", "name_", project.statusid);
            return View(project);
        }

        // GET: projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            projects project = db.projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.progressstatus = new SelectList(db.progress, "Id", "name_", project.progressstatus);
            ViewBag.customerid = new SelectList(db.users, "Id", "email", project.customerid);
            ViewBag.statusid = new SelectList(db.status, "Id", "name_", project.statusid);
            return View(project);
        }

        // POST: projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,titel,description,customerid,time_,statusid,progressstatus")] projects project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.progressstatus = new SelectList(db.progress, "Id", "name_", projects.progressstatus);
            //ViewBag.customerid = new SelectList(db.users, "Id", "email", project.customerid);
            //ViewBag.statusid = new SelectList(db.status, "Id", "name_", project.statusid);
            return View(project);
        }

        // GET: projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            projects project = db.projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            projects project = db.projects.Find(id);
            db.projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult listprojects()
        {
            var sessionId = Convert.ToInt32(Session["Uid"]);

         //   var userid = 1;
            var project = db.projectRequest.Where(x => x.projects.customerid == sessionId);
            return View(project.ToList());
        }

        public ActionResult AcceptRequest(int id)
        {
            var project = db.projectRequest.Where(x => x.projectid == id).FirstOrDefault();
            if (project.statusid != 3)
            {
                project.statusid = 2;
                projectteams team = new projectteams();
                team.projectid = id;
                team.userid = project.managerid;
                db.projectteams.Add(team);
                db.SaveChanges();
                return RedirectToAction("listprojects");
            }
            return RedirectToAction("listprojects");
        }

        public ActionResult rejectRequest(int id)
        {
            var project = db.projectRequest.Where(x => x.projectid == id).FirstOrDefault();
            if (project.statusid != 2)
            {
                project.statusid = 3;
                db.SaveChanges();
                return RedirectToAction("listprojects");
            }
            return RedirectToAction("listprojects");
        }

        public ActionResult listInformation()
        {
          //  var userid = 1;
            var sessionId = Convert.ToInt32(Session["Uid"]);
            var list = db.users.Where(x => x.Id == sessionId);

            return View(list.ToList());
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
