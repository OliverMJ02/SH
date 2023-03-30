using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace CurryFit.model.api
{
    /// <summary>
    /// A class for the Api client towards Dabas's free database
    /// </summary>
    public class DabasClient : IApiClient
    {
        private static readonly DabasAdapter adapter = new DabasAdapter();

        /// <summary>
        /// A method for sending a GET request to an URI to recieve the wanted "product"
        /// </summary>
        /// <param name="gtin">The gtin number of a product</param>
        public async Task<FoodProduct> GetProductAsync(string gtin)
        {
            DabasProduct product;
            product = await GetDabasProduct(gtin);
            return adapter.ConvertToFoodProduct(product);
        }

        /// <summary>
        /// A method for recieving a DabasProduct object from the API
        /// </summary>
        /// <param name="gtin">The gtin number of a product</param>
        private async Task<DabasProduct> GetDabasProduct(string gtin)
        {
            HttpClient client = new HttpClient();
            gtin = "0" + gtin;
            string url = "https://api.dabas.com/DABASService/V2/article/gtin/";
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("apikey", "8e8d6aa6-4112-4eae-a287-22bca4ab5207");

            string end = "/JSON?apikey=8e8d6aa6-4112-4eae-a287-22bca4ab5207";
            string productString = Path.Combine(url, gtin);
            string call = String.Concat(productString, end);

           
            DabasProduct product = new DabasProduct();
            HttpResponseMessage response = await client.GetAsync(call);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<DabasProduct>();
            }
            client.Dispose();
            return product;       
        }
    }
}
