using FruitsStore_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace FruitsStore_API.DAL
{
    public interface IFruitsJsonRecords
    {
        Task<FruitListResponse> FetchFruitDetails(int id);
    }
    public class FruitsJsonRecords : IFruitsJsonRecords
    {          
        public async Task<FruitListResponse> FetchFruitDetails(int id)
        {
            try
            {
                string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fruits.json");
                using (StreamReader reader = new StreamReader(jsonFilePath))
                {
                    string jsonContent = await reader.ReadToEndAsync();
                    List<FruitListResponse> allFruits = JsonConvert.DeserializeObject<List<FruitListResponse>>(jsonContent);
                    FruitListResponse fruitDetail = allFruits.FirstOrDefault(fruit => fruit.Id == id);
                    return fruitDetail;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}