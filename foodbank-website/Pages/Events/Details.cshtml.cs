using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using foodbank_website.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace foodbank_website.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly foodbank_website.Data.ApplicationDbContext _context;

        public DetailsModel(foodbank_website.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Event Event { get; set; }
        public List<EventVolunteer> eventVolunteerList { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Include allows e.Owner.UserName to be used
            Event = await _context.Events.Include(e => e.Owner).FirstOrDefaultAsync(m => m.ID == id);

            //Get list of current members. ID = Event unique ID
            eventVolunteerList = await _context.EventVolunteers.Where(v => v.Event.ID == id).ToListAsync();

            //Log current members
/*            foreach (EventVolunteer v in eventVolunteerList)
            {
                var currentVolunteer = _context.Users.Where(u => u.Id == v.Volunteer.Id).FirstOrDefault();
                //Debug.WriteLine(currentVolunteer.NormalizedEmail);
            }*/

            //Log count of current members
            
            Event.currentMembers = eventVolunteerList.Count();

            if (Event == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
