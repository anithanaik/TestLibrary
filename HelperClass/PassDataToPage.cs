using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAutomation

{
    static class PassDataToPage
    {
        public static void EnterText(this IWebElement element, string value)
        {
            element.Clear();
            element.SendKeys(value);
        }

        public static void ClickButton(this IWebElement element)
        {
            element.Click();
        }

        public static void SubmitButton(this IWebElement element)
        {
            element.Submit();
        }
        public static void SelectFromDropdown_ByIndex(this IWebElement element, int index)
        {           
            new SelectElement(element).SelectByIndex(index);
        }

        public static void SelectFromDropdown_ByValue(this IWebElement element, string value)
        {
           new SelectElement(element).SelectByValue(value);
        }

        public static void SelectFromDropdown_ByText(this IWebElement element, string value)
        {
            new SelectElement(element).SelectByText(value);
        }
    }
}
