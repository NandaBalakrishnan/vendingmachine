using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class InventoryStore
    {
        public static Dictionary<Item, long> LoadFromPersistent()
        {
            Dictionary<Item, long> items = new Dictionary<Item, long>();
            Currency currency = new Currency() { BaseRate = 67, DisplayName = "Rupees", Symbol = "RS", Name = "INR" };


            items.Add(new Item() { ItemId = 1, ItemName = "Pepsi", ItemCurrency = currency, Price = 30, MinQuantity = 2 }, 10);
            items.Add(new Item() { ItemId = 2, ItemName = "Lays", ItemCurrency = currency, Price = 10, MinQuantity = 1 }, 5);
            items.Add(new Item() { ItemId = 3, ItemName = "Snickers", ItemCurrency = currency, Price = 20, MinQuantity = 1 }, 1);
            return items;

        }

        public static bool SaveInventory(Dictionary<Item, long> items)
        {
            File.WriteAllText("data.json", JsonConvert.SerializeObject(items, Formatting.Indented));

            return true;
        }
    }
}
