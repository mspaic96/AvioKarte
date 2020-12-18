using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Proba1.Data;

namespace Proba1.Models
{
    public class SeedData
    {
        public static async void  CreateRoles(IServiceProvider serviceProvider)
    { 
        //initializing custom roles 
        var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        string[] roleNames = { "Administrator", "Agent", "Korisnik" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            
            var roleExist =  await RoleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                //create the roles and seed them to the database: Question 1
                roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        //Here you could create a super user who will maintain the web app
        var poweruser = new AppUser
        {

            UserName = "admin@admin.com",
            Email = "admin@admin.com",
        };
    //Ensure you have these values in your appsettings.json file
        string userPWD = "Admin987*";
        var _user = await UserManager.FindByEmailAsync("admin@admin.com");

       if(_user == null)
       {
            var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
            if (createPowerUser.Succeeded)
            {
                //here we tie the new user to the role
                await UserManager.AddToRoleAsync(poweruser, "Administrator");

            }
       }
    }

      public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.Letovi.Any())
                {
                    return;   // DB has been seeded
                }

                context.Letovi.AddRange(
                    new Let
                    {
                        MestoPolaska = "Kraljevo",
                        MestoDolaska = "Beograd",
                        DatumLeta= DateTime.Parse("2021-1-13 16:00"),
                        BrojMestaNaLetu = 50,
                        BrojPresedanja = 2,
                        BrojSlobodnihMesta = 50,
                        StatusLeta = "Aktivan"
                    },

                     new Let
                    {
                        MestoPolaska = "Kraljevo",
                        MestoDolaska = "Priština",
                        DatumLeta= DateTime.Parse("2021-1-17 15:00"),
                        BrojMestaNaLetu = 60,
                        BrojPresedanja = 3,
                        BrojSlobodnihMesta = 60,
                        StatusLeta = "Aktivan"
                    },

                    new Let
                    {
                        MestoPolaska = "Niš",
                        MestoDolaska = "Beograd",
                        DatumLeta= DateTime.Parse("2021-1-20 18:00"),
                        BrojMestaNaLetu = 30,
                        BrojPresedanja = 1,
                        BrojSlobodnihMesta =30,
                        StatusLeta = "Aktivan"
                    },

                   
                    new Let
                    {
                        MestoPolaska = "Kraljevo",
                        MestoDolaska = "Niš",
                        DatumLeta= DateTime.Parse("2021-1-18 17:00"),
                        BrojMestaNaLetu = 4,
                        BrojPresedanja = 0,
                        BrojSlobodnihMesta = 4,
                        StatusLeta = "Aktivan"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}