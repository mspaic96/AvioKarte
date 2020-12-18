using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Proba1.Models
{
    public class Let
    {
        public Let()
        {
            Rezervacije = new HashSet<Rezervacija>();
        }

        [Key]
        public int Id {get; set;}
         [Display(Name = "Mesto Polaska")]
         [StringLength(60, MinimumLength = 3)]
         [Required]
         [RegularExpression(@"^(Beograd|Kraljevo|Niš|Priština)$", ErrorMessage = "Za mesto moguće vrednosti su: Beograd,Niš,Kraljevo,Priština")]
        public string MestoPolaska { get; set; }
        [Display(Name = "Mesto Dolaska")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        [RegularExpression(@"^(Beograd|Kraljevo|Niš|Priština)$" , ErrorMessage = "Za mesto moguće vrednosti su: Beograd,Niš,Kraljevo,Priština")]
        public string MestoDolaska { get; set; }
        [Display(Name = "Datum Leta")]
        [DataType(DataType.DateTime)]
        
       
        
        public DateTime DatumLeta { get; set; }
        [Display(Name = "Broj Presedanja")]
        [Range(0, 20)]
        
        public int BrojPresedanja { get; set; }
        [Display(Name = "Broj Mesta Na Letu")]
        [Range(0, 1000)]
        

        public int BrojMestaNaLetu { get; set; }

        [Display(Name = "Broj Slobodnih Mesta")]
        [Range(0, 1000)]
       
        public int BrojSlobodnihMesta { get; set; }
        [Display(Name = "Status Leta")]
    
        public string  StatusLeta { get; set; }

      

        public AppUser User {get; set; }

        public string UserID { get; set;}

        public virtual ICollection<Rezervacija> Rezervacije  {get; set;}

    }
}