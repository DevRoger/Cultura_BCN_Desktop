using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultura_BCN
{
    public class DayResults
    {
        public string date;
        public int sales;
        public DayResults(string date, int sales)
        {
            this.date = date;
            this.sales = sales;
        }
    }
}
