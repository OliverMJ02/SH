using System;
using System.Threading.Tasks;

namespace CurryFit.model.firebase
{
    /// <summary>
    /// This interface is the connection between the app and the broker.
    /// </summary>
    public interface IFirebaseService
    {
        Task AddDataAsync<T>(T data, string key);
        Task RemoveDataAsync<T>(T data, string key);
        Task<bool> CheckDataExistsAsync<T>(T data, string key);
        Task<T> GetDataAsync<T>(T data, string key);
        Task<T> GetListOfDataAsync<T>(T data);
    }
}
