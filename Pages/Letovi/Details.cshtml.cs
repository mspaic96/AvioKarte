using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proba1.Data;
using Proba1.Models;

namespace Proba1.Pages.Letovi
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Let Let { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Let = await _context.Letovi.FirstOrDefaultAsync(m => m.Id == id);

            if (Let == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
