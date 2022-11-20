using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurryFit.model.api
{
    interface IApiClient
    {
        Task<FoodProduct> GetProductAsync(string gtin);
    }
}
