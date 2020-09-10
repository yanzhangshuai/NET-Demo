using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Demo1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        // GET
        public IActionResult Index()
        {
            //    发行人issuer
            var iss = "JWTBearer.Auth";
            //    受众人audience
            var aud = "api.auth";
            //定义许多种的声明Claim,信息存储部分,Claims的实体一般包含用户和一些元数据
            var claims = new Claim[]
            {
                new Claim(JwtClaimTypes.Id,"1"),
                new Claim(JwtClaimTypes.Name,"i3yuan"),
                new Claim(JwtClaimTypes.Role,"admin")
            };
            var nbf = DateTime.UtcNow; // 生效时间K
            var exp = DateTime.UtcNow.AddMinutes(5);
            
            //signingCredentials  签名凭证
            var sign = "q2xiARx$4x3TKqBJ";
            var secret = Encoding.UTF8.GetBytes(sign);
            var key = new SymmetricSecurityKey(secret);
            var signcreds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            
            var jwt = new JwtSecurityToken(iss,aud,claims,nbf,exp,signcreds);
            var jwtHander = new JwtSecurityTokenHandler();
            var token = jwtHander.WriteToken(jwt);
            return Ok(new
            {
                access_token = token,
                token_type = "Bearer",
            });
        }
        
    }
    
}