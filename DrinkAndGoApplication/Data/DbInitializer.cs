using VinylStop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VinylStop.Data
{
    public class DbInitializer
    {
            public static void SeedData(AppDbContext dbContext)
            {

                if (!dbContext.Categories.Any())
                {
                    dbContext.Categories.AddRange(Categories.Select(c => c.Value));
                }

                if (!dbContext.Albums.Any())
                {
                    dbContext.AddRange
                    (
                        new Album
                        {
                            Name = "Axis: Bold as Love",
                            Price = 20.95M,
                            Artist = "The Jimi Hendrix Experience",
                            YearReleased = 1967,
                            AlbumLabel = "Track Albums",
                            LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                            Category = Categories["Classic Rock"],
                            ImageUrl = "https://albummad.co.za/wp-content/uploads/2017/06/Hendrix-axis.jpg",
                            InStock = 23,
                            IsPreferredAlbum = false,
                        },
                        new Album
                        {
                            Name = "Sounds of Silence",
                            Price = 15.95M,
                            Artist = "Simon & Garfunkel",
                            YearReleased = 1966,
                            AlbumLabel = "Columbia",
                            LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                            Category = Categories["Folk"],
                            ImageUrl = "http://www.amiright.com/album-covers/images/album-Simon--Garfunkel-Sounds-of-Silence.jpg",
                            InStock = 6,
                            IsPreferredAlbum = true,
                        },
                        new Album
                        {
                            Name = "Night Beat",
                            Price = 18.95M,
                            Artist = "Sam Cooke",
                            YearReleased = 1963,
                            AlbumLabel = "RCA Victor",
                            LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                            Category = Categories["Soul"],
                            ImageUrl = "https://img.discogs.com/U52SVzcbsM7ACvVh275INDNaYe0=/fit-in/600x600/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-2770525-1300276096.jpeg.jpg",
                            InStock = 45,
                            IsPreferredAlbum = false,
                        },
                        new Album
                        {
                            Name = "The Doors",
                            Price = 21.95M,
                            Artist = "The Doors",
                            YearReleased = 1967,
                            AlbumLabel = "Elektra",
                            LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                            Category = Categories["Classic Rock"],
                            ImageUrl = "http://www.covermesongs.com/wp-content/uploads/2013/01/doors-the-doors-cover-front-500x500.jpg",
                            InStock = 29,
                            IsPreferredAlbum = false,
                        },
                        new Album
                        {
                            Name = "Abbey Road",
                            Price = 22.00M,
                            Artist = "The Beatles",
                            YearReleased = 1969,
                            AlbumLabel = "Apple Albums",
                            LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                            Category = Categories["Classic Rock"],
                            ImageUrl = "https://i.pinimg.com/originals/f7/15/99/f71599b69cfbb6f0867ad8c7d514c3ca.jpg",
                            InStock = 35,
                            IsPreferredAlbum = false,
                        },
                        new Album
                        {
                            Name = "Highway 61 Revisited",
                            Price = 23.95M,
                            Artist = "Bob Dylan",
                            YearReleased = 1965,
                            AlbumLabel = "Columbia",
                            LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                            Category = Categories["Folk"],
                            ImageUrl = "https://is4-ssl.mzstatic.com/image/thumb/Music/08/dc/91/mzi.cscwtxfp.jpg/600x600bf.jpg",
                            InStock = 25,
                            IsPreferredAlbum = true,
                        },
                        new Album
                        {
                            Name = "Goodbye Yellow Brick Road",
                            Price = 23.95M,
                            Artist = "Elton John",
                            YearReleased = 1965,
                            AlbumLabel = "MCA",
                            LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                            Category = Categories["Classic Rock"],
                            ImageUrl = "https://img.discogs.com/BIDepbwx81NGbelNO_9pxAt-i5M=/fit-in/600x600/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-1048715-1188023963.jpeg.jpg",
                            InStock = 25,
                            IsPreferredAlbum = true,
                        },
                        new Album
                        {
                            Name = "Sgt. Pepper's Lonely Hearts Club Band",
                            Price = 23.95M,
                            Artist = "The Beatles",
                            YearReleased = 1967,
                            AlbumLabel = "Capitol",
                            LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                            Category = Categories["Classic Rock"],
                            ImageUrl = "http://zurlocker.typepad.com/.a/6a00d83452e46469e201b7c8fe3f7c970b-500wi",
                            InStock = 25,
                            IsPreferredAlbum = false,
                        },
                        new Album
                        {
                            Name = "A Night At The Opera",
                            Price = 25.95M,
                            Artist = "Queen",
                            YearReleased = 1975,
                            AlbumLabel = "EMI",
                            LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                            Category = Categories["Classic Rock"],
                            ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51gvoVZz8hL.jpg",
                            InStock = 25,
                            IsPreferredAlbum = false,
                        },
                        new Album
                        {
                            Name = "Fathers And Sons",
                            Price = 21.95M,
                            Artist = "Muddy Waters",
                            YearReleased = 1969,
                            AlbumLabel = "Columbia",
                            LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                            Category = Categories["Blues"],
                            ImageUrl = "https://cps-static.rovicorp.com/3/JPG_500/MI0000/617/MI0000617167.jpg?partner=allrovi.com",
                            InStock = 5,
                            IsPreferredAlbum = false,
                        },
                        new Album
                        {
                            Name = "At Folsom Prison",
                            Price = 25.95M,
                            Artist = "Johnny Cash",
                            YearReleased = 1968,
                            AlbumLabel = "Columbia",
                            LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                            Category = Categories["Country"],
                            ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/61JCbWRp7DL.jpg",
                            InStock = 4,
                            IsPreferredAlbum = false,
                        },
                        new Album
                        {
                            Name = "Red Headed Stranger",
                            Price = 19.95M,
                            Artist = "Willie Nelson",
                            YearReleased = 1975,
                            AlbumLabel = "Columbia",
                            LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                            Category = Categories["Country"],
                            ImageUrl = "https://img.discogs.com/nSDWPkPyQfJUFBGAN4OimfSE2xI=/fit-in/600x600/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-1560450-1279124434.jpeg.jpg",
                            InStock = 6,
                            IsPreferredAlbum = false,
                        },
                        new Album
                        {
                            Name = "King of Blue",
                            Price = 26.95M,
                            Artist = "Miles Davis",
                            YearReleased = 1959,
                            AlbumLabel = "Columbia",
                            LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                            Category = Categories["Jazz"],
                            ImageUrl = "https://img.discogs.com/VpsC-8l7SSC79uPzhrbHqk8lWgI=/fit-in/600x581/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-368061-1464275203-2925.jpeg.jpg",
                            InStock = 3,
                            IsPreferredAlbum = true,
                        },
                        new Album
                        {
                            Name = "Lucille",
                            Price = 22.95M,
                            Artist = "B.B. King",
                            YearReleased = 1968,
                            AlbumLabel = "Mobile Fidelity Sound Lab",
                            LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                            Category = Categories["Blues"],
                            ImageUrl = "https://static.qobuz.com/images/covers/82/51/0000881105182_600.jpg",
                            InStock = 8,
                            IsPreferredAlbum = false,
                        },
                        new Album
                        {
                            Name = "Sunshine of Your Love",
                            Price = 15.95M,
                            Artist = "Ella Fitzgerald",
                            YearReleased = 1969,
                            AlbumLabel = "MPS Records",
                            LongDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                            Category = Categories["Jazz"],
                            ImageUrl = "https://img.discogs.com/ALBcAzAqZ2Vz7sf-6RPsYz2pQPA=/fit-in/600x593/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-9111828-1474976614-9144.jpeg.jpg",
                            InStock = 3,
                            IsPreferredAlbum = false,
                        }
                    );
                }

                dbContext.SaveChanges();
            }

            private static Dictionary<string, Category> categories;
            public static Dictionary<string, Category> Categories
            {
                get
                {
                    if (categories == null)
                    {
                        var genresList = new Category[]
                        {
                            new Category { CategoryName = "Classic Rock", Description="All Classic Rock Albums" },
                            new Category { CategoryName = "Folk", Description="All Folk Albums" },
                            new Category { CategoryName = "Soul", Description="All Soul Albums" },
                            new Category { CategoryName = "Country", Description="All Country Albums" },
                            new Category { CategoryName = "Pop", Description="All Pop Albums" },
                            new Category { CategoryName = "Jazz", Description="All Jazz Albums" },
                            new Category { CategoryName = "Blues", Description="All Blues Albums" },
                        };

                        categories = new Dictionary<string, Category>();

                        foreach (Category genre in genresList)
                        {
                            categories.Add(genre.CategoryName, genre);
                        }
                    }

                    return categories;
                }
            }

        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            //adding custom roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                //creating the roles and seeding them to the database
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            //creating a super user who could maintain the web app
            var admin = new ApplicationUser
            {
                FirstName = Configuration.GetSection("AdminSettings")["FirstName"],
                LastName = Configuration.GetSection("AdminSettings")["LastName"],
                UserName = Configuration.GetSection("AdminSettings")["UserEmail"],
                Email = Configuration.GetSection("AdminSettings")["UserEmail"]
            };

            string UserPassword = Configuration.GetSection("AdminSettings")["UserPassword"];
            var _user = await UserManager.FindByEmailAsync(Configuration.GetSection("AdminSettings")["UserEmail"]);

            if (_user == null)
            {
                var createAdmin = await UserManager.CreateAsync(admin, UserPassword);
                if (createAdmin.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await UserManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
    }

