using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    [Serializable]
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Price { get; set; }
        public Currency ItemCurrency { get; set; }
        public int MinQuantity { get; set; }
       

        public void Print()
        {
         
            Console.WriteLine($"{this.ItemName} | {this.Price} |  {this.ItemCurrency.DisplayName} |  {this.ItemId}");
        }
    }

}
