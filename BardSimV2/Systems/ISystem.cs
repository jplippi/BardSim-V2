using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    interface ISystem
    {
        void Update(ulong timer, Keyboard keyboard);
    }
}
