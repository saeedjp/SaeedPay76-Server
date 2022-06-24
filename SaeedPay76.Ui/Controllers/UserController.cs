using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaeedPay76.Data.DatabaseContext;
using SaeedPay76.Data.Infrastructure;
using SaeedPay76.Data.Models;
using SaeedPay76.Services.Auth.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SaeedPay76.presentation.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _db;
        public UserController(IAuthService authService, IUnitOfWork db)
        {
            _db = db;
            _authService = authService;
        }

        [HttpGet]
        public async Task<ActionResult> Register(CancellationToken cancellation)
        {
            var user = new UserEntity()
            {
                Addres = "",
                IsActive = true,
                Status = true,

            };
            var u = await _authService.Register(user, "hfahfkjsdhfkahkah", cancellation);

            return Ok(u);
        }

       
    }
}
