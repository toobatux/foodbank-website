using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using foodbank_website.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace foodbank_website.Pages.Events
{
    [Authorize(Roles = "ADMIN")]
    public class CreateModel : PageModel
    {
        private readonly foodbank_website.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(foodbank_website.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Event Event { get; set; }


        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            IdentityUser user = _context.Users.FirstOrDefault(x =>
                x.Id == _userManager.GetUserId(HttpContext.User));

            Event.Owner = user;

            _context.Events.Add(Event);
            await _context.SaveChangesAsync();

            return RedirectToPage("../AllEvents");
        }
    }
}
