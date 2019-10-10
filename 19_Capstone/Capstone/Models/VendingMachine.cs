using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Models
{
    public class VendingMachine
    {
        // TODO Relevant methods should update this
        // TODO Should this be rouded to 2 decimal places?
        public decimal Balance;

        // TODO Assign this in constructor
        //private readonly string inputFilePath;

        // TODO Relevant methods should update this
        public decimal TotalSales { get; private set; }

        // TODO Set up summary
        // TODO Set up Product class
        // TODO Should this be private?
        // TODO Is this needed at all now that we have Slot?
        public List<Product> Products { get; private set; }


        // TODO Set up summary
        public List<Slot> Slots { get; private set; }

        // Constructor
        public VendingMachine()
        {
            Products = new List<Product>();
            Slots = new List<Slot>();
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

            return true;
        }

        public void Stock(string[] inputLines)
        {
            foreach (string line in inputLines)
            {
                string[] stockElements = line.Split("|");
                string identifier = stockElements[0];
                string nameProduct = stockElements[1];
                string priceProduct = stockElements[2];
                string productClass = stockElements[3];

                decimal priceDecimal = 0;
                bool priceWasParsed = decimal.TryParse(priceProduct, out priceDecimal);

                if (priceWasParsed)
                {
                    // Find if Product already exists
                    Product product = null;
                    if (Products.Count > 0)
                    {
                        foreach (Product productExisting in Products)
                        {
                            if (productExisting.Name == nameProduct)
                            {
                                product = productExisting;
                            }
                        }
                    }
                    if (product == null)
                    {
                        // Create new product
                        switch (productClass)
                        {
                            case "Chip":
                                product = new Chip(nameProduct);
                                break;
                            case "Beverage":
                                product = new Beverage(nameProduct);
                                break;
                            case "Gum":
                                product = new Gum(nameProduct);
                                break;
                            case "Candy":
                                product = new Candy(nameProduct);
                                break;
                        }
                        Products.Add(product);
                    }


                    // Create slot and put product in it
                    Slots.Add(new Slot(identifier, product, priceDecimal));
                }
            }
        }

        // TODO Set up summary
        public void FeedMoney(int money)
        {

            Balance += money;
            string logText = ($"FEED MONEY: ${money}.00"); 
            TransactionLog(logText);
            // TODO Use C to make money into currency?
            //TODO transaction log - test that this works
            // TODO Determine return value
        }

        //TODO check path location for Log and Write a Catch block
        private void  TransactionLog(string logText)
        {
            // TODO Make sure this works
            string time = DateTime.Now.ToString();
            string balance = Balance.ToString();
            using (StreamWriter log = new StreamWriter(@"..\..\..\..\Log.Txt", true))
            {
                log.WriteLine($"{time} {logText} {balance}");
            }
        }

        // TODO Set up summary
        public void Purchase(Slot slot)
        {
            bool slotDispensed = slot.Dispense();
            if (slotDispensed)
            {
                slot.Product.SellProduct();
                TotalSales += slot.Price;
                Balance -= slot.Price;
                TransactionLog($"{slot.Product.Name} {slot.Identifier} {slot.Price:C}");
                // TODO Make sure transaction log works
            }
            // TODO Determine return value
        }

        // TODO Set up summary
        public string FinishTransaction()
        {
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
            Balance = 0;
            string totalChange = $"Your Change is {quarters} Quarters, {dimes} Dimes, and {nickels} Nickels";
            return totalChange;
            // TODO Determine return value - new Money class with subclasses of Quarter, Dime, and Nickel?
        }

        // TODO Sales report
    }
}
