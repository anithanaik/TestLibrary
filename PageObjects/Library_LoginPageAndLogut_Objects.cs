using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;

namespace LibraryAutomation
{
    class Library_LoginPageAndLogut_Objects
    {

        IWebDriver commonDriver;
#pragma warning disable CS0169 // The field 'Library_LoginPageAndLogut_Objects.HM' is never used
        Library_HomePage_Objects HM;
#pragma warning restore CS0169 // The field 'Library_LoginPageAndLogut_Objects.HM' is never used


        public Library_LoginPageAndLogut_Objects(IWebDriver driver)
        {
            commonDriver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(commonDriver, TimeSpan.FromSeconds(90)));
            // HM = new Library_HomePage_Objects(driver);
        }


        //***** Login Page Object using Page Factory  *****//

        [FindsBy(How = How.Id, Using = "username")]
        public IWebElement inputUsername { get; set; }


        [FindsBy(How = How.Id, Using = "password")]
        public IWebElement inputPassword { get; set; }

        [FindsBy(How = How.Id, Using = "btnLogin")]
        public IWebElement btnLogin { get; set; }

        //*****End of Login Page Object using Page Factory  *****//

        //NCULOGIN Login Information

        public string directLoginURL = "d2l/login?noredirect=1";

        [FindsBy(How = How.Id, Using = "userName")]
        public IWebElement NCUinputUsername { get; set; }

        [FindsBy(How = How.Id, Using = "password")]
        public IWebElement NCUinputPassword { get; set; }

        [FindsBy(How = How.ClassName, Using = "d2l-button")]
        public IWebElement NCUbtnLogin { get; set; }

        [FindsBy(How = How.LinkText, Using = "Library")]

        public IWebElement NCULibrary { get; set; }



        //***** LogOut  Object using Page Factory  *****//
        [FindsBy(How = How.LinkText, Using = "Logout")]
        public IWebElement LogOutLink { get; set; }

        //*****End of Login Page Object using Page Factory  *****//



        //***** Login Page Methods *****//

        //public void LoginToLibraryApp()
        //{
        //    //HM.Clicking_Login_Link();
        //    UserNameTextField.SendKeys("rgopavarapu");
        //    PasswordTextField.SendKeys("Guest2011");
        //    LoginButton.Click();
        //}

        public void Clicking_NCUbtnLogin()
        {
            NCUbtnLogin.Click();
        }
        public void Clicking_NCULibrary()
        {
            NCULibrary.Click();
        }

        public void Clicking_LogoutLink()
        {

            LogOutLink.Click();
        }






    }
}
