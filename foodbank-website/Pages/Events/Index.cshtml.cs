using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using foodbank_website.Data;

namespace foodbank_website.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly foodbank_website.Data.ApplicationDbContext _context;

        public IndexModel(foodbank_website.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get;set; }

        public async Task OnGetAsync()
        {
            Event = await _context.Events.Include(e => e.Owner).ToListAsync();
        }
    }
}
