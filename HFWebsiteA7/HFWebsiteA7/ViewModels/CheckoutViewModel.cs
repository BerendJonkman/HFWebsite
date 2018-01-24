using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}