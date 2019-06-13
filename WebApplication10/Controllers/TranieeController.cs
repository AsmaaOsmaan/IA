using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class TranieeController : Controller
    {
        private testtestEntities4 db = new testtestEntities4();
        //public ActionResult Index()
        //{
        //    var projectRequest = db.projectRequest.Include(p => p.status).Include(p => p.projects).Include(p => p.users);
        //    return View(projectRequest.ToList());
        //}

        public ActionResult listpesonalinformation()
        {
            // var id = 1;


            var sessionId = Convert.ToInt32(Session["Uid"]);

            var users = db.users.Where(x => x.Id == sessionId);
            return View(users.ToList());
        }
        // error the list requests donot work

        // GET: tryy
        public ActionResult requests()
        {
           // var userid = 1;
            var sessionId = Convert.ToInt32(Session["Uid"]);
           
            var requeste = db.hiringRequest.Where(X => X.statusid == 1 && X.userid == sessionId);
            return View(requeste.ToList());
        }

        public ActionResult AccepteRequest(int id)
        {
            var request = db.hiringRequest.Where(x => x.Id == id).FirstOrDefault();
            if (request != null)
            {
                if (request.statusid != 3)
                {
                    request.statusid = 2;
                    projectteams projectteam = new projectteams();
                    projectteam.projectid = request.projectid;
                    projectteam.userid = request.userid;
                    db.projectteams.Add(projectteam);
                    db.SaveChanges();
                    return RedirectToAction("requests");
                }
            }

            return RedirectToAction("requests");
        }
        public ActionResult RejectRequests(int id)
        {
            var request = db.hiringRequest.Where(x => x.Id == id).FirstOrDefault();
            if (request != null)
            {
                if (request.statusid != 2)
                {
                    request.statusid = 3;

                    db.SaveChanges();
                    return RedirectToAction("requests");
                }
            }

            return RedirectToAction("requests");
        }

        public ActionResult current_view()
        {
            var sessionId = Convert.ToInt32(Session["Uid"]);

          //  var userid = 1;
            var current = db.projectteams.Where(x => x.userid == sessionId && x.projects.progressstatus == 2);


            return View(current.ToList());
        }

        public ActionResult previous()
        {
          //  var userid = 1;
            var sessionId = Convert.ToInt32(Session["Uid"]);

            var current = db.projectteams.Where(x => x.userid == sessionId && x.projects.progressstatus == 3);


            return View(current.ToList());
        }

    }
}
    
