using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Blog.Core.Common;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Web.Util.jwt
{
    public class JwtHelper
    {
        public static string IssueJwt(TokenModelJwt tokenModelJwt)
        {
            var iss = Appsettings.App("Audience", "Issuer");
            var aud = Appsettings.App("Audience", "Audience");
            var secret = Appsettings.App("Audience", "Secret");
            var claims = new List<Claim>
            {
                /*
               * 特别重要：
                 1、这里将用户的部分信息，比如 uid 存到了Claim 中，如果你想知道如何在其他地方将这个 uid从 Token 中取出来，请看下边的SerializeJwt() 方法，或者在整个解决方案，搜索这个方法，看哪里使用了！
                 2、你也可以研究下 HttpContext.User.Claims ，具体的你可以看看 Policys/PermissionHandler.cs 类中是如何使用的。
               */     
                new Claim(JwtRegisteredClaimNames.Jti, tokenModelJwt.Uid.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                //    过期时间
                new Claim(JwtRegisteredClaimNames.Exp,
                    $"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeMilliseconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss, iss),
                new Claim(JwtRegisteredClaimNames.Aud, aud),
            };
            //    一个用户多个身份
            claims.AddRange(tokenModelJwt.Role.Split(',').Select(s => new Claim(ClaimTypes.Role,s)));
            
            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            
            var jwt = new JwtSecurityToken(claims:claims,signingCredentials:creds);
            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);
            return encodedJwt;
        }

        public static TokenModelJwt SerializeJwt(string jwtStr)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtStr);
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out object role);
                var tm = new TokenModelJwt
                {
                    Uid = long.Parse(jwtToken.Id),
                    Role = role != null ? role.ToString() : "",
                };
                return tm;
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    /// <summary>
    ///     令牌
    /// </summary>
    public class TokenModelJwt
    {
        /// <summary>
        ///     用户ID
        /// </summary>
        public long Uid { get; set; }

        /// <summary>
        ///     角色
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        ///     职能
        /// </summary>
        public string work { get; set; }
    }
}