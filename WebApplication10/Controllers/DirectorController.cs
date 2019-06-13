using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class DirectorController : Controller
    {
        private testtestEntities4 db = new testtestEntities4();
        //the id =1 is the defult value instead of session["id"] **from login**
        // GET: Director
        public ActionResult list()
        {
            var sessionId = Convert.ToInt32(Session["Uid"]);

           // var id = 1;
            var users = db.users.Where(x => x.Id == sessionId);
            return View(users.ToList());
        }



        // GET: Director
        public ActionResult listprojects()
        {

            var projects = db.projects.Where(x => x.statusid == 2 && x.progressstatus == 1);
            return View(projects.ToList());
        }

        //[HttpGet]
        public ActionResult sendrequest(int id)
        {
            var sessionId = Convert.ToInt32(Session["Uid"]);

            var project = db.projectRequest.Where(X => X.projectid == id&&X.managerid== sessionId);
            if (project == null || project.Count() == 0)
            {

                projectRequest projectRequest = new projectRequest();
                projectRequest.projectid = id;
             
                //*defult value instead of session["id"]
                // projectRequest.managerid = 2;
                projectRequest.managerid = sessionId;
                projectRequest.statusid = 1;
                //projectRequest.time = DateTime.Now;
                db.projectRequest.Add(projectRequest);
                db.SaveChanges();
                return RedirectToAction("listprojects");



            }
            else
            {
                return View("tryy");
            }
        }

        //************************************

        public ActionResult check()
        {
            var sessionId = Convert.ToInt32(Session["Uid"]);

          //  var managerid = 2;
            var request = db.projectRequest.Where(x => x.managerid == sessionId);
            return View(request.ToList());
        }
        //************************


        [HttpGet]

        public ActionResult ScheduleProject(int? id)
        {


            var list = db.progress.ToList();
            ViewBag.progressStatusid = list;
            //ViewBag.progressStatusid = new SelectList(db.progress, "Id", "name");
            // int x = 0;
            return View();
        }



        [HttpPost]
        public ActionResult ScheduleProject(projectdetails proiectdetails, int? id)
        {
            if (ModelState.IsValid)
            {
                proiectdetails.projectid = id;
                db.projectdetails.Add(proiectdetails);
                db.SaveChanges();
                return RedirectToAction("check");
            }
            ViewBag.progressStatusid = new SelectList(db.progress, "Id", "name", proiectdetails.progressStatusid);


            return View("ScheduleProject", proiectdetails);
        }

        /////// function of invite team members

        [HttpGet]

        public ActionResult inviteTeam(int proid)
        {
            var list = db.users.ToList();
            ViewBag.userid = list;

            return View();
        }



        [HttpPost]
        public ActionResult inviteTeam(int proid, hiringRequest hiringrequst)
        {
            {
                var project = db.hiringRequest.Where(X => X.userid == hiringrequst.userid && X.projectid == proid);
                if (project == null || project.Count() == 0)
                {
                    hiringRequest request = new hiringRequest();

                    request.projectid = proid;
                    request.userid = hiringrequst.userid;
                    request.statusid = 1;
                    db.hiringRequest.Add(request);
                    db.SaveChanges();
                    return RedirectToAction("check");
                }


                return RedirectToAction("check");
            }


        }
        //********************************
        //*******************************************
        public IEnumerable<projectteams> GetPrevious()
        {
            var sessionId = Convert.ToInt32(Session["Uid"]);

            var previous = db.projectteams.Where(x => x.userid == sessionId && x.projects.progressstatus == 3).ToList();
            return previous;
        }

        public ActionResult previous()
        {

            var previous = GetPrevious();


            return View(previous);
        }



        //public ActionResult previous()
        //{
        //    var userid = 1;
        //    var  current = db.projectteams.Where(x => x.userid == userid && x.projects.progressstatus == 3);


        //    return View(current.ToList());
        //}
        //*******************************************
        public IEnumerable<projectteams> Getcurrent()
        {
            var sessionId = Convert.ToInt32(Session["Uid"]);

            var current = db.projectteams.Where(x => x.userid == sessionId && x.projects.progressstatus == 2).ToList();
            return current;
        }

        public ActionResult current()
        {

            var current = Getcurrent();


            return View(current);
        }


        public ActionResult Delverd(int id)
        {

            var current = db.projectteams.SingleOrDefault(x => x.Id == id);
            current.projects.progressstatus = 3;

            db.SaveChanges();

            return RedirectToAction("current");
        }
        //**********try leave project action**//////

        public ActionResult leaveproject(int id)
        {


            var current = db.projectteams.SingleOrDefault(x => x.Id == id);
            current.projectid = null;
            current.userid = null;

            db.SaveChanges();
            return RedirectToAction("current");

        }
        //public ActionResult Delverd(int id)
        //{

        //    //var current = db.projectteams.Where(x => x.projectid == id);
        //    //current.projects.progressstatus = 3;

        //    //db.SaveChanges();
        //    var current = db.projectteams.Where(x => x.projectid == id).FirstOrDefault();

        //    current.projects.progressstatus = 3;
        //    db.SaveChanges();
        //    return RedirectToAction("current");
        //}

        public ActionResult ViewTeam(int? id)
        {

            var team = db.projectteams.Where(x => x.projectid == id && x.users.usertypeId != 3);
            return View(team.ToList());
        }
        [HttpPost]
        public ActionResult ViewTeam(int id)
        {

            var team = db.projectteams.Where(x => x.projectid == id && x.users.usertypeId != 3);
            return View(team.ToList());
        }
       
    

        public ActionResult rej(int id)
        {
            var obj = db.projectteams.Where(x => x.Id == id).FirstOrDefault();
            if (obj != null)
            {
                db.projectteams.Remove(obj);
                db.SaveChanges();

                return Content("okyy");
            }
            return Content("no");
        }


    }
}
   