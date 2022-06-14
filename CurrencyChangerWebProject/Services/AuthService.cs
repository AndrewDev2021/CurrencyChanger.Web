using CurrencyExсhanger.Web.Domain;
using CurrencyExсhanger.Web.Extensions;
using CurrencyExсhanger.Web.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CurrencyExсhanger.Web.Services
{
    public class AuthService : IAuthService, IAsyncDisposable
    {
        private readonly AppDbContext _db;
        private readonly HttpContext _httpContext;
        public AuthService(AppDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<string> LogInAsync(LogInModel model)
        {
            var user = await _db.Users
                .Include(u => u.FK_Role_Id)
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
                return "Incorrect login";

            if (user.Password != model.Password.GetHash())
                return "Incorrect password";

            await Authenticate(user);

            return null;
        }

        public async Task<bool> LogoutAsync()
        {
            await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return true;
        }

        public async Task<string> RegisterAsync(RegisterModel model)
        {
            if (await _db.Users.AnyAsync(s => s.Email == model.Email))
                return "There is already a user with this email";

            var user = new User()
            {
                Email = model.Email,
                FirstName = model.LastName,
                LastName = model.LastName,
                Age = model.Age,
                Password = model.Password.GetHash(),
                FK_Role_Id = await _db.Roles.FirstOrDefaultAsync(r => r.Name == "user")
            };

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            await Authenticate(user);

            return null;
        }


        #region PrivateMethods

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new("Id", user.Id.ToString()),
                new(ClaimsIdentity.DefaultRoleClaimType, user.FK_Role_Id.Name)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public ValueTask DisposeAsync()
        {
            return _db.DisposeAsync();
        }

        #endregion
    }
}
