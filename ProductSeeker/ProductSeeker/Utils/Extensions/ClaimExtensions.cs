using System.Security.Claims;

namespace ProductSeeker.Services.Extensions
{
    static class ClaimExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"))?.Value;
        }
        public static string GetClaimValue(this ClaimsPrincipal user, string claimType)
        {
            var claim = user.Claims.FirstOrDefault(c => c.Type == claimType);
            return claim?.Value ?? string.Empty;
        }
        public static bool HasClaim(this ClaimsPrincipal user, string claimType)
        {
            return user.Claims.Any(c => c.Type == claimType);
        }
        public static bool HasAnyClaim(this ClaimsPrincipal user, params string[] claimTypes)
        {
            return claimTypes.Any(user.HasClaim);
        }
    }
}
