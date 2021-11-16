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
    class Library_LeftMenu_Objects
    {
        String DisplayedMonth;
        String DisplayedNextMonth;
#pragma warning disable CS0649 // Field 'Library_LeftMenu_Objects.StroeTitleFromCaledar' is never assigned to, and will always have its default value null
        String StoreTitleFromCalendar;
#pragma warning restore CS0649 // Field 'Library_LeftMenu_Objects.StroeTitleFromCaledar' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'Library_LeftMenu_Objects.StroeTitleFromTab' is never assigned to, and will always have its default value null
        String StoreTitleFromTab;
#pragma warning restore CS0649 // Field 'Library_LeftMenu_Objects.StroeTitleFromTab' is never assigned to, and will always have its default value null
#pragma warning disable CS0169 // The field 'Library_LeftMenu_Objects.NumberofLi' is never used
        int NumberofLi;
#pragma warning restore CS0169 // The field 'Library_LeftMenu_Objects.NumberofLi' is never used


        IWebDriver commonDriver;

        public Library_LeftMenu_Objects(IWebDriver driver)
        {
            commonDriver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(commonDriver, TimeSpan.FromSeconds(90)));
        }

        //***** Home Page Object using Page Factory   *****//

        /*****Left Menu Objects *****/

        [FindsBy(How = How.LinkText, Using = "Find Answers to Frequently Asked Questions")]
        public IWebElement FindAnswerstoFrequentlyAskedQuestionsLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "Access RefWorks")]
        public IWebElement AccessRefWorksLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "Request Interlibrary Loan Items")]
        public IWebElement RequestInterlibraryLoanItemsLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "Test My Library Skills")]
        public IWebElement TestMyLibrarySkillsLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "Request In-depth Research Support")]
        public IWebElement RequestIndepthResearchSupportLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "Access Alumni Resources")]
        public IWebElement AccessAlumniResourcesLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "Access JFKU Resources")]
        public IWebElement AccessJFKUResourcesLink { get; set; }


        [FindsBy(How = How.CssSelector, Using = "[aria-label='Previous month']")]
        public IWebElement PreviousmonthButtom { get; set; }


        [FindsBy(How = How.CssSelector, Using = "[aria-label='Next month']")]
        public IWebElement NextmonthButton { get; set; }



        //[FindsBy(How = How.XPath, Using = "//a[@id='cal-mini-showall']")]
        //public IWebElement ShowallLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "Show All")]
        public IWebElement ShowallLink { get; set; }


        [FindsBy(How = How.XPath, Using = "//div[@id='UpcomingEvents']/iframe[@src='https://api3.libcal.com/embed_mini_calendar.php?mode=month&iid=3351&cal_id=2938&l=5&tar=0&h=450&audience=&c=&z=']")]
      
        public IWebElement iframe { get; set; }


        [FindsBy(How = How.CssSelector, Using = ".cal-mini-hasevent")]
        public IWebElement EventLinkOnCalendar { get; set; }

        [FindsBy(How = How.ClassName, Using = "calevent-mini-a")]
        public IWebElement GetEventNameOnCalendar { get; set; }


        [FindsBy(How = How.ClassName, Using = "s-lc-c-erh")]
        public IWebElement ShowAllLinkOpenedConfirmation { get; set; }


        //[FindsBy(How = How.XPath, Using = "//a[@id='cal-mini-showall']")]
        //public IWebElement iframeScroll { get; set; }


        [FindsBy(How = How.LinkText, Using = "Show All")]
        public IWebElement iframeScroll { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='calevent-mini-list']//a[@class='calevent-mini-a']")]
        public IWebElement LiCount { get; set; }


        [FindsBy(How = How.CssSelector, Using = ".cal-mini-title")]
        public IWebElement NextMonthValue { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".cal-mini-title")]
        public IWebElement CurrentMonth { get; set; }


        //[FindsBy(How = How.XPath, Using = "//a[@id='cal-mini-showall']")]
        //public IWebElement ScrollPage { get; set; }


        [FindsBy(How = How.LinkText, Using = "Show All")]
        public IWebElement ScrollPage { get; set; }

        [FindsBy(How = How.ClassName, Using = "calevent-mini-a")]
        public IWebElement TitleName { get; set; }


        [FindsBy(How = How.Id, Using = "s-lc-public-header-title")]
        public IWebElement TitleID { get; set; }
        







        /*****Start Of  Left Menu Methods *****/


        public void GetCountOfLi()
        {
                        IList<IWebElement> numOfLi = commonDriver.FindElements(By.XPath("//div[@id='calevent-mini-list']//a[@class='calevent-mini-a']"));
            Console.WriteLine(numOfLi.Count);


        }


        public void ScrollIframe()
        {
            //IWebElement element =commonDriver.FindElement(By.XPath("//a[@id='cal-mini-showall']"));
            ((IJavaScriptExecutor)commonDriver).ExecuteScript("arguments[0].scrollIntoView(true);", ScrollPage);
           

            PageServices.WaitForPageToCompletelyLoaded(commonDriver, 90);
        }

        public void Clicking_ShowAllLink()
        {
            ShowallLink.Click();
        }

        public void AssuringPageIsOpenedByClickingShowAllLink()
        {
            Assert.True(ShowAllLinkOpenedConfirmation.Displayed);
        }

        public void Clicking_EventLinkOnCalendar()
        {


            EventLinkOnCalendar.Click();
        }
        public void Clicking_EventNameFromEvenSection()
        {
            Thread.Sleep(3000);
            GetEventNameOnCalendar.Click();
        }

        public void StoreEventNameFromCalendar()
        {


            //IWebElement TitleName = commonDriver.FindElement(By.ClassName("calevent-mini-a"));
            String StoreTitleFromCalendar = TitleName.GetAttribute("innerText");
            Console.WriteLine(StoreTitleFromCalendar);
        }

        public void StoreEventNameAfterPageOpened()
        {

            //IWebElement TitleID = commonDriver.FindElement(By.Id("s-lc-public-header-title"));
            String StoreTitleFromTab = TitleID.GetAttribute("innerText");
            Console.WriteLine(StoreTitleFromTab);

        }

        public void ComparingEventTextInEventSectionAndTab()
        {
            Assert.AreEqual(StoreTitleFromCalendar, StoreTitleFromTab);
        }


        public void SwichingToiframe()
        {
            commonDriver.SwitchTo().Frame(iframe);

        }
        public void Clicking_NextmonthButton()
        {
            //IWebElement iframe = driver.FindElement(By.XPath("//div[@id='UpcomingEvents']/iframe[@src='https://api3.libcal.com/embed_mini_calendar.php?mode=month&iid=3351&cal_id=2938&l=5&tar=0&h=450&audience=&c=&z=']"));

            //System.Threading.Thread.Sleep(2000);

            NextmonthButton.Click();
        }

        public void Clicking_PreviousmonthButton()
        {
            Thread.Sleep(3000);
            PageServices.WaitForPageToCompletelyLoaded(commonDriver, 120);
            //System.Threading.Thread.Sleep(2000);
            PreviousmonthButtom.Click();
        }

        public void Clicking_FindAnswerstoFrequentlyAskedQuestionsLink()
        {
            FindAnswerstoFrequentlyAskedQuestionsLink.Click();
        }
        public void Clicking_AccessRefWorksLink()
        {
            AccessRefWorksLink.Click();
        }

        public void Clicking_RequestInterlibraryLoanItemsLink()
        {

            RequestInterlibraryLoanItemsLink.Click();
        }
        public void Clicking_TestMyLibrarySkillsLink()
        {
            TestMyLibrarySkillsLink.Click();
        }

        public void Clicking_RequestIndepthResearchSupportLink()
        {
            RequestIndepthResearchSupportLink.Click();
        }

        public void Clicking_AccessAlumniResourcesLink()
        {
            AccessAlumniResourcesLink.Click();
        }

        public void Clicking_AccessJFKUResourcesLink()
        {
            AccessJFKUResourcesLink.Click();
        }


        public void PageScroll()
        {
            Actions actions = new Actions(commonDriver);

            actions.SendKeys(Keys.PageDown).Build().Perform();

        }

        public void PageScrollUp()
        {
            Actions actions = new Actions(commonDriver);

            actions.SendKeys(Keys.PageUp).Build().Perform();
   

        }

        public void iFramePageScroll()
        {
            Actions actions = new Actions(commonDriver);
            actions.MoveToElement(iframe);
            actions.SendKeys(Keys.PageDown).Build().Perform();

        }

       
        public void GetNextMonthValue()
        {
             PageServices.WaitForPageToCompletelyLoaded(commonDriver, 90);
           // IWebElement NextMonthValue = commonDriver.FindElement(By.CssSelector(".cal-mini-title"));
            DisplayedNextMonth = NextMonthValue.GetAttribute("innerText");
                   }

        public void GetPreviousMonthValue()
        {
             PageServices.WaitForPageToCompletelyLoaded(commonDriver, 90);
           // IWebElement CurrentMonth = commonDriver.FindElement(By.CssSelector(".cal-mini-title"));
            DisplayedMonth = CurrentMonth.GetAttribute("innerText");

        }

        public void ComparingNextPreviousValues()
        {
            Thread.Sleep(2000);
            Assert.AreNotEqual(DisplayedMonth, DisplayedNextMonth);
             PageServices.WaitForPageToCompletelyLoaded(commonDriver, 90);
            //  Assert.True(commonDriver.FindElement(By.CssSelector(".s-lc-c-erh")).Displayed);


        }

        public interface IWebElements
        {
        }
    }
}
