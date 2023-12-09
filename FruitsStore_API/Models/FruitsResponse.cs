using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FruitsStore_API.Models
{
    public class FruitsResponse
    {

        public class Rootobject
        {
            public Fruits[] Fruits { get; set; }
        }

        public class Fruits
        {
            public string name { get; set; }
            public int id { get; set; }
            public Nutritions nutritions { get; set; }
        }

        public class Nutritions
        {
            public int calories { get; set; }
            public float fat { get; set; }
            public float sugar { get; set; }
            public float carbohydrates { get; set; }
            public float protein { get; set; }
        }

    }
}