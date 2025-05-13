using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultura_BCN
{
    public class Pegi
    {   
        public int value { get; set; }
        public string name { get; set; }
        public Pegi(int value,string name) {
            this.value = value;
            this.name = name;
        }
    }
}
