using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Purchase
    {
        Inventory _inventory;
        private Item _selectedItem = null;
        private int _selectedQuantity = -1;
        private int _money = 0;
        CashDispense _cashDispenser;
        List<Cassette> _noteMix = new List<Cassette>();

        public Purchase(Inventory inventory, CashDispense cashDispenser)
        {
            _inventory = inventory;
            _cashDispenser = cashDispenser;
        }

        public void SelectItem()
        {
            Item item = null;
            while(item == null)
            {
                int selectedItemCode = 0;
                while (selectedItemCode <= 0)
                {
                    Console.WriteLine("Please select Items");
                    string selectedItem = Console.ReadLine();

                    //Basic data type check
                    if (!int.TryParse(selectedItem, out selectedItemCode))
                        Console.WriteLine("Enter a valid item");
                }

                //Check valid product code is selected
                item = _inventory.GetItem(selectedItemCode);
            }

            _selectedItem = item;
        }

        public void SelectQuantity()
        {
            int quantity = -1;

            bool isValidQuanity = false;

            //Check if the required quantity is available in the stock
            while (!isValidQuanity)
            {
                quantity = -1;

                while (quantity <= 0)
                {
                    Console.WriteLine("Enter quantity");
                    string quantityInput = Console.ReadLine();

                    //Validate the data type
                    if (!int.TryParse(quantityInput, out quantity))
                    {
                        Console.WriteLine("Enter a valid quantity");
                    }
                }

                isValidQuanity = _inventory.CheckItemAvailability(this._selectedItem, quantity);

                if(!isValidQuanity)
                {
                    Console.WriteLine("We do not have requested quanity. Please try with less quantity");
                }
            }

            _selectedQuantity = quantity;
        }

        public void GetMoney()
        {
            int money = 0;
            bool isEnoughMoney = false;
            while (!isEnoughMoney)
            {
                money = 0;
                while (money <= 0)
                {
                    Console.WriteLine("Enter Money");
                    string quantityInput = Console.ReadLine();

                    if (!int.TryParse(quantityInput, out money))
                    {
                        Console.WriteLine("Enter a valid quantity");
                    }

                    _money += money;
                }
                

                isEnoughMoney = (0 <= this.GetBalance());

                if (!isEnoughMoney)
                    Console.WriteLine("Given money is not matching with purchase amount. Please provide money again.");
            }        
        }

        public void EnterDenomination()
        {
            var requiredAmount = this._money;

            List<Cassette> noteMix = new List<Cassette>();

            while(requiredAmount > 0)
            {
                Console.WriteLine("Enter Denomination");
                int deno = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter count");
                int count = int.Parse(Console.ReadLine());
                Cassette cassette = new Cassette() { Denom = deno, Count = count };
                requiredAmount -= deno * count;
                noteMix.Add(cassette);
            }

            _noteMix = noteMix;
        }

        public bool GetConfirmation()
        {
            List<Cassette> noteMix = null;

            //Validate the remining amount can be dispensed
            if (!_cashDispenser.CanWithDraw(this.GetBalance(), out noteMix))
            {
                Console.WriteLine("Balance cannot be dispensed using available coins");

                return false;
            }

            Console.WriteLine($"Your about to purchase {this._selectedQuantity} no of {this._selectedItem.ItemName}. You paid {this._money}");
            Console.WriteLine("Please collect your change");
            if (noteMix != null)
            {
                foreach (var item in noteMix)
                {
                    item.Print();
                }
            }

            Console.WriteLine("Please enter 1 to confirm");

            return (Console.ReadLine() == "1");               
        }

        public void Complete()
        {
            //Reduce the quanity from inventory
            _inventory.DispenseItem(this._selectedItem, this._selectedQuantity);

            //Deposit the cash
            _cashDispenser.Deposit(_noteMix);

            Console.WriteLine("Your purchase completed.");

            var balance = this.GetBalance();

            if (balance > 0)
            {
                _cashDispenser.WithDraw(balance);

                Console.WriteLine($"Please collect the balance of {balance}");
            }
        }

        private int GetBalance()
        {
            var totalPrice = _selectedItem.Price * _selectedQuantity;
            return _money - totalPrice;
        }
    }
}
