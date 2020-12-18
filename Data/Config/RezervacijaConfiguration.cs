using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proba1.Models;

namespace Proba1.Data.Config
{
    public class RezervacijaConfiguration : IEntityTypeConfiguration<Rezervacija>
    {
        public void Configure(EntityTypeBuilder<Rezervacija> builder)
        {   
                  builder.Property( r => r.Id).IsRequired();
                  builder.Property(r => r.BrojRezervisanihMesta).IsRequired();
                  builder.Property(r => r.LetId).IsRequired();
                  builder.Property(r => r.KorisnikId).IsRequired();
                  builder.Property(r => r.Status).HasDefaultValue("Čekanje");
                  builder.HasOne(r => r.Let).WithMany( l => l.Rezervacije )
                  .HasForeignKey(r => r.LetId);
                  builder.HasOne(r => r.Korisnik).WithMany( u => u.KorisnikRezervacije)
                  .HasForeignKey(r => r.KorisnikId);
                  builder.HasOne(r => r.Agent).WithMany( u => u.AgentOdobreneRezervacije)
                  .HasForeignKey(r => r.AgentId);
                  

                  
            
        }
    }
}