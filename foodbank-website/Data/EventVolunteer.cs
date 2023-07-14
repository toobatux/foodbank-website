using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foodbank_website.Data
{
    public class EventVolunteer
    {
        public int ID { get; set; }
        public IdentityUser Volunteer { get; set; }
        public Event Event { get; set; }
    }
}
