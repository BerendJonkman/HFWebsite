using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class AdminUser
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Username is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password {get; set;}
    }
}