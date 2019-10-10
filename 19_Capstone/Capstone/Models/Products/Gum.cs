using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Gum : Product
    {
        //constructor
        public Gum(string name) : base(name)
        {

        }
        // TODO Set up summary
        public override void YumYum()
        {
            // TODO Should this write directly to the console or return a value?
            Console.WriteLine("Chew Chew, Yum!");
        }
    }
}
