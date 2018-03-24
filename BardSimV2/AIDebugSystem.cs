using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class AIDebugSystem : ISystem
    {
        public void Update(Stopwatch timer, Keyboard keyboard)
        {
            //if(timer.ElapsedMilliseconds % 3000 > 2900 && timer.ElapsedMilliseconds % 3000 < 100)
            //{
                keyboard.keysDictionary[Keys.Num1] = true;
            //}else if (timer.ElapsedMilliseconds % 3300 > 3200 && timer.ElapsedMilliseconds % 3300 < 100)
            //{
            //    keyboard.keysDictionary[Keys.Num1] = false;
            //}
        }
    }
}
