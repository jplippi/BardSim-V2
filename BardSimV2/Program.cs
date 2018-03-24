using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ISystem> _systems = new List<ISystem>();

            // Starts the timer
            Stopwatch timer = Stopwatch.StartNew();

            // Sets up a keyboard to watch for inputs
            Keyboard keyboard = new Keyboard();

            while (true)
            {
                foreach (ISystem sys in _systems)
                {
                    sys.Update(timer, keyboard);
                };
            }
        }
    }
}
