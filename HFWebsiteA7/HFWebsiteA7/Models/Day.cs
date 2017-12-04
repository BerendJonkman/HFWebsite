using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class Day
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual String Name { get; set; }
    }
}