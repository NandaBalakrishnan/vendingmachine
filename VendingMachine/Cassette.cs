using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Cassette
    {
        public int Denom { get; set; } //Denomination: (USD)50, (USD)100, etc.
        public int Count { get; set; } //Number of notes.

        public void Print()
        {
            Console.WriteLine($"Denom {this.Denom} Count {this.Count}");
        }
    }
}
