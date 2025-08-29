using AnonChat.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnonChat.Data.Configurations
{
    public class MatchConfiguration : IEntityTypeConfiguration<MatchModel>
    {
        public void Configure(EntityTypeBuilder<MatchModel> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(m => m.User1)
                   .WithMany()
                   .HasForeignKey(m => m.User1Id)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.User2)
                   .WithMany()
                   .HasForeignKey(m => m.User2Id)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(m => m.Messages)
                   .WithOne(msg => msg.Match)
                   .HasForeignKey(msg => msg.MatchId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
