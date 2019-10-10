using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Models
{
    public class VendingMachine
    {
        // TODO Relevant methods should update this
        public decimal Balance;

        // TODO Assign this in constructor
        //private readonly string inputFilePath;

        // TODO Relevant methods should update this
        private decimal totalSales;

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
            // TODO Read from input file
            // TODO Create Product instances
            // TODO Add Products to Products
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
            WriteLog(logText);
            //TODO transaction log
            // TODO Set up parameters
            // TODO Adjust Balance
            // TODO Determine return value
        }


        // TODO Set up summary
        private void MakeChange()
        {
            // TODO Set up parameters
            // TODO Adjust Balance
            // TODO Calculate quarters, dimes, and nickels
            // TODO Determine return value
        }

        // TODO Set up summary
        public void Purchase()
        {
            // TODO Set up parameters (Product? Slot?)
            // TODO Call Dispense() - this will call some kind of adjust balance method back here?
            // TODO Determine return value
            // TODO Should this be private? Abstract?
            // TODO Update total sales
        }

        // TODO Set up summary
        public void FinishTransaction()
        {
            // TODO Call MakeChange()
            // TODO Determine return value
        }

        // TODO Log transaction

        // TODO Sales report
    }
}
