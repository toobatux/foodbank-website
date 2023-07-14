using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using foodbank_website.Data;
using Microsoft.AspNetCore.Authorization;

namespace foodbank_website.Pages.UserRoles
{
    //[Authorize(Roles = "ADMIN")]
    public class CreateModel : PageModel
    {
        private readonly foodbank_website.Data.ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        //By injecting the role manager (and editing the services) you can create/delete roles
        public CreateModel(foodbank_website.Data.ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public IList<Microsoft.AspNetCore.Identity.IdentityRole> Roles { get;set; }

        [BindProperty]
        public string NewRole { get; set; } = "";

        public async Task OnGetAsync()
        {
            Roles = await _roleManager.Roles.ToListAsync();
        }

        public async Task OnPostAsync()
        {
            if (NewRole != "")
            {
                bool exists = await _roleManager.RoleExistsAsync(NewRole.Trim().ToUpper());

                if (!exists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(NewRole.Trim().ToUpper()));
                }
            }

            Roles = await _roleManager.Roles.ToListAsync();
            NewRole = "";
        }
    }
}
