using ChatApp.Domain.Entities;
using ChatApp.Domain.Enums;
using ChatApp.Infrastructure.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Infrastructure.Persistence.Configurations;

public class UserConnectionConfiguration
    : IEntityTypeConfiguration<UserConnection>
{
    public void Configure(
        EntityTypeBuilder<UserConnection> builder)
    {
        builder.ToTable("UserConnections");



        builder.HasKey(uc => new
        {
            uc.UserId,
            uc.ConnectionId
        });



        builder.Property(uc => uc.UserId)
            .IsRequired();



        builder.Property(uc => uc.ConnectionId)
            .IsRequired();



        builder.Property(uc => uc.Status)

            .HasConversion<string>()

            .HasMaxLength(20)

            .IsRequired();



        builder.Property(uc => uc.ConnectedAt)
            .IsRequired();



        builder.Property(uc => uc.LastSeenAt)
            .IsRequired(false);



        builder.HasIndex(uc => uc.Status);



        builder.HasOne<ApplicationUser>()

            .WithMany()

            .HasForeignKey(uc => uc.UserId)

            .OnDelete(DeleteBehavior.Cascade);
    }
}