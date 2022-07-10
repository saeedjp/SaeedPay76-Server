using AutoMapper;
using SaeedPay76.Data.Dto.Site.Admin.Bank;
using SaeedPay76.Data.Dto.Site.Admin.Phpto;
using SaeedPay76.Data.Dto.Site.Admin.Users;
using SaeedPay76.Data.Models;
using System.Linq;

namespace SaeedPay76.Ui.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserEntity, UserForListDto>();
            CreateMap<UserEntity, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt =>
                {
                    //opt.MapFrom(src => src.DateOfBirth.ToAge();
                }); 
            CreateMap<Photo, PhotoForUserDetailDto>();
            CreateMap<BankCard, BankCardsForDetailDto>();
            CreateMap<UserForUpdateDto, UserEntity>();
        }
    }
}
