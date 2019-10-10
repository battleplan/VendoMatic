using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Slot
    {
        // TODO Set up summary
        public string Identifier { get; }

        // TODO Set up summary
        // TODO Determine what methods affect this (Stock(), Purchase())
        public int QuantityRemaining { get; private set; }

        // TODO Set up summary
        public bool HasStock => QuantityRemaining > 0;

        // TODO Set up summary
        public Product Product { get; }

        // TODO Set up summary
        public decimal Price { get; }

        // Constructor
        public Slot(string identifier, Product product, decimal price)
        {
            Identifier = identifier;
            Product = product;
            Price = price;
            QuantityRemaining = 5;
        }

        public bool Dispense()
        {
            if (QuantityRemaining > 0)
            {
                QuantityRemaining--;
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
