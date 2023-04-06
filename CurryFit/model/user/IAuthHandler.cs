using System;
using System.Threading.Tasks;

namespace CurryFit.model.user
{
    /// <summary>
    /// Interface for the authentication model.
    /// </summary>
    public interface IAuthHandler
    {
        /// <summary>
        /// Checks if a user is logged in.
        /// </summary>
        /// <returns>True if a user is logged in, false otherwise.</returns>
        bool IsLoggedIn();

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The user's UID.</returns>
        Task<string> Login(string email, string password);

        /// <summary>
        /// Logs in a user with a third party provider.
        /// </summary>
        /// <param name="idToken">The id token of the user.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The user's UID.</returns>
        Task<string> LoginWithProvider(string idToken, string provider);

        /// <summary>
        /// Signs up a user.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The user's UID.</returns>
        Task<string> SignUp(string email, string password);

        /// <summary>
        /// Logs out a user.
        /// </summary>
        void Logout();
    }
}
