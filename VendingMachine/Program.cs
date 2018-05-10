using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            PurchaseController purchaseController = new PurchaseController();

            while (1 == 1)
            {                
                purchaseController.StartPurchase();

                Console.WriteLine("Enter 1 to exit");

                if (Console.ReadLine() == "1")
                    break;
            }

            purchaseController.Close();
            
        }
    }
}
