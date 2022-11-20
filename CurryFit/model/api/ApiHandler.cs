using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurryFit.model.api
{
    public static class ApiHandler
    {
        private static IApiClient dabasClient = new DabasClient();
        public async static Task<FoodProduct> GetProduct(string path)
        {
            FoodProduct product = null;
            try
            {
                product = await dabasClient.GetProductAsync(path);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return product;
        }
    }
}
