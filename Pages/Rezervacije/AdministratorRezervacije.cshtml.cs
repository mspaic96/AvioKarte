using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proba1.Data;
using Proba1.Models;

namespace Proba1.Pages.Rezervacije
{   [Authorize(Roles="Administrator")]
    public class AdministratorRezervacijeModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public AdministratorRezervacijeModel(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

          public IList<AppUser> Agenti { get; set;}

        public async Task OnGetAsync()
        {
              var agenti = await _userManager.GetUsersInRoleAsync("Agent");

              Agenti = agenti;

              foreach(var agent in Agenti)
              {
                  agent.AgentOdobreneRezervacije =  _context.Rezervacije.Where(r => r.AgentId == agent.Id && r.Status == "Odobrena").ToList();
              }
          
        }
    }
}
