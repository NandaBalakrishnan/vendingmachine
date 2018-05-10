using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class PurchaseController
    {

        Inventory _inventory;
        CashDispense _cashDispenser = new CashDispense();

        public PurchaseController()
        {
            _inventory = new Inventory();
            var items = InventoryStore.LoadFromPersistent();

            foreach (var item in items)
            {
                _inventory.LoadItem(item.Key, item.Value);
            }            
        }

        public void StartPurchase()
        {
            Console.WriteLine("Following Items are available for purchase");
            _inventory.Display();

            Purchase newPurchase = new Purchase(_inventory, _cashDispenser);

            newPurchase.SelectItem();
            newPurchase.SelectQuantity();
            newPurchase.GetMoney();
            newPurchase.EnterDenomination();
            if(newPurchase.GetConfirmation())
                newPurchase.Complete();
        }

        public void Close()
        {
            InventoryStore.SaveInventory(_inventory.GetStock());
        }
    }
}
