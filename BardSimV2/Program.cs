using ExtensionMethods;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {

                // Asks for run mode and duration

                int numberOfRuns = 0;
                decimal input = 0;
                bool verbose = true;

                SimulationParameters mode = SimulationParameters.Invalid;
                SimulationParameters target = SimulationParameters.Invalid;
                SimulationType type = SimulationType.Invalid;

                while(type == SimulationType.Invalid)
                {
                    Console.WriteLine("\nEnter 'S' for a single simulation (verbose), or 'M' for multiple simulations:");
                    char typeChar = Console.ReadLine()[0];

                    if (typeChar == 'S')
                    {
                        numberOfRuns = 1;
                        type = SimulationType.SingleRun;
                    }
                    else if (typeChar == 'M')
                    {
                        verbose = false;
                        type = SimulationType.MultipleRuns;
                    }
                }

                while(type == SimulationType.MultipleRuns && numberOfRuns == 0)
                {
                    Console.WriteLine("Input the number of simulations:");
                    int.TryParse(Console.ReadLine(), out numberOfRuns);
                }

                while (mode == SimulationParameters.Invalid)
                {
                    Console.WriteLine("\nEnter 'R' for real-time mode, or 'F' for fast mode:");
                    char modeChar = Console.ReadLine()[0];

                    if (modeChar == 'R')
                    {
                        mode = SimulationParameters.RealTime;
                    }
                    else if (modeChar == 'F')
                    {
                        mode = SimulationParameters.FastTime;
                    }
                }

                while (target == SimulationParameters.Invalid)
                {

                    Console.WriteLine("\nEnter 'D' for damage target, or 'T' for time target:");
                    char targetChar = Console.ReadLine()[0];

                    if (targetChar == 'D')
                    {
                        target = SimulationParameters.DamageTarget;
                    }
                    else if (targetChar == 'T')
                    {
                        target = SimulationParameters.TimeTarget;
                    }
                }

                while (input == 0)
                {

                    if (target == SimulationParameters.DamageTarget)
                    {
                        Console.WriteLine("Input the damage target:");
                        decimal.TryParse(Console.ReadLine(), out input);
                    }
                    else if (target == SimulationParameters.TimeTarget)
                    {
                        Console.WriteLine("Input the time target:");
                        decimal.TryParse(Console.ReadLine(), out input);
                    }
                }

                // Main loop

                List<decimal> results = new List<decimal>();
                Engine engine = new Engine();
                Stopwatch timer = new Stopwatch();
                timer.Start();

                for (int i = 0; i < numberOfRuns; i++)
                {
                    results.Add(engine.Simulate(mode, target, input, verbose));
                    engine.Reinitialize();
                }
                timer.Stop();

                if(numberOfRuns > 1)
                {
                    Console.WriteLine("Average DPS: {0:0.00}\nMax: {1:0.00}\nMin: {2:0.00}",results.Average(),results.Max(),results.Min());
                }

                Console.WriteLine("\nStopwatch: {0}", timer.ElapsedMilliseconds.MilliToSeconds());
            }
        }
    }
}
