using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class LoginController : Controller
    {
        private testtestEntities4 db = new testtestEntities4();
        // GET: Login
        public ActionResult Index()
        {

            return View();
        }



        public ActionResult login()
        {


            return View();
        }
        [HttpPost]
        public ActionResult login(users user)
        {

            var users = db.users.ToList();
            var mail = user.email;
            //var result = db.users.Where(x => x.email == user.email).FirstOrDefault();
            //  var result = db.users.SingleOrDefault(x => x.email.Equals(user.email));
            var result = db.users.SingleOrDefault(x => x.email.Equals(user.email)&&x.PassWord.Equals(user.PassWord));

            if (result != null )
            {
                Session["Uid"] = result.Id;
                Session["Role"] = result.UserRole.titleRole;
                Session["UserTypeId"] = result.usertypeId;
                Session["FirstName"] = result.firstname;
                Session["LastName"] = result.lastname;
                Session["Email"] = result.email;
                return RedirectToAction("Create", "home");
            }
            else
            {
                return RedirectToAction("AddorEdit", "User");
            }
            


        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("login");
        }

    }
}