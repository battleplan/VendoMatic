using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Slot
    {
        // TODO Set up summary
        // TODO Populate in constructor
        public string Identifier { get; }

        // TODO Set up summary
        // TODO Determine what methods affect this (Stock(), Purchase())
        public int QuantityRemaining { get; private set; }

        // TODO Set up summary
        // TODO Is this needed/used?
        public bool HasStock => QuantityRemaining > 0;

        // TODO Set up summary
        // TODO Is this where this should go?
        // TODO Populate in constructor
        public Product Product { get; }

        // TODO Set up summary
        // TODO Populate in constructor
        public decimal Price { get; }

        //constructor

        //constructor
        public Slot(string identifier, Product product, decimal price)
        {
            Identifier = identifier;
            Product = product;
            Price = price;
            QuantityRemaining = 5;
        }
    }
}
