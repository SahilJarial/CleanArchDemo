using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users"); // Table name (optional for in-memory but good practice)

            // Primary Key
            builder.HasKey(u => u.Id);

            // Properties configuration
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            // Indexes (optional but good practice)
            builder.HasIndex(u => u.Email)
                .IsUnique();


            // Seed data - Generate deterministic GUIDs
        
            builder.HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    CreatedAt = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    CreatedAt = new DateTime(2024, 1, 14, 9, 15, 0, DateTimeKind.Utc)
                }
            );


        }
    }
}
