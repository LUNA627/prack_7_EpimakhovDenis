using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFWebAPI
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";
        public int ReleaseYear { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
