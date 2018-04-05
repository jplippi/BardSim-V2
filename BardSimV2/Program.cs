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

namespace BardSimV2
{
    class Program
    {
        
        static void Main(string[] args)
        {
            while (true)
            {

                // Asks for run mode and duration
                List<string> ariyalaSets = new List<string>();
                Dictionary<AttributeType, int> attributes = new Dictionary<AttributeType, int>();

                int numberOfSets = 0;
                int numberOfRuns = 0;
                decimal input = 0;
                bool validCode = false;

                Regex ariyalaMatch = new Regex("^(http:\\/\\/)?(ffxiv.ariyala.com\\/)?([a-zA-Z0-9]{5})$");
                Regex attributesMatch = new Regex("(.*)? ");
                string ariyalaLink = "";

                SimulationParameters target = SimulationParameters.Invalid;

                while (numberOfSets == 0)
                {
                    Console.WriteLine("\nInput the number of gearsets:");
                    int.TryParse(Console.ReadLine(), out numberOfSets);
                }

                List<Dictionary<AttributeType, decimal>> attributesDictionaryList = new List<Dictionary<AttributeType, decimal>>();

                ChromeOptions options = new ChromeOptions();
                options.AddArgument("headless");

                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;

                IWebDriver driver = new ChromeDriver(service, options);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                for ( int i = 0; i < numberOfSets; i++)
                {

                    validCode = false;
                    Console.WriteLine("");
                    while (!validCode)
                    {
                        Console.WriteLine("Enter the Ariyala link or code #{0}:", i+1);
                        string ariyalaCode = Console.ReadLine();
                        if (!ariyalaMatch.IsMatch(ariyalaCode))
                        {
                            Console.Write("\nInvalid link or code. ");
                        }
                        else
                        {
                            ariyalaLink = "http://ffxiv.ariyala.com/" + ariyalaMatch.Match(ariyalaCode).Groups[3].Value;
                            validCode = true;

                            try
                            {

                                driver.Url = ariyalaLink;
                                driver.Navigate();

                                wait.Until(new Func<IWebDriver, bool>(IsLoaded));

                                List<string> attributesList = new List<string>();


                                var elementList = driver.FindElements(By.XPath("//td[@class=\"th attributeName total totalPos\"]"));

                                foreach (var element in elementList)
                                {
                                    attributesList.Add(element.Text);
                                }

                                if (driver.FindElement(By.Id("categoryBoxContentName")).Text != "Bard")
                                {
                                    throw new NotImplementedException();
                                }

                                Console.WriteLine("");
                                List<string> attributesNames = new List<string> { "DEX: ", "DHIT: ", "CRIT: ", "DET: ", "SKS: ", "VIT: " };
                                List<AttributeType> attributesTypes = new List<AttributeType> { AttributeType.Dexterity, AttributeType.DirectHit, AttributeType.CriticalHit, AttributeType.Determination, AttributeType.SkillSpeed, AttributeType.Vitality };
                                Dictionary<AttributeType, decimal> attributesDictionary = new Dictionary<AttributeType, decimal>
                                {
                                    { AttributeType.Strenght, 264 },
                                    { AttributeType.Dexterity, 386 },
                                    { AttributeType.Vitality, 292 },
                                    { AttributeType.Intelligence, 247 },
                                    { AttributeType.Mind, 232},
                                    { AttributeType.CriticalHit, 364},
                                    { AttributeType.Determination, 292},
                                    { AttributeType.DirectHit, 364},
                                    { AttributeType.SkillSpeed, 364},
                                    { AttributeType.SpellSpeed, 364},
                                    { AttributeType.Tenacity, 364},
                                    { AttributeType.Piety, 292 },
                                    { AttributeType.WeaponDamage, 4 },
                                    { AttributeType.WeaponDelay, 2.8m }
                                };

                                for (int j = 0; j < attributesList.Count; j++)
                                {
                                    GroupCollection match = attributesMatch.Match(attributesList[j]).Groups;

                                    decimal.TryParse(match[1].Value, out decimal attribute);
                                    attributesDictionary[attributesTypes[j]] = attribute;

                                    Console.WriteLine(attributesNames[j] + match[1].Value);
                                }

                                decimal weaponDamage = 0;
                                decimal weaponDelay = 0;

                                while (weaponDamage == 0)
                                {
                                    Console.WriteLine("\nPlease manually input the Weapon Damage:");
                                    decimal.TryParse(Console.ReadLine(), out weaponDamage);
                                }

                                while (weaponDelay == 0)
                                {
                                    Console.WriteLine("\nPlease manually input the Weapon Delay:");
                                    decimal.TryParse(Console.ReadLine(), out weaponDelay);
                                }

                                attributesDictionary[AttributeType.WeaponDamage] = weaponDamage;
                                attributesDictionary[AttributeType.WeaponDelay] = weaponDelay;

                                attributesDictionaryList.Add(attributesDictionary);

                            }
                            catch (WebDriverTimeoutException)
                            {
                                Console.Write("\nRequest timed out. Invalid link or code. ");
                                validCode = false;
                            }
                            catch (NotImplementedException)
                            {
                                Console.Write("\nNot a Bard gearset. ");
                                validCode = false;
                            }
                        }
                    }
                }
                driver.Dispose();
                service.Dispose();

                while(numberOfRuns == 0)
                {
                    Console.WriteLine("\nInput the number of simulations:");
                    int.TryParse(Console.ReadLine(), out numberOfRuns);
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
                        Console.WriteLine("\nInput the damage target:");
                        decimal.TryParse(Console.ReadLine(), out input);
                    }
                    else if (target == SimulationParameters.TimeTarget)
                    {
                        Console.WriteLine("\nInput the time target:");
                        decimal.TryParse(Console.ReadLine(), out input);
                    }
                }

                // Main loop
                for (int k = 0; k < numberOfSets; k++)
                {
                    List<decimal> results = new List<decimal>();
                    Engine engine = new Engine(attributesDictionaryList[k]);
                    Stopwatch timer = new Stopwatch();
                    timer.Start();

                    for (int i = 0; i < numberOfRuns; i++)
                    {
                        results.Add(engine.Simulate(target, input));
                        engine.Reinitialize();
                    }
                    timer.Stop();

                    StreamWriter stream = new StreamWriter(@"C:\Users\jplip\Documents\dps.csv");
                    CsvWriter csvWriter = new CsvWriter(stream);

                    foreach (decimal d in results)
                    {
                        csvWriter.WriteRecord((double)d);
                        csvWriter.NextRecord();

                    }

                    stream.Dispose();


                    Console.WriteLine("\nAverage DPS: {0:0.00}\nMax: {1:0.00}\nMin: {2:0.00}",results.Average(),results.Max(),results.Min());

                    Console.WriteLine("\nStopwatch: {0}", timer.ElapsedMilliseconds);
                }
            }
        }

        public static bool IsLoaded(IWebDriver driver)
        {
            var element = driver.FindElement(By.Id("updateIndicator"));

            if(element == null || element.GetAttribute("style") != "display: none;")
            {
                return false;
            }

            return true;
        }
    }
}
