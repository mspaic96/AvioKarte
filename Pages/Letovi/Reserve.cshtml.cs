using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Proba1.Data;
using Proba1.Models;
using Proba1.SignalRCommunication;


namespace Proba1.Pages.Letovi
{
    [Authorize(Roles = "Korisnik")]
    public class ReserveModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public static Let let;
        private  readonly IHubContext<AvioHub> _hubContext ;

        public ReserveModel(ApplicationDbContext context, UserManager<AppUser> userManager, IHubContext<AvioHub> hubContext)
        {
           

            _userManager = userManager;
            _context = context;
            _hubContext = hubContext;
            
        }




        public Let Let { get; set; }

        [BindProperty]
        [DisplayName("Broj Rezervacija")]
        [Range(1, 5, ErrorMessage = "Broj rezervacija mora biti izmedju 1 i 5!")]
        public int BrojRezervisanihMesta { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Let = await _context.Letovi.FirstOrDefaultAsync(m => m.Id == id);
            ReserveModel.let = Let;



            if (Let == null)
            {
                return NotFound();
            }
            return Page();

        }
        public async Task<IActionResult> OnPostAsync()
        {
            Rezervacija Rez = new Rezervacija();
            Rez.LetId = ReserveModel.let.Id;
            Rez.Let = await _context.Letovi.FirstOrDefaultAsync(l => l.Id == Rez.LetId);
            Rez.Status = "Čekanje";
            Rez.BrojRezervisanihMesta = BrojRezervisanihMesta;
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            Rez.Korisnik = user;
            Rez.KorisnikId = user.Id;




            if (!ModelState.IsValid)
            {

                return Page();
            }

            _context.Rezervacije.Add(Rez);
            

            await _context.SaveChangesAsync();

            
            
            await _hubContext.Clients.All.SendAsync("ReceiveReservation", Rez.Let.MestoPolaska,Rez.Let.MestoDolaska,
            Rez.Let.DatumLeta.ToString().Replace('T',' '),Rez.Let.StatusLeta,Rez.BrojRezervisanihMesta,Rez.Status,user.UserName,Rez.Id);
            


            return RedirectToPage("../Rezervacije/KorisnikRezervacije");
        }
    }
}
