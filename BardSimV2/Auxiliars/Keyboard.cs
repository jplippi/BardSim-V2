using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class Keyboard
    {
        //NOTE: I'll have a set of keys here. Let's try with just some keybinds for now.
        public readonly Dictionary<Keys,bool> keysDictionary;

        public Keyboard()
        {
            keysDictionary = new Dictionary<Keys, bool>
            {
                {Keys.Num1, false },
                {Keys.Num2, false },
                {Keys.F3, false },
                {Keys.R, false },
                {Keys.Num6, false },
                {Keys.T, false }
            };
        }
    }
}
