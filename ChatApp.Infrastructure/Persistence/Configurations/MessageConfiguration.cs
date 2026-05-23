using ChatApp.Domain.Entities;
using ChatApp.Domain.Enums;
using ChatApp.Infrastructure.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Infrastructure.Persistence.Configurations;

public class MessageConfiguration
    : IEntityTypeConfiguration<Message>
{
    public void Configure(
        EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Messages");



        builder.HasKey(m => m.Id);



        builder.Property(m => m.ConversationId)
            .IsRequired();



        builder.Property(m => m.Content)

            .IsRequired()

            .HasMaxLength(1000);



        builder.Property(m => m.SenderId)

            .IsRequired()

            .HasMaxLength(450);



        builder.Property(m => m.ReceiverId)

            .IsRequired()

            .HasMaxLength(450);



        builder.Property(m => m.SentAt)
            .IsRequired();



        builder.Property(m => m.SeenAt)
            .IsRequired(false);



        builder.Property(m => m.EditedAt)
            .IsRequired(false);



        builder.Property(m => m.Status)

            .HasConversion<string>()

            .HasMaxLength(20)

            .IsRequired();



        builder.Property(m => m.IsDeletedBySender)

            .IsRequired()

            .HasDefaultValue(false);



        builder.Property(m => m.IsDeletedByReceiver)

            .IsRequired()

            .HasDefaultValue(false);



        builder.HasIndex(m => new
        {
            m.ConversationId,
            m.SentAt
        });



        builder.HasIndex(m => m.ReceiverId);



        builder.HasOne(m => m.Conversation)

            .WithMany(c => c.Messages)

            .HasForeignKey(m => m.ConversationId)

            .OnDelete(DeleteBehavior.Cascade);



        builder.HasOne<ApplicationUser>()

            .WithMany()

            .HasForeignKey(m => m.SenderId)

            .OnDelete(DeleteBehavior.Restrict);



        builder.HasOne<ApplicationUser>()

            .WithMany()

            .HasForeignKey(m => m.ReceiverId)

            .OnDelete(DeleteBehavior.Restrict);
    }
}