using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection.Metadata;
using Microsoft.Extensions.Configuration;
using BankingCreditSystem.Core.Security.Encryption;
using BankingCreditSystem.Core.Security.Extensions;

public class JwtHelper : ITokenHelper
{
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration){
        _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }

    public AccessToken CreateToken(User user, IList<OperationClaim> operationClaims)
    {
       _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
       var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
       var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
       var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
       var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
       var token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken
        {
            Token = token,
            Expiration = _accessTokenExpiration
        };
    }

    private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, IList<OperationClaim> operationClaims)
    {
        return new JwtSecurityToken(
            issuer: tokenOptions.Issuer,
            audience: tokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetClaims(user, operationClaims),
            signingCredentials: signingCredentials
        );
    }

    private IEnumerable<Claim> SetClaims(User user, IList<OperationClaim> operationClaims)
    {
        var claims = new List<Claim>();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddEmail(user.Email);
        claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
        return claims;
    }
}