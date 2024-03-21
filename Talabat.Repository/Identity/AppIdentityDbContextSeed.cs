using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user= new AppUser()
                {
                    DisplayName="Salah Mohamed",
                    Email="salahbalbaa.sb@gmail.com",
                    UserName="salahbalbaa",
                    PhoneNumber="01001001000",
                };
             await userManager.CreateAsync(user,"Salah@balba3");    
            }

        }
    }
}
