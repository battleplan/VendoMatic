using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public abstract class Product
    {
        public string Name { get; }
        public string Price { get; }
        private int quantitySold;

        // TODO Override this method
        public abstract void YumYum();
    }
}
