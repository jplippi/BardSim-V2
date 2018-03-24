using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class Keyboard
    {
        //NOTE: I'll have a set of keys here. Let's try with just 'tab' for now.
        public readonly Key Tab;

        public Keyboard()
        {
            Tab = new Key();
        }
    }
}
