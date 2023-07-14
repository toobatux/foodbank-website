using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using foodbank_website.Data;

namespace foodbank_website.Pages.UserRoles
{
    public class IndexModel : PageModel
    {
        private readonly foodbank_website.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        //By injecting the userManager (add editing the services) you can see which roles belong to a user
        public IndexModel(foodbank_website.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<IdentityUser> UserList { get; set; }

        public async Task OnGetAsync()
        {
            UserList = await _context.Users.ToListAsync();
        }

        public async Task<string> GetRoles(IdentityUser user)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);
            return string.Join(",", roles); ;
        }
    }
}