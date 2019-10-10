using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Beverage : Product
    {
        // TODO Set up summary
        public override void YumYum()
        {
            // TODO Should this write directly to the console or return a value?
            Console.WriteLine("Glug Glug, Yum!");
        }
    }
}
