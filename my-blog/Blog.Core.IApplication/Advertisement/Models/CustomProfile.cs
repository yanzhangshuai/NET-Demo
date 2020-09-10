using AutoMapper;

namespace Blog.Core.IApplication.Advertisement.Models
{
    public class CustomProfile:Profile
    {
        public CustomProfile()
        {
            CreateMap<CreateAdvertisementDto, Model.Models.Advertisement>();
            CreateMap<Model.Models.Advertisement, AdvertisementDto>();
        }
    }
}