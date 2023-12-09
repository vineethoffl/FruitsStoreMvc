using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FruitsStore_API.Models
{
    public class FruitDetailsResponse
    {
        public FruitListResponse MappedDetails { get; set; }
        public List<FruitsResponse.Fruits> FruitsData { get; set; }
    }
}