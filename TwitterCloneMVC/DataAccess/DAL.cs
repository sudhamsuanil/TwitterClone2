using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using TwitterCloneMVC.Models;

namespace TwitterCloneMVC.DataAccess
{
    public class DAL
    {
        public bool AddToFollow(string userId)
        {
            Person presentPerson = new Person();
            Person toBeFollowedPerson = new Person();
            using (FSDEntities dbContext = new FSDEntities())
            {
                try
                {
                    List<FollowersEntity> ent = new List<FollowersEntity>();
                   
                    toBeFollowedPerson = dbContext.People.Where(x => x.user_id == userId).FirstOrDefault();
                    dbContext.People.Where(x => x.user_id == userId).First().People.Add(toBeFollowedPerson);
                    //presentPerson.People.Add(toBeFollowedPerson);
                    dbContext.SaveChanges();
                    return true;
                }

                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                    return false;
                }
            }
        }

        public Person ManageAccount(string userid)
        {
            using (FSDEntities dbContext = new FSDEntities())
            {
                Person present = dbContext.People.Where(x => x.user_id == userid).First();
                return (present);
            }
        }

        public bool UpdateAccount(Person updatePerson, string userid)
        {
            try
            {
                using (FSDEntities dbContext = new FSDEntities())
                {
                    Person present = dbContext.People.Where(x => x.user_id == userid).First();
                    present.email = updatePerson.email;
                    present.fullName = updatePerson.fullName;
                    present.active = updatePerson.active;
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<Person> SearchtoFollow(string userId)
        {
            List<Person> follwers = new List<Person>();
            using (FSDEntities dbContext = new FSDEntities())
            {
                try
                {
                    List<FollowersEntity> ent = new List<FollowersEntity>();

                    follwers = dbContext.People.Where(x => x.user_id != userId).ToList();

                    return follwers;
                }

                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }
            }
        }
        public bool DeleteTweet(int id)
        {
            try
            {
                using (FSDEntities dbContext = new FSDEntities())
                {

                    var PresntTweet = (Tweet)dbContext.Tweets.Where(x => x.tweet_id == id).First();
                    dbContext.Tweets.Remove(PresntTweet);
                    dbContext.SaveChanges();
                    return true;

                }
            }
            catch
            {
                return false;
            }
            
        }
        public List<Tweet> ManageTweets(string userid)
        {
            List<Tweet> tweets = new List<Tweet>();
            using (FSDEntities dbContext = new FSDEntities())
            {

                tweets = dbContext.Tweets.Where(x => x.user_id == userid).ToList();

            }
            return tweets;

        }
        public Tweet EditTweet(int id)
        {
            using (FSDEntities dbContext = new FSDEntities())
            {

                var tweet = (Tweet)dbContext.Tweets.Where(x => x.tweet_id == id).First();
                return tweet;

            }
        }

        public bool UpdateTweet(Tweet tweet)
        {
            try
            {
                using (FSDEntities dbContext = new FSDEntities())
                {

                    var PresntTweet = (Tweet)dbContext.Tweets.Where(x => x.tweet_id == tweet.tweet_id).First();
                    PresntTweet.message = tweet.message;
                    dbContext.SaveChanges();
                    return true;

                }
            }

            catch
            {
                return false;
            }
        }

        public List<FollowersEntity> GetFollowers(string userId)
        {
            List<FollowersEntity> ent = new List<FollowersEntity>();
            using (FSDEntities dbContext = new FSDEntities())
            {
                try
                {
                    List<Person> follwers = new List<Person>();

                    follwers = dbContext.People.Where(x => x.user_id == userId).ToList();
                    foreach (Person p in follwers)
                    {
                        foreach (Person p1 in p.Person1)
                        {
                            ent.Add(new FollowersEntity { followerId = p1.user_id, followerName = p1.fullName });
                        }
                    }
                    return ent;
                }

                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }
            }
        }

        public List<FollowersEntity> GetFollowing(string userId)
        {
            List<FollowersEntity> ent = new List<FollowersEntity>();
            using (FSDEntities dbContext = new FSDEntities())
            {
                try
                {
                    
                    List<Person> follwers = new List<Person>();
                    follwers = dbContext.People.Where(x => x.user_id == userId).ToList();
                    foreach (Person p in follwers)
                    {
                        foreach (Person p1 in p.People)
                        {
                            ent.Add(new FollowersEntity { followerId = p1.user_id, followerName = p1.fullName });
                        }
                    }
                    return ent;
                }

                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }
            }
        }
        public bool AddTweet(Tweet tweetObj)
        {
            using (FSDEntities dbContext = new FSDEntities())
            {
                try
                {
                    dbContext.Tweets.Add(tweetObj);
                    dbContext.SaveChanges();
                    return true;
                }

                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }
            }
        }
        public List<Tweet> FetchTweetsForuser(string userid)
        {
            List<Tweet> tweets = new List<Tweet>();
            using (FSDEntities dbContext = new FSDEntities())
            {
                Person present = dbContext.People.Where(x => x.user_id == userid).First();
                foreach (Person p in present.People)
                {
                    tweets.AddRange(dbContext.Tweets.Where(x => x.user_id == p.user_id).ToList());

                }
            }
            return tweets;
        }
        public Person Authenticateuser(UserAcccount user)
        {
            Person p =new Person();
            using (FSDEntities dbContext = new FSDEntities())
            {
                try
                {
                    p = dbContext.People.Single(x => x.user_id == user.user_id && x.password == user.password);

                }
                catch
                {
                    p = null;
                }
                
                
            }
            return p;
        }

        public bool RegisterUser(Person person)
        {
            using (TwitterDbContext dbContext = new TwitterDbContext())
            {
                try
                {
                    dbContext.person.Add(person);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                   
                }
            }
        }
    }
}