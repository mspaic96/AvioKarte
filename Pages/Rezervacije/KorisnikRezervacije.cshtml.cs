using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proba1.Data;
using Proba1.Models;

namespace Proba1.Pages.Rezervacije
{   [Authorize(Roles="Korisnik")]
    public class KorisnikRezervacijeModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public KorisnikRezervacijeModel(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public IList<Rezervacija> Rezervacije { get; set;}
        public async Task OnGetAsync()
        {
            var rezervacije = from r in _context.Rezervacije
                             select r;
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
           rezervacije = rezervacije.Where(r => r.KorisnikId == user.Id);
           foreach ( var rez in rezervacije)
           {
               rez.Let = await _context.Letovi.FirstOrDefaultAsync( l => l.Id == rez.LetId);
           }
           Rezervacije = await rezervacije.ToListAsync();
           

        }
    }
}
