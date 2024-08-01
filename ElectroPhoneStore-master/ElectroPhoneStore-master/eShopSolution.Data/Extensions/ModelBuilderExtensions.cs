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
                     Thumbnail = "",
                     ProductImage = "https://cdn.tgdd.vn/Products/Images/42/306994/Slider/vi-vn-samsung-galaxy-s23-fe-slider--(7).jpg"
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
                     Thumbnail = "/user-content/5514321e-1846-4e9d-890a-45aed0e91042.jpg",
                    ProductImage = "/user-content/5514321e-1846-4e9d-890a-45aed0e91042.jpg"

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
                 Thumbnail = "",
                ProductImage = "https://cdn.tgdd.vn/Products/Images/42/306994/Slider/vi-vn-samsung-galaxy-s23-fe-slider--(7).jpg"
                },

                new Product()
                {
                    Id = 4,
                    Name = "Vivo V21 5G",
                    CategoryId = 4,
                    Price = 9990000,
                    Stock = 5,
                    DateCreated = DateTime.Now,
                    Description = "",
                    Details = ""
                },

                new Product()
                {
                    Id = 5,
                    Name = "Xiaomi Redmi Note 10",
                    CategoryId = 5,
                    Price = 5090000,
                    Stock = 5,
                    DateCreated = DateTime.Now,
                    Description = "",
                    Details = "hhhhhhhhhhhhhhhhhhhhhhhhhhhhh"
                },

                new Product()
                {
                    Id = 6,
                    Name = "Nokia 5.4",
                    CategoryId = 6,
                    Price = 3290000,
                    Stock = 5,
                    DateCreated = DateTime.Now,
                    Description = "",
                    Details = "https://cdn.tgdd.vn/Products/Images/42/306994/Slider/vi-vn-samsung-galaxy-s23-fe-slider--(7).jpg"
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
