using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace APVN.LeadsPlatform.Utility
{
    public class JWTHelper
    {
        private static readonly object LockObject = new object();
        private static JWTHelper _instance;

        public static JWTHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObject)
                    {
                        if (_instance == null)
                            _instance = new JWTHelper();
                    }
                }

                return _instance;
            }
        }

        public string CreateToken(int userId, string username, string groupId, string checksumKey, DateTime expires, string roles)
        {
            //Set issued at date
            var issuedAt = DateTime.UtcNow;

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.SerialNumber, userId.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Sid, checksumKey),
                new Claim(ClaimTypes.Expired, Utils.ConvertToUnixTime(expires).ToString()),
                new Claim(ClaimTypes.Role, roles),
                new Claim(ClaimTypes.PrimaryGroupSid,groupId)
            });

            string sec = AppKeys.JWTSecretKey;
            string issuer = AppKeys.JWTIssuer;
            string audience = AppKeys.JWTAudience;
            //var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

            //create the jwt
            var token = tokenHandler.CreateJwtSecurityToken(issuer: issuer, audience: audience, subject: claimsIdentity, notBefore: issuedAt,
                expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public string GetChecksumKey(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var result = tokenHandler.ReadJwtToken(token);
            if (result != null)
            {
                var checksumObj = result.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                if (checksumObj != null && checksumObj.Value != null)
                    return checksumObj.Value;
            }
            return string.Empty;
        }

        public string GenerateChecksumKey(string username)
        {
            return string.Format("{0}:{1}", username.ToLower(), Guid.NewGuid().ToString());
        }

    }
}
