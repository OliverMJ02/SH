using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Auth.Requests;

namespace CurryFit.model.user
{
    public class AuthHandler
    {
        private readonly FirebaseAuthClient client;
        UserCredential userCredential;

        public AuthHandler(string apikey)
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = apikey,
                AuthDomain = "",
                Providers = new FirebaseAuthProvider[]
                {
                    new GoogleProvider().AddScopes("email"),
                    new EmailProvider(),
                    new FacebookProvider()
                }
            };
            client = new FirebaseAuthClient(config);
        }
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
            userCredential = await client.SignInWithEmailAndPasswordAsync(email, password);
            return userCredential.User.Uid;
        }

        public Task Logout()
        {
            client.SignOut();
            return Task.CompletedTask;
        }

        public async Task<string> SignUp(string email, string password)
        {
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
