using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proba1.Data;
using Proba1.Models;

namespace Proba1.Pages.Letovi
{   [Authorize(Roles="Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Let = await _context.Letovi.FindAsync(id);

            if (Let != null)
            {
                Let.StatusLeta = "Otkazan";
               
            }
             _context.Attach(Let).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LetExists(Let.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return RedirectToPage("./Index");
        }
        private bool LetExists(int id)
        {
            return _context.Letovi.Any(e => e.Id == id);
        }
    }
}
