using System;
using System.Linq;
using System.Security.Claims;

namespace CurrencyExсhanger.Web.Extensions
{
    public static class HttpContextExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            return Convert.ToInt32(user.Claims.First(s => s.Type == "Id").Value);
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.Claims.First(s => s.Type == ClaimsIdentity.DefaultNameClaimType).Value;
        }
    }
}
