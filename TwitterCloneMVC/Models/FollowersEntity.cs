using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TwitterCloneMVC.Models
{
    public class FollowersEntity
    {
        [Key]
        public string  followerId {get;set;}
        public string  followerName { get; set; }

    }
}