using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcMovie.Models
{
    public static class SeedData
    {
        
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcMovieContext>>()))
            {
                // Look for any movies.
                if (!context.Movie.Any())
                {
                    context.Movie.AddRange(
                        new Movie
                        {
                            Title = "When Harry Met Sally",
                            Genre = "Romantic Comedy",
                            Price = 7.99M,
                            Rating = "R"
                        },

                        new Movie
                        {
                            Title = "Ghostbusters ",
                            Genre = "Comedy",
                            Rating = "R",
                            Price = 8.99M
                        },

                        new Movie
                        {
                            Title = "Ghostbusters 2",
                            Genre = "Comedy",
                            Rating = "R",
                            Price = 9.99M
                        },

                        new Movie
                        {
                            Title = "Rio Bravo",
                            Genre = "Western",
                            Rating = "R",
                            Price = 3.99M
                        }
                    );
                }

                if (context.Game.Any())
                {
                    context.Remove(context.Game.Single(g => g.Name == "Call of Duty"));
                    context.Remove(context.Game.Single(g => g.Name == "Witcher 3"));
                    context.Remove(context.Game.Single(g => g.Name == "Dark Souls 3"));
                    var movie = context.Movie.Single(m => m.Title == "Rio Bravo");
                    context.Game.AddRange(
                        new Game()
                        {
                            Name = "Call of Duty",
                            Count = 2,
                            CrossPlatformMultiplayer = true,
                            Perspective = Perspective.FirstPerson,
                            Price = 2000.00M,
                            TotalHours = 200,
                            BasedOnGameMovie = movie
                        },
                        new Game()
                        {
                            Name = "Witcher 3",
                            Count = 3,
                            CrossPlatformMultiplayer = false,
                            Perspective = Perspective.ThirdPerson,
                            Price = 1000.00M,
                            TotalHours = 250,
                            BasedOnGameMovie = context.Movie.Single(m=>m.Title=="Ghostbusters 2")
                        },
                        new Game()
                        {
                            Name = "Dark Souls 3",
                            Count = 18,
                            CrossPlatformMultiplayer = true,
                            Perspective = Perspective.ThirdPerson,
                            Price = 1500.00M,
                            TotalHours = 60,
                            BasedOnGameMovie = context.Movie.Single(m=>m.Title=="When Harry Met Sally")

                        }
                    );
                }
                context.SaveChanges();
            }
        }
    }
}