using AutoMapper;
using FruitsStore_API.DAL;
using FruitsStore_API.Models;
using FruitsStore_API.Services;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FruitsStore_API.Controllers
{
    public class FruitController : Controller
    {
        private readonly FruitService _fruitService;
        private readonly IMapper _mapper;
        private readonly LoggerService _log;
        public FruitController()
        {
            _fruitService = new FruitService();
            _mapper = CreateMapper();
            _log = new LoggerService();
        }
        private IMapper CreateMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FruitsResponse.Fruits, FruitsResponse.Fruits>();
                cfg.CreateMap<FruitsJsonRecords, FruitListResponse>();
            });

            return mapperConfig.CreateMapper();
        }
        public async Task<ActionResult> FruitsDetails()
        {
            _log.StartRequest();
            try
            {                
                _log.LogInformation("FruitsDetails API invoked successfully.");

                List<FruitsResponse.Fruits> fruitsData = await _fruitService.GetFruitsDataAsync();

                _log.LogInformation("Datas fetched from fruityvice API ");

                var mappedData = _mapper.Map<List<FruitsResponse.Fruits>>(fruitsData);

                _log.LogInformation("FruitsDetails API completed successfully.");

                return View(mappedData);
                
            }
            catch (Exception ex)
            {
                _log.LogError("Error in FruitsDetails API.", ex);
                throw ex;
            }
            finally
            {                
                _log.LogInformation("Exiting from FruitsDetails API");
            }
        }
        public async Task<ActionResult> FruitWithDetails(int id)
        {
            _log.StartRequest();
            try
            {                
                _log.LogInformation("FruitWithDetails API invoked successfully.");

                FruitsJsonRecords _jsonRecords = new FruitsJsonRecords();
                List<FruitsResponse.Fruits> fruitsData = await _fruitService.GetFruitsDataAsync();
                var fruitWithId = fruitsData.FirstOrDefault(fruit => fruit.id == id);

                _log.LogInformation("Datas fetched from https://fruityvice.com/api/fruit/all API ");

                var details = await _jsonRecords.FetchFruitDetails(id);

                _log.LogInformation("Datas fetched from Fruits.json file ");

                var mappedDetails = _mapper.Map<FruitListResponse>(details);

                var combinedData = new FruitDetailsResponse
                {
                    MappedDetails = mappedDetails,
                    FruitsData = new List<FruitsResponse.Fruits> { fruitWithId }
                };
                _log.LogInformation("FruitWithDetails API completed successfully.");

                return View(combinedData);
            }
            catch (Exception ex)
            {
                _log.LogError("Error in FruitWithDetails API.", ex);
                throw ex;
            }
            finally
            {
                _log.LogInformation("Exiting from FruitWithDetails API");
            }
        }
    }
}