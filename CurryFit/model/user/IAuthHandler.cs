using System;
using System.Threading.Tasks;

namespace CurryFit.model.user
{
    public interface IAuthHandler
    {
        bool IsLoggedIn();
        Task<string> Login(string email, string password);
        Task<string> LoginWithProvider(string idToken, string provider);
        Task<string> SignUp(string email, string password);
        Task Logout();
    }
}
