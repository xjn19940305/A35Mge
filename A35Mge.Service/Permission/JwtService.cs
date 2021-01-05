using A35Mge.Model.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace A35Mge.Service.Permission
{
    public class JwtService
    {
        private readonly JwtConfig jwtConfig;
        private readonly ILogger<JwtService> logger;

        public JwtService(IOptions<JwtConfig> JwtConfig,
            ILogger<JwtService> logger)
        {
            jwtConfig = JwtConfig.Value;
            this.logger = logger;
        }
        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetToken(string Id)
        {
            var claims = new List<Claim>();
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            DateTime now = DateTime.UtcNow;
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtConfig.Issuer,
                audience: jwtConfig.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(jwtConfig.Expiration)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.SecurityKey)), SecurityAlgorithms.HmacSha256)
            );
            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return "Bearer " + token;
            //return token;
        }
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public JwtSecurityToken SerializeJwt(string jwtStr)
        {
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
                return jwtToken;
            }
            catch (Exception e)
            {
                logger.LogError($"{DateTime.Now} {e.Message}");
                throw e;
            }
        }
    }
}
