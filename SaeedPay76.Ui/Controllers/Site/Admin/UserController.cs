using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedPay76.Data.Dto.Site.Admin.Users;
using SaeedPay76.Data.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaeedPay76.Ui.Controllers.Site.Admin
{
    [Authorize]
    [ApiExplorerSettings(GroupName = "Site")]
    [Route("site/admin/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _db.userRepository.GetWithFilterAsync(null, null, "Photos,BankCards");
            var usersToRetuen = _mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(usersToRetuen);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _db.userRepository.GetWithFilterAsync(u=>u.Id == id, null, "Photos");
            var userToRetuen = _mapper.Map<UserForDetailedDto>(user.SingleOrDefault());

            return Ok(userToRetuen);
        }
    }
}
