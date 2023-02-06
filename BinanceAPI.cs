using Project.Models.Binance;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class BinanceAPI
    {
        private readonly string domain = "https://api.binance.com";
        public string _apiKey { get; set; }
        public string _secret { get; set; }


        public BinanceAPI(string apiKey, string secret)
        {
            _apiKey = apiKey;
            _secret = secret;
        }


        public async Task<BinanceAccount> GetAccountDetailsAsync()
        {
            using (HttpClient client = new HttpClient() { BaseAddress = new Uri(domain) })
            {
                client.DefaultRequestHeaders.Add("X-MBX-APIKEY", _apiKey);
                long timestamp = DateTime.Now.AddSeconds(-1).GetUnixEpoch();
                string signature = Cryptography.CreateSignature($"timestamp={timestamp}", _secret);
                var response = await client.GetAsync($"/api/v3/account?timestamp={timestamp}&signature={signature}");
                return await response.Content.ReadAsAsync<BinanceAccount>();
            }
        }







        public async Task<BinanceDepositResponse> GetWalletAddressAsync(string symbol)
        {
            using (HttpClient client = new HttpClient() { BaseAddress = new Uri(domain) })
            {
                client.DefaultRequestHeaders.Add("X-MBX-APIKEY", _apiKey);
                long timestamp = DateTime.Now.AddSeconds(-1).GetUnixEpoch();
                string signature = Cryptography.CreateSignature($"timestamp={timestamp}&coin={symbol}", _secret);
                var response = await client.GetAsync($"/sapi/v1/capital/deposit/address?timestamp={timestamp}&coin={symbol}&signature={signature}");
                return await response.Content.ReadAsAsync<BinanceDepositResponse>();
            }
        }


        public async Task<BinanceWithdrawResponse> ApplyWithdrawAsync(BinanceWithdrawRequest request)
        {
            using (HttpClient client = new HttpClient() { BaseAddress = new Uri(domain) })
            {
                client.DefaultRequestHeaders.Add("X-MBX-APIKEY", _apiKey);
                long timestamp = DateTime.Now.AddSeconds(-1).GetUnixEpoch();
                var parameters = $"timestamp={timestamp}&{request.GetQueryString()}";
                string signature = Cryptography.CreateSignature(parameters, _secret);
                var response = await client.PostAsync($"/sapi/v1/capital/withdraw/apply?{parameters}&signature={signature}", null);
                return await response.Content.ReadAsAsync<BinanceWithdrawResponse>();
            }
        }




        //Histories
        public async Task<IEnumerable<BinanceDepositHistory>> GetDepositHistoryAsync(object model)
        {
            using (HttpClient client = new HttpClient() { BaseAddress = new Uri(domain) })
            {
                client.DefaultRequestHeaders.Add("X-MBX-APIKEY", _apiKey);
                long timestamp = DateTime.Now.AddSeconds(-1).GetUnixEpoch();
                string parameters = $"timestamp={timestamp}&{model.GetQueryString()}";
                string signature = Cryptography.CreateSignature(parameters, _secret);
                var response = await client.GetAsync($"/sapi/v1/capital/deposit/hisrec?{parameters}&signature={signature}");
                return await response.Content.ReadAsAsync<IEnumerable<BinanceDepositHistory>>();
            }
        }


        public async Task<IEnumerable<BinanceWithdrawHistory>> GetWithdrawHistoryAsync(object model)
        {
            using (HttpClient client = new HttpClient() { BaseAddress = new Uri(domain) })
            {
                client.DefaultRequestHeaders.Add("X-MBX-APIKEY", _apiKey);
                long timestamp = DateTime.Now.AddSeconds(-1).GetUnixEpoch();
                string parameters = $"timestamp={timestamp}&{model.GetQueryString()}";
                string signature = Cryptography.CreateSignature(parameters, _secret);
                var response = await client.GetAsync($"/sapi/v1/capital/withdraw/history?{parameters}&signature={signature}");
                return await response.Content.ReadAsAsync<IEnumerable<BinanceWithdrawHistory>>();
            }
        }


    }






    public static class Cryptography
    {
        public static string CreateSignature(string queryString, string secret)
        {

            byte[] keyBytes = Encoding.UTF8.GetBytes(secret);
            byte[] queryStringBytes = Encoding.UTF8.GetBytes(queryString);
            HMACSHA256 hmacsha256 = new HMACSHA256(keyBytes);

            byte[] bytes = hmacsha256.ComputeHash(queryStringBytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
