using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    [Serializable]
    public class Currency
    {
        public string Name { get; set; }
        public double BaseRate { get; set; }
        public string Symbol { get; set; }
        public string DisplayName { get; set; }

    }
}
