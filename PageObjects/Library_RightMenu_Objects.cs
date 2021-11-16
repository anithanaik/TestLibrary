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
    class Library_RightMenu_Objects
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

        public Library_RightMenu_Objects(IWebDriver driver)
        {
            commonDriver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(commonDriver, TimeSpan.FromSeconds(90)));
        }

        //***** Home Page Object using Page Factory   *****//

        /*****Left Menu Objects *****/

        [FindsBy(How = How.CssSelector, Using = "#divSpringShareAskALibrarian .panel-title")]
        public IWebElement SubmitAQuestion { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#divSpringShareAskALibrarian .btn-sschat")]
        public IWebElement AskALibrarian { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#divEmailLibrary strong")]
        public IWebElement TextEmailUs { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#divEmailLibrary")]
        public IWebElement TextEmailUsContent { get; set; }



        [FindsBy(How = How.LinkText, Using = "Email us")]
        public IWebElement LinkEmailUs { get; set; }


        [FindsBy(How = How.CssSelector, Using = "#divSpringShareChat strong")]
        public IWebElement TextChatWithALibrarian { get; set; }


        [FindsBy(How = How.LinkText, Using = "Chat with Us!")]
        public IWebElement btnChatWithUs { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#divCallLibrary")]
        public IWebElement TextCallUs { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#divTextLibrary")]
        public IWebElement TextUs { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".table-bordered tr")]
        public IWebElement TableHeaders { get; set; }


        [FindsBy(How = How.CssSelector, Using = "[class='col-md-3']:nth-of-type(4) tbody")]
        public IWebElement TableContent { get; set; }

        /*****Start Of  Right Menu Methods *****/

        public void VerifyingTableContent()
        {

            String TableContentValue = TableContent.GetAttribute("innerText").Replace("\n", "").Replace("\r", " ").Replace("\t", "");

            Assert.AreEqual("Mon.8am - 9pm Tue.8am - 9pm Wed.8am - 9pm Thurs.8am - 9pm Fri.8am - 5pm Sun.10am - 7pm", TableContentValue);

        }

        public void VerifyingTableHeaders()
        {
            String TableHeadersValue = TableHeaders.GetAttribute("innerText").Replace("\n", "").Replace("\r", " ").Replace("\t", "");
            Assert.AreEqual("Days  Hours (AZ time)", TableHeadersValue);
        }

        public void VerifyingTextUnderTextUs()

        {
            string TextUsValue = TextUs.GetAttribute("innerText").Replace("\n", "").Replace("\r", " ").Replace("\t", "");

            Console.WriteLine(TextUsValue);


           // Assert.IsTrue(TextUsValue.Contains("Text Us Text (928) 550-6552 to get live help on your mobile phone."));

            Assert.AreEqual(" Text Us Text (928) 550-6552 to get live help on your mobile phone.", TextUsValue);

        }
        public void VerifyingTextUnderCallUs()

        {
            string TextCallUsValue= TextCallUs.GetAttribute("innerText");
            Console.WriteLine(TextCallUsValue);

          //  Assert.IsTrue(TextCallUsValue.Contains("Phone: 888-628-1569"));
            Assert.IsTrue(TextCallUsValue.Contains("Phone: 888-628-1569"));

        }

        public void Clicking_btnChatWithUs()
        {
            btnChatWithUs.Click();
        }

                public void VerifyingTextChatWithALibrarian()
        {
            String ChatWithALibrarianValue = TextChatWithALibrarian.GetAttribute("innerText");
            Assert.AreEqual("Chat with a Librarian", ChatWithALibrarianValue);
        }


        public void VerifyingTextEmailUsContent()
        {
            String TextEmailUsContentValue = TextEmailUsContent.GetAttribute("innerText");
            Console.WriteLine(TextEmailUsContentValue);

            Assert.IsTrue(TextEmailUsContentValue.Contains("Email us with your question."));
        }


        public void Clicking_AskALibrarian()
        {
            AskALibrarian.Click();
        }

        public void VerifyingSubmitAQuestionText()
        {
            String TextSubmitAQuestionValue = SubmitAQuestion.GetAttribute("innerText");
            Console.WriteLine(TextSubmitAQuestionValue);
            //Thread.Sleep(5000);
            PageServices.WaitForPageToCompletelyLoaded(commonDriver, 90);
            Assert.AreEqual("Submit a question", TextSubmitAQuestionValue);

        }


        public void VerifyingEmailUsText()
        {
          string  TextEmailUsValue = TextEmailUs.GetAttribute("innerText");
            Assert.AreEqual("Email Us", TextEmailUsValue);
        }


        public void VerifyingEmailIDTextLink()
        {
            // LinkEmailUs.Click();

            String EmailIDTextValue = LinkEmailUs.GetAttribute("href");
            Console.WriteLine(EmailIDTextValue);

            Assert.AreEqual("mailto:library@ncu.edu", EmailIDTextValue);

        }





    }
}


















