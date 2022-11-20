using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace CurryFit.model.api
{
    public static class DabasClient
    {
        private static HttpClient client = new HttpClient();

        public static async Task<FoodProduct> GetDabasProduct(string gtin)
        {
            string url = "https://api.dabas.com/DABAService/V2/article/gtin/";
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("apikey", "8e8d6aa6-4112-4eae-a287-22bca4ab5207");

            try
            {
                string end = "/JSON?apikey=8e8d6aa6-4112-4eae-a287-22bca4ab5207";
                string productString = Path.Combine(url, gtin, end);
                FoodProduct product = await GetProductAsync(productString);
                client.Dispose();
                return product;
               // FoodProduct product = await GetProductAsync(
                 //   " https://api.dabas.com/DABASService/V2/article/gtin/02002059100005/JSON?apikey=8e8d6aa6-4112-4eae-a287-22bca4ab5207");
            }
            catch
            {
                Console.WriteLine("Fail");
                return null;
            }
            
        }
        private static async Task<FoodProduct> GetProductAsync(string path)
        {
            FoodProduct product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            Console.WriteLine(response.Headers.Location);
            Console.WriteLine(response.Content);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<FoodProduct>();
            }
            return product;
        }

    }
}
