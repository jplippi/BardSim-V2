using CsvHelper;
using ExtensionMethods;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Data;

namespace BardSimV2
{
    class Program
    {
        
        static void Main(string[] args)
        {
            while (true)
            {
                LogData logData = new LogData();
                DataTable dpsTable = new DataTable("DPS");
                List<string> ariyalaSets = new List<string>();

                int numberOfSets = 0;
                int numberOfRuns = 0;
                decimal simulationTarget = 0;
                List<AttributesDictionary> listOfAttDicts = new List<AttributesDictionary>();
                AttributesDictionary singleAttDict = new AttributesDictionary();

                bool validSetNumber = true;
                bool validCode = true;
                bool validRunsNumber = true;
                bool validSimulationTarget = true;
                SimulationParameters simulationParameter = SimulationParameters.Invalid;
                SimulationType simulationType = SimulationType.Invalid;

                // Asks for type of simulation
                do
                {
                    Console.WriteLine("\nEnter 'V' for a single verbose run, or 'M' for multiple runs:");

                    try
                    {
                        char typeChar = Console.ReadLine()[0];

                        if (typeChar == 'V')
                        {
                            simulationType = SimulationType.Verbose;
                        }
                        else if (typeChar == 'M')
                        {
                            simulationType = SimulationType.Multiple;
                        }
                        else
                        {
                            throw new InvalidDataException("Invalid type of simulation.");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        simulationType = SimulationType.Invalid;
                    }
                }
                while (simulationType == SimulationType.Invalid);

                // Multiple simulation
                if(simulationType == SimulationType.Multiple)
                {
                    // Asks for number of gearsets
                    do
                    {
                        Console.WriteLine("\nInput the number of gearsets:");

                        try
                        {
                            int.TryParse(Console.ReadLine(), out numberOfSets);
                            if (numberOfSets <= 0)
                            {
                                throw new InvalidDataException("Number must be greater than zero.");
                            }
                            validSetNumber = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            validSetNumber = false;
                        }
                    }
                    while (!validSetNumber);

                    // Instantiates the HTML scraper for Ariyala
                    AriyalaScraper scraper = new AriyalaScraper();

                    // Iterates over each gearset
                    for (int i = 0; i < numberOfSets; i++)
                    {
                        do
                        {
                            Console.WriteLine("\nEnter the Ariyala link or code #{0}:", i + 1);

                            try
                            {
                                string ariyalaCode = Console.ReadLine();
                                listOfAttDicts.Add(scraper.GetAriyalaSet(ariyalaCode));
                                validCode = true;

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                validCode = false;
                            }
                        }
                        while (!validCode);
                    }

                    // Closes the browser
                    scraper.Dispose();

                    // Asks for number of simulations
                    do
                    {
                        Console.WriteLine("\nInput the number of simulations:");

                        try
                        {
                            int.TryParse(Console.ReadLine(), out numberOfRuns);
                            if (numberOfRuns <= 0)
                            {
                                throw new InvalidDataException("Number must be greater than zero.");
                            }
                            validRunsNumber = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            validRunsNumber = false;
                        }
                    }
                    while (!validRunsNumber);

                    // Asks for type of simulation
                    do
                    {
                        Console.WriteLine("\nEnter 'D' for damage target, or 'T' for time target:");

                        try
                        {
                            char targetChar = Console.ReadLine()[0];

                            if (targetChar == 'D')
                            {
                                simulationParameter = SimulationParameters.DamageTarget;
                            }
                            else if (targetChar == 'T')
                            {
                                simulationParameter = SimulationParameters.TimeTarget;
                            }
                            else
                            {
                                throw new InvalidDataException("Invalid type of simulation.");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            simulationParameter = SimulationParameters.Invalid;
                        }
                    }
                    while (simulationParameter == SimulationParameters.Invalid);

                    // Asks for target of simulation (seconds or total damage)
                    do
                    {
                        try
                        {
                            if (simulationParameter == SimulationParameters.DamageTarget)
                            {
                                Console.WriteLine("\nInput the  total damage target:");
                                decimal.TryParse(Console.ReadLine(), out simulationTarget);
                            }
                            else if (simulationParameter == SimulationParameters.TimeTarget)
                            {
                                Console.WriteLine("\nInput the time target (in seconds):");
                                decimal.TryParse(Console.ReadLine(), out simulationTarget);
                            }

                            if (simulationTarget <= 0)
                            {
                                throw new InvalidDataException("Value must be greater than zero.");
                            }
                            validSimulationTarget = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            validSimulationTarget = false;
                        }
                    }
                    while (!validSimulationTarget);

                    // Main loop
                    for (int k = 0; k < numberOfSets; k++)
                    {
                        List<decimal> results = new List<decimal>();
                        Engine engine = new Engine(listOfAttDicts[k]);
                        Stopwatch timer = new Stopwatch();

                        // Simulates each gearset
                        timer.Start();
                        for (int i = 0; i < numberOfRuns; i++)
                        {
                            results.Add(engine.Simulate(simulationParameter, simulationTarget));
                            engine.Reinitialize();
                        }
                        timer.Stop();


                        Console.WriteLine("\nAverage DPS: {0:0.00}\nMax: {1:0.00}\nMin: {2:0.00}", results.Average(), results.Max(), results.Min());

                        Console.WriteLine("\nSimulation time: {0}", timer.ElapsedMilliseconds.MilliToSeconds());

                        // Writes to CSV
                        Console.WriteLine("\nWriting to CSV.");
                        StreamWriter stream = new StreamWriter(@"C:\Users\jplip\Documents\dps" + k.ToString() + @".csv");
                        CsvWriter csvWriter = new CsvWriter(stream);

                        timer.Restart();
                        foreach (decimal d in results)
                        {
                            csvWriter.WriteRecord((double)d);
                            csvWriter.NextRecord();

                        }
                        timer.Stop();

                        Console.WriteLine("\nWrite time: {0}", timer.ElapsedMilliseconds.MilliToSeconds());

                        stream.Dispose();
                    }
                }

                // Single verbose simulation
                else
                {
                    // Instantiates the HTML scraper for Ariyala
                    AriyalaScraper scraper = new AriyalaScraper();

                    // Asks for an Ariyala code
                    do
                    {
                        Console.WriteLine("\nEnter the Ariyala link or code:");

                        try
                        {
                            string ariyalaCode = Console.ReadLine();
                            singleAttDict = scraper.GetAriyalaSet(ariyalaCode);
                            validCode = true;

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            validCode = false;
                        }
                    }
                    while (!validCode);

                    // Closes the browser
                    scraper.Dispose();

                    // Asks for type of simulation
                    do
                    {
                        Console.WriteLine("\nEnter 'D' for damage target, or 'T' for time target:");

                        try
                        {
                            char targetChar = Console.ReadLine()[0];

                            if (targetChar == 'D')
                            {
                                simulationParameter = SimulationParameters.DamageTarget;
                            }
                            else if (targetChar == 'T')
                            {
                                simulationParameter = SimulationParameters.TimeTarget;
                            }
                            else
                            {
                                throw new InvalidDataException("Invalid type of simulation.");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            simulationParameter = SimulationParameters.Invalid;
                        }
                    }
                    while (simulationParameter == SimulationParameters.Invalid);

                    // Asks for target of simulation (seconds or total damage)
                    do
                    {
                        try
                        {
                            if (simulationParameter == SimulationParameters.DamageTarget)
                            {
                                Console.WriteLine("\nInput the  total damage target:");
                                decimal.TryParse(Console.ReadLine(), out simulationTarget);
                            }
                            else if (simulationParameter == SimulationParameters.TimeTarget)
                            {
                                Console.WriteLine("\nInput the time target (in seconds):");
                                decimal.TryParse(Console.ReadLine(), out simulationTarget);
                            }

                            if (simulationTarget <= 0)
                            {
                                throw new InvalidDataException("Value must be greater than zero.");
                            }
                            validSimulationTarget = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            validSimulationTarget = false;
                        }
                    }
                    while (!validSimulationTarget);

                    // Main loop

                    decimal result = 0;
                    Engine engine = new Engine(singleAttDict);
                    Stopwatch timer = new Stopwatch();


                    timer.Start();

                    result = engine.Simulate(simulationParameter, simulationTarget, logData);
                    engine.Reinitialize();

                    timer.Stop();


                    Console.WriteLine("\nDPS: {0:0.00}", result);

                    Console.WriteLine("\nSimulation time: {0}", timer.ElapsedMilliseconds.MilliToSeconds());

                    List<string> logStrings = logData.GetLog();
                    
                    foreach(string s in logStrings)
                    {
                        Console.Write(s);
                    }

                }
            }
        }
    }
}
