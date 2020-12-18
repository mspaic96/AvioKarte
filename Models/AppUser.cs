using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Proba1.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            AgentKreiraniLetovi = new HashSet<Let>();
           KorisnikRezervacije = new HashSet<Rezervacija>();
            AgentOdobreneRezervacije = new HashSet<Rezervacija>();
        }

        public virtual ICollection<Rezervacija> KorisnikRezervacije { get; set; }
        public virtual ICollection<Rezervacija> AgentOdobreneRezervacije { get; set; }

         public virtual ICollection<Let>  AgentKreiraniLetovi {get;set;}
    }
}