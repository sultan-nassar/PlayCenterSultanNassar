using gameCenter.Projects.CurrencyConverter.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace gameCenter.Projects.CurrencyConverter.Services
{
    internal class CurrencyService
    {

        private const string BaseApiEndPoint = "http://api.exchangeratesapi.io/latest";
        private string ? ApiKey = Environment.GetEnvironmentVariable("currenciesApiKey");
        private HttpClient Http_Client=new HttpClient(); //בעזרתו אפשר לייצר מטודות כדי לבצע בקשות
        public async Task<Dictionary<string, double>> GetExchangeRatesAsync()
        {
            string requestUrl = $"{BaseApiEndPoint}?access_key={ApiKey}";
            string response = await Http_Client.GetStringAsync(requestUrl); //GETSTRINGASYNC מחזירה רק את הבאדי של הבקשה לא כל הבקשה

            //response now in string and i want in exchange respone which name RATES (dictionary string key and double value)

            //we add JsonSerializerOptions because there is different in the capital letter and small letter in ExchangeResponse(succes Timestamp Date Base
            //just like this we can solve this problem and make all the letters uniform.
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            //the exchangeData in type ExchangeResponse we receive it by pe3nooh response to tybe ExchangeResponse
            //for this we use the class JsonSerializer with method Deserialize to transfeer to C# to tybe ExchangeResponse.
            ExchangeResponse exchangeData = JsonSerializer.Deserialize<ExchangeResponse>(response, options);



            if (exchangeData == null || exchangeData.Rates == null)
                throw new InvalidOperationException("Failed to fetch exchange rates.");
            return exchangeData.Rates;
        }
    }
}
