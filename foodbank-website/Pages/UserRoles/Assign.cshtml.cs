using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using foodbank_website.Data;
using Microsoft.AspNetCore.Authorization;

namespace foodbank_website.Pages.UserRoles
{
    //[Authorize(Roles = "ADMIN")]
    public class AssignModel : PageModel
    {
        private readonly foodbank_website.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        //By injecting the both managers, we can see what roles exist and assign/remove them to a user
        public AssignModel(foodbank_website.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult OnGet()
        {
            List<Microsoft.AspNetCore.Identity.IdentityUser> users = _context.Users.ToList();
            ViewData["users"] = new SelectList(users, "Id", "UserName");

            List<IdentityRole> roles = _roleManager.Roles.ToList();
            ViewData["roles"] = new SelectList(roles, "Name", "Name");

            return Page();
        }

        [BindProperty]
        public string UserValue { get; set; }
        [BindProperty]
        public string RoleValue { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (UserValue == "" || RoleValue == "")
            {
                return Page();
            }

            //NOTE: Users can have multiple roles, but to make this example simple I remove all roles and only allow the user to below to one
            IdentityUser updateUser = await _context.Users.Where(u => u.Id == UserValue).FirstOrDefaultAsync();

            IList<string> roles = await _userManager.GetRolesAsync(updateUser);

            await _userManager.RemoveFromRolesAsync(updateUser, roles);
            await _userManager.AddToRoleAsync(updateUser, RoleValue);

            return RedirectToPage("./Index");
        }
    }
}
