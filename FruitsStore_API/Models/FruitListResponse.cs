using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FruitsStore_API.Models
{
    public class FruitListResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Benefits { get; set; }
    }
}