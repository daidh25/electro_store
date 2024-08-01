using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Extentions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = "vi", Name = "Tiếng Việt", IsDefault = true },
                new Language() { Id = "en", Name = "English", IsDefault = false }
                );
            #region Seed Category
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "iPhone"
                },

                new Category()
                {
                    Id = 2,
                    Name = "Samsung"
                },

                new Category()
                {
                    Id = 3,
                    Name = "Oppo"
                },

                new Category()
                {
                    Id = 4,
                    Name = "Vivo"
                },

                new Category()
                {
                    Id = 5,
                    Name = "Xiaomi"
                },

                new Category()
                {
                    Id = 6,
                    Name = "Nokia"
                }
              );
            #endregion

            #region Seed Product
            modelBuilder.Entity<Product>().HasData(
                 new Product()
                 {
                     Id = 1,
                     Name = "iPhone 12 Pro",
                     CategoryId = 1,
                     Price = 28890000,
                     Stock = 5,
                     DateCreated = DateTime.Now,
                     Description = "",
                     Details = "",
                     Thumbnail = "/user-content/f5083bb6-8796-424a-9d3e-4cbbf80890d1.jpg",
                     ProductImage = "/user-content/86bfa232-c545-4e71-8ab8-29714ea486b3.jpg"
                 },

                new Product()
                {
                    Id = 2,
                    Name = "Samsung Galaxy S21+",
                    CategoryId = 2,
                    Price = 20990000,
                    Stock = 5,
                    DateCreated = DateTime.Now,
                    Description = "",
                    Details = "",
                    Thumbnail = "/user-content/a2953f58-51f0-4235-8bb8-518df305a36c.jpg",
                    ProductImage = "/user-content/299cc6cf-2df2-4659-bbad-06f700323a5d.png"

                },

                new Product()
                {
                    Id = 3,
                    Name = "Oppo Reno 5",
                    CategoryId = 3,
                    Price = 8290000,
                    Stock = 5,
                    DateCreated = DateTime.Now,
                    Description = "",
                    Details = "",
                    Thumbnail = "/user-content/ee920e1b-ba76-4463-8465-a1ef2b1885df.jpg",
                    ProductImage = "/user-content/90ebbf1b-68ca-44f1-a72e-916f9ae92db3.jpeg"
                },

                new Product()
                {
                    Id = 4,
                    Name = "Vivo V21 5G",
                    CategoryId = 4,
                    Price = 9990000,
                    Stock = 2,
                    DateCreated = DateTime.Now,
                    Description = "",
                    Details = "",
                    Thumbnail = "/user-content/4457e699-d439-4184-9901-54a028766eda.jpg",
                    ProductImage = "/user-content/26f9c7ce-c2f8-457d-8f3f-cc921c26d6d2.jpeg"

                },

                new Product()
                {
                    Id = 5,
                    Name = "Xiaomi Redmi Note 10",
                    CategoryId = 5,
                    Price = 5090000,
                    Stock = 8,
                    DateCreated = DateTime.Now,
                    Description = "",
                    Details = "",
                    Thumbnail = "/user-content/4bf2bb59-f2ce-4e7c-ae44-33a2b07cabb9.jpg",
                    ProductImage = "/user-content/010b9955-1c6e-4e64-83d5-468e5761c32e.jpeg"
                },

                new Product()
                {
                    Id = 6,
                    Name = "Nokia 5.4",
                    CategoryId = 6,
                    Price = 3290000,
                    Stock = 10,
                    DateCreated = DateTime.Now,
                    Description = "",
                    Details = "",
                    Thumbnail = "/user-content/8632b935-70dc-485d-bea0-2dbbb33ad178.jpg",
                    ProductImage = "/user-content/05805492-967e-4366-9a75-450f2491b18a.jpg"
                }
             );
            #endregion

            // tạo data cho user mặc định
            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");

            modelBuilder.Entity<AppRole>().HasData(
            new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            }
            );
            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "cuongdoladola2002@gmail.com",
                PhoneNumber = "0123456789",
                Address = "ABCDXYZ",
                NormalizedEmail = "CUONGDOLADOLA2002@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                Name = "Tuan Minh",
            });
            // gán role admin và admin user
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
