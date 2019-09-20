using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterCloneMVC.DataAccess;
using TwitterCloneMVC.Models;

namespace TwitterCloneMVC.Controllers
{
    public class TwitterController : Controller
    {
        DAL dal = new DAL();
        // GET: Twitter
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTweet(FormCollection Form)
        {
            Tweet tweetObj = new Tweet();
            tweetObj.user_id = Session["UserId"].ToString();
            tweetObj.message = Form["tweetTextArea"].ToString();
            tweetObj.Created = DateTime.Now;

            if (dal.AddTweet(tweetObj))
                return RedirectToAction("LoggedIn", "Account");
            else
            {
                ModelState.AddModelError("", "UserName/Password are incorrect");
                return View();
            }
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult ManageFollowing()
        {
            List<FollowersEntity> ent = new List<FollowersEntity>();
            string userId = Session["UserId"].ToString();
            if (Session["UserId"] != null)
            {

                ent= dal.GetFollowing(Session["UserId"].ToString());
                return View(ent);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        public ActionResult ViewFollowers()
        {
            //List<Person> follwers = new List<Person>();
            List<FollowersEntity> ent = new List<FollowersEntity>();
            if (Session["UserId"] != null) {

                ent = dal.GetFollowers(Session["UserId"].ToString());
                return View(ent);



            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult ManageTweets()
        {

            if (Session["UserId"] != null)
            {
                List<Tweet> tweets = new List<Tweet>();
                string userid = Session["UserId"].ToString();
                tweets=dal.ManageTweets(userid);

                return View(tweets);
            }
            else
            {
                return RedirectToAction("Login","Home");
            }
           
        }

        public ActionResult EditTweet(int id)
        {
            if (Session["UserId"] != null)
            {
                //Tweet tweet = new Tweet();

               var tweet= dal.EditTweet(id);
                return View(tweet);

                
            }
            else
            {
                return RedirectToAction("Login","Home");
            }

        }
        [HttpPost]
        public ActionResult EditTweet(Tweet tweet)
        {
            if (Session["UserId"] != null)
            {
                //Tweet tweet = new Tweet();

                //using (FSDEntities dbContext = new FSDEntities())
                //{

                //    var PresntTweet = (Tweet)dbContext.Tweets.Where(x => x.tweet_id == tweet.tweet_id).First();
                //    PresntTweet.message = tweet.message;
                //    dbContext.SaveChanges();
                //    return RedirectToAction("ManageTweets");

                //}
                if (dal.UpdateTweet(tweet))
                    return RedirectToAction("ManageTweets");
                else
                {
                    ModelState.AddModelError("", "Something went wrong, please contact Admin");
                    return View();
                }


            }
            else
            {
                return RedirectToAction("Login","Account");
            }

        }

        public ActionResult DeleteTweet(int id)
        {
            if (Session["UserId"] != null)
            {
                //Tweet tweet = new Tweet();

                //using (FSDEntities dbContext = new FSDEntities())
                //{

                //    var PresntTweet = (Tweet)dbContext.Tweets.Where(x => x.tweet_id == id).First();
                //    dbContext.Tweets.Remove(PresntTweet);
                //    dbContext.SaveChanges();
                //    return RedirectToAction("ManageTweets");

                //}
                if (dal.DeleteTweet(id))
                    return RedirectToAction("ManageTweets");
                else
                {
                    ModelState.AddModelError("", "Something went wrong, please contact Admin");
                    return View();
                }


            }
            else
            {
                return RedirectToAction("Login","Account");
            }
        }

        public ActionResult SearchToFollow()
        {
            List<Person> follwers = new List<Person>();
            if (Session["UserId"] != null)
            {

                //using (FSDEntities dbContext = new FSDEntities())
                //{
                //    try
                //    {
                //        List<FollowersEntity> ent = new List<FollowersEntity>();
                //        string userId = Session["UserId"].ToString();
                //        follwers = dbContext.People.Where(x => x.user_id != userId).ToList();

                //        return View(follwers);
                //    }

                //    catch (DbEntityValidationException ex)
                //    {
                //        // Retrieve the error messages as a list of strings.
                //        var errorMessages = ex.EntityValidationErrors
                //                .SelectMany(x => x.ValidationErrors)
                //                .Select(x => x.ErrorMessage);

                //        // Join the list to a single string.
                //        var fullErrorMessage = string.Join("; ", errorMessages);

                //        // Combine the original exception message with the new one.
                //        var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                //        // Throw a new DbEntityValidationException with the improved exception message.
                //        throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                //    }
                //}

                follwers = dal.SearchtoFollow(Session["UserId"].ToString());
            }
            return View();
        }

        public ActionResult ManageAccounts()
        {
            if (Session["UserId"] != null)
            {
                string userid = Session["UserId"].ToString();
                //using (FSDEntities dbContext = new FSDEntities())
                //{
                //    Person present = dbContext.People.Where(x => x.user_id == userid).First();
                //    return View(present);
                //}
                Person present = dal.ManageAccount(userid);
                return View(present);

            }

            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult ManageAccounts(Person updatePerson)
        {
            if (Session["UserId"] != null)
            {
                string userid = Session["UserId"].ToString();
                //using (FSDEntities dbContext = new FSDEntities())
                //{
                //    Person present = dbContext.People.Where(x => x.user_id == userid).First();
                //    present.email = updatePerson.email;
                //    present.fullName = updatePerson.fullName;
                //    present.active = updatePerson.active;
                //    dbContext.SaveChanges();
                //    return RedirectToAction("LoggedIn", "Account");
                //}
                if (dal.UpdateAccount(updatePerson, userid))
                    return RedirectToAction("LoggedIn", "Account");
                else
                {
                    ModelState.AddModelError("", "Something went wrong while update persond details, please contact Admin");
                    return View();
                }
                    


            }

            return RedirectToAction("Login", "Account");
        }
        public ActionResult AddToFollow(string id)
        {
            Person presentPerson = new Person();
            Person toBeFollowedPerson = new Person();
            if (Session["UserId"] != null)
            {
                if (dal.AddToFollow(Session["UserId"].ToString()))
                    return RedirectToAction("ManageFollowing");
                else
                    return RedirectToAction("Error");

                //using (FSDEntities dbContext = new FSDEntities())
                //{
                //    try
                //    {
                //        List<FollowersEntity> ent = new List<FollowersEntity>();
                //        string userId = Session["UserId"].ToString();
                //        toBeFollowedPerson = dbContext.People.Where(x => x.user_id == id).FirstOrDefault();
                //        dbContext.People.Where(x => x.user_id == userId).First().People.Add(toBeFollowedPerson);
                //        //presentPerson.People.Add(toBeFollowedPerson);
                //        dbContext.SaveChanges();
                //        return RedirectToAction("ManageFollowing");
                //    }

                //    catch (DbEntityValidationException ex)
                //    {
                //        // Retrieve the error messages as a list of strings.
                //        var errorMessages = ex.EntityValidationErrors
                //                .SelectMany(x => x.ValidationErrors)
                //                .Select(x => x.ErrorMessage);

                //        // Join the list to a single string.
                //        var fullErrorMessage = string.Join("; ", errorMessages);

                //        // Combine the original exception message with the new one.
                //        var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                //        // Throw a new DbEntityValidationException with the improved exception message.
                //        throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                //    }
                //}


            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        public ActionResult Error()
        {
            return View();
        }
    }
}