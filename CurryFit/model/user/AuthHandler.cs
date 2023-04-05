using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Auth.Requests;
using System.Linq;

namespace CurryFit.model.user
{
    public class AuthHandler : IAuthHandler
    {
        private readonly FirebaseAuthClient client;
        UserCredential userCredential;

        public AuthHandler(string apikey)
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = apikey,
                AuthDomain = "",,
                Providers = new FirebaseAuthProvider[]
                {
                    new GoogleProvider().AddScopes("email"),
                    new EmailProvider(),
                    new FacebookProvider(),
                    new TwitterProvider()
                }
            };
            client = new FirebaseAuthClient(config);
        }
        /// <summary>
        /// Checks if a user is logged in.
        /// </summary>
        /// <returns>True if a user is logged in, false otherwise.</returns>
        public bool IsLoggedIn()
        {
            return client.User != null;
        }
        
        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The user's UID.</returns>
        public async Task<string> Login(string email, string password)
        {
            try
            {
                userCredential = await client.SignInWithEmailAndPasswordAsync(email, password);
                return userCredential.User.Uid;
            }
            catch (Exception ex)
            {
                throw(ex);
            }
        }
        /// <summary>
        /// Logs in a user with a third party provider.
        /// </summary>
        /// <param name="idToken">The id token of the user.</param>
        /// <param name="provider">The provider of the user.</param>
        /// <returns>The user's UID.</returns>
        public async Task<string> LoginWithProvider(string idToken, string provider)
        {
            try{
            switch(provider)
            {
                case "google":
                    return await LoginWithGoogle(idToken);
                case "facebook":
                    return await LoginWithFacebook(idToken);
                case "twitter":
                    return await LoginWithTwitter(idToken);
                default:
                    throw new Exception("Invalid provider");
            }
            }
            catch (Exception ex)
            {
                throw(ex);
            }
        }


        private async Task<string> LoginWithGoogle(string idToken){
                var credential = GoogleProvider.GetCredential(idToken);
                userCredential = await client.SignInWithCredentialAsync(credential);
                return userCredential.User.Uid;
               
        }
        private async Task<string> LoginWithFacebook(string idToken){
                var credential = FacebookProvider.GetCredential(idToken);
                userCredential = await client.SignInWithCredentialAsync(credential);
                return userCredential.User.Uid;
        }
        // needs looking in to
        private async Task<string> LoginWithTwitter(string idToken){
                var credential = TwitterProvider.GetCredential(idToken, "?");
                userCredential = await client.SignInWithCredentialAsync(credential);
                return userCredential.User.Uid;
        }
        /// <summary>
        /// Logs out a user.
        /// </summary>
        public Task Logout()
        {
            client.SignOut();
            return Task.CompletedTask;
            
        }
        /// <summary>
        /// Signs up a user.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The user's UID.</returns>
        public async Task<string> SignUp(string email, string password)
        {
            if (email == null || password == null)
                throw new ArgumentNullException("Email or password cannot be null.");
            try{
            userCredential = await client.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    throw new Exception("CreateUserWithEmailAndPasswordAsync was canceled.");
                }
                if (task.IsFaulted)
                {
                    throw task.Exception;
                }
                return task.Result;
                
            });
            var user = userCredential.User;
            var uid = user.Uid;
            string name = user.Info.DisplayName;
            
            return uid;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
