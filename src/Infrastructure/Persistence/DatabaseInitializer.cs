using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public  class DatabaseInitializer
    {

        public static async Task Initialize(ApplicationDbContext context)
        {
            // For in-memory database, we don't need to call EnsureCreated
            // as it's automatically created when the context is first used

            // Check if database already has data
            if (context.Users.Any())
            {
                return; // Database has been seeded
            }

            // Seed initial data
            var users = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Bob",
                LastName = "Johnson",
                Email = "bob.johnson@example.com",
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            }
        };

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }

    }
}

