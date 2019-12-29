using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.Extensions.Options;
using acb_app.Helpers;

namespace acb_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly AppSettings _appSettings;
        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager, IOptions<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _appSettings = appSettings.Value;
        }
        [HttpPost,Route("Register")]
        public async Task<IActionResult> Register(RegisterAccount account)
        {
            var user = new IdentityUser { UserName = account.Username, Email = account.Email };
            var result =  await userManager.CreateAsync(user, account.Password);
            if (result.Succeeded)
            {
              await  signInManager.SignInAsync(user, isPersistent: false);
            }
            return Ok(result);
        }
     

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login(Login entity)
        {

            var result = await signInManager.PasswordSignInAsync(entity.Username, entity.Password,entity.RememberMe, false);

            if (result.Succeeded)
            {
                //generate token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Token);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier,entity.Username),
                    new Claim(ClaimTypes.Name, entity.Password)
                }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return Ok(new
                {
                    token = tokenString,
                    expiration = token.ValidTo
                }) ;
            }
            return Unauthorized();
        }

        [HttpPost, Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok("Logout");
        }

    }
}