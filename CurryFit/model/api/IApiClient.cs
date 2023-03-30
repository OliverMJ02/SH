using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurryFit.model.api
{
    /// <summary>
    /// An interface for the API client, which is used to send requests to the API
    /// </summary>
    interface IApiClient
    {
        /// <summary>
        /// A method for sending a GET request to an URI to recieve the wanted "product"
        /// </summary>
        /// <param name="gtin">The gtin number of a product</param>
        /// <returns>A FoodProduct object from the model, a generic type that the program should call its methods on </returns>
        Task<FoodProduct> GetProductAsync(string gtin);
    }
}
