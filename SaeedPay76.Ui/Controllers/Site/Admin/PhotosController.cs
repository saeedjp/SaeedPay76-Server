
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SaeedPay76.Common.Helper;
using SaeedPay76.Data.Dto.Site.Admin.Phpto;
using SaeedPay76.Data.Infrastructure;
using SaeedPay76.Data.Models;
using SaeedPay76.Services.Site.Admin.Auth.Interface;

namespace SaeedPay76.Ui.Controllers.Site.Admin
{

    [Route("site/admin/{userId}/photos")]
    [ApiExplorerSettings(GroupName = "site")]
    // [Route("api/v1/site/admin/users/{userId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly Cloudinary _cloudinary;

        public PhotosController(IUnitOfWork dbContext, IMapper mapper,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {

            _db = dbContext;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;

            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.APIKey,
                _cloudinaryConfig.Value.APISecret
                );

            _cloudinary = new Cloudinary(account);

        }

        //[Authorize(Policy = "AccessProfile")]
        //[ServiceFilter(typeof(UserCheckIdFilter))]
        //[HttpGet(SiteV1Routes.Photos.GetPhoto, Name = "GetPhoto")]
        //public async Task<IActionResult> GetPhoto(string id)
        //{
        //    var photoFromRepo = await _db.PhotoRepository.GetByIdAsync(id);
        //    if (photoFromRepo != null)
        //    {
        //        if (photoFromRepo.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value)
        //        {
        //            var photo = _mapper.Map<PhotoForReturnProfileDto>(photoFromRepo);

        //            return Ok(photo);
        //        }
        //        else
        //        {
        //            _logger.LogError($"کاربر   {RouteData.Values["userId"]} قصد دسترسی به عکس شخص دیگری را دارد");

        //            return BadRequest(new ReturnMessage()
        //            {
        //                status = false,
        //                title = "خطا",
        //                message = $"شما اجازه دسترسی به عکس کاربر دیگری را ندارید"
        //            });

        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("عکسی وجود ندارد");
        //    }

        //}

        public async Task<IActionResult> ChangeUserPhoto(string userId, [FromForm] PhotoForProfileDto photoForProfileDto, CancellationToken cancellationToken)
        {
            //var userFromRepo = await _db.UserRepository.GetByIdAsync(userId);

            // var uplaodRes = _uploadService.UploadToCloudinary(photoForProfileDto.File);

            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != userId)
            {
                return Unauthorized("شما اجاز ه ویرایش تصویر این کاربر را ندارید ");

            }
            // var userFromRepo = await _db.userRepository.GetByIdAsync(userId);

            var file = photoForProfileDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(250).Height(250),
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForProfileDto.Url = uploadResult.Uri.ToString();
            photoForProfileDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForProfileDto);


            photo.IsMain = true;
            photo.UserId = userId;
            photo.DesCription = "photo pic";
            photo.Alt = "photo pic";

            await _db.photoRepository.InsertAsync(photo);

            if (await _db.SaveChangeAsync(cancellationToken) > 0)
            {
                var photoForReturn = _mapper.Map<PhotoForReturnProfileDto>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoForReturn);
            }

            else
            {
                return BadRequest("خطایی در اپلود دوباره امتحان کنید");
            }

        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(string id)
        {
            var photoFromRepo = await _db.photoRepository.GetByIdAsync(id);
            var photo = _mapper.Map<PhotoForReturnProfileDto>(photoFromRepo);
            return Ok(photo);


        }
    }
}