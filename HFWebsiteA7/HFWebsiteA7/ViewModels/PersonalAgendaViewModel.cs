using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.ViewModels
{
    public class PersonalAgendaViewModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public List<int> EventIdList { get; set; }
        public List<int> PassPartoutTypeList { get; set; }
        public List<Concert> ConcertList { get; set; }
        public List<Event> EventList { get; set; }
    }
}