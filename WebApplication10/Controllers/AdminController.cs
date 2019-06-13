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
    public class AdminController : Controller
    {
        private testtestEntities4 db = new testtestEntities4();

        // GET: Admin
        public ActionResult Index()
        {
            var users = db.users.Include(u => u.UserRole);
            return View(users.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewBag.usertypeId = new SelectList(db.UserRole, "Id", "titleRole");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,email,firstname,lastname,phone,photo,jobDescription,usertypeId")] users users)
        {
            if (ModelState.IsValid)
            {  
                db.users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.usertypeId = new SelectList(db.UserRole, "Id", "titleRole", users.usertypeId);
            return View(users);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.usertypeId = new SelectList(db.UserRole, "Id", "titleRole", users.usertypeId);
            return View(users);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,email,firstname,lastname,phone,photo,jobDescription,usertypeId")] users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.usertypeId = new SelectList(db.UserRole, "Id", "titleRole", users.usertypeId);
            return View(users);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            users users = db.users.Find(id);
            db.users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // to select pesonal information

        public ActionResult PersonalInformation()
        {
            // var id = 3;
            var sessionId = Convert.ToInt32(Session["Uid"]);
            var users = db.users.Where(x => x.Id == sessionId);
            return View(users.ToList());

            
        }

        //to select all posts pinnded
        public ActionResult selectpostsPinneded()
        {
            var obj = db.projects.Where(x => x.statusid == 1);
            return View(obj.ToList());
        }


        //to select all posts was accepted
        public ActionResult selectpostsaccepted()
        {
            var obj = db.projects.Where(x => x.statusid == 2);
            return View(obj.ToList());
        }


        //to select all posts rejected
        public ActionResult selectpostsrejected()
        {
            var obj = db.projects.Where(x => x.statusid == 3);
            return View(obj.ToList());
        }
        // to accept the post 
        public ActionResult AcceptPost(int id)
        {
            var obj = db.projects.Single(x => x.id == id);
            obj.statusid = 2;
            db.SaveChanges();
            return RedirectToAction("selectpostsPinneded");
           
        }
        // to reject the post 
        public ActionResult RejectPost(int id)
        {
            var obj = db.projects.Single(x => x.id == id);
            obj.statusid = 3;
            db.SaveChanges();
            return RedirectToAction("selectpostsPinneded");

        }
        public ActionResult userrole(int id)
        {
            var obj = db.users.Single(x => x.Id == id);
            return View();

        }
        [HttpPost]
        public ActionResult userrole(int id, users rt)
        {
            //var obj = db.users.Single(x => x.Id == id);
            //obj.usertypeId = role;
            //db.SaveChanges();

            var obj = db.users.Find(id);
            obj.usertypeId = rt.usertypeId;
            //obj.usertypeId = role;
        db.SaveChanges();



            return RedirectToAction("index");

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
