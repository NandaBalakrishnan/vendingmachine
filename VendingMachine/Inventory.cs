using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Inventory
    {
        private Dictionary<Item, long> _itemsInventory = new Dictionary<Item, long>();

        public void LoadItem(Item item, long quantity)
        {
            //Item not in the inventory
            if (_itemsInventory.Keys.Count(i => i.ItemId == item.ItemId) == 0)
            {
                _itemsInventory.Add(item, quantity);
            }
            else
            {
                _itemsInventory[item] = _itemsInventory[item] + quantity;
            }
        }

        public Item GetItem(int itemCode)
        {
            var item = _itemsInventory.FirstOrDefault(i => i.Key.ItemId == itemCode);

            return item.Key;
        }

        public void Display()
        {
            Console.WriteLine($"Product Name | Price | Currency | Selection Code");
            foreach (var item in this._itemsInventory)
            {
                if (item.Value <= 0)
                    continue;

                item.Key.Print();
            }
        }

        public bool CheckItemAvailability(Item item, long quantity)
        {
            var itemFromInventory = _itemsInventory.FirstOrDefault(i => i.Key.ItemId == item.ItemId);

            return (quantity <= itemFromInventory.Value);
        }

        public void DispenseItem(Item item, long quantity)
        {
            var itemFromInventory = _itemsInventory.FirstOrDefault(i => i.Key.ItemId == item.ItemId);

            var newQuantity = itemFromInventory.Value - quantity;

            _itemsInventory[item] = newQuantity;

            if (newQuantity <= item.MinQuantity)
            {
                Console.WriteLine("Alert!! You running out of stock..");
            }
        }

        public Dictionary<Item, long> GetStock()
        {
            return _itemsInventory;
        }
    }
}
