using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.IServices;
using Talabat.Core.Models.Identity;
using Talabat.Dtos;
using Talabat.Errors;

namespace Talabat.Controllers
{

    public class AccountController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _token;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,ITokenService token)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null) { return Unauthorized(new ApiResponse(401)); }
            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!result.Succeeded) { return Unauthorized(new ApiResponse(401)); }
            return Ok(new UserDto()
            {

                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _token.CreatTokenAsync(user,_userManager)
            });
        }

        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            var user = new AppUser()
            {
                Email = register.Email,
                DisplayName = register.DisplayName,
                PhoneNumber = register.PhoneNumber,
                UserName = register.Email.Split('@')[0]
            };
            var result=await _userManager.CreateAsync(user);
            if (!result.Succeeded) { return BadRequest(new ApiResponse(400)); }
            return Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _token.CreatTokenAsync(user, _userManager)
            });
        }

    }
}
