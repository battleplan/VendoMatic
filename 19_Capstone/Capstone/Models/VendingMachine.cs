using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Models
{
    public class VendingMachine
    {
        public decimal Balance;

        // TODO Assign this in constructor
        //private readonly string inputFilePath;

        public decimal TotalSales { get; private set; }

        // TODO Set up summary
        public Dictionary<string, Product> Products { get; private set; }


        // TODO Set up summary
        public Dictionary<string, Slot> Slots { get; private set; }

        // Constructor
        public VendingMachine()
        {
            Products = new Dictionary<string, Product>();
            Slots = new Dictionary<string, Slot>();
        }


        // TODO Set up summary
        // TODO Determine return value
        public bool StockFromFile(string inputFilePath)
        {
            List<string> inputLines = new List<string>();
            // TODO Read from input file - check this!!
            //TODO bool?
            try
            {
                
                using (StreamReader sr = new StreamReader(inputFilePath))
                {
                    while (!sr.EndOfStream)
                    {
                        inputLines.Add(sr.ReadLine()); 

                    }
                }
            }
            catch (FileNotFoundException)
            {
                //TODO dont forget about this
                 return false;
            }
            catch (Exception)
            {
                return false;
            }

            return Stock(inputLines.ToArray());
        }

        public bool Stock(string[] inputLines)
        {
            foreach (string line in inputLines)
            {
                string identifier = "";
                string nameProduct = "";
                string priceProduct = "";
                string productClass = "";
                try
                {
                    string[] stockElements = line.Split("|");
                    identifier = stockElements[0];
                    nameProduct = stockElements[1];
                    priceProduct = stockElements[2];
                    productClass = stockElements[3];
                }
                catch (Exception ex)
                {
                    // TODO What happens if it was unable to read the elements?
                    Console.WriteLine($"An error occurred stocking the machine: {ex.Message}");
                    Console.ReadKey();
                    return false;
                }

                decimal priceDecimal = 0;
                bool priceWasParsed = decimal.TryParse(priceProduct, out priceDecimal);

                if (priceWasParsed)
                {
                    // Find if Product already exists
                    Product product = null;
                    if (Products.ContainsKey(nameProduct))
                    {
                        product = Products[nameProduct];
                    }
                    if (product == null)
                    {
                        // Create new product
                        switch (productClass)
                        {
                            case "Chip":
                                product = new Chip(nameProduct);
                                break;
                            case "Drink":
                                product = new Beverage(nameProduct);
                                break;
                            case "Gum":
                                product = new Gum(nameProduct);
                                break;
                            case "Candy":
                                product = new Candy(nameProduct);
                                break;
                        }
                        Products[nameProduct] = product;
                    }
                    
                    // Create slot and put product in it
                    Slots[identifier] = (new Slot(identifier, product, priceDecimal));
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        // TODO Set up summary
        public void FeedMoney(int money)
        {

            Balance += money;
            string logText = ($"FEED MONEY: {money:C}"); 
            TransactionLog(logText);
            //TODO transaction log - test that this works
            // TODO Determine return value
        }

        //TODO check path location for Log and Write a Catch block
        private void  TransactionLog(string logText)
        {
            // TODO Make sure this works
            string time = DateTime.Now.ToString();
            string balance = Balance.ToString();
            try
            {
                using (StreamWriter log = new StreamWriter(@"..\..\..\..\Log.Txt", true))
                {
                    log.WriteLine($"{time} {logText} {balance}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred writing the transaction log: {ex.Message}");
                Console.ReadKey();
            }
        }

        // TODO Set up summary
        public bool Purchase(string slotIdentifier)
        {
            Slot slot;
            if (Slots.ContainsKey(slotIdentifier))
            {
                slot = Slots[slotIdentifier];
            }
            else
            {
                return false;
            }
            bool slotDispensed = slot.Dispense();
            if (slotDispensed)
            {
                slot.Product.SellProduct();
                TotalSales += slot.Price;
                Balance -= slot.Price;
                TransactionLog($"{slot.Product.Name} {slot.Identifier} {slot.Price:C}");
                return true;
                // TODO Make sure transaction log works
            }
            else
            {
                return false;
            }
            // TODO Determine return value
        }

        public List<string> GetSlotsDisplayNames()
        {
            List<string> slots = new List<string>();
            List<string> slotIdentifiers = new List<string>(Slots.Keys);
            foreach (string key in slotIdentifiers)
            {
                slots.Add(Slots[key].DisplayName);
            }

            return slots;
        }

        // TODO Set up summary
        public string FinishTransaction()
        {
            decimal changeGiven = Balance;
            int quarters = 0;
            int dimes = 0;
            int nickels = 0;
            while (Balance > 0)
            {
                if (Balance >= (decimal).25)
                {
                    Balance -= (decimal).25;
                    quarters++;
                }
                else if (Balance >= (decimal).10)
                {
                    Balance -= (decimal).10;
                    dimes++;
                }
                else if (Balance >= (decimal).05)
                {
                    Balance -= (decimal).05;
                    nickels++;
                }
            }
            //Balance = 0;

            TransactionLog($"GIVE CHANGE: {changeGiven:C}");

            string totalChange = $"Your Change is {quarters} Quarters, {dimes} Dimes, and {nickels} Nickels";
            return totalChange;
            // TODO Determine return value - new Money class with subclasses of Quarter, Dime, and Nickel?
            // TODO Make sure this log works
        }

        // TODO Sales report
    }
}
