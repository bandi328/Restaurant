using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.classes
{
    public class Order
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public List<MenuItem> Meals { get; set; }
        public DateTime Time { get; set; }

    }
}
