using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaeedPay76.Common.Eroor;
using SaeedPay76.Data.Dto.Site.Admin.Users;
using SaeedPay76.Data.Infrastructure;
using SaeedPay76.Services.Site.Admin.Auth.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
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
        private readonly IUserService _userService;

        public UserController(IUnitOfWork db, IMapper mapper, IUserService userService)
        {
            _db = db;
            _mapper = mapper;
            _userService = userService;
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
            //if (User.FindFirst(ClaimTypes.NameIdentifier).Value != id)
            //{
            //    return Unauthorized("شما اجازه دسترسی ندارید");

            //}
            var user = await _db.userRepository.GetWithFilterAsync(u => u.Id == id, null, "Photos");
            var userToRetuen = _mapper.Map<UserForDetailedDto>(user.SingleOrDefault());

            return Ok(userToRetuen);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UserForUpdateDto userForUpdateDto, CancellationToken cancellationToken)
        {
            if (id != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized("شما اجازه ویزایش ندارید");
            }

            var userFromRepo = await _db.userRepository.GetByIdAsync(id);
            _mapper.Map(userForUpdateDto, userFromRepo);
            _db.userRepository.Update(userFromRepo);

            if (await _db.SaveChangeAsync(cancellationToken) > 0)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(new ReturnEroor()
                {
                    Status = false,
                    Title = "خطا",
                    Message = $"  وجود ندارد {userForUpdateDto.Name}  امکان ویرایش برای کاربر ",
                    Code = "400"
                });
            }
        }

        [Route("ChangeUserPassword/{id}")]
        [HttpPut]
        public async Task<IActionResult> ChangeUserPassword(string id, PasswordForChangeDto passwordForChange, CancellationToken cancellationToken)
        {
            var userFromRepo = await _userService.GetUserForPassChange(id, passwordForChange.OldPassword, cancellationToken);

            if (userFromRepo ==null)
            {
                return BadRequest(new ReturnEroor()
                {
                    Status = false,
                    Title = "خطا",
                    Message = " پسورد قبلی به درستی وارد نشده است ",
                    Code = "400"
                });
            }

            if (await _userService.UpdateUserPass(userFromRepo , passwordForChange.NewPassword, cancellationToken))
            {
                return NoContent();
            }
            else
            {
                return BadRequest(new ReturnEroor()
                {
                    Status = false,
                    Title = "خطا",
                    Message = " ویرایش پسورد کاربر انجام نشد ",
                    Code = "400"
                });
            }

        }
    }
}
