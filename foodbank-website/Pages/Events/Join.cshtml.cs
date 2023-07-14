using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using foodbank_website.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace foodbank_website.Pages.Events
{
    [Authorize]
    public class JoinModel : PageModel
    {
        private readonly foodbank_website.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public JoinModel(foodbank_website.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Event Event { get; set; }
        public bool CurrentUserIsInEvent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IdentityUser user = _context.Users.FirstOrDefault(x =>
                x.Id == _userManager.GetUserId(HttpContext.User));

            Event = await _context.Events.FirstOrDefaultAsync(m => m.ID == id);

            EventVolunteer existingVolunteer = await _context.EventVolunteers.Where(ev => ev.Event.ID == id).FirstOrDefaultAsync();

            if (existingVolunteer == null)
            {
                CurrentUserIsInEvent = false;
            }
            else
            {
                CurrentUserIsInEvent = true;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IdentityUser user = _context.Users.FirstOrDefault(x =>
                x.Id == _userManager.GetUserId(HttpContext.User));

            Event = await _context.Events.FirstOrDefaultAsync(m => m.ID == id);

            EventVolunteer existingVolunteer = await _context.EventVolunteers.
                Where(ev => ev.Event.ID == id && ev.Volunteer.Id == user.Id).FirstOrDefaultAsync();

            if (existingVolunteer == null)
            {
                CurrentUserIsInEvent = false;
            }
            else
            {
                CurrentUserIsInEvent = true;
            }

            if (CurrentUserIsInEvent)
            {
                _context.EventVolunteers.Remove(existingVolunteer);
                CurrentUserIsInEvent = false; // now false
            }
            else
            {
                EventVolunteer newInsert = new EventVolunteer();
                newInsert.Volunteer = user;
                newInsert.Event = Event;
                _context.EventVolunteers.Add(newInsert);
                CurrentUserIsInEvent = true; // now true
            }

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}
