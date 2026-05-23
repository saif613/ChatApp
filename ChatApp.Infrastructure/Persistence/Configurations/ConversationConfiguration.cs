using ChatApp.Domain.Entities;
using ChatApp.Infrastructure.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Infrastructure.Persistence.Configurations;

public class ConversationConfiguration
    : IEntityTypeConfiguration<Conversation>
{
    public void Configure(
        EntityTypeBuilder<Conversation> builder)
    {
        builder.ToTable("Conversations");



        builder.HasKey(c => c.Id);



        builder.Property(c => c.User1Id)
            .IsRequired();



        builder.Property(c => c.User2Id)
            .IsRequired();



        builder.Property(c => c.Type)

            .HasConversion<string>()

            .HasMaxLength(20)

            .IsRequired();



        builder.Property(c => c.CreatedAt)
            .IsRequired();



        builder.Property(c => c.LastMessageAt)
            .IsRequired();



        builder.HasIndex(c => new
        {
            c.User1Id,
            c.User2Id
        })
        .IsUnique();



        builder.HasOne<ApplicationUser>()

            .WithMany()

            .HasForeignKey(c => c.User1Id)

            .OnDelete(DeleteBehavior.Restrict);



        builder.HasOne<ApplicationUser>()

            .WithMany()

            .HasForeignKey(c => c.User2Id)

            .OnDelete(DeleteBehavior.Restrict);



        builder.HasMany(c => c.Messages)
            .WithOne(m => m.Conversation)
            .HasForeignKey(m => m.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}