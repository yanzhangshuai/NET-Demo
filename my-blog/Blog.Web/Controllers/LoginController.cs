using Blog.Web.Util.Consts;
using Blog.Web.Util.jwt;
using Blog.Web.utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        [EnableCors(CorsConsts.Web)]
        // GET
        public IActionResult Login(string name,string pass)
        {
            // 获取用户的角色名，请暂时忽略其内部是如何获取的，可以直接用 var userRole="Admin"; 来代替更好理解。
            var userRole = "Admin"; //await _sysUserInfoServices.GetUserRoleNameStr(name, pass);
            if (userRole != null)
            {
                // 将用户id和角色名，作为单独的自定义变量封装进 token 字符串中。
                TokenModelJwt tokenModel = new TokenModelJwt {Uid = 1, Role = userRole};
                var jwtStr = JwtHelper.IssueJwt(tokenModel);//登录，获取到一定规则的 Token 令牌
                return Ok(new
                {
                    success = true,
                    token = jwtStr
                });
            }
            else
            {
                  return Ok(new
                            {
                                success = false,
                                token = "login fail!!!"
                            });
            }

          
        }
    }
}