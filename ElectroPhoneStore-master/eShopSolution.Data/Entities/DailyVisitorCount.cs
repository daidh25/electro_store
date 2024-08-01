using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Entities
{
    public class DailyVisitorCount
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}
