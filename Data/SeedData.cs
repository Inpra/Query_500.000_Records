using System;
using System.Collections.Generic;
using Data_query.Models; // Correct namespace for DataContext

namespace Data_query.Data
{
    public static class SeedData
    {
        public static void Initialize(TableQueryContext context)
        {
            // ... existing code ...

            // Tạo một công ty mới
            var company = new Company
            {
                Name = "Example Company",
                Domain = "example.com",
                CreatedAt = DateTime.Now
            };

            context.Companies.Add(company);
            context.SaveChanges();

            // Tạo 3 vai trò
            var roles = new List<Role>
            {
                new Role { Name = "Admin" },
                new Role { Name = "Manager" },
                new Role { Name = "Team Lead" },
                new Role { Name = "Employee" },
                new Role { Name = "HR" }
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();

            // Tạo 1000 người dùng
            for (int i = 1; i <= 1000; i++)
            {
                var user = new User
                {
                    Email = $"user{i}@example.com",
                    Name = $"User {i}",
                    RoleId = (i == 1) ? roles[0].Id : // Admin for the first user
                              (i == 2) ? roles[1].Id : // Manager for the second user
                              roles[2 + new Random().Next(roles.Count - 2)].Id, // Randomly assign Team Lead, Employee, or HR
                    CompanyId = company.Id,
                    CreatedAt = DateTime.Now
                };

                context.Users.Add(user);
            }

            context.SaveChanges(); // Lưu tất cả người dùng vào cơ sở dữ liệu
            // ... existing code ...
        }
    }
}
