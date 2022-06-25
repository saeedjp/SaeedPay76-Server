using Microsoft.AspNetCore.Mvc;
using SaeedPay76.Data.Infrastructure;
using SaeedPay76.Data.Models;
using SaeedPay76.Services.Auth.Interface;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaeedPay76.Ui.Controllers.Site.Admin
{
    [Route("site/admin/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _db;

        public AuthController(IAuthService authService, IUnitOfWork db)
        {
            _authService = authService;
            _db = db;

        }
        // GET: api/<AuthController>
        [HttpGet]
        public async Task<IActionResult> Register(string userName, string passWord, CancellationToken cancellationToken)
        {
            userName = userName.ToLower();
            if (!(await _db.userRepository.UserExist(userName)))
            {
                return BadRequest("این د نام کاربری وجود دارد ");
            }
            var user = new UserEntity()
            {
                UserName = userName,
                Addres = "",
                City = "",
                DateOfBirth = "",
                Gender = "",
                IsActive = true,
                Name = "",
                PhoneNumber = "",
                Status = true ,

            };
            var createdauaser = await _authService.Register(user, passWord, cancellationToken);

            return StatusCode(201);
        }

        // POST api/<AuthController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
