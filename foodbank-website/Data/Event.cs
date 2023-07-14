using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foodbank_website.Data
{
    public class Event
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int totalMembers { get; set; }
        public int currentMembers { get; set; }
        public IdentityUser Owner { get; set; }
    }
}
