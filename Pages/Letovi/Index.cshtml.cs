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
{    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

         public IList<Let> Let { get;set; }
         [BindProperty(SupportsGet = true)]
         public string SearchStringMestoPolaska { get; set; }
         [BindProperty(SupportsGet = true)]
         public string SearchStringMestoDolaska { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public bool IsChecked { get; set; }
         
         
       
        public async Task OnGetAsync()
        {
               var letovi = from l in _context.Letovi
                          select l;

              if(IsChecked)
              {
                  letovi = letovi.Where(s => s.BrojPresedanja == 0);
              }
              if (!string.IsNullOrEmpty(SearchStringMestoPolaska))
             {
                letovi = letovi.Where(s => s.MestoPolaska.Contains(SearchStringMestoPolaska));
             }  
             
             if (!string.IsNullOrEmpty(SearchStringMestoDolaska))
             {
                letovi = letovi.Where(s => s.MestoDolaska.Contains(SearchStringMestoDolaska));
             }


    
            Let = await letovi.ToListAsync();
        }
    }
}
