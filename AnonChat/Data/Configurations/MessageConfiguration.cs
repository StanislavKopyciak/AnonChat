using AnonChat.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnonChat.Data.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<MessageModel>
    {
        public void Configure(EntityTypeBuilder<MessageModel> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Text)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(m => m.SentAt)
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(m => m.Sender)
                   .WithMany(u => u.Messages)
                   .HasForeignKey(m => m.SenderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Match)
                   .WithMany(m => m.Messages)
                   .HasForeignKey(m => m.MatchId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
