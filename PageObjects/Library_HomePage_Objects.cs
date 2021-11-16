using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Interactions;
//using FindsByAttribute = SeleniumExtras.PageObjects.FindsByAttribute;
//using How = SeleniumExtras;
//using OpenQA.Selenium.Support.PageObjects;


namespace LibraryAutomation
{



    public class Library_HomePage_Objects
    {

        IWebDriver commonDriver;
        string EnterTextInSearchField = "Test";

        public Library_HomePage_Objects(IWebDriver driver)
        {
            commonDriver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(commonDriver, TimeSpan.FromSeconds(90)));
        }

        //***** Home Page Object using Page Factory  *****//

        /*****Start Of Resaerch Resources Objects *****/

        [FindsBy(How = How.CssSelector, Using = "a[href ^= '#TabsId-1']")]
        public IWebElement RoadRunnerSearchTab { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a[href ^= '#TabsId-2'")]
        public IWebElement FindAResourcetab { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a[href ^= '#TabsId-3'")]
        public IWebElement LibraryGuidetab { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a[href ^= '#TabsId-4'")]
        public IWebElement AskALibrariantab { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[class='col-md-2'] [href]")]
        public IWebElement LoginLink{ get; set; }


        [FindsBy(How = How.Name, Using = "uquery")]
        public IWebElement TypeKeyWordHereTextBox { get; set; }

        [FindsBy(How = How.Id, Using = "btnSubmitHomeEBSCOhostSearch")]
        public IWebElement SubmitHomeSearch { get; set; }



        [FindsBy(How = How.Id, Using = "fulltext_checkbox_all")]
        public IWebElement FullTextCheckBox { get; set; }

        [FindsBy(How = How.Id, Using = "navheader1")]
        public IWebElement ResearchResourcesMenu { get; set; }


        [FindsBy(How = How.LinkText, Using = "A-Z Databases")]
        public IWebElement AToZ_DatabasesSubMenu { get; set; }

        [FindsBy(How = How.LinkText, Using = "Find an Article")]
        public IWebElement FindAnArticleSubmenu { get; set; }


        [FindsBy(How = How.LinkText, Using = "Find an eBook")]
        public IWebElement FindaneBookSubmenu { get; set; }


        [FindsBy(How = How.LinkText, Using = "Find a Video")]
        public IWebElement FindaVideo { get; set; }


        [FindsBy(How = How.LinkText, Using = "Find a Resource")]
        public IWebElement FindaResource { get; set; }


        [FindsBy(How = How.LinkText, Using = "Dissertation Resources")]
        public IWebElement DissertationResources { get; set; }

        [FindsBy(How = How.LinkText, Using = "Open Access Resources")]
        
        [FindsBy(How = How.LinkText, Using = "Open Access Resources")]
        public IWebElement OpenAccessResources { get; set; }

        [FindsBy(How = How.LinkText, Using = "Alumni Resources")]
        public IWebElement AlumniResources { get; set; }

        /*****----------------------End of  Resaerch Resources Objects---------------------- *****/

        /*****Start Of  Resaerch Help Objects *****/


        [FindsBy(How = How.Id, Using = "navheader2")]
        public IWebElement ResearchHelpMainMenu { get; set; }

       
        [FindsBy(How = How.LinkText, Using = "Learn the Library")]
             public IWebElement LearntheLibrary { get; set; }

        [FindsBy(How = How.LinkText, Using = "Library Guides")]
        public IWebElement LibraryGuides { get; set; }

       
        [FindsBy(How = How.LinkText, Using = "Research Process")]
        public IWebElement ResearchProcess { get; set; }


        
        [FindsBy(How = How.LinkText, Using = "Information Literacy Tutorial")]
        public IWebElement InformationLiteracyTutorial { get; set; }

        
        [FindsBy(How = How.LinkText, Using = "Ask a Librarian")]
        public IWebElement AskaLibrarian { get; set; }

        /*****----------------------End of  Resaerch Help Objects---------------------- *****/

        /*****Start Of  Services Objects *****/


        [FindsBy(How = How.Id, Using = "navheader3")]
        public IWebElement ServicesMainMenu { get; set; }

        [FindsBy(How = How.LinkText, Using = "Ask a Librarian")]
        public IWebElement AskALibrarian_Services { get; set; }

       [FindsBy(How = How.LinkText, Using = "Research Consultations")]
        public IWebElement ResearchConsultations { get; set; }

        [FindsBy(How = How.LinkText, Using = "Interlibrary Loan")]
        public IWebElement InterlibraryLoan { get; set; }

        [FindsBy(How = How.LinkText, Using = "Library Events")]
        public IWebElement LibraryEvents { get; set; }

        [FindsBy(How = How.LinkText, Using = "Library Disability Services")]
        public IWebElement LibraryDisabilityServices { get; set; }

        /*****----------------------End of  Services Objects---------------------- *****/



        /*****Start Of  About Us Objects *****/


        [FindsBy(How = How.Id, Using = "navheader4")]
        public IWebElement AboutUs { get; set; }

        /*****----------------------End of  Resaerch Help Objects---------------------- *****/


        /*****Start Of  A-Z Databases Objects *****/
        [FindsBy(How = How.Id, Using = "navheader5")]
        public IWebElement AToZDatabasesMainMenu { get; set; }

        /*****----------------------End of  A-Z Databases Objects---------------------- *****/





        //***** Home Page Methods *****//


        /*****Start Of Resaerch Resources Methods *****/
        public void ClickOnFindAResourceTab()
        {
            FindAResourcetab.Click();

        }

        public void ClickOnRoadrunnerSearchTab()
        {
            RoadRunnerSearchTab.Click();
        }

        public void Click_Library_Guide_tab()
        {
            LibraryGuidetab.Click();
        }

        public void Click_Ask_A_Librarian_tab()
        {
            AskALibrariantab.Click();
        }

        public void Clicking_Login_Link()
        {
            LoginLink.Click();
        }


        public void Enter_Text_In_TypeKeyWordHereTextBox()
        {
            TypeKeyWordHereTextBox.SendKeys(EnterTextInSearchField);

        }


        public void Click_Submit_Button_On_HomePage()
        {
            SubmitHomeSearch.Click();

        }

        public void CheckBox_Check()
        {
            FullTextCheckBox.Click();

        }
        public void Click_Resource_Research_Menu()
        {
            Actions action = new Actions(commonDriver);
            action.MoveToElement(ResearchResourcesMenu).Perform();
            
          //  ResearchResourcesMenu.Click();

        }
        
              public void Click_AToZ_Databases_SubMenu()
        {
            AToZ_DatabasesSubMenu.Click();
        }

             public void Clicking_Find_An_Article_Submenu()
        {
            FindAnArticleSubmenu.Click();
        }



        public void Clicking_Find_an_eBookSubmenuSubmenu()
        {
            FindaneBookSubmenu.Click();
        }

        public void Clicking_Find_a_Video_Submenu()
        {
            FindaVideo.Click();
        }
        public void Clicking_Find_A_Resource_Submenu()
        {
            FindaResource.Click();
        }
        public void Clicking_Dissertation_Resources_Submenu()
        {
           DissertationResources.Click();
        }
        public void Clicking_Open_Access_Resources_Submenu()
        {
            OpenAccessResources.Click();
        }
                
              public void Clicking_AlumniResources_Submenu()
        {
            AlumniResources.Click();
        }
        /*****End Of  Resaerch Resources  Methods *****/

        /*****Start Of   Resources Help Methods *****/

        public void Clicking_Research_Help_MainMenu()
        {
            Actions action = new Actions(commonDriver);
            action.MoveToElement(ResearchHelpMainMenu).Perform();

         }
        
        public void Clicking_Learn_the_Library_Submenu()
        {
            LearntheLibrary.Click();
        }


        public void Clicking_Library_Guides_Submenu()
        {
            LibraryGuides.Click();
        }
        public void Clicking_Research_Process_Submenu()
        {
            ResearchProcess.Click();
        }
        public void Clicking_Information_LiteracyTutorial_Submenu()
        {
            InformationLiteracyTutorial.Click();
        }
        public void Clicking_AskaLibrarian_Submenu()
        {
            AskaLibrarian.Click();
        }

        /*****End Of   Resources Help Methods *****/





        /*****Start Of  Services Methods *****/

        public void Clicking_Services_MainMenu()
        {
            Actions action = new Actions(commonDriver);
            action.MoveToElement(ServicesMainMenu).Perform();

        }

        public void Clicking_AskaLibrarian_Services_Submenu()
        {
           AskALibrarian_Services .Click();
        }

        public void Clicking_ResearchConsultations_Submenu()
        {
            ResearchConsultations.Click();
        }
        public void Clicking_InterlibraryLoan_Submenu()
        {
            InterlibraryLoan.Click();
        }
        public void Clicking_LibraryEvents_Submenu()
        {
            LibraryEvents.Click();
        }
        public void Clicking_LibraryDisabilityServices_Submenu()
        {
            LibraryDisabilityServices.Click();
        }
        /*****End Of Services  Methods *****/


        /*****Start Of  About Us Methods *****/
              public void Clicking_AboutUsMainMenu()
        {
            AboutUs.Click();
        }

        /*****End Of About Us  Methods *****/

            
        /*****Start Of  A-Z Databases Main menu Methods *****/
        public void Clicking_AToZDatabasesMainMenu_Submenu()
        {
            AToZDatabasesMainMenu.Click();
        }


        /*****End Of A-Z Databases Main menu  Methods *****/



    }

}
