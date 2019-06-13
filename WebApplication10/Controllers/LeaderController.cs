using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class LeaderController : Controller
    {
        private testtestEntities4 db = new testtestEntities4();
        // GET: Leader
        // to list the requests from directors 
       public ActionResult list()
        {
            var sessionId = Convert.ToInt32(Session["Uid"]);
           // var userid = 1;
            var hiringRequests = db.hiringRequest. Where(x=>x.userid== sessionId && x.statusid==1);
            return View(hiringRequests.ToList());
        }
        //************
        // to accept request from dierectors
        public ActionResult AcceptRequest(int id)
        {

            var hiring = db.hiringRequest.Where(x => x.Id == id).FirstOrDefault();
             if (hiring != null)
            {
                if (hiring.statusid != 3)
                {
                    hiring.statusid = 2;
                    projectteams team = new projectteams();
                    team.projectid = id;
                    team.userid = hiring.userid;
                    db.projectteams.Add(team);
                    db.SaveChanges();
                }
                return RedirectToAction("list");
            }


            return RedirectToAction("list");
        }
        //*******************
       // to reject request from dierectors
        public ActionResult rejectRequest(int id)
        {

            var hiring = db.hiringRequest.SingleOrDefault(x => x.Id == id);
            if (hiring != null)
            {
                if (hiring.statusid != 2)
                {
                    hiring.statusid = 3;
                    
                    db.SaveChanges();
                }
                return RedirectToAction("list");
            }


            return Content("sorry");
        }
       
        //****to view the current  projects

        public IEnumerable<projectteams> Getcurrent()
        {
            // x.userid==2 **instead of session[id]
            var sessionId = Convert.ToInt32(Session["Uid"]);


            var current = db.projectteams.Where(x => x.userid == sessionId && x.projects.progressstatus == 2).ToList();
            return current;
        }

        public  PartialViewResult  current()
        {

            var current = Getcurrent();


            return PartialView("_current", current);
        }
        //***//****to view the  previous projects



        public IEnumerable<projectteams> GetPrevious()
        {
            // x.userid==2 **instead of session[id]

            var sessionId = Convert.ToInt32(Session["Uid"]);

            var previous = db.projectteams.Where(x => x.userid == sessionId && x.projects.progressstatus == 3).ToList();
            return previous;
        }

        public PartialViewResult previous()
        {

            var previous = GetPrevious();


            return PartialView("_previous", previous);
        }





        //******list information the leader

        public ActionResult listinformation()
        {
            var sessionId = Convert.ToInt32(Session["Uid"]);

           // var id = 2;
            var users = db.users.Where(x => x.Id == sessionId);
            return View(users.ToList());
        }
        // view team
       
        public ActionResult ViewTeam(int id )
        {
           
            var team = db.projectteams.Where(x => x.projectid == id&&x.users.usertypeId==2);
            return View(team.ToList());
        }
       
        
       


        /////evaluat the traniee////////////////////////////////////////


        //public ActionResult evaluate(int projectid, int userid)
        //{
        //    var obj = db.feedback.SingleOrDefault(x => x.projectId == projectid && x.traineeId == userid);
        //    if (obj == null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        // return View(obj.rate);

        //        return View();
        //    }
        //}

        //[HttpPost]
        //public ActionResult evaluate(int projectid,int userid, int rate)
        //{
        //    var obj = db.feedback.SingleOrDefault(x=>x.projectId== projectid&&x.traineeId== userid);
        //    if (obj == null)
        //    {
        //        //instead of sesstion[id]
        //        var teamleader = 1;
        //        feedback evaluate = new feedback();
        //        evaluate.projectId = projectid;
        //        evaluate.traineeId = userid;
        //        evaluate.teamLeaderId = teamleader;
        //        evaluate.rate = rate;
        //        db.feedback.Add(evaluate);
        //        db.SaveChanges();
        //        return View();
        //    }
        //    return View(obj.rate);
        //  //  return RedirectToAction("ViewTeam");
        //}

        //***************  tryy**///////////


        public ActionResult evaluate(int projectid, int userid)
        {
            var obj = db.feedback.SingleOrDefault(x => x.projectId == projectid && x.traineeId == userid);
            if (obj == null)
            {
                return View();
            }
            else
            {
                // return View(obj.rate);

                return View();
            }
        }

        [HttpPost]
        public ActionResult evaluate(int projectid, int userid, int rate)
        {
            var obj = db.feedback.SingleOrDefault(x => x.projectId == projectid && x.traineeId == userid);

            //instead of sesstion[id]
            //  var teamleader = 1;
            var sessionId = Convert.ToInt32(Session["Uid"]);

            feedback evaluate = new feedback();
                evaluate.projectId = projectid;
                evaluate.traineeId = userid;
                evaluate.teamLeaderId = sessionId;
                evaluate.rate = rate;
                db.feedback.Add(evaluate);
                db.SaveChanges();
              //  return View();
           
           // return View(obj.rate);
              return RedirectToAction("listinformation");
        }


    }
}