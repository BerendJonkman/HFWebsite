using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class StartTimes
    {
        public string startTimeString { get; set; }
        public IEnumerable<DinnerSession> startTimeSession { get; set; }

    }
}