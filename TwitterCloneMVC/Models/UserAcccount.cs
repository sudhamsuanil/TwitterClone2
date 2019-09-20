using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TwitterCloneMVC.Models
{
    public class UserAcccount
    {
        [Key]
        [Required(ErrorMessage = "Enter a valid userId")]
        [Display(Name = "User Id")]        
        public string user_id { get; set; }

        [Required(ErrorMessage = "Enter a Passwod")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
         
        [Required(ErrorMessage = "Full name is required")]
        [Display(Name = "FullName")]
        public string fullName { get; set; }

        [Required(ErrorMessage = "Email Id is required")]
        [Display(Name = "EmailId")]
        [EmailAddress]
        public string email { get; set; }

       
    }
}