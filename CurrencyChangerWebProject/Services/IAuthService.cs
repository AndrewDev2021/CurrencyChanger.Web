using CurrencyExсhanger.Web.Model;
using System.Threading.Tasks;

namespace CurrencyExсhanger.Web.Services
{
    public interface IAuthService
    {
        Task<string> LogInAsync(LogInModel model);
        Task<string> RegisterAsync(RegisterModel model);
        Task<bool> LogoutAsync();
    }
}
