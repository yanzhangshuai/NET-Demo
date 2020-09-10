using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.IApplication.Advertisement;
using Blog.Core.IApplication.Advertisement.Models;
using Blog.Web.Util.Consts;
using Blog.Web.Util.swagger;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    // [Authorize(Policy =PolicyConst.Admin)]
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementService _advertisementService;

        public AdvertisementController(
            IAdvertisementService advertisementService
        )
        {
            _advertisementService = advertisementService;
        }

        [HttpPost]
        public async Task Add([FromBody] CreateAdvertisementDto data)
        {
            await _advertisementService.Add(data);
        }
        
        [EnableCors(CorsConsts.Web)]
        [HttpGet]
        public async Task<IList<AdvertisementDto>> List()
        {
            return await _advertisementService.getAll();
        }
        
        /// <summary>
        /// 获取博客测试信息 v2版本
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //MVC自带特性 对 api 进行组管理
        // [ApiExplorerSettings(GroupName = "V2")]
        // //路径 如果以 / 开头，表示绝对路径，反之相对 controller 的想u地路径
        // [Route("/api/v2/blog/Advertisement")]
        
        [CustomRoute(ApiVersions.V2,"AdvertisementV2")]
        public async Task<object> V2_Advertisement()
        {
            return Ok(new { status = 220, data = "我是第二版的博客信息" });

        }
    }
}