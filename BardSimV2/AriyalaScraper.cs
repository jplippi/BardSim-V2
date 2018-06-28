using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BardSimV2
{
    class AriyalaScraper : IDisposable
    {
        private ChromeOptions options;
        private ChromeDriverService service;
        private IWebDriver driver;
        private Regex ariyalaRegex;
        private Regex attributeRegex;
        private WebDriverWait wait;

        public AriyalaScraper()
        {
            options = new ChromeOptions();
            options.AddArgument("headless");

            service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            driver = new ChromeDriver(service, options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            {
                Message = "\nRequest timed out. Invalid link or code. "
            };

            ariyalaRegex = new Regex("^(http:\\/\\/)?(ffxiv.ariyala.com\\/)?([a-zA-Z0-9]{5})$");
            attributeRegex = new Regex("(.*)? ");

        }

        public AttributesDictionary GetAriyalaSet(string code)
        {
            // Checks if the input is valid
            if (!ariyalaRegex.IsMatch(code))
            {
                throw new FormatException("\nInvalid link or code. ");
            }

            // Navigates the page
            string ariyalaLink = "http://ffxiv.ariyala.com/" + ariyalaRegex.Match(code).Groups[3].Value;

            driver.Url = ariyalaLink;
            driver.Navigate();

            wait.Until(new Func<IWebDriver, bool>(IsLoaded));

            //TODO: Change for all jobs
            // Checks if it's a BRD gearset by reading the content of the html element
            if (driver.FindElement(By.Id("categoryBoxContentName")).Text != "Bard")
            {
                throw new NotImplementedException("\nNot a bard gearset. ");
            }

            // Reads the attributes from the html elements
            IReadOnlyCollection<IWebElement> elementList = driver.FindElements(By.XPath("//td[@class=\"th attributeName total totalPos\"]"));
            AttributesDictionary attributesFromAriyala = new AttributesDictionary();

            if(elementList.Count != JobSettings.brdAttributeTypes.Count)
            {
                throw new NotFoundException("\nNumber of attributes read and expected mismatch. ");
            }

            for (int i = 0; i < elementList.Count; i++)
            {
                int.TryParse(attributeRegex.Match(elementList.ElementAt(i).Text).Groups[1].Value, out int attribute);

                if(attribute == 0)
                {
                    throw new FormatException("\nInvalid attribute. ");
                }

                // Stores the read attribute into the dictionary
                attributesFromAriyala[JobSettings.brdAttributeTypes[i]] = attribute;
            }


            bool validWeapDamage = true;
            bool validWeapDelay = true;
            // Asks for weapon damage and weapon delay
            do
            {
                try
                {
                    Console.WriteLine("\nPlease manually input the Weapon Damage:");
                    int.TryParse(Console.ReadLine(), out int weaponDamage);
                    if (weaponDamage <= 0)
                    {
                        throw new InvalidDataException("\nNumber must be greater than zero.");
                    }
                    attributesFromAriyala[AttributeType.WeaponDamage] = weaponDamage;
                    validWeapDamage = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    validWeapDamage = false;
                }
            }
            while (!validWeapDamage);

            do
            {
                try
                {
                    Console.WriteLine("\nPlease manually input the Weapon Delay:");
                    decimal.TryParse(Console.ReadLine(), out decimal weaponDelay);
                    if (weaponDelay <= 0)
                    {
                        throw new InvalidDataException("\nNumber must be greater than zero.");
                    }
                    attributesFromAriyala[AttributeType.WeaponDelay] = weaponDelay;
                    validWeapDelay = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    validWeapDelay = false;
                }
            }
            while (!validWeapDelay);

            return attributesFromAriyala;
        }

        private static bool IsLoaded(IWebDriver driver)
        {
            var element = driver.FindElement(By.Id("updateIndicator"));

            if (element == null || element.GetAttribute("style") != "display: none;")
            {
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            driver.Dispose();
            service.Dispose();
        }
    }
}
