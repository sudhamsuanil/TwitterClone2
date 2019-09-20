using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TwitterCloneMVC.DataAccess;
using TwitterCloneMVC.Models;

namespace TwitterCloneMVC.Controllers
{
    public class AccountController : Controller
    {

        DAL dal = new DAL();
        public ActionResult Register()
        {
            return View();  
        }

        [HttpPost]
        public ActionResult Register(UserAcccount account )
        {
            Person person = new Person();
            StringBuilder sbHash = new StringBuilder();
            person.user_id = account.user_id;
            person.email = account.email;
            person.fullName = account.fullName;
            person.active = true;
        
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(account.password));
                foreach (Byte b in result)
                    sbHash.Append(b.ToString("x2"));

            }

            person.password = sbHash.ToString();
            if (ModelState.IsValid)
            {
                if (dal.RegisterUser(person))
                {
                    ModelState.Clear();
                    ViewBag.Message = $"{account.fullName } Created Succesfully!";
                }
            }

            return View();
        }

        //Login

        public ActionResult Login()
        {
            if (Session["UserName"] != null)
                Session.Abandon();
            return View();
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            if (Session["UserName"] != null)
                Session.Abandon();
            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult Login(UserAcccount user)
        {
           
            StringBuilder sbHash = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(user.password));
                foreach (Byte b in result)
                    sbHash.Append(b.ToString("x2"));

            }

            user.password = sbHash.ToString();
            var usr = dal.Authenticateuser(user);
            if (usr != null)
            {
                Session["UserId"] = usr.user_id.ToString();
                Session["UserName"] = usr.fullName.ToString();
                return RedirectToAction("LoggedIn");
            }
            else
            {
                ModelState.AddModelError("", "UserName/Password are incorrect");
            }
            //using (FSDEntities dbContext = new FSDEntities())
            //{
            //    try
            //    {
            //        var usr = dbContext.People.Single(x => x.user_id == user.user_id && x.password == user.password);
            //        if (usr != null)
            //        {
            //            Session["UserId"] = usr.user_id.ToString();
            //            Session["UserName"] = usr.fullName.ToString();
            //            return RedirectToAction("LoggedIn");
            //        }
            //        else
            //        {
            //            ModelState.AddModelError("", "UserName/Password are incorrect");
            //        }
            //    }

            //    catch {
            //        ModelState.AddModelError("", "UserName/Password are incorrect");
            //    }
            //}

            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserId"] != null)
            {
                List<Tweet> tweets = new List<Tweet>();
                string userid = Session["UserId"].ToString();
                tweets = dal.FetchTweetsForuser(userid);
                return View(tweets);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult TimeLinePartial()
        {
            if (Session["UserId"] != null)
            {
                List<Tweet> tweets = new List<Tweet>();
                string userid = Session["UserId"].ToString();
                tweets = dal.FetchTweetsForuser(userid);
                return View(tweets);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}