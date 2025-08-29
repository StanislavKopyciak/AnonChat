using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnonChat.Models;

namespace AnonChat.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");

            builder.HasMany(u => u.Messages)
                   .WithOne(m => m.Sender)
                   .HasForeignKey(m => m.SenderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Matches)
                   .WithOne() 
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}