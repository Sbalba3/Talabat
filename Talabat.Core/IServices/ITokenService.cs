using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Identity;

namespace Talabat.Core.IServices
{
    public interface ITokenService
    {
        Task<string> CreatTokenAsync(AppUser user,UserManager<AppUser> userManager);
    }
}
