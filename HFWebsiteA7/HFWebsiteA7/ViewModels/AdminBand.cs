using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.ViewModels
{
    public class AdminBand
    {
        public Band Band { get; set; }
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Image is required")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File { get; set; }
    }
}