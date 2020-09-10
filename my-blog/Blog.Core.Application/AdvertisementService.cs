using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Core.Cache;
using Blog.Core.IApplication.Advertisement;
using Blog.Core.IApplication.Advertisement.Models;
using Blog.Core.IRepository.Base;
using Blog.Core.Model.Models;

namespace Blog.Core.Application
{
    public class AdvertisementService:IAdvertisementService
    {
        private readonly IRepository<Advertisement> _advertisementRepository;
        private readonly ICacheManager _cacheManager;
        private IMapper _mapper;

        public AdvertisementService(
            IRepository<Advertisement> dal,
            ICacheManager cacheManager,
            IMapper mapper
            )
        {
            _advertisementRepository = dal?? throw  new ArgumentNullException(nameof(dal));
            _mapper = mapper?? throw  new ArgumentNullException(nameof(mapper));
            _cacheManager = cacheManager?? throw  new ArgumentNullException(nameof(cacheManager));
        }

        public async Task Add(CreateAdvertisementDto input)
        {
            var data =_mapper.Map<Advertisement>(input);
            await  _advertisementRepository.InsertAsync(data,true);
        }

        public async Task<IList<AdvertisementDto>> getAll()
        {
            var data = await _advertisementRepository.GetListAsync();
            return _mapper.Map<IList<AdvertisementDto>>(data);
        }
    }
}