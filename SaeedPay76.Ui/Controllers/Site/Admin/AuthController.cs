using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SaeedPay76.Common.Eroor;
using SaeedPay76.Data.Dto.Site.Admin;
using SaeedPay76.Data.Infrastructure;
using SaeedPay76.Data.Models;
using SaeedPay76.Services.Auth.Interface;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaeedPay76.Ui.Controllers.Site.Admin
{
    [Authorize]
    [Route("site/admin/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _db;
        private readonly IConfiguration _config;
        public AuthController(IAuthService authService, IUnitOfWork db, IConfiguration config)
        {
            _authService = authService;
            _db = db;
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto, CancellationToken cancellationToken)
        {
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();
            if ((await _db.userRepository.UserExist(userForRegisterDto.UserName)))
            {
                return BadRequest(new ReturnEroor()
                {
                    Status = false,
                    Title = "خطا",
                    Message = "این نام کاربری وجود دارد ",
                    Code = "400"
                });
            }
            var user = new UserEntity()
            {
                UserName = userForRegisterDto.UserName,
                Addres = "",
                City = "",
                Gender = "",
                IsActive = true,
                Name = userForRegisterDto.Name,
                PhoneNumber = userForRegisterDto.PhoneNumber,
                Status = true,

            };
            var createdauaser = await _authService.Register(user, userForRegisterDto.Password, cancellationToken);

            return StatusCode(201);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto, CancellationToken cancellationToken)
        {
            var userFromRepo = await _authService.Login(userForLoginDto.UserName, userForLoginDto.Password, cancellationToken);
            if (userFromRepo == null)
            {
                return Unauthorized(new ReturnEroor()
                {
                    Status = false,
                    Title = "خطا",
                    Message = "کاربری با این مشخصات وجود ندارد ",
                    Code = "400"
                });
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.UserName),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = userForLoginDto.IsRemember ? System.DateTime.Now.AddDays(2) : System.DateTime.Now.AddHours(2),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesc);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)

            });
        }
        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
