using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using car_heap.Core.Abstract;
using car_heap.Infrastructure.ConfigPocos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace car_heap.Tools
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions jwtOptions;
        private readonly JwtSecurityTokenHandler tokenHanlder;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions, JwtSecurityTokenHandler tokenHanlder)
        {
            this.tokenHanlder = tokenHanlder;
            this.jwtOptions = jwtOptions.Value;  
        }

        public async Task<string> GenerateJwtAsync(string id, string username, string password, JsonSerializerSettings serializerSettings)
        {
            var identity = GenerateClaimsIdentity(username, id);
            var authToken = await GenerateEncodedToken(username, identity);
            var expiresIn = (int)jwtOptions.ValidFor.TotalSeconds;

            var jwt = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = authToken,
                expires_in = expiresIn,
                username = username
            };

            return JsonConvert.SerializeObject(jwt, serializerSettings);
        }
        private ClaimsIdentity GenerateClaimsIdentity(string username, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(username, "Token"), new []
            {
                new Claim(AppConstants.Strings.JwtClaimIdentifiers.Id, id),
                    new Claim(AppConstants.Strings.JwtClaimIdentifiers.Rol, AppConstants.Strings.JwtClaims.ApiAccess)
            });
        }

        private async Task<string> GenerateEncodedToken(string username, ClaimsIdentity identity)
        {
            var claims = new []
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, await jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                identity.FindFirst(AppConstants.Strings.JwtClaimIdentifiers.Rol),
                identity.FindFirst(AppConstants.Strings.JwtClaimIdentifiers.Id)
            };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer : jwtOptions.Issuer,
                audience : jwtOptions.Audience,
                claims : claims,
                notBefore : jwtOptions.NotBefore,
                expires : jwtOptions.Expiration,
                signingCredentials : jwtOptions.SigningCredentials);

            var encodedJwt = tokenHanlder.WriteToken(jwt);

            return encodedJwt;
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)=>(long)Math.Round((date.ToUniversalTime()-
                new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
            .TotalSeconds);
    }
}