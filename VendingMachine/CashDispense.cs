using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class CashDispense
    {
        List<Cassette> OriginalCashCassettes = new List<Cassette>();
        private List<Cassette> CloneCashCassettes = new List<Cassette>();
        public CashDispense()
        {
            OriginalCashCassettes.Add(new Cassette { Denom = 10, Count = 5 });
            
            OriginalCashCassettes.Add(new Cassette { Denom = 100, Count = 1 });
            CloneCashCassettes = OriginalCashCassettes;
        }

        public void Deposit(List<Cassette> amount)
        {
            foreach (var item in amount)
            {
                var coin = OriginalCashCassettes.Find(i => i.Denom == item.Denom);

                if (coin == null)
                {
                    OriginalCashCassettes.Add(item);
                }
                else
                {
                    coin.Count += item.Count;
                }
            }
        }

        public bool CanWithDraw(int reqAmount, out List<Cassette> noteMix)
        {
            noteMix = new List<Cassette>();

            CloneCashCassettes = OriginalCashCassettes;

            int userAmount = reqAmount;

            //1. Check if the amount is higher than combined counts.
            int totalCounts = 0;
            foreach (var item in CloneCashCassettes)
            {
                totalCounts += item.Denom * item.Count;
            }

            if (totalCounts < reqAmount)
            {
                Console.WriteLine("You're trying too high - maximum amount available is: " + totalCounts);
                return false;
            }

            //2. Check if the amount is dispensable with current denoms.
            int lowestDenom = CloneCashCassettes.Min(c => c.Denom);
            if (reqAmount % lowestDenom != 0)
            {
                Console.WriteLine("Unable to dispense amount with current denoms");
                return false;
            }

            //3. Calculate note mix to dispense.          

            do
            {
                //Sort cash cassettes by highest count first.
                CloneCashCassettes = CloneCashCassettes.OrderByDescending(c => c.Count).ToList();

                if (CloneCashCassettes.Count <= 0)
                    break;

                //Check if highest count denom can cover the amount.
                if (CloneCashCassettes[0].Denom <= reqAmount)
                {

                    //Check if this denom already exists in the mix.
                    Cassette noteMixCassette = noteMix.Find(n => n.Denom == CloneCashCassettes[0].Denom);
                    if (noteMixCassette == null)
                    {
                        //Add denom to the note mix.
                        noteMix.Add(new Cassette { Denom = CloneCashCassettes[0].Denom, Count = 1 });
                    }
                    else
                    {
                        //Increase denom count in the note mix.
                        noteMixCassette.Count += 1;
                    }

                    //Reduce denom count in the cash cassette.
                    CloneCashCassettes[0].Count -= 1;

                    //Reduce the amount by denom.
                    reqAmount -= CloneCashCassettes[0].Denom;

                    if (CloneCashCassettes[0].Count == 0)
                        CloneCashCassettes.RemoveAt(0);                    
                }
                else
                {
                    //The amount is smaller than denom => the denom is unusable - remove it.
                    CloneCashCassettes.RemoveAt(0);
                }

                //Keep looping until the amount is 0.
            } while (reqAmount > 0);

            double notemixtotal = 0;

            foreach (var item in noteMix)
            {
                notemixtotal += (item.Count * item.Denom);
            }

            if (notemixtotal != userAmount)
                return false;
            else
                return true;             
        }

        public void WithDraw(int reqAmount)
        {
            CloneCashCassettes = OriginalCashCassettes;

            int userAmount = reqAmount;

            //1. Check if the amount is higher than combined counts.
            int totalCounts = 0;
            foreach (var item in CloneCashCassettes)
            {
                totalCounts += item.Denom * item.Count;
            }

            if (totalCounts < reqAmount)
            {
                Console.WriteLine("You're trying too high - maximum amount available is: " + totalCounts);
                return;
            }

            //2. Check if the amount is dispensable with current denoms.
            int lowestDenom = CloneCashCassettes.Min(c => c.Denom);
            if (reqAmount % lowestDenom != 0)
            {
                Console.WriteLine("Unable to dispense amount with current denoms");
                return;
            }

            //3. Calculate note mix to dispense.
            List<Cassette> noteMix = new List<Cassette>();

            do
            {
                //Sort cash cassettes by highest count first.
                CloneCashCassettes = CloneCashCassettes.OrderByDescending(c => c.Count).ToList();

                //Check if highest count denom can cover the amount.
                if (CloneCashCassettes[0].Denom <= reqAmount)
                {

                    //Check if this denom already exists in the mix.
                    Cassette noteMixCassette = noteMix.Find(n => n.Denom == CloneCashCassettes[0].Denom);
                    if (noteMixCassette == null)
                    {
                        //Add denom to the note mix.
                        noteMix.Add(new Cassette { Denom = CloneCashCassettes[0].Denom, Count = 1 });
                    }
                    else
                    {
                        //Increase denom count in the note mix.
                        noteMixCassette.Count += 1;
                    }

                    //Reduce denom count in the cash cassette.
                    CloneCashCassettes[0].Count -= 1;                    

                    //Reduce the amount by denom.
                    reqAmount -= CloneCashCassettes[0].Denom;

                    if (CloneCashCassettes[0].Count == 0)
                        CloneCashCassettes.RemoveAt(0);
                }
                else
                {
                    //The amount is smaller than denom => the denom is unusable - remove it.
                    CloneCashCassettes.RemoveAt(0);
                }

                //Keep looping until the amount is 0.
            } while (reqAmount > 0);

            OriginalCashCassettes = CloneCashCassettes;
        }
    }
}
