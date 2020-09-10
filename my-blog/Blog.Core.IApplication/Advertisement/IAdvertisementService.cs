using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.IApplication.Advertisement.Models;

namespace Blog.Core.IApplication.Advertisement
{
    public interface IAdvertisementService
    {
        Task Add(CreateAdvertisementDto input);

        Task<IList<AdvertisementDto>> getAll();
    }
}