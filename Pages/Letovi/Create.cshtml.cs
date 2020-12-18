using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proba1.Data;
using Proba1.Models;

namespace Proba1.Pages.Letovi
{   [Authorize(Roles="Agent")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
             
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Let Let { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            Let.BrojSlobodnihMesta = Let.BrojMestaNaLetu;
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            
            Let.UserID = user.Id;
            Let.User = user;
            Let.StatusLeta ="Aktivan";
            
            

            if (!ModelState.IsValid)
            {

                return Page();
            }

            _context.Letovi.Add(Let);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
