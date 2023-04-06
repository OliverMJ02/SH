using System;
using System.Threading.Tasks;

namespace CurryFit.model.firebase
{
    /// <summary>
    /// This interface is the connection between the app and the broker.
    /// </summary>
    public interface IFirebaseService
    {
        /// <summary>
        /// Adds data to the Firebase Database.
        /// </summary>
        /// <param name="data">The data type to be added.</param>
        /// <param name="key">The key of the data to be added.</param>
        Task AddDataAsync<T>(T data, string key);

        /// <summary>
        /// Removes data from the Firebase Database.
        /// </summary>
        /// <param name="data">The data type to be removed.</param>
        /// <param name="key">The key of the data to be removed.</param>
        Task RemoveDataAsync<T>(T data, string key);

        /// <summary>
        /// Checks if data exists in the Firebase Database.
        /// </summary>
        /// <param name="data">The data type to be checked.</param>
        /// <param name="key">The key of the data to be checked.</param>
        /// <returns>True if the data exists, false otherwise.</returns>
        Task<bool> CheckDataExistsAsync<T>(T data, string key);

        /// <summary>
        /// Gets data from the Firebase Database.
        /// </summary>
        /// <param name="data">The data type to be retrieved.</param>
        /// <param name="key">The key of the data to be retrieved.</param>
        /// <returns>The data retrieved from the Firebase Database.</returns>
        Task<T> GetDataAsync<T>(T data, string key);

        /// <summary>
        /// Gets a list of data from the Firebase Database.
        /// </summary>
        /// <param name="data">The data type to be retrieved.</param>
        /// <returns>The data retrieved from the Firebase Database.</returns>
        Task<T> GetListOfDataAsync<T>(T data);
    }
}
