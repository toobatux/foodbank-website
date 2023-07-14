using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using foodbank_website.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace foodbank_website.Pages
{
    public class EventsModel : PageModel
    {
        private readonly ILogger<EventsModel> _logger;
        private readonly foodbank_website.Data.ApplicationDbContext _context;

        public EventsModel(foodbank_website.Data.ApplicationDbContext context, ILogger<EventsModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<Event> Event { get; set; }
        public List<EventVolunteer> eventVolunteerList { get; set; }

        public async Task OnGetAsync()
        {
            Event = await _context.Events.ToListAsync();

            foreach (Event v in Event)
            {
                int id = v.ID;
                eventVolunteerList = await _context.EventVolunteers.Where(u => u.Event.ID == id).ToListAsync();
                v.currentMembers = eventVolunteerList.Count();
            }

        }
    }
}
