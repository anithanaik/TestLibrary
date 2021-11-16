using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAutomation

{
    static class GetDataFromPage
    {
        public static string GetTextFromTextBox(this IWebElement element)
        {
            return element.GetAttribute("value");
        }
        public static string GetSelectedValueFromDropDown(this IWebElement element)
        {
            return new SelectElement(element).AllSelectedOptions.SingleOrDefault().Text;

        }
    }
}
