using FruitsStore_API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using static FruitsStore_API.Models.FruitsResponse;

namespace FruitsStore_API.Services
{
    public interface IFruitService
    {
        Task<List<FruitsResponse.Fruits>> GetFruitsDataAsync();
    }
    public class FruitService : IFruitService
    {
        private readonly HttpClient _httpClient;
        private readonly LoggerService _log;
        public FruitService()
        {
            _httpClient = new HttpClient();
            _log = new LoggerService();
        }        
        public async Task<List<FruitsResponse.Fruits>> GetFruitsDataAsync()
        {   
            try
            {
                _log.LogInformation(" entered into GetFruitsDataAsync function");
                string apiUrl = WebConfigurationManager.AppSettings["fruityviceURL"];
                var response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    _log.LogInformation("IsSuccessStatusCode value entered into Deserialization");
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<FruitsResponse.Fruits> fruitsList = JsonConvert.DeserializeObject<List<FruitsResponse.Fruits>>(jsonResponse);
                    return fruitsList;

                }
                else
                {
                    _log.LogInformation($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _log.LogError("Error in GetFruitsDataAsync.", ex);
                throw ex;
            }
            finally
            {
                _log.LogInformation("Ended - GetFruitsDataAsync");
            }
        }
    }
}