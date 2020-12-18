using System.ComponentModel.DataAnnotations;

namespace Proba1.Models
{
    public class Rezervacija
    { 
         [Key]
         public int Id { get; set; }

         public Let Let { get; set;}
         
         public int LetId {get; set;}

         public string Status { get; set; }

         public int BrojRezervisanihMesta { get; set;}

         public virtual AppUser Agent {get;set;}

         public string AgentId {get; set; }


         public virtual AppUser Korisnik {get; set;}

         public string KorisnikId { get; set;}
    }
}