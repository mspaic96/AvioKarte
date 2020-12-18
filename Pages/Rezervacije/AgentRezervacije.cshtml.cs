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
using Proba1.SignalRCommunication;
using Microsoft.AspNetCore.SignalR;

namespace Proba1.Pages.Rezervacije
{    [IgnoreAntiforgeryToken(Order=1001)]
    public class AgentRezervacijeModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        private readonly IHubContext<AvioHub> _hubContext;

        public AgentRezervacijeModel(ApplicationDbContext context, UserManager<AppUser> userManager,IHubContext<AvioHub> hubContext)
        {
            _hubContext = hubContext;
            _userManager = userManager;
            _context = context;
        }
        public IList<Rezervacija> Rezervacije { get; set;}

        [BindProperty]
        public  Rezervacija Rezervacija { get; set;}
        public async Task OnGetAsync()
        {
            
            var rezervacije = from r in _context.Rezervacije
                             select r;
            
          // rezervacije = rezervacije.Where(r => r.Status == "Čekanje");
           foreach ( var rez in rezervacije)
           {
               rez.Let = await _context.Letovi.FirstOrDefaultAsync( l => l.Id == rez.LetId);
               rez.Korisnik = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == rez.KorisnikId);
           }
           Rezervacije = await rezervacije.ToListAsync();
           
        }
       
         public async Task<IActionResult> OnPostAsync(int? id)
        {
             if (id == null)
            {
                return NotFound();
            }

            Rezervacija = await _context.Rezervacije.FindAsync(id);

            if (Rezervacija != null)
            {   Rezervacija.Let = await _context.Letovi.FirstOrDefaultAsync(l => l.Id == Rezervacija.LetId);
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                Rezervacija.AgentId = user.Id;
                if(Rezervacija.BrojRezervisanihMesta <= Rezervacija.Let.BrojSlobodnihMesta)
                {
                   Rezervacija.Status ="Odobrena";
                   Rezervacija.Let.BrojSlobodnihMesta -= Rezervacija.BrojRezervisanihMesta;
                }
                else 
                {
                     Rezervacija.Status ="Odbijena";
                } 
                
               
            }
             _context.Attach(Rezervacija).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("ConfirmReservation",Rezervacija.Id,Rezervacija.Status);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RezervacijaExists(Rezervacija.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return RedirectToPage("./AgentRezervacije");

        }
         private bool RezervacijaExists(int id)
        {
            return _context.Rezervacije.Any(e => e.Id == id);
        }
    }
}
