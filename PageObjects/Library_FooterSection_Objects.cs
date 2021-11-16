using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
using System.Threading;

namespace LibraryAutomation.PageObjects
{
    class Library_FooterSection_Objects
    {
        String DisplayedMonth;
        String DisplayedNextMonth;
        String StoreTitleFromCalendar;
        int NumberofLi;

        IWebDriver commonDriver;

        public Library_FooterSection_Objects(IWebDriver driver)
        {
            commonDriver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(commonDriver, TimeSpan.FromSeconds(90)));
        }

        //Footer Section Objects
        [FindsBy(How = How.LinkText, Using = "PRIVACY POLICY AND CONSUMER INFORMATION")]
        public IWebElement PrivacyPolicyAndConsumerInformation { get; set; }

        [FindsBy(How = How.LinkText, Using = "LIBRARY SITEMAP")]
        public IWebElement LibrarySiteMap { get; set; }

        [FindsBy(How = How.LinkText, Using = "CONTACT US")]
        public IWebElement ContactUs { get; set; }

        [FindsBy(How = How.LinkText, Using = "LEAVE FEEDBACK")]
        public IWebElement LeaveFeedBack { get; set; }

        [FindsBy(How = How.CssSelector, Using = "footer td > .libFooter:nth-of-type(1)")]
        public IWebElement FooterSectionText{ get; set; }

        [FindsBy(How = How.CssSelector, Using = "[src='/Images/facebook.gif']")]
        public IWebElement Facebook { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[src='/Images/icon-twitter.gif']")]
        public IWebElement Twitter { get; set; }


        [FindsBy(How = How.CssSelector, Using = "[src='/Images/icon-youtube.gif']")]
        public IWebElement youTube { get; set; }

        //** Admin link is not available

        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/div/div/footer/table/tbody/tr/td/span/span[1]/a")]

        public IWebElement LibraryAdmin { get; set; }



        //-----------------------------------------------------------------------------------------------------//

        //Start Of FooterSection Menu Methods

        public void Clicking_YouTubeIcon()
        {

            youTube.Click();

        }
        public void Clicking_TwitterIcon()
        {

            Twitter.Click();

        }


        public void Clicking_FacebookIcon()
        {

            Facebook.Click();

        }


        public void Clicking_PrivacyPolicyAndConsumerInformation()
        {

            PrivacyPolicyAndConsumerInformation.Click();
          
        }

        public void Clicking_ContactUs()
        {

            ContactUs.Click();
        }

        public void Clicking_LibrarySiteMap()
        {

            LibrarySiteMap.Click();
        }

        public void Clicking_LeaveFeedBack()
        {

            LeaveFeedBack.Click();
        }

        public void Clicking_LibraryAdmin()
        {
            LibraryAdmin.Click();
        }

        public void VerifyingTextDisplayedinFooterSection()
        {
           string GetFooterSectionValue = FooterSectionText.GetAttribute("innerText");
           Console.WriteLine(GetFooterSectionValue);
           Assert.IsTrue(GetFooterSectionValue.Contains("NCU is regionally accredited by WASC Senior College and University Commission (WSCUC), 985 Atlantic Avenue, Suite 100, Alameda, CA 94501, 510.748.9001, http://www.wascsenior.org. Copyright 2015. All rights reserved."));
        }
    }
}


















