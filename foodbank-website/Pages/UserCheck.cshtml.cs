using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using foodbank_website.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foodbank_website.Pages
{
    public class UserCheckModel : PageModel
    {
        private readonly foodbank_website.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public string Message { get; set; } = "";

        public UserCheckModel(foodbank_website.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            IdentityUser currentUser = _context.Users.FirstOrDefault(x =>
                x.Id == _userManager.GetUserId(HttpContext.User));

            if (currentUser == null)
            {
                Message = "No user is logged in!";
            }
            else
            {
                bool isAdmin = await _userManager.IsInRoleAsync(currentUser, "ADMIN");

                if (isAdmin)
                {
                    Message = currentUser.Email + " is an admin. ";
                }
                else
                {
                    Message = currentUser.Email + " is not an admin. ";
                }

                //Not directly related to this example, but this will pull all Cars the current user owns.
                //var EventOwners = await _context.EventOwners.Where(co => co.Owner.Id == currentUser.Id).Include(co => co.Owner).Include(c => c.Event).ToListAsync();
                //Message += "They created " + EventOwners.Count().ToString() + " events";
            }
        }
    }
}
