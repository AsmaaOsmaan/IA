using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class UserController : Controller
    {
       
        
            users user = new users();
            // GET: User
            public ActionResult AddorEdit(int id = 0)
            {
                return View(user);
            }
            [HttpPost]
            public ActionResult AddorEdit(users user, HttpPostedFileBase file)
            {
                using (testtestEntities4 db = new testtestEntities4())
                {
                    if (db.users.Any(x => x.email == user.email))
                    {
                        ViewBag.DuplicateMessage = "email Already Exists.";
                        return View("AddOrEdit", user);
                    }
                    
                   
                    if (file != null && file.ContentLength > 0)
                    {
                        string filename = Path.GetFileName(file.FileName);
                        string imgpath = Path.Combine(Server.MapPath("~/user-images"), file.FileName);
                        file.SaveAs(imgpath);
                        user.photo = file.FileName;
                        user.usertypeId = 2;
                     

                        db.users.Add(user);
                        db.SaveChanges();

                    }

                    ModelState.Clear();

                    ViewBag.SuccessMessage = "Registration Successful.";
                    return View("AddOrEdit", new users());
                }

            }

        }

    }




