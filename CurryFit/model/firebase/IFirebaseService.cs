using System;
using System.Threading.Tasks;

namespace CurryFit.model.firebase
{
    /// <summary>
    /// This interface is the connection between the app and the broker.
    /// </summary>
    public interface IFirebaseService<T>
    {
        Task AddDataAsync(T data, string key);
        Task RemoveDataAsync(T data, string key);
        Task<bool> CheckDataExistsAsync(T data, string key);
    }
}
