using System;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;

namespace CurryFit.model.firebase
{
    /// <summary>
    /// This class acts as a broker and gives concrete implementation to the IFirebaseService interface.
    /// </summary>
    public class FirebaseService : IFirebaseService
    {
        /// <summary>
        /// The Firebase Database client.
        /// </summary>
        private readonly FirebaseClient client;

        /// <summary>
        /// Constructor for the FirebaseService class.
        /// </summary>
        /// <param name="url">The URL of the Firebase Database.</param>
        public FirebaseService(string url)
        {
            client = new FirebaseClient(url);

        }
        /// <summary>
        /// Adds data to the Firebase Database.
        /// </summary>
        /// <param name="data">The data to be added.</param>
        /// <param name="key">The key of the data to be added.</param>
        public async Task AddDataAsync<T>(T data, string key)
        {
            try
            {
                var db = GetDatabaseReference<T>();
                await db.Child(key).PutAsync(data);
            }
            catch(Exception ex)
            {
                throw(ex);
            }
            
        }
        /// <summary>
        /// Removes data from the Firebase Database.
        /// </summary>
        /// <param name="data">The data to be removed.</param>
        /// <param name="key">The key of the data to be removed.</param>
        public async Task RemoveDataAsync<T>(T data, string key)
        {
            var db = GetDatabaseReference<T>();
            if (await CheckDataExistsAsync<T>(data, key)){
                await db.Child(key).DeleteAsync();
            }
            else
            {
                throw new Exception("Item does not exist");
            }
        }
        /// <summary>
        /// Checks if data exists in the Firebase Database.
        /// </summary>
        /// <param name="data">The data to be checked.</param>
        /// <param name="key">The key of the data to be checked.</param>
        public async Task<bool> CheckDataExistsAsync<T>(T data, string key)
        {
            var db = GetDatabaseReference<T>();
            var data_snapshot = (await db.Child(key).OnceAsync<T>());
            if (data_snapshot.Equals(data))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Gets  a single item from the Firebase Database.
        /// </summary>
        /// <param name="key">The key of the data to be retrieved.</param>
        /// <returns>The data retrieved from the Firebase Database.</returns>
        public async Task<T> GetDataAsync<T>(T data, string key)
        {
            var db = GetDatabaseReference<T>();
            var data_snapshot = (await db.Child(key).OnceAsync<T>());
            return data_snapshot.FirstOrDefault().Object;
        }

        /// <summary>
        /// Gets a list of items from the Firebase Database.
        /// </summary>
        /// <returns>The data retrieved from the Firebase Database.</returns>
        public async Task<T> GetListOfDataAsync<T>(T data)
        {
            var db = GetDatabaseReference<T>();
            var data_snapshot = await db.OnceAsync<T>();
            return data_snapshot.Select(x => x.Object).ToList().FirstOrDefault();
        }

        private ChildQuery GetDatabaseReference<T>()
        {
            var db = client.Child(typeof(T).Name);
            return db;
        }
    }
}
