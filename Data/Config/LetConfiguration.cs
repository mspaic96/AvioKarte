using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proba1.Models;

namespace Proba1.Data.Config
{
    public class LetConfiguration : IEntityTypeConfiguration<Let>
    {
        public void Configure(EntityTypeBuilder<Let> builder)
        {   
            builder.Property( l => l.UserID).IsRequired();
            builder.HasOne(u => u.User).WithMany( u => u.AgentKreiraniLetovi)
            .HasForeignKey(l => l.UserID);
            builder.Property(l => l.StatusLeta)
            .HasDefaultValue("Aktivan");
        }

    }
}