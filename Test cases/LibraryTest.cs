using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using AventStack.ExtentReports;
using System.IO;
using AventStack.ExtentReports.Reporter;
using System.Diagnostics;
using NUnit.Framework.Interfaces;
using AventStack.ExtentReports.Reporter.Configuration;
using LibraryAutomation.PageObjects;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace LibraryAutomation
{
    public class LibraryTest
    {
        Library_LoginPageAndLogut_Objects LoginToLibrary;
        Library_FooterSection_Objects FooterSectionObjects;
        Library_LeftMenu_Objects LM;
        Library_MiddleMenu_Objects MiddleMenuObjects;
        Library_RightMenu_Objects RightMenuObjects;
        Library_HomePage_Objects HM;       
        string browser;
        string expectedError;
        private ExtentReports extent;
        ExtentHtmlReporter htmlReporter;
        ExtentTest test;
        public IWebDriver driver;
        public TestContext TestContext { get; set; }
        public string LibraryURL;
        public string ncuOneURL;
        public string environmentSetup;
        static string errorPath;
        static string reportPath;
        static string userName;
        static string password;
        string PrivacyError = "Privacy error";

        public LibraryTest()
        {
            browser = TestContext.Parameters.Get("Browser").ToString();
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            environmentSetup = TestContext.Parameters.Get("Environment").ToString();
            errorPath = PageServices.SetReportAndLogPath().Item1;
            reportPath = PageServices.SetReportAndLogPath().Item2;           
            Console.WriteLine(reportPath);
            //htmlReporter = new ExtentHtmlReporter(reportPath);
            htmlReporter = new ExtentHtmlReporter(reportPath + environmentSetup + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + "\\");
            htmlReporter.Config.Theme = Theme.Dark;
            htmlReporter.Config.DocumentTitle = "LibraryReport";
            htmlReporter.Config.ReportName = "LibraryReport";
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Machine Name", Environment.MachineName.ToString());
            extent.AddSystemInfo("Platform Name", Environment.OSVersion.Platform.ToString());
            extent.AddSystemInfo("User Name", Environment.UserName.ToString());
            extent.AddSystemInfo("Browser", browser.ToString());
            //environmentSetup = TestContext.Parameters.Get("Environment").ToString();
            ////errorPath = PageServices.GetProjectPath() + TestContext.Parameters.Get("ErrorLogFilePath").Trim();
            ////reportPath = PageServices.GetProjectPath() + TestContext.Parameters.Get("ReportPath").Trim();
            //errorPath = PageServices.SetReportAndLogPath().Item1;
            //reportPath = PageServices.SetReportAndLogPath().Item2;
            //Console.WriteLine(errorPath);
            //Console.WriteLine(reportPath);
            //// htmlReporter = new ExtentHtmlReporter(reportPath);
            //htmlReporter = new ExtentHtmlReporter(reportPath + environmentSetup + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + "\\");
            //htmlReporter.Config.Theme = Theme.Dark;
            //htmlReporter.Config.DocumentTitle = "LibraryReport";
            //htmlReporter.Config.ReportName = "LibraryReport";
            //extent = new ExtentReports();
            //extent.AttachReporter(htmlReporter);
            //extent.AddSystemInfo("Machine Name", Environment.MachineName.ToString());
            //extent.AddSystemInfo("Platform Name", Environment.OSVersion.Platform.ToString());
            //extent.AddSystemInfo("User Name", Environment.UserName.ToString());
            //extent.AddSystemInfo("Browser", browser.ToString());
        }

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("********************** Test Cases is Running against **********: [{0}] ", environmentSetup);
            Console.WriteLine("********************** URL Accessed is ************** : [{0}] ", ncuOneURL);
            Console.WriteLine("********************** You Can check Error from this Path *********** :  [{0}] ", errorPath);
            Console.WriteLine("********************** You Can check Report from this Path *********** :  [{0}] ", reportPath);

            driver = BrowserType.SelectBrowser(this.browser);
            driver.Manage().Window.Maximize();
            LoginToLibrary = new Library_LoginPageAndLogut_Objects(driver);
            FooterSectionObjects = new Library_FooterSection_Objects(driver);
            LM = new Library_LeftMenu_Objects(driver);
            MiddleMenuObjects = new Library_MiddleMenu_Objects(driver);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            LibraryURL = TestContext.Parameters.Get("LibraryAppUrl").ToString();
            userName = TestContext.Parameters.Get("appUserName").ToString();
            password = TestContext.Parameters.Get("appPassword").ToString();
            ncuOneURL = TestContext.Parameters.Get("ncuOneAppUrl").ToString();
            RightMenuObjects = new Library_RightMenu_Objects(driver);
            HM = new Library_HomePage_Objects(driver);
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            Console.WriteLine(TestContext.CurrentContext.Test.Name + " is Running for " + environmentSetup + " Environment");
            driver.Navigate().GoToUrl(LibraryURL);
            LoginToLibrary.inputUsername.SendKeys(userName);
            LoginToLibrary.inputPassword.SendKeys(password);
            LoginToLibrary.btnLogin.Click();
                       

        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            test.Log(Status.Info);
            extent.Flush();
        }

        [Test, Category("FooterSectionTest")]
        //[Order(1)]
        public void FooterSection()
        {
            try
            {

                Console.WriteLine("Start Footer Section");

               //test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
               
                FooterSectionObjects.Clicking_PrivacyPolicyAndConsumerInformation();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    Thread.Sleep(5000);
                    String PageUrlPrivacyPolicyAndConsumerInformation = driver.Url;
                    String PageTitlePrivacyPolicyandConsumerInformation = driver.Title;
                    Assert.AreEqual("https://www.ncu.edu/privacy", PageUrlPrivacyPolicyAndConsumerInformation);
                    Assert.AreEqual("Privacy Statement | Northcentral University", PageTitlePrivacyPolicyandConsumerInformation);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine(PageUrlPrivacyPolicyAndConsumerInformation, PageTitlePrivacyPolicyandConsumerInformation);

                //Footer Section: Contact Us
                    FooterSectionObjects.Clicking_ContactUs();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    Thread.Sleep(5000);
                    String PageUrlContactUs = driver.Url;
                    String PageTitleContactUs = driver.Title;
                    Assert.AreEqual("https://ncu.libguides.com/about", PageUrlContactUs);
                    Assert.AreEqual("Home - About Us - LibGuides at Northcentral University", PageTitleContactUs);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine(PageUrlContactUs, PageTitleContactUs);

                //Footer Section: Library Sitemap
                    FooterSectionObjects.Clicking_LibrarySiteMap();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    Thread.Sleep(5000);
                    String PageUrlLibrarySiteMap = driver.Url;
                    String PageTitleLibrarySiteMap = driver.Title;
                    if (environmentSetup=="QA")
                    {
                        Assert.AreEqual("https://library.qa1.ncu.edu/SiteMap/GetSiteMap", PageUrlLibrarySiteMap);
                        Assert.AreEqual("Northcentral Library", PageTitleLibrarySiteMap);
                    }
                    if (environmentSetup == "QA2")
                    {
                        Assert.AreEqual("https://library.qa2.ncu.edu/SiteMap/GetSiteMap", PageUrlLibrarySiteMap);
                        Assert.AreEqual("Northcentral Library", PageTitleLibrarySiteMap);
                }
                    if (environmentSetup == "UAT")
                    {
                        Assert.AreEqual("https://library.uat.ncu.edu/SiteMap/GetSiteMap", PageUrlLibrarySiteMap);
                        Assert.AreEqual("Northcentral Library", PageTitleLibrarySiteMap);
                }
                    driver.Navigate().GoToUrl(LibraryURL);
                    Console.WriteLine(PageUrlLibrarySiteMap);

                //Footer Section: Leave Feedback
                    FooterSectionObjects.Clicking_LeaveFeedBack();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    Thread.Sleep(5000);
                    String PageUrlLeaveFeedBack = driver.Url;
                    String PageTitleLeaveFeedback = driver.Title;
                    Assert.AreEqual("https://ncu.libwizard.com/f/libraryfeedback", PageUrlLeaveFeedBack);
                    Assert.AreEqual("Library Feedback", PageTitleLeaveFeedback);
                    Console.WriteLine(PageUrlLeaveFeedBack);
                    driver.Navigate().GoToUrl(LibraryURL);

                //Footer Section: Verify the verbiage in the footer. section. 
                    Thread.Sleep(1000);
                    FooterSectionObjects.VerifyingTextDisplayedinFooterSection();
                    Console.WriteLine("verifying Verbiage in Footer");

                //Footer Section: Verify the Facebook icon in the footer section. 
                FooterSectionObjects.Clicking_FacebookIcon();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                Thread.Sleep(5000);
                String PageUrlFacebook = driver.Url;
                Assert.AreEqual("https://www.facebook.com/NorthcentralU", PageUrlFacebook);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine(PageUrlFacebook);


                //Footer Section: Verify the Twitter icon in the footer section. 
                FooterSectionObjects.Clicking_TwitterIcon();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                Thread.Sleep(5000);
                String PageUrlTwitter = driver.Url;
                String PageTitleTwitter = driver.Title;
                Assert.AreEqual("https://twitter.com/NorthcentralU", PageUrlTwitter);
                Assert.AreEqual("Northcentral U (@NorthcentralU) / Twitter", PageTitleTwitter);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine(PageUrlTwitter, PageTitleTwitter);

                //Footer Section: Verify the YouTube icon in the footer section. 
                FooterSectionObjects.Clicking_YouTubeIcon();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                Thread.Sleep(5000);
                String PageUrlYouTube = driver.Url;
                String PageTitleYouTube = driver.Title;
                Assert.AreEqual("https://www.youtube.com/user/NorthcentralUniv", PageUrlYouTube);
                Assert.AreEqual("Northcentral University - YouTube", PageTitleYouTube);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine(PageUrlYouTube, PageTitleYouTube);

                Console.WriteLine("End Footer Section");

            }
            catch (Exception ex)
            {
                Console.WriteLine("RESULT: FAILED - CHECK THE LOG FILE");
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                string path = errorPath + TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".png";
                ss.SaveAsFile(path);
                TestContext.AddTestAttachment(path);
                test.Fail(ex.StackTrace);
                ErrorLog.SaveExeptionToLog(ex, TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup, errorPath + TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xml");
                //test.Fail(ex.StackTrace);
                //ErrorLog.SaveExeptionToLog(ex, TestContext.CurrentContext.Test.Name + "_" + environmentSetup, errorPath + TestContext.CurrentContext.Test.Name + "_" + environmentSetup + ".xml");
            }
        }

        [Test, Category("LeftMenuTest")]

        public void LeftMenu()
        {
            try
            {
                // test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                Console.WriteLine("Start LeftMenuTest");

                //Left Menu: Find Answers to Frequently Asked Questions
                LM.Clicking_FindAnswerstoFrequentlyAskedQuestionsLink();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageUrlFrequentlyAskedQuestions = driver.Url;
                String PageTitleFrequentlyAskedQuestions = driver.Title;
                Assert.AreEqual("https://ncu.libanswers.com/", PageUrlFrequentlyAskedQuestions);
                Assert.AreEqual("NCU Library - Ask Us!", PageTitleFrequentlyAskedQuestions);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("FAQ Success");


                //Left Menu: Access RefWorks
                LM.Clicking_AccessRefWorksLink();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageUrlAccessRefWorksLink = driver.Url;
                String PageTitleAccessRefWorksLink = driver.Title;
                Assert.AreEqual("https://ncu.libguides.com/organize_research/refworks", PageUrlAccessRefWorksLink);
                Assert.AreEqual("Welcome to RefWorks! - RefWorks - LibGuides at Northcentral University", PageTitleAccessRefWorksLink);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("RefWorks Success");

                //Left Menu: Request Interlibrary Loan Items
                LM.Clicking_RequestInterlibraryLoanItemsLink();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageUrlRequestInterlibraryLoanItemsLink = driver.Url;
                String PageTitleRequestInterLibraryLoanItemsLink = driver.Title;
                Assert.AreEqual("https://ncu.libguides.com/interlibrary_loan", PageUrlRequestInterlibraryLoanItemsLink);
                Assert.AreEqual("Home - Interlibrary Loan - LibGuides at Northcentral University", PageTitleRequestInterLibraryLoanItemsLink);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Interlibrary Loan Success");

                //Left Menu: Test My Library Skills
                LM.Clicking_TestMyLibrarySkillsLink();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                System.Threading.Thread.Sleep(5000);
                String PageUrlTestMyLibrarySkillsLink = driver.Url;
                String PageTitleTestMyLibrarySkillsLink = driver.Title;
                Assert.AreEqual("https://ncu.libwizard.com/f/iltutorial", PageUrlTestMyLibrarySkillsLink);
                Assert.AreEqual("Information Literacy Tutorial", PageTitleTestMyLibrarySkillsLink);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Test My Library Skills Success");

                //Left Menu: Request In-depth Research Support
                LM.Clicking_RequestIndepthResearchSupportLink();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageUrlRequestIndepthResearchSupportLink = driver.Url;
                String PageTitleRequestIndepthResearchSupportLink = driver.Title;
                Assert.AreEqual("https://ncu.libguides.com/researchconsultations", PageUrlRequestIndepthResearchSupportLink);
                Assert.AreEqual("Home - Research Consultations - LibGuides at Northcentral University", PageTitleRequestIndepthResearchSupportLink);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Request In-depth Research Support Success");

                //Left Menu: AccessAlumniResourcesLink
                LM.Clicking_AccessAlumniResourcesLink();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageUrlAccessAlumniResourcesLink = driver.Url;
                String PageTitleAccessAlumniResourcesLink = driver.Title;
                Assert.AreEqual("https://ncu.libguides.com/alumni", PageUrlAccessAlumniResourcesLink);
                Assert.AreEqual("Home - Alumni Library Resources and Services - LibGuides at Northcentral University", PageTitleAccessAlumniResourcesLink);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Access Alumni Resources Success");

                //Left Menu: Access JFKU Resources
                LM.Clicking_AccessJFKUResourcesLink();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageUrlAccessJFKUResourcesLink = driver.Url;
                String PageTitleAccessJFKUResourcesLink = driver.Title;
                Assert.AreEqual("https://ncu.libguides.com/learn/jfku", PageUrlAccessJFKUResourcesLink);
                Assert.AreEqual("JFKU - Learn the Library - LibGuides at Northcentral University", PageTitleAccessJFKUResourcesLink);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Access JFKU Resources Success");

                //Left Menu: Clicking on Next Month icon in Calendar
                //Thread.Sleep(500);
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                LM.PageScroll();
                LM.SwichingToiframe();
                LM.Clicking_NextmonthButton();
                LM.GetNextMonthValue();
                LM.Clicking_PreviousmonthButton();
                LM.GetPreviousMonthValue();
                LM.ComparingNextPreviousValues();
                Console.WriteLine("Next Month Icon Success");

                //Left Menu: Clicking on Previous Month icon in Calendar
               // LM.SwichingToiframe();
                LM.Clicking_PreviousmonthButton();
                LM.GetPreviousMonthValue();
                LM.Clicking_NextmonthButton();
                LM.GetNextMonthValue();
                LM.ComparingNextPreviousValues();
                Console.WriteLine("Previous Month Icon Success");

                //Left Menu: Clicking on Event date in Calendar and Clicking on Event and Comparing Event data in separate Tab
               LM.Clicking_NextmonthButton();
               // LM.SwichingToiframe();
                bool elementDisplayed = LM.EventLinkOnCalendar.Displayed;
                if (elementDisplayed)
                {
                    LM.Clicking_EventLinkOnCalendar();
                    Thread.Sleep(3000);
                    Console.WriteLine("Clicked Event");
                    //Thread.Sleep(500);
                    PageServices.WaitForPageToCompletelyLoaded(driver, 200);

                    LM.StoreEventNameFromCalendar();
                    LM.Clicking_EventNameFromEvenSection();
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    // Thread.Sleep(500);
                    PageServices.WaitForPageToCompletelyLoaded(driver, 100);
                    LM.ComparingEventTextInEventSectionAndTab();
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                }

                //Left Menu: Clicking on ShowAll Link in Calendar >> Event section
                driver.SwitchTo().Window(driver.WindowHandles.First());
                driver.Navigate().Refresh();


                LM.SwichingToiframe();
               // LM.Clicking_NextmonthButton();
                Console.WriteLine("Switched to Iframe");
                Thread.Sleep(5000);
                PageServices.WaitForPageToCompletelyLoaded(driver, 200);
                //Thread.Sleep(5000);
                IList<IWebElement> numOfLi = driver.FindElements(By.XPath("//div[@id='calevent-mini-list']//a[@class='calevent-mini-a']"));
                    Console.WriteLine(numOfLi.Count);

                    if (numOfLi.Count >= 5)
                    {
                        LM.Clicking_ShowAllLink();
                        Thread.Sleep(5000);
                        Console.WriteLine("Clicked Show all");

                        driver.SwitchTo().Window(driver.WindowHandles.Last());
                        driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                        driver.SwitchTo().Window(driver.WindowHandles.First());

                    //Thread.Sleep(10000);
                    PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                   // LoginToLibrary.Clicking_LogoutLink();
                    }
                    else
                    {
                        driver.SwitchTo().Window(driver.WindowHandles.First());
                        Thread.Sleep(2000);
                       // LoginToLibrary.Clicking_LogoutLink();
                        Console.WriteLine("Clicked Logout when show all not visible");

                    }

                        Console.WriteLine("End LeftMenuTest");

                }
                catch (Exception ex)
                {

                Console.WriteLine("RESULT: FAILED - CHECK THE LOG FILE");
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                string path = errorPath + TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".png";
                ss.SaveAsFile(path);
                TestContext.AddTestAttachment(path);
                test.Fail(ex.StackTrace);
                ErrorLog.SaveExeptionToLog(ex, TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup, errorPath + TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xml");
                //test.Fail(ex.StackTrace);
                //ErrorLog.SaveExeptionToLog(ex, TestContext.CurrentContext.Test.Name + "_" + environmentSetup, errorPath + TestContext.CurrentContext.Test.Name + "_" + environmentSetup + ".xml");
            }
            }

        [Test, Category("RightMenuTest")]
        public void RightMenu()
        {
            try
            {
                // test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
                Console.WriteLine("Start RightMenuTest");

                //Right Menu: Submit Question
                    RightMenuObjects.VerifyingSubmitAQuestionText();

                //Right Menu: Ask a Librarian
                    RightMenuObjects.Clicking_AskALibrarian();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    Thread.Sleep(5000);
                    String PageUrlAskALibrarian = driver.Url;
                    String PageTitleAskALibrarian = driver.Title;
                    Assert.AreEqual("https://ncu.libanswers.com/", PageUrlAskALibrarian);
                    Assert.AreEqual("NCU Library - Ask Us!", PageTitleAskALibrarian);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Ask A Librarian Success");

                //Right Menu: EmailUsText
                RightMenuObjects.VerifyingEmailUsText();

                //Right Menu: TextEmailUsContent
                    RightMenuObjects.VerifyingTextEmailUsContent();

                //Right Menu: Verifying EmailID Text Link
                    RightMenuObjects.VerifyingEmailIDTextLink();

                //Right Menu: Verifying ChatWithALibrarian Text
                    RightMenuObjects.VerifyingTextChatWithALibrarian();

                //Right Menu: btn ChatWithUs
                    RightMenuObjects.Clicking_btnChatWithUs();
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    Thread.Sleep(5000);
                    String PageUrlChatWithUs = driver.Url;

                

                if (environmentSetup == "QA")
                    {
                        Assert.AreEqual("https://library.qa1.ncu.edu/SpringShare/LibAnswersChat", PageUrlChatWithUs);
                    }
                    if (environmentSetup == "QA2")
                    {
                        Assert.AreEqual("https://library.qa2.ncu.edu/SpringShare/LibAnswersChat", PageUrlChatWithUs);
                    }   
                    if (environmentSetup == "UAT")
                    {
                        Assert.AreEqual("https://library.uat1.ncu.edu/SpringShare/LibAnswersChat", PageUrlChatWithUs);
                    }
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Chat With Us - Success");

                //Right Menu: Verifying Text Under CallUs
                    RightMenuObjects.VerifyingTextUnderCallUs();

                //Right Menu: Verifying Text Under TextUs
                    RightMenuObjects.VerifyingTextUnderTextUs();

                //Right Menu: Verifying Text Under TextUs
                    RightMenuObjects.VerifyingTextUnderTextUs();

                //Right Menu: Verifying Table Header
                    RightMenuObjects.VerifyingTableHeaders();

                //Right Menu: Verifying Text Table Content
                    RightMenuObjects.VerifyingTableContent();


                Console.WriteLine("End RightMenuTest");

            }
            catch (Exception ex)
            {
                Console.WriteLine("RESULT: FAILED - CHECK THE LOG FILE");
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                string path = errorPath + TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".png";
                ss.SaveAsFile(path);
                TestContext.AddTestAttachment(path);
                test.Fail(ex.StackTrace);
                ErrorLog.SaveExeptionToLog(ex, TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup, errorPath + TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xml");
                //test.Fail(ex.StackTrace);
                //ErrorLog.SaveExeptionToLog(ex, TestContext.CurrentContext.Test.Name + "_" + environmentSetup, errorPath + TestContext.CurrentContext.Test.Name + "_" + environmentSetup + ".xml");
            }
        }

        [Test, Category("HomePageTest")]
        public void Clicking_TopMenu()
        {
            
            try
            {

                Console.WriteLine("Start HomePageTest");

                //Research Resources: A-Z Databases
                    HM.Click_Resource_Research_Menu();
                    HM.Click_AToZ_Databases_SubMenu();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    String PageUrlAToZ = driver.Url;
                    String PageTitleAToZ = driver.Title;
                    Assert.AreEqual("https://ncu.libguides.com/az.php", PageUrlAToZ);
                    Assert.AreEqual("A-Z Databases", PageTitleAToZ);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("A-Z Databases Success");

                //Research Resources: 'Find An Article' Submenu

                    HM.Click_Resource_Research_Menu();
                    HM.Clicking_Find_An_Article_Submenu();
                    Thread.Sleep(5000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    String PageUrlFindAnArticle = driver.Url;
                    String PageTitleFindAnArticle = driver.Title;
                    Assert.AreEqual("https://ncu.libguides.com/az.php?t=21597", PageUrlFindAnArticle);
                    Assert.AreEqual("A-Z Databases: Journal Articles", PageTitleFindAnArticle);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Find an Article Success");

                //Research Resources: 'Find An Ebook' submenu

                    HM.Click_Resource_Research_Menu();
                    HM.Clicking_Find_an_eBookSubmenuSubmenu();
                    Thread.Sleep(5000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    string PageUrlFindAneBook = driver.Url;
                    String PageTitleFindAneBook = driver.Title;
                    Assert.AreEqual("https://ncu.libguides.com/az.php?t=21594", PageUrlFindAneBook);
                    Assert.AreEqual("A-Z Databases: e-books", PageTitleFindAneBook);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Find An eBook Success");

                //Research Resources: 'Find a Video' submenu

                    HM.Click_Resource_Research_Menu();
                    HM.Clicking_Find_a_Video_Submenu();
                    Thread.Sleep(5000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    string PageUrlFindAVideo = driver.Url;
                    String PageTitleFindAVideo = driver.Title;
                    Assert.AreEqual("https://ncu.libguides.com/az.php?t=21599", PageUrlFindAVideo);
                    Assert.AreEqual("A-Z Databases: Videos", PageTitleFindAVideo);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Find A Video Success");

                //Research Resources: 'Find a Resource' submenu

                    HM.Click_Resource_Research_Menu();
                    HM.Clicking_Find_A_Resource_Submenu();
                    Thread.Sleep(5000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    String PageUrlFindAResource = driver.Url;
                    String PageTitleFindAResource = driver.Title;
                    Assert.AreEqual("http://xt6nc6eu9q.search.serialssolutions.com/ejp/?libHash=XT6NC6EU9Q#/?language=en-US&titleType=ALL", PageUrlFindAResource);
                    Assert.AreEqual("NCU Library - Find a Resource", PageTitleFindAResource);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("NCU Library - Find a Resource Success");

                //Research Resources: 'Dissertation Resources' submenu

                    HM.Click_Resource_Research_Menu();
                    HM.Clicking_Dissertation_Resources_Submenu();
                    Thread.Sleep(5000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    String PageUrlDissertationResources = driver.Url;
                    String PageTitleDissertationResource = driver.Title;
                    Assert.AreEqual("https://ncu.libguides.com/az.php?t=21596", PageUrlDissertationResources);
                    Assert.AreEqual("A-Z Databases: Dissertations & Theses", PageTitleDissertationResource);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Dissertation Resource Success");

                //Research Resources: 'Open Access Resources' submenu

                    HM.Click_Resource_Research_Menu();
                    HM.Clicking_Open_Access_Resources_Submenu();
                    Thread.Sleep(5000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    String PageUrlOpenAccessResource = driver.Url;
                    String PageTitleOpenAccessResource = driver.Title;
                    Assert.AreEqual("https://ncu.libguides.com/oar", PageUrlOpenAccessResource);
                    Assert.AreEqual("OAR Home - Open Access Resources - LibGuides at Northcentral University", PageTitleOpenAccessResource);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Open Access Resources Success");

                //Research Resources: 'Alumni Resources Submenu  *****/

                    HM.Click_Resource_Research_Menu();
                    HM.Clicking_AlumniResources_Submenu();
                    Thread.Sleep(5000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    String PageUrlAlumniResources = driver.Url;
                    String PageTitleAlumniResources = driver.Title;
                    Assert.AreEqual("https://ncu.libguides.com/alumni", PageUrlAlumniResources);
                    Assert.AreEqual("Home - Alumni Library Resources and Services - LibGuides at Northcentral University", PageTitleAlumniResources);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Alumni Resources Success");

                //Research Help: Learn the Library

                    HM.Clicking_Research_Help_MainMenu();
                    HM.Clicking_Learn_the_Library_Submenu();
                    Thread.Sleep(5000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    String PageUrlLearnTheLibrary = driver.Url;
                    String PageTitleLearnTheLibrary = driver.Title;
                    Assert.AreEqual("https://ncu.libguides.com/learn", PageUrlLearnTheLibrary);
                    Assert.AreEqual("Home - Learn the Library - LibGuides at Northcentral University", PageTitleLearnTheLibrary);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Learn the Library Success");

                //Research Help: Main Menu - Library Guides

                    HM.Clicking_Research_Help_MainMenu();
                    HM.Clicking_Library_Guides_Submenu();
                    Thread.Sleep(5000);
                    Thread.Sleep(1000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    String PageUrlLibraryGuides = driver.Url;
                    String PageTitleLibraryGuides = driver.Title;
                    Thread.Sleep(5000);
                    Assert.AreEqual("https://ncu.libguides.com/?b=s", PageUrlLibraryGuides);
                    Assert.AreEqual("Guides By Subject - LibGuides at Northcentral University", PageTitleLibraryGuides);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Library Guides Success");

                //Research Help: Main Menu - Research Process

                    HM.Clicking_Research_Help_MainMenu();
                    HM.Clicking_Research_Process_Submenu();
                    Thread.Sleep(5000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    String PageUrlResearchProcess = driver.Url;
                    String PageTitleResearchProcess = driver.Title;
                    Assert.AreEqual("https://ncu.libguides.com/researchprocess", PageUrlResearchProcess);
                    Assert.AreEqual("Home - Research Process - LibGuides at Northcentral University", PageTitleResearchProcess);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Research Process Success");


                //Research: Main Menu - InformationLiteracyTutorial

                    HM.Clicking_Research_Help_MainMenu();
                    HM.Clicking_Information_LiteracyTutorial_Submenu();
                    Thread.Sleep(5000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    Thread.Sleep(5000);
                    String PageUrlInformationLiteracyTutorial = driver.Url;
                    String PageTitleInformationLiteracyTutorial = driver.Title;
                    // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(500);
                    Thread.Sleep(7000);
                    Assert.AreEqual("https://ncu.libwizard.com/f/iltutorial", PageUrlInformationLiteracyTutorial);
                    Assert.AreEqual("Information Literacy Tutorial", PageTitleInformationLiteracyTutorial);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Information Literacy Tutorial Success");
                    

                //Research: Main Menu - Ask A Librarian

                    HM.Clicking_Research_Help_MainMenu();
                    HM.Clicking_AskaLibrarian_Submenu();
                    Thread.Sleep(5000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    String PageUrlAskaLibrarian = driver.Url;
                    String PageTitleAskaLibrarian = driver.Title;
                    Assert.AreEqual("https://ncu.libanswers.com/", PageUrlAskaLibrarian);
                    Assert.AreEqual("NCU Library - Ask Us!", PageTitleAskaLibrarian);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Ask a Librarian Success");

                //Services Help: Main Menu - Ask A Librarian

                    HM.Clicking_Services_MainMenu();
                    HM.Clicking_AskaLibrarian_Services_Submenu();
                    Thread.Sleep(5000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    String PageUrlAskaLibrarianServices = driver.Url;
                    String PageTitleAskaLibrarianServices = driver.Title;
                    Assert.AreEqual("https://ncu.libanswers.com/", PageUrlAskaLibrarianServices);
                    Assert.AreEqual("NCU Library - Ask Us!", PageTitleAskaLibrarianServices);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Ask a Librarian Success");

                //Services Help Main Menu - Research Consultations

                    HM.Clicking_Services_MainMenu();
                    HM.Clicking_ResearchConsultations_Submenu();
                    Thread.Sleep(5000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    String PageUrlResearchConsultations = driver.Url;
                    String PageTitleResearchConsultations = driver.Title;
                    Assert.AreEqual("https://ncu.libguides.com/researchconsultations", PageUrlResearchConsultations);
                    Assert.AreEqual("Home - Research Consultations - LibGuides at Northcentral University", PageTitleResearchConsultations);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Research Consulations Success");

                ///Services Help Main Menu - InterLibrary Loan

                    HM.Clicking_Services_MainMenu();
                    HM.Clicking_InterlibraryLoan_Submenu();
                    Thread.Sleep(5000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    String PageUrlInterlibraryLoan = driver.Url;
                    String PageTitleInterlibraryLoan = driver.Title;
                    Assert.AreEqual("https://ncu.libguides.com/interlibrary_loan", PageUrlInterlibraryLoan);
                    Assert.AreEqual("Home - Interlibrary Loan - LibGuides at Northcentral University", PageTitleInterlibraryLoan);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Interlibrary Loan Success");


                //Service Help Main Menu - Library Events

                    HM.Clicking_Services_MainMenu();
                    HM.Clicking_LibraryEvents_Submenu();
                    Thread.Sleep(5000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    String PageUrlLibraryEvents = driver.Url;
                    String PageTitleLibraryEvents = driver.Title;
                    Assert.AreEqual("https://ncu.libcal.com/calendar/workshops/?cid=2938&t=d&d=0000-00-00&cal=2938&inc=0", PageUrlLibraryEvents);
                    Assert.AreEqual("NCU Library Events Calendar - LibCal - NCU Library", PageTitleLibraryEvents);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Library Events Success");

                //Services Help: Main Menu - Library Disability Services

                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    HM.Clicking_Services_MainMenu();
                    HM.Clicking_LibraryDisabilityServices_Submenu();
                    Thread.Sleep(5000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    String PageUrlLibraryDisabilityServices = driver.Url;
                    String PageTitleLibraryDisabilityServices = driver.Title;
                    Assert.AreEqual("https://ncu.libguides.com/about/librarydisabilityservices", PageUrlLibraryDisabilityServices);
                    Assert.AreEqual("Library Disability Services - About Us - LibGuides at Northcentral University", PageTitleLibraryDisabilityServices);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("Library Disabilities Services Success");

                //About Us: Main Menu
                    
                    HM.Clicking_Services_MainMenu();
                    HM.Clicking_AboutUsMainMenu();
                    Thread.Sleep(5000);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    String PageUrlAboutUs = driver.Url;
                    String PageTitleAboutUs = driver.Title;
                    Assert.AreEqual("https://ncu.libguides.com/about", PageUrlAboutUs);
                    Assert.AreEqual("Home - About Us - LibGuides at Northcentral University", PageTitleAboutUs);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("About Us Success");


                //A To Z Databases: Main Menu

                    HM.Clicking_Services_MainMenu();
                    HM.Clicking_AToZDatabasesMainMenu_Submenu();
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    Thread.Sleep(5000);
                    String PageUrlAToZDatabasesMainMenu = driver.Url;
                    String PageTitleAToZDatabasesMainMenu = driver.Title;
                    Assert.AreEqual("https://ncu.libguides.com/az.php", PageUrlAToZDatabasesMainMenu);
                    Assert.AreEqual("A-Z Databases", PageTitleAToZDatabasesMainMenu);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("A-Z Databases Success");

                Console.WriteLine("End HomePageTest");

               
            }

            catch (Exception ex)
            {
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                string path = errorPath + TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".png";
                ss.SaveAsFile(path);
                TestContext.AddTestAttachment(path);
                test.Fail(ex.StackTrace);
                ErrorLog.SaveExeptionToLog(ex, TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup, errorPath + TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xml");
                //test.Fail(ex.StackTrace);
                //ErrorLog.SaveExeptionToLog(ex, TestContext.CurrentContext.Test.Name + "_" + environmentSetup, errorPath + TestContext.CurrentContext.Test.Name + "_" + environmentSetup + ".xml");
            }
        }

        [Test, Category("MiddleMenuTest")]
        public void MiddleMenu()
        {
            try
            {
                //test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                Console.WriteLine("Start MiddleMenuTest");

                //Carousel Section
                //Carousel Right and Left Arrow clicking
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_RightArrow();
                MiddleMenuObjects.Clicking_LeftArrow();
                Console.WriteLine("Clicked Left and Right");
                //Clicking on First Carousel
                driver.Navigate().Refresh();
                Thread.Sleep(3000);
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_FirstCarousel();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageUrlFirstCarousel = driver.Url;
                String PageTitleFirstCarousel = driver.Title;
                Assert.AreEqual("https://ncu.libcal.com/calendar/workshops/?cid=2938&t=d&d=0000-00-00&cal=2938&ct=47681&inc=0", PageUrlFirstCarousel);
                Assert.AreEqual("NCU Library Events Calendar - LibCal - NCU Library", PageTitleFirstCarousel);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Library Events Calendar - Clicked First Image Success");

                //Clicking on Second Carousel
                driver.Navigate().Refresh();
                Thread.Sleep(3000);
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_RightArrow();
                MiddleMenuObjects.Clicking_SecondCarousel();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageUrlSecondCarousel = driver.Url;
                String PageTitleSecondCarousel = driver.Title;
                Assert.AreEqual("https://ncu.libguides.com/researchprocess", PageUrlSecondCarousel);
                Assert.AreEqual("Home - Research Process - LibGuides at Northcentral University", PageTitleSecondCarousel);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Research Process - Clicked Second Image Success");

                //Clicking on Third Carousel
                driver.Navigate().Refresh();
                Thread.Sleep(3000);
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_RightArrow();
                MiddleMenuObjects.Clicking_RightArrow();
                MiddleMenuObjects.Clicking_ThirdCarousel();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageUrlThirdCarousel = driver.Url;
                String PageTitleThirdCarousel = driver.Title;
                Assert.AreEqual("https://ncu.libguides.com/interlibrary_loan", PageUrlThirdCarousel);
                Assert.AreEqual("Home - Interlibrary Loan - LibGuides at Northcentral University", PageTitleThirdCarousel);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Interlibrary Loan- Clicked Third Image Success");

                //Clicking on Fourth Carousel
                driver.Navigate().Refresh();
                Thread.Sleep(3000);
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_RightArrow();
                MiddleMenuObjects.Clicking_RightArrow();
                MiddleMenuObjects.Clicking_RightArrow();
                MiddleMenuObjects.Clicking_FourthCarousel();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageUrlFourthCarousel = driver.Url;
                String PageTitleFourthCarousel = driver.Title;
                Assert.AreEqual("https://ncu.libguides.com/learn", PageUrlFourthCarousel);
                Assert.AreEqual("Home - Learn the Library - LibGuides at Northcentral University", PageTitleFourthCarousel);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Learn the Library - Clicked Fourth Image Success");

                //New Start Here
                MiddleMenuObjects.Clicking_NewStartHere();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageUrlNewStartHerePage = driver.Url;
                String PageTitleNewStartHerePage = driver.Title;
                Assert.AreEqual("https://ncu.libguides.com/learn/", PageUrlNewStartHerePage);
                Assert.AreEqual("Home - Learn the Library - LibGuides at Northcentral University", PageTitleNewStartHerePage);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("New Start Here Success");

                //Quick Start Videos
                MiddleMenuObjects.Clicking_QuickStartVideos();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageUrlQuickStartVideos = driver.Url;
                String PageTitleQuickStartVideos = driver.Title;
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                Assert.AreEqual("https://ncu.libguides.com/learn/quick", PageUrlQuickStartVideos);
                Assert.AreEqual("Quick Tutorial Videos - Learn the Library - LibGuides at Northcentral University", PageTitleQuickStartVideos);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Quick Start Videos Success");

                //Searching 101

                MiddleMenuObjects.Clicking_Searching101();
                //driver.SwitchTo().Window(driver.WindowHandles.Last());
                Thread.Sleep(3000);               
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                Thread.Sleep(3000);
                String PageUrlSearching101 = driver.Url;
                String PageTitleSearching101 = driver.Title;
                Assert.AreEqual("https://www.youtube.com/watch?v=w4bFesEvYPM&feature=youtu.be", PageUrlSearching101);
                Assert.AreEqual("Searching 101 - YouTube", PageTitleSearching101);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Searching101 - Success");

                //Recorded Library Workshop
                MiddleMenuObjects.Clicking_RecordedLibraryWorkshops();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                Thread.Sleep(3000);
                String PageUrlRecordedLibraryWorkshops = driver.Url;
                String PageTitleRecordedLibraryWorkshops = driver.Title;
                Assert.AreEqual("https://ncu.libguides.com/learn/workshops", PageUrlRecordedLibraryWorkshops);
                Assert.AreEqual("Library Workshop Videos - Learn the Library - LibGuides at Northcentral University", PageTitleRecordedLibraryWorkshops);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("RecordedLibraryWorkshops - Success");

                //Research Process
                MiddleMenuObjects.Clicking_ReserachProcess();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                Thread.Sleep(3000);
                String PageUrlResearchProcess = driver.Url;
                String PageTitleResearchProcess = driver.Title;
                Assert.AreEqual("https://ncu.libguides.com/researchprocess", PageUrlResearchProcess);
                Assert.AreEqual("Home - Research Process - LibGuides at Northcentral University", PageTitleResearchProcess);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Reserach Process - Success");

                //Tutorials
                MiddleMenuObjects.Clicking_Tutorials();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                Thread.Sleep(3000);
                String PageUrlTutorials = driver.Url;
                String PageTitleTutorials = driver.Title;
                Assert.AreEqual("https://ncu.libguides.com/learn/tutorials", PageUrlTutorials);
                Assert.AreEqual("Interactive Tutorials - Learn the Library - LibGuides at Northcentral University", PageTitleTutorials);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Tutorials - Success");

                Console.WriteLine("End MiddleMenuTest");
            }

            catch (Exception ex)
            {
                Console.WriteLine("RESULT: FAILED - CHECK THE LOG FILE");
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                string path = errorPath + TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".png";
                ss.SaveAsFile(path);
                TestContext.AddTestAttachment(path);
                test.Fail(ex.StackTrace);
                ErrorLog.SaveExeptionToLog(ex, TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup, errorPath + TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xml");
                //test.Fail(ex.StackTrace);
                //ErrorLog.SaveExeptionToLog(ex, TestContext.CurrentContext.Test.Name + "_" + environmentSetup, errorPath + TestContext.CurrentContext.Test.Name + "_" + environmentSetup + ".xml");
            }
        }

    [Test, Category("MiddleMenuTest_SearchQueries")]

        public void MiddleMenu_SearchQueries()
        {
            string RRResultTitle = "Result List: test: Roadrunner Search Discovery Service";
            string RRResultTITitle = "Result List: TI test: Roadrunner Search Discovery Service";
            try
            {

                //Middle Menu: 
                Console.WriteLine("Start MiddleMenuTest_SearchQueries");
                Console.WriteLine("Middle Menu Page Title With KeywordTest");
                MiddleMenuObjects.MiddleMenuVerification("Keyword", RRResultTitle, PrivacyError, false, false);
                MiddleMenuObjects.Verifying_CheckBoxFullTextisCheckedInResultPageNotChecked();
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("PageTitleWithKeywordTest1");
                    MiddleMenuObjects.Clear_TypeKeywordHereTextField();


                Console.WriteLine("Middle Menu Page Title With Keyword Test And Full checkbox Checked");
                     MiddleMenuObjects.MiddleMenuVerification("Keyword", RRResultTitle, PrivacyError, true, false);
                MiddleMenuObjects.Verifying_CheckBoxFullTextisCheckedInResultPageisChecked();
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    MiddleMenuObjects.Clear_FullCheckBox();
                    MiddleMenuObjects.Clear_TypeKeywordHereTextField();
                    Console.WriteLine("PageTitleWithKeywordTest2");



                Console.WriteLine("Middle Menu Page Title With Keyword Test And Scholar checkbox Checked");
                
                MiddleMenuObjects.MiddleMenuVerification("Keyword", RRResultTitle, PrivacyError, false, true);
                MiddleMenuObjects.Verifying_CheckBoxScholarlyPeerReviewedJournalsResultsPageisChecked();
                    PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    MiddleMenuObjects.Clear_BoxScholarlyPeerReviewedJournals();
                    MiddleMenuObjects.Clear_TypeKeywordHereTextField();
                    Console.WriteLine("PageTitleWithKeywordTest3");

                Console.WriteLine("Middle Menu Page Title With Keyword Test And Full and Scholar checkbox Checked");
                
                MiddleMenuObjects.MiddleMenuVerification("Keyword", RRResultTitle, PrivacyError, true, true);
                MiddleMenuObjects.Verifying_CheckBoxScholarlyPeerReviewedJournalsResultsPageisChecked();
                    MiddleMenuObjects.Verifying_CheckBoxFullTextisCheckedInResultPageisChecked();
                    PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    MiddleMenuObjects.Clear_BoxScholarlyPeerReviewedJournals();
                    MiddleMenuObjects.Clear_FullCheckBox();
                    MiddleMenuObjects.Clear_TypeKeywordHereTextField();
                    Console.WriteLine("PageTitleWithKeywordTest4");

                Console.WriteLine("Middle Menu - Enter Test in TypeKeyworHereTextField and select Title and Click Submit");
                
                MiddleMenuObjects.MiddleMenuVerification("Title", RRResultTITitle, PrivacyError, false, false);
                MiddleMenuObjects.Verifying_CheckBoxFullTextisCheckedInResultPageNotChecked();
                    PageServices.WaitForPageToCompletelyLoaded(driver, 60);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    MiddleMenuObjects.Clear_TypeKeywordHereTextField();
                    Console.WriteLine("PageTitleWithKeywordTest5");


                Console.WriteLine("Middle Menu - Enter Test in TypeKeyworHereTextField and select Title and Full Text Checkbox Click Submit");
                MiddleMenuObjects.MiddleMenuVerification("Title", RRResultTITitle, PrivacyError, true, false);
                MiddleMenuObjects.Verifying_CheckBoxFullTextisCheckedInResultPageisChecked();
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    MiddleMenuObjects.Clear_TypeKeywordHereTextField();
                    MiddleMenuObjects.Clear_FullCheckBox();
                    Console.WriteLine("PageTitleWithKeywordTest6");

                Console.WriteLine("Middle Menu - Enter Test in TypeKeyworHereTextField and select Title and ScholarlyPeerReviewedJournals Checkbox Click Submit");
                MiddleMenuObjects.MiddleMenuVerification("Title", RRResultTITitle, PrivacyError, false, true);
                MiddleMenuObjects.Verifying_CheckBoxScholarlyPeerReviewedJournalsResultsPageisChecked();
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    MiddleMenuObjects.Clear_BoxScholarlyPeerReviewedJournals();
                    MiddleMenuObjects.Clear_TypeKeywordHereTextField();
                    Console.WriteLine("PageTitleWithKeywordTestAndScholarcheckedTitle7");

                Console.WriteLine("Middle Menu - Enter Test in TypeKeyworHereTextField and select Title and Full and ScholarlyPeerReviewedJournals Checkbox Click Submit");
                MiddleMenuObjects.MiddleMenuVerification("Title",RRResultTITitle,PrivacyError,true,true);
                    MiddleMenuObjects.Verifying_CheckBoxScholarlyPeerReviewedJournalsResultsPageisChecked();
                    MiddleMenuObjects.Verifying_CheckBoxFullTextisCheckedInResultPageisChecked();
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    MiddleMenuObjects.Clear_BoxScholarlyPeerReviewedJournals();
                    MiddleMenuObjects.Clear_FullCheckBox();
                    MiddleMenuObjects.Clear_TypeKeywordHereTextField();
                    Console.WriteLine("PageTitleWithKeywordTestAndScholarcheckedTitle8");

                //Middle Menu: RoadRunner Search -- WhatisRoadrunnerSearch
                    MiddleMenuObjects.Clicking_WhatisRoadrunnerSearch();
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    PageServices.WaitForPageToCompletelyLoaded(driver, 60);
                    String PageUrlWhatisRoardrunnerSearch = driver.Url;
                    String PageTitleWhatisRoadrunnerSearch = driver.Title;
                    Assert.AreEqual("https://ncu.libanswers.com/faq/168487", PageUrlWhatisRoardrunnerSearch);
                    Assert.AreEqual("What is Roadrunner Search? How do I use it? - Ask Us!", PageTitleWhatisRoadrunnerSearch);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("PageTitleWithKeywordTestAndScholarcheckedTitle9");


                //Middle Menu: RoadRunner Search -- AdvancedSearch
                    MiddleMenuObjects.Clicking_AdvancedSearch();
                    driver.SwitchTo().Window(driver.WindowHandles.Last());                    
                    Thread.Sleep(10000);
                   
                string PageUrlAdvancedSearch = driver.Url;
                    string PageTitleAdvancedSearch = driver.Title;
                   // Assert.AreEqual("https://eds-b-ebscohost-com.proxy3.ncu.edu/eds/search/advanced?vid=0&sid=7efef573-d399-49f8-bd63-9575bcb81bac%40sessionmgr103", PageUrlAdvancedSearch);
                    Assert.AreEqual("Advanced Search: Roadrunner Search Discovery Service", PageTitleAdvancedSearch);
                    driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Console.WriteLine("PageTitleWithKeywordTestAndScholarcheckedTitle10");

                Console.WriteLine("End MiddleMenuTest_SearchQueries");

            }
            catch (Exception ex)
            {
                Console.WriteLine("RESULT: FAILED - CHECK THE LOG FILE");
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                string path = errorPath + TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".png";
                ss.SaveAsFile(path);
                TestContext.AddTestAttachment(path);
                test.Fail(ex.StackTrace);
                ErrorLog.SaveExeptionToLog(ex, TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup, errorPath + TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xml");
                //test.Fail(ex.StackTrace);
                //ErrorLog.SaveExeptionToLog(ex, TestContext.CurrentContext.Test.Name + "_" + environmentSetup, errorPath + TestContext.CurrentContext.Test.Name + "_" + environmentSetup + ".xml");
            }
        }

        [Test, Category("MiddleMenuTest_LibGuides")]
        public void MiddleMenu_LibGuides()
        {
            try
            {
                //test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

                Console.WriteLine("Start MiddleMenuTest_LibGuides");

                /***** Find Resource Tab *****/

                MiddleMenuObjects.Clicking_FindaResource();
                MiddleMenuObjects.VerifyDispalyedtInTextOnFindaResource();
                PageServices.WaitForPageToCompletelyLoaded(driver, 120);
                MiddleMenuObjects.DropdownOptionInFindaResource("Title begins with");
                MiddleMenuObjects.Enter_InTypeJournalOrBookTitleHere("Test Anxiety");
                MiddleMenuObjects.Clicking_SearchButtonInFindResource();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                Thread.Sleep(10000);

                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                string PageTitleFindaResourceTitlebeginswith = driver.Title;
                Assert.AreEqual("NCU Library - Find a Resource", PageTitleFindaResourceTitlebeginswith);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine(PageTitleFindaResourceTitlebeginswith);
                Console.WriteLine("Find Resource1");

                MiddleMenuObjects.DropdownOptionInFindaResource("Title equals");
                MiddleMenuObjects.Clicking_SearchButtonInFindResource();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
               PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                string PageTitleFindaResourceTitleequals = driver.Title;
                Assert.AreEqual("NCU Library - Find a Resource", PageTitleFindaResourceTitleequals);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Find Resource2");

                MiddleMenuObjects.DropdownOptionInFindaResource("Title contains all words");
                MiddleMenuObjects.Clicking_SearchButtonInFindResource();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                string PageTitleFindaResourceTitlecontainsallwords = driver.Title;
                Assert.AreEqual("NCU Library - Find a Resource", PageTitleFindaResourceTitlecontainsallwords);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Find Resource3");

                MiddleMenuObjects.DropdownOptionInFindaResource("ISSN equals");
                MiddleMenuObjects.Clear_TextInTypeJournalOrBookTitleHereInFindresource();
                MiddleMenuObjects.Enter_InTypeJournalOrBookTitleHere("9780306457296");
                MiddleMenuObjects.Clicking_SearchButtonInFindResource();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                string PageTitleFindaResourceISSNequals = driver.Title;
                Assert.AreEqual("NCU Library - Find a Resource", PageTitleFindaResourceISSNequals);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Find Resource4");

                MiddleMenuObjects.Clicking_WhatisRoadrunnerSearch();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                string PageTitleWhatisRoadrunnerSearchInFindaResource = driver.Title;
                Assert.AreEqual("What is Roadrunner Search? How do I use it? - Ask Us!", PageTitleWhatisRoadrunnerSearchInFindaResource);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Find a Resource - What is Roadrunner Search Success");

                /************** Library Guide  ***********/


                MiddleMenuObjects.Clicking_libraryGuidesTab();
                Console.WriteLine("Guide1");
                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                Console.WriteLine("Guide2");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.VerifyingdisplayedTxtLibraryGuideGuideList();
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                Console.WriteLine("Guide3");
                MiddleMenuObjects.VerifyingdisplayedTxtOnLibraryGuideSelectaGuide();
                MiddleMenuObjects.dropdownVerifyingDefaultValueInGuideList();
                Console.WriteLine("Guide End");





                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("About Us");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);

                //WebDriverExtensions.FindElement(driver, By.CssSelector("div.s-lg-widget-content>div>form > button"), 90).Click();
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfAboutUs = driver.Title;
                string PageURLOfAboutUs = driver.Url;
                Assert.AreEqual("Home - About Us - LibGuides at Northcentral University", PageTitleOfAboutUs);
                Assert.AreEqual("https://ncu.libguides.com/about", PageURLOfAboutUs);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("About us success");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Accounting & Advanced Accounting");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfAccountingAdvancedAccounting = driver.Title;
                string PageURLOfAccountingAdvancedAccounting = driver.Url;
                Assert.AreEqual("Home - Accounting & Advanced Accounting - LibGuides at Northcentral University", PageTitleOfAccountingAdvancedAccounting);
                Assert.AreEqual("https://ncu.libguides.com/accounting", PageURLOfAccountingAdvancedAccounting);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("PageURLOf PageURLOfAccountingAdvancedAccounting");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Addictions & Rehabilitation");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfAddictionsRehabilitation = driver.Title;
                string PageURLOfAddictionsRehabilitation = driver.Url;
                Assert.AreEqual("Home - Addictions & Rehabilitation - LibGuides at Northcentral University", PageTitleOfAddictionsRehabilitation);
                Assert.AreEqual("https://ncu.libguides.com/addictions", PageURLOfAddictionsRehabilitation);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Addictions & Rehabilitation");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Adult Learning and Workforce Education");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfAdultLearningandWorkforceEducation = driver.Title;
                string PageURLOfAdultLearningandWorkforceEducation = driver.Url;
                Assert.AreEqual("Home - Adult Learning and Workforce Education - LibGuides at Northcentral University", PageTitleOfAdultLearningandWorkforceEducation);
                Assert.AreEqual("https://ncu.libguides.com/adultlearning", PageURLOfAdultLearningandWorkforceEducation);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Adult Learning and Workforce Education");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Alumni Library Resources and Services");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfAlumniLibraryResourcesandServices = driver.Title;
                string PageURLOfAlumniLibraryResourcesandServices = driver.Url;
                Assert.AreEqual("Home - Alumni Library Resources and Services - LibGuides at Northcentral University", PageTitleOfAlumniLibraryResourcesandServices);
                Assert.AreEqual("https://ncu.libguides.com/alumni", PageURLOfAlumniLibraryResourcesandServices);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Alumni Library Resources and Services");

                 

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Assessment in Higher Education");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfAssessmentinHigherEducation = driver.Title;
                string PageURLOfAssessmentinHigherEducation = driver.Url;
                Assert.AreEqual("Home - Assessment in Higher Education - LibGuides at Northcentral University", PageTitleOfAssessmentinHigherEducation);
                Assert.AreEqual("https://ncu.libguides.com/assessment", PageURLOfAssessmentinHigherEducation);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Assessment in Higher Education");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Athletic Coaching");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfAthleticCoaching = driver.Title;
                string PageURLOfAthleticCoaching = driver.Url;
                Assert.AreEqual("Home - Athletic Coaching - LibGuides at Northcentral University", PageTitleOfAthleticCoaching);
                Assert.AreEqual("https://ncu.libguides.com/athleticcoaching", PageURLOfAthleticCoaching);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Athletic Coaching");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Best Practices in Teaching Online");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfBestPracticesinTeachingOnline = driver.Title;
                string PageURLOfBestPracticesinTeachingOnline = driver.Url;
                Assert.AreEqual("Home - Best Practices in Teaching Online - LibGuides at Northcentral University", PageTitleOfBestPracticesinTeachingOnline);
                Assert.AreEqual("https://ncu.libguides.com/teachingonline", PageURLOfBestPracticesinTeachingOnline);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Best Practices in Teaching Online");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Building Creativity");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfBuildingCreativity = driver.Title;
                string PageURLOfBuildingCreativity = driver.Url;
                Assert.AreEqual("Home - Building Creativity - LibGuides at Northcentral University", PageTitleOfBuildingCreativity);
                Assert.AreEqual("https://ncu.libguides.com/creativity", PageURLOfBuildingCreativity);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Building Creativity");

                 

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Business Ethics");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfBusinessEthics = driver.Title;
                string PageURLOfBusinessEthics = driver.Url;
                Assert.AreEqual("Home - Business Ethics - LibGuides at Northcentral University", PageTitleOfBusinessEthics);
                Assert.AreEqual("https://ncu.libguides.com/businessethics", PageURLOfBusinessEthics);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Business Ethics");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("California Licensure");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfCaliforniaLicensure = driver.Title;
                string PageURLOfCaliforniaLicensure = driver.Url;
                Assert.AreEqual("Home - California Licensure - LibGuides at Northcentral University", PageTitleOfCaliforniaLicensure);
                Assert.AreEqual("https://ncu.libguides.com/calicensure", PageURLOfCaliforniaLicensure);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("California Licensure");

                

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Change & Innovation");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfChangeInnovation = driver.Title;
                string PageURLOfChangeInnovation = driver.Url;
                Assert.AreEqual("Home - Change & Innovation - LibGuides at Northcentral University", PageTitleOfChangeInnovation);
                Assert.AreEqual("https://ncu.libguides.com/change", PageURLOfChangeInnovation);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Change & Innovation");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Child and Adolescent Developmental Psychology");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfChildandAdolescentDevelopmentalPsychology = driver.Title;
                string PageURLOfChildandAdolescentDevelopmentalPsychology = driver.Url;
                Assert.AreEqual("Home - Child and Adolescent Developmental Psychology - LibGuides at Northcentral University", PageTitleOfChildandAdolescentDevelopmentalPsychology);
                Assert.AreEqual("https://ncu.libguides.com/childpsychology", PageURLOfChildandAdolescentDevelopmentalPsychology);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Child and Adolescent Developmental Psychology");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Child and Adolescent Therapy");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfChildandAdolescentTherapy = driver.Title;
                string PageURLOfChildandAdolescentTherapy = driver.Url;
                Assert.AreEqual("Home - Child and Adolescent Therapy - LibGuides at Northcentral University", PageTitleOfChildandAdolescentTherapy);
                Assert.AreEqual("https://ncu.libguides.com/childadolescent", PageURLOfChildandAdolescentTherapy);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Child and Adolescent Therapy");

           

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Cloud Computing");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfCloudComputing = driver.Title;
                string PageURLOfCloudComputing = driver.Url;
                Assert.AreEqual("Home - Cloud Computing - LibGuides at Northcentral University", PageTitleOfCloudComputing);
                Assert.AreEqual("https://ncu.libguides.com/cloudcomputing", PageURLOfCloudComputing);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Cloud Computing");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Community College Leadership");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfCommunityCollegeLeadership = driver.Title;
                string PageURLOfCommunityCollegeLeadership = driver.Url;
                Assert.AreEqual("Home - Community College Leadership - LibGuides at Northcentral University", PageTitleOfCommunityCollegeLeadership);
                Assert.AreEqual("https://ncu.libguides.com/communitycollege", PageURLOfCommunityCollegeLeadership);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Community College Leadership");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Computer and Information Security / Cybersecurity");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfComputerandInformationSecurityCybersecurity = driver.Title;
                string PageURLOfComputerandInformationSecurityCybersecurity = driver.Url;
                Assert.AreEqual("Home - Computer and Information Security / Cybersecurity - LibGuides at Northcentral University", PageTitleOfComputerandInformationSecurityCybersecurity);
                Assert.AreEqual("https://ncu.libguides.com/cis", PageURLOfComputerandInformationSecurityCybersecurity);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Computer and Information Security / Cybersecurity");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Computer Science");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfComputerScience = driver.Title;
                string PageURLOfComputerScience = driver.Url;
                Assert.AreEqual("Home - Computer Science - LibGuides at Northcentral University", PageTitleOfComputerScience);
                Assert.AreEqual("https://ncu.libguides.com/computersci", PageURLOfComputerScience);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Computer Science");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Corporate Wellness");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfCorporateWellness = driver.Title;
                string PageURLOfCorporateWellness = driver.Url;
                Assert.AreEqual("Home - Corporate Wellness - LibGuides at Northcentral University", PageTitleOfCorporateWellness);
                Assert.AreEqual("https://ncu.libguides.com/corporatewellness", PageURLOfCorporateWellness);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Corporate Wellness");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Counseling Psychology");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfCounselingPsychology = driver.Title;
                string PageURLOfCounselingPsychology = driver.Url;
                Assert.AreEqual("Home - Counseling Psychology - LibGuides at Northcentral University", PageTitleOfCounselingPsychology);
                Assert.AreEqual("https://ncu.libguides.com/counseling", PageURLOfCounselingPsychology);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Counseling Psychology");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Couple Therapy");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfCoupleTherapy = driver.Title;
                string PageURLOfCoupleTherapy = driver.Url;
                Assert.AreEqual("Home - Couple Therapy - LibGuides at Northcentral University", PageTitleOfCoupleTherapy);
                Assert.AreEqual("https://ncu.libguides.com/coupletherapy", PageURLOfCoupleTherapy);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Couple Therapy");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Criminal Justice");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfCriminalJustice = driver.Title;
                string PageURLOfCriminalJustice = driver.Url;
                Assert.AreEqual("Home - Criminal Justice - LibGuides at Northcentral University", PageTitleOfCriminalJustice);
                Assert.AreEqual("https://ncu.libguides.com/criminal_justice", PageURLOfCriminalJustice);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Criminal Justice");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Culture, Diversity and Social Justice in a Global Context");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfCultureDiversityandSocialJusticeinaGlobalContext = driver.Title;
                string PageURLOfCultureDiversityandSocialJusticeinaGlobalContext = driver.Url;
                Assert.AreEqual("Home - Culture, Diversity and Social Justice in a Global Context - LibGuides at Northcentral University", PageTitleOfCultureDiversityandSocialJusticeinaGlobalContext);
                Assert.AreEqual("https://ncu.libguides.com/socialjustice", PageURLOfCultureDiversityandSocialJusticeinaGlobalContext);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Culture, Diversity and Social Justice in a Global Context");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Curriculum and Teaching");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfCurriculumandTeaching = driver.Title;
                string PageURLOfCurriculumandTeaching = driver.Url;
                Assert.AreEqual("Home - Curriculum and Teaching - LibGuides at Northcentral University", PageTitleOfCurriculumandTeaching);
                Assert.AreEqual("https://ncu.libguides.com/curriculum", PageURLOfCurriculumandTeaching);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Curriculum and Teaching");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Data Mining");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfDataMining = driver.Title;
                string PageURLOfDataMining = driver.Url;
                Assert.AreEqual("Overview - Data Mining - LibGuides at Northcentral University", PageTitleOfDataMining);
                Assert.AreEqual("https://ncu.libguides.com/datamining", PageURLOfDataMining);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Data Mining");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Data Science");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfDataScience = driver.Title;
                string PageURLOfDataScience = driver.Url;
                Assert.AreEqual("Home - Data Science - LibGuides at Northcentral University", PageTitleOfDataScience);
                Assert.AreEqual("https://ncu.libguides.com/data", PageURLOfDataScience);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Data Science");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("DIP9901 Course Guide");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfDIP9901CourseGuide = driver.Title;
                string PageURLOfDIP9901CourseGuide = driver.Url;
                Assert.AreEqual("Home - DIP9901 Course Guide - LibGuides at Northcentral University", PageTitleOfDIP9901CourseGuide);
                Assert.AreEqual("https://ncu.libguides.com/dip9901", PageURLOfDIP9901CourseGuide);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("DIP9901 Course Guide");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("DIP9902 Course Guide");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfDIP9902CourseGuide = driver.Title;
                string PageURLOfDIP9902CourseGuide = driver.Url;
                Assert.AreEqual("Home - DIP9902 Course Guide - LibGuides at Northcentral University", PageTitleOfDIP9902CourseGuide);
                Assert.AreEqual("https://ncu.libguides.com/dip9902", PageURLOfDIP9902CourseGuide);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("DIP9902 Course Guide");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("DIS9901 Course Guide");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfDIS9901CourseGuide = driver.Title;
                string PageURLOfDIS9901CourseGuide = driver.Url;
                Assert.AreEqual("Home - DIS9901 Course Guide - LibGuides at Northcentral University", PageTitleOfDIS9901CourseGuide);
                Assert.AreEqual("https://ncu.libguides.com/dis9901", PageURLOfDIS9901CourseGuide);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("DIS9901 Course Guide");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("DIS9902 Course Guide");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfDIS9902CourseGuide = driver.Title;
                string PageURLOfDIS9902CourseGuide = driver.Url;
                Assert.AreEqual("Home - DIS9902 Course Guide - LibGuides at Northcentral University", PageTitleOfDIS9902CourseGuide);
                Assert.AreEqual("https://ncu.libguides.com/dis9902", PageURLOfDIS9902CourseGuide);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("DIS9902 Course Guide");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("DIS9903 Course Guide");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfDIS9903CourseGuide = driver.Title;
                string PageURLOfDIS9903CourseGuide = driver.Url;
                Assert.AreEqual("Home - DIS9903 Course Guide - LibGuides at Northcentral University", PageTitleOfDIS9903CourseGuide);
                Assert.AreEqual("https://ncu.libguides.com/dis9903", PageURLOfDIS9903CourseGuide);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("DIS9903 Course Guide");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("DIS9904 Course Guide");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                string PageTitleOfDIS9904CourseGuide = driver.Title;
                string PageURLOfDIS9904CourseGuide = driver.Url;
                Assert.AreEqual("Home - DIS9904 Course Guide - LibGuides at Northcentral University", PageTitleOfDIS9904CourseGuide);
                Assert.AreEqual("https://ncu.libguides.com/dis9904", PageURLOfDIS9904CourseGuide);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("DIS9904 Course Guide");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Disability Studies");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfDisabilityStudies = driver.Title;
                String PageURLOfDisabilityStudies = driver.Url;
                Assert.AreEqual("Home - Disability Studies - LibGuides at Northcentral University", PageTitleOfDisabilityStudies);
                Assert.AreEqual("https://ncu.libguides.com/disabilitystudies", PageURLOfDisabilityStudies);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Disability Studies");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Diversity in Education");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfDiversityinEducation = driver.Title;
                String PageURLOfDiversityinEducation = driver.Url;
                Assert.AreEqual("Home - Diversity in Education - LibGuides at Northcentral University", PageTitleOfDiversityinEducation);
                Assert.AreEqual("https://ncu.libguides.com/diversityeducation", PageURLOfDiversityinEducation);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Diversity in Education");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("E-Books");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfEBooks = driver.Title;
                String PageURLOfEBooks = driver.Url;
                Assert.AreEqual("Home - E-Books - LibGuides at Northcentral University", PageTitleOfEBooks);
                Assert.AreEqual("https://ncu.libguides.com/ebooks", PageURLOfEBooks);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("E-Books");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("E-Learning");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfELearning = driver.Title;
                String PageURLOfELearning = driver.Url;
                Assert.AreEqual("Home - E-Learning - LibGuides at Northcentral University", PageTitleOfELearning);
                Assert.AreEqual("https://ncu.libguides.com/e-learning", PageURLOfELearning);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("E-Learning");



                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Early Childhood Education");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfEarlyChildhoodEducation = driver.Title;
                String PageURLOfEarlyChildhoodEducation = driver.Url;
                Assert.AreEqual("Home - Early Childhood Education - LibGuides at Northcentral University", PageTitleOfEarlyChildhoodEducation);
                Assert.AreEqual("https://ncu.libguides.com/earlychildhood", PageURLOfEarlyChildhoodEducation);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Early Childhood Education");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Education and Supervision");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfEducationandSupervision = driver.Title;
                String PageURLOfEducationandSupervision = driver.Url;
                Assert.AreEqual("Home - Education and Supervision - LibGuides at Northcentral University", PageTitleOfEducationandSupervision);
                Assert.AreEqual("https://ncu.libguides.com/mftsupervision", PageURLOfEducationandSupervision);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Education and Supervision");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Educational Leadership");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfEducationalLeadership = driver.Title;
                String PageURLOfEducationalLeadership = driver.Url;
                Assert.AreEqual("Home - Educational Leadership - LibGuides at Northcentral University", PageTitleOfEducationalLeadership);
                Assert.AreEqual("https://ncu.libguides.com/educationalleadership", PageURLOfEducationalLeadership);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Educational Leadership");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Educational Psychology");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfEducationalPsychology = driver.Title;
                String PageURLOfEducationalPsychology = driver.Url;
                Assert.AreEqual("Home - Educational Psychology - LibGuides at Northcentral University", PageTitleOfEducationalPsychology);
                Assert.AreEqual("https://ncu.libguides.com/educationalpsychology", PageURLOfEducationalPsychology);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Educational Psychology");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Elementary Education");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfElementaryEducation = driver.Title;
                String PageURLOfElementaryEducation = driver.Url;
                Assert.AreEqual("Home - Elementary Education - LibGuides at Northcentral University", PageTitleOfElementaryEducation);
                Assert.AreEqual("https://ncu.libguides.com/elementaryeducation", PageURLOfElementaryEducation);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Elementary Education");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("English as a Second Language");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfEnglishasaSecondLanguage = driver.Title;
                String PageURLOfEnglishasaSecondLanguage = driver.Url;
                Assert.AreEqual("Home - English as a Second Language - LibGuides at Northcentral University", PageTitleOfEnglishasaSecondLanguage);
                Assert.AreEqual("https://ncu.libguides.com/englishsecondlanguage", PageURLOfEnglishasaSecondLanguage);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("English as a Second Language");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("English Language Arts Excellence in the Common Core");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfEnglishLanguageArtsExcellenceintheCommonCore = driver.Title;
                String PageURLOfEnglishLanguageArtsExcellenceintheCommonCore = driver.Url;
                Assert.AreEqual("Home - English Language Arts Excellence in the Common Core - LibGuides at Northcentral University", PageTitleOfEnglishLanguageArtsExcellenceintheCommonCore);
                Assert.AreEqual("https://ncu.libguides.com/languagearts", PageURLOfEnglishLanguageArtsExcellenceintheCommonCore);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("English Language Arts Excellence in the Common Core");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("English Language Learning");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfEnglishLanguageLearning = driver.Title;
                String PageURLOfEnglishLanguageLearning = driver.Url;
                Assert.AreEqual("Home - English Language Learning - LibGuides at Northcentral University", PageTitleOfEnglishLanguageLearning);
                Assert.AreEqual("https://ncu.libguides.com/ell", PageURLOfEnglishLanguageLearning);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("English Language Learning");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Entrepreneurship");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfEntrepreneurship = driver.Title;
                String PageURLOfEntrepreneurship = driver.Url;
                Assert.AreEqual("Home - Entrepreneurship - LibGuides at Northcentral University", PageTitleOfEntrepreneurship);
                Assert.AreEqual("https://ncu.libguides.com/entrepreneurship", PageURLOfEntrepreneurship);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Entrepreneurship");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Financial Management");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfFinancialManagement = driver.Title;
                String PageURLOfFinancialManagement = driver.Url;
                Assert.AreEqual("Home - Financial Management - LibGuides at Northcentral University", PageTitleOfFinancialManagement);
                Assert.AreEqual("https://ncu.libguides.com/financial", PageURLOfFinancialManagement);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Financial Management");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Forensic Psychology");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfForensicPsychology = driver.Title;
                String PageURLOfForensicPsychology = driver.Url;
                Assert.AreEqual("Home - Forensic Psychology - LibGuides at Northcentral University", PageTitleOfForensicPsychology);
                Assert.AreEqual("https://ncu.libguides.com/forensicpsychology", PageURLOfForensicPsychology);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Forensic Psychology");
 
 
                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("General Business Research");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfGeneralBusinessResearch = driver.Title;
                String PageURLOfGeneralBusinessResearch = driver.Url;
                Assert.AreEqual("Home - General Business Research - LibGuides at Northcentral University", PageTitleOfGeneralBusinessResearch);
                Assert.AreEqual("https://ncu.libguides.com/business_research", PageURLOfGeneralBusinessResearch);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("General Business Research");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("General Education");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfGeneralEducation = driver.Title;
                String PageURLOfGeneralEducation = driver.Url;
                Assert.AreEqual("Home - General Education - LibGuides at Northcentral University", PageTitleOfGeneralEducation);
                Assert.AreEqual("https://ncu.libguides.com/generaleducation", PageURLOfGeneralEducation);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("General Education");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("General Family Therapy");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfGeneralFamilyTherapy = driver.Title;
                String PageURLOfGeneralFamilyTherapy = driver.Url;
                Assert.AreEqual("Home - General Family Therapy - LibGuides at Northcentral University", PageTitleOfGeneralFamilyTherapy);
                Assert.AreEqual("https://ncu.libguides.com/generalfamilytherapy", PageURLOfGeneralFamilyTherapy);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("General Family Therapy");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("General Psychology");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfGeneralPsychology = driver.Title;
                String PageURLOfGeneralPsychology = driver.Url;
                Assert.AreEqual("Home - General Psychology - LibGuides at Northcentral University", PageTitleOfGeneralPsychology);
                Assert.AreEqual("https://ncu.libguides.com/generalpsychology", PageURLOfGeneralPsychology);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("General Psychology");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Gerontology");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfGerontology = driver.Title;
                String PageURLOfGerontology = driver.Url;
                Assert.AreEqual("Home - Gerontology - LibGuides at Northcentral University", PageTitleOfGerontology);
                Assert.AreEqual("https://ncu.libguides.com/gerontology", PageURLOfGerontology);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Gerontology");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Geropsychology and Elder Care");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfGeropsychologyandEldeCare = driver.Title;
                String PageURLOfGeropsychologyandElderCare = driver.Url;
                Assert.AreEqual("Home - Geropsychology and Elder Care - LibGuides at Northcentral University", PageTitleOfGeropsychologyandEldeCare);
                Assert.AreEqual("https://ncu.libguides.com/geropsychology", PageURLOfGeropsychologyandElderCare);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Geropsychology and Elder Care");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Global Training and Development");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfGlobalTrainingandDevelopment = driver.Title;
                String PageURLOfGlobalTrainingandDevelopment = driver.Url;
                Assert.AreEqual("Home - Global Training and Development - LibGuides at Northcentral University", PageTitleOfGlobalTrainingandDevelopment);
                Assert.AreEqual("https://ncu.libguides.com/globaltraining", PageURLOfGlobalTrainingandDevelopment);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Global Training and Development");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Health Care Administration");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfHealthCareAdministration = driver.Title;
                String PageURLOfHealthCareAdministration = driver.Url;
                Assert.AreEqual("Home - Health Care Administration - LibGuides at Northcentral University", PageTitleOfHealthCareAdministration);
                Assert.AreEqual("https://ncu.libguides.com/healthcare", PageURLOfHealthCareAdministration);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Health Care Administration");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Health Psychology");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfHealthPsychology = driver.Title;
                String PageURLOfHealthPsychology = driver.Url;
                Assert.AreEqual("Home - Health Psychology - LibGuides at Northcentral University", PageTitleOfHealthPsychology);
                Assert.AreEqual("https://ncu.libguides.com/healthpsychology", PageURLOfHealthPsychology);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Health Psychology");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Homeland Security");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfHomelandSecurity = driver.Title;
                String PageURLOfHomelandSecurity = driver.Url;
                Assert.AreEqual("Home - Homeland Security - LibGuides at Northcentral University", PageTitleOfHomelandSecurity);
                Assert.AreEqual("https://ncu.libguides.com/homeland", PageURLOfHomelandSecurity);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Homeland Security");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Human Performance and Coaching");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfHumanPerformanceandCoaching = driver.Title;
                String PageURLOfHumanPerformanceandCoaching = driver.Url;
                Assert.AreEqual("Home - Human Performance and Coaching - LibGuides at Northcentral University", PageTitleOfHumanPerformanceandCoaching);
                Assert.AreEqual("https://ncu.libguides.com/humanperformance", PageURLOfHumanPerformanceandCoaching);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Human Performance and Coaching");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Human Resources Management");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfHumanResourcesManagement = driver.Title;
                String PageURLOfHumanResourcesManagement = driver.Url;
                Assert.AreEqual("Home - Human Resources Management - LibGuides at Northcentral University", PageTitleOfHumanResourcesManagement);
                Assert.AreEqual("https://ncu.libguides.com/hr", PageURLOfHumanResourcesManagement);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Human Resources Management");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Human Sexuality");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfHumanSexuality = driver.Title;
                String PageURLOfHumanSexuality = driver.Url;
                Assert.AreEqual("Home - Human Sexuality - LibGuides at Northcentral University", PageTitleOfHumanSexuality);
                Assert.AreEqual("https://ncu.libguides.com/sexuality", PageURLOfHumanSexuality);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Human Sexuality");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Industrial/Organizational Psychology - Business");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfIndustrialOrganizationalPsychologyBusiness = driver.Title;
                String PageURLOfIndustrialOrganizationalPsychologyBusiness = driver.Url;
                Assert.AreEqual("Home - Industrial/Organizational Psychology - Business - LibGuides at Northcentral University", PageTitleOfIndustrialOrganizationalPsychologyBusiness);
                Assert.AreEqual("https://ncu.libguides.com/IOP_Business", PageURLOfIndustrialOrganizationalPsychologyBusiness);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Industrial/Organizational Psychology - Business");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Industrial/Organizational Psychology - SSBS");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfIndustrialOrganizationalPsychologySSBS = driver.Title;
                String PageURLOfIndustrialOrganizationalPsychologySSBS = driver.Url;
                Assert.AreEqual("Home - Industrial/Organizational Psychology - SSBS - LibGuides at Northcentral University", PageTitleOfIndustrialOrganizationalPsychologySSBS);
                Assert.AreEqual("https://ncu.libguides.com/IOP_Psychology", PageURLOfIndustrialOrganizationalPsychologySSBS);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Industrial/Organizational Psychology - SSBS");
 
                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Information Technology");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfInformationTechnology = driver.Title;
                String PageURLOfInformationTechnology = driver.Url;
                Assert.AreEqual("Home - Information Technology - LibGuides at Northcentral University", PageTitleOfInformationTechnology);
                Assert.AreEqual("https://ncu.libguides.com/IT", PageURLOfInformationTechnology);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Information Technology");
 

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Instructional Design");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfInstructionalDesign = driver.Title;
                String PageURLOfInstructionalDesign = driver.Url;
                Assert.AreEqual("Home - Instructional Design - LibGuides at Northcentral University", PageTitleOfInstructionalDesign);
                Assert.AreEqual("https://ncu.libguides.com/instructionaldesign", PageURLOfInstructionalDesign);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Instructional Design");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Instructional Leadership");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfInstructionalLeadership = driver.Title;
                String PageURLOfInstructionalLeadership = driver.Url;
                Assert.AreEqual("Home - Instructional Leadership - LibGuides at Northcentral University", PageTitleOfInstructionalLeadership);
                Assert.AreEqual("https://ncu.libguides.com/instrucationalleadership", PageURLOfInstructionalLeadership);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Instructional Leadership");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Instructional Strategies");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfInstructionalStrategies = driver.Title;
                String PageURLOfInstructionalStrategies = driver.Url;
                Assert.AreEqual("Home - Instructional Strategies - LibGuides at Northcentral University", PageTitleOfInstructionalStrategies);
                Assert.AreEqual("https://ncu.libguides.com/instructionalstrategies", PageURLOfInstructionalStrategies);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Instructional Strategies");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Interlibrary Loan");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfInterlibraryLoan = driver.Title;
                String PageURLOfInterlibraryLoan = driver.Url;
                Assert.AreEqual("Home - Interlibrary Loan - LibGuides at Northcentral University", PageTitleOfInterlibraryLoan);
                Assert.AreEqual("https://ncu.libguides.com/interlibrary_loan", PageURLOfInterlibraryLoan);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Interlibrary Loan");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("International Business");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfInternationalBusiness = driver.Title;
                String PageURLOfInternationalBusiness = driver.Url;
                Assert.AreEqual("Home - International Business - LibGuides at Northcentral University", PageTitleOfInternationalBusiness);
                Assert.AreEqual("https://ncu.libguides.com/international_business", PageURLOfInternationalBusiness);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("International Business");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("International Economics");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfInternationalEconomics = driver.Title;
                String PageURLOfInternationalEconomics = driver.Url;
                Assert.AreEqual("Home - International Economics - LibGuides at Northcentral University", PageTitleOfInternationalEconomics);
                Assert.AreEqual("https://ncu.libguides.com/internationaleconomics", PageURLOfInternationalEconomics);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("International Economics");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("International Education");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfInternationalEducation = driver.Title;
                String PageURLOfInternationalEducation = driver.Url;
                Assert.AreEqual("Home - International Education - LibGuides at Northcentral University", PageTitleOfInternationalEducation);
                Assert.AreEqual("https://ncu.libguides.com/internationaleducation", PageURLOfInternationalEducation);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("International Education");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("International Students");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfInternationalStudents = driver.Title;
                String PageURLOfInternationalStudents = driver.Url;
                Assert.AreEqual("Home - International Students - LibGuides at Northcentral University", PageTitleOfInternationalStudents);
                Assert.AreEqual("https://ncu.libguides.com/international", PageURLOfInternationalStudents);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("International Students");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("JFKU Museum Studies Program");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfJFKUMuseumStudiesProgram = driver.Title;
                String PageURLOfJFKUMuseumStudiesProgram = driver.Url;
                Assert.AreEqual("Museum reference - JFKU Museum Studies Program - LibGuides at Northcentral University", PageTitleOfJFKUMuseumStudiesProgram);
                Assert.AreEqual("https://ncu.libguides.com/museum", PageURLOfJFKUMuseumStudiesProgram);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("JFKU Museum Studies Program");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Knowledge Management");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfKnowledgeManagement = driver.Title;
                String PageURLOfKnowledgeManagement = driver.Url;
                Assert.AreEqual("Home - Knowledge Management - LibGuides at Northcentral University", PageTitleOfKnowledgeManagement);
                Assert.AreEqual("https://ncu.libguides.com/km", PageURLOfKnowledgeManagement);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Knowledge Management");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Language and Literacy Education");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleLanguageandLiteracyEducation = driver.Title;
                String PageURLLanguageandLiteracyEducation = driver.Url;
                Assert.AreEqual("Home - Language and Literacy Education - LibGuides at Northcentral University", PageTitleLanguageandLiteracyEducation);
                Assert.AreEqual("https://ncu.libguides.com/languageliteracyed", PageURLLanguageandLiteracyEducation);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Language and Literacy Education");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Leadership for Improved Student Achievement");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfLeadershipforImprovedStudentAchievement = driver.Title;
                String PageURLOfLeadershipforImprovedStudentAchievement = driver.Url;
                Assert.AreEqual("Home - Leadership for Improved Student Achievement - LibGuides at Northcentral University", PageTitleOfLeadershipforImprovedStudentAchievement);
                Assert.AreEqual("https://ncu.libguides.com/studentachievement", PageURLOfLeadershipforImprovedStudentAchievement);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Leadership for Improved Student Achievement");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Leadership in Higher Education");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfLeadershipinHigherEducation = driver.Title;
                String PageURLOfLeadershipinHigherEducation = driver.Url;
                Assert.AreEqual("Home - Leadership in Higher Education - LibGuides at Northcentral University", PageTitleOfLeadershipinHigherEducation);
                Assert.AreEqual("https://ncu.libguides.com/highereducation", PageURLOfLeadershipinHigherEducation);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Leadership in Higher Education");
 

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Learn the Library");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfLearntheLibrary = driver.Title;
                String PageURLOfLearntheLibrary = driver.Url;
                Assert.AreEqual("Home - Learn the Library - LibGuides at Northcentral University", PageTitleOfLearntheLibrary);
                Assert.AreEqual("https://ncu.libguides.com/learn", PageURLOfLearntheLibrary);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Learn the Library");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Learning Analytics in Higher Education");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfLearningAnalyticsinHigherEducation = driver.Title;
                String PageURLOfLearningAnalyticsinHigherEducation = driver.Url;
                Assert.AreEqual("Home - Learning Analytics in Higher Education - LibGuides at Northcentral University", PageTitleOfLearningAnalyticsinHigherEducation);
                Assert.AreEqual("https://ncu.libguides.com/learninganalytics", PageURLOfLearningAnalyticsinHigherEducation);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Learning Analytics in Higher Education");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Learning Analytics K-12");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfLearningAnalytics12 = driver.Title;
                String PageURLOfLearningAnalytics12 = driver.Url;
                Assert.AreEqual("Home - Learning Analytics K-12 - LibGuides at Northcentral University", PageTitleOfLearningAnalytics12);
                Assert.AreEqual("https://ncu.libguides.com/learninganalyticsk12", PageURLOfLearningAnalytics12);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Learning Analytics K-12");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Legal Resources");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfLegalResources = driver.Title;
                String PageURLOfLegalResources = driver.Url;
                Assert.AreEqual("Home - Legal Resources - LibGuides at Northcentral University", PageTitleOfLegalResources);
                Assert.AreEqual("https://ncu.libguides.com/law", PageURLOfLegalResources);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Legal Resources");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Legal Studies");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfLegalStudies = driver.Title;
                String PageURLOfLegalStudies = driver.Url;
                Assert.AreEqual("Home - Legal Studies - LibGuides at Northcentral University", PageTitleOfLegalStudies);
                Assert.AreEqual("https://ncu.libguides.com/legalstudies", PageURLOfLegalStudies);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Legal Studies");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("LGBTQ Couple and Family Therapy");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfLGBTQCoupleandFamilyTherapy = driver.Title;
                String PageURLOfLGBTQCoupleandFamilyTherapy = driver.Url;
                Assert.AreEqual("Home - LGBTQ Couple and Family Therapy - LibGuides at Northcentral University", PageTitleOfLGBTQCoupleandFamilyTherapy);
                Assert.AreEqual("https://ncu.libguides.com/lgbtq", PageURLOfLGBTQCoupleandFamilyTherapy);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("LGBTQ Couple and Family Therapy");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Loss and Bereavement");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfLossandBereavement = driver.Title;
                String PageURLOfLossandBereavement = driver.Url;
                Assert.AreEqual("Home - Loss and Bereavement - LibGuides at Northcentral University", PageTitleOfLossandBereavement);
                Assert.AreEqual("https://ncu.libguides.com/lossbereavement", PageURLOfLossandBereavement);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Loss and Bereavement");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Management");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfManagement = driver.Title;
                String PageURLOfManagement = driver.Url;
                Assert.AreEqual("Home - Management - LibGuides at Northcentral University", PageTitleOfManagement);
                Assert.AreEqual("https://ncu.libguides.com/management", PageURLOfManagement);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Management");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Management Information Systems");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfManagementInformationSystems = driver.Title;
                String PageURLOfManagementInformationSystems = driver.Url;
                Assert.AreEqual("Home - Management Information Systems - LibGuides at Northcentral University", PageTitleOfManagementInformationSystems);
                Assert.AreEqual("https://ncu.libguides.com/MIS", PageURLOfManagementInformationSystems);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Management Information Systems");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Management of Engineering and Technology");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfManagementofEngineeringandTechnology = driver.Title;
                String PageURLOfManagementofEngineeringandTechnology = driver.Url;
                Assert.AreEqual("Home - Management of Engineering and Technology - LibGuides at Northcentral University", PageTitleOfManagementofEngineeringandTechnology);
                Assert.AreEqual("https://ncu.libguides.com/engineering-and-technology", PageURLOfManagementofEngineeringandTechnology);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Management of Engineering and Technology");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Marketing");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfMarketing = driver.Title;
                String PageURLOfMarketing = driver.Url;
                Assert.AreEqual("Home - Marketing - LibGuides at Northcentral University", PageTitleOfMarketing);
                Assert.AreEqual("https://ncu.libguides.com/marketing", PageURLOfMarketing);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Marketing");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Mathematics Excellence in the Common Core");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfMathematicsExcellenceintheCommonCore = driver.Title;
                String PageURLOfMathematicsExcellenceintheCommonCore = driver.Url;
                Assert.AreEqual("Home - Mathematics Excellence in the Common Core - LibGuides at Northcentral University", PageTitleOfMathematicsExcellenceintheCommonCore);
                Assert.AreEqual("https://ncu.libguides.com/mathematics", PageURLOfMathematicsExcellenceintheCommonCore);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Mathematics Excellence in the Common Core");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("MBA Program");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfMBAProgram = driver.Title;
                String PageURLOfMBAProgram = driver.Url;
                Assert.AreEqual("Home - MBA Program - LibGuides at Northcentral University", PageTitleOfMBAProgram);
                Assert.AreEqual("https://ncu.libguides.com/mba", PageURLOfMBAProgram);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("MBA Program");
 
                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Medical Family Therapy");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfMedicalFamilyTherapy = driver.Title;
                String PageURLOfMedicalFamilyTherapy = driver.Url;
                Assert.AreEqual("Home - Medical Family Therapy - LibGuides at Northcentral University", PageTitleOfMedicalFamilyTherapy);
                Assert.AreEqual("https://ncu.libguides.com/medicalfamilytherapy", PageURLOfMedicalFamilyTherapy);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Medical Family Therapy");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Mental Health Administration");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfMentalHealthAdministration = driver.Title;
                String PageURLOfMentalHealthAdministration = driver.Url;
                Assert.AreEqual("Home - Mental Health Administration - LibGuides at Northcentral University", PageTitleOfMentalHealthAdministration);
                Assert.AreEqual("https://ncu.libguides.com/mentalhealthadmin", PageURLOfMentalHealthAdministration);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Mental Health Administration");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Mental Health Policy and Practice");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfMentalHealthPolicyandPractice = driver.Title;
                String PageURLOfMentalHealthPolicyandPractice = driver.Url;
                Assert.AreEqual("Home - Mental Health Policy and Practice - LibGuides at Northcentral University", PageTitleOfMentalHealthPolicyandPractice);
                Assert.AreEqual("https://ncu.libguides.com/mentalhealthpolicy", PageURLOfMentalHealthPolicyandPractice);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Mental Health Policy and Practice");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("MFS Evidence Based Practice");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfMFSEvidenceBasedPractice = driver.Title;
                String PageURLOfMFSEvidenceBasedPractice = driver.Url;
                Assert.AreEqual("Home - MFS Evidence Based Practice - LibGuides at Northcentral University", PageTitleOfMFSEvidenceBasedPractice);
                Assert.AreEqual("https://ncu.libguides.com/mfsevidencebasedpractice", PageURLOfMFSEvidenceBasedPractice);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("MFS Evidence Based Practice");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Military Family Therapy");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfMilitaryFamilyTherapy = driver.Title;
                String PageURLOfMilitaryFamilyTherapy = driver.Url;
                Assert.AreEqual("Home - Military Family Therapy - LibGuides at Northcentral University", PageTitleOfMilitaryFamilyTherapy);
                Assert.AreEqual("https://ncu.libguides.com/militaryfamilytherapy", PageURLOfMilitaryFamilyTherapy);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Military Family Therapy");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Nonprofit Management");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfNonprofitManagement = driver.Title;
                String PageURLOfNonprofitManagement = driver.Url;
                Assert.AreEqual("Home - Nonprofit Management - LibGuides at Northcentral University", PageTitleOfNonprofitManagement);
                Assert.AreEqual("https://ncu.libguides.com/nonprofit", PageURLOfNonprofitManagement);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Nonprofit Management");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Nursing Education");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfNursingEducation = driver.Title;
                String PageURLOfNursingEducation = driver.Url;
                Assert.AreEqual("Home - Nursing Education - LibGuides at Northcentral University", PageTitleOfNursingEducation);
                Assert.AreEqual("https://ncu.libguides.com/nursingeducation", PageURLOfNursingEducation);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Nursing Education");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Nursing Leadership");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfNursingLeadership = driver.Title;
                String PageURLOfNursingLeadership = driver.Url;
                Assert.AreEqual("Home - Nursing Leadership - LibGuides at Northcentral University", PageTitleOfNursingLeadership);
                Assert.AreEqual("https://ncu.libguides.com/nursingleadership", PageURLOfNursingLeadership);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Nursing Leadership");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Open Access Resources");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfOpenAccessResources = driver.Title;
                String PageURLOfOpenAccessResources = driver.Url;
                Assert.AreEqual("OAR Home - Open Access Resources - LibGuides at Northcentral University", PageTitleOfOpenAccessResources);
                Assert.AreEqual("https://ncu.libguides.com/oar", PageURLOfOpenAccessResources);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Open Access Resources");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Organizational Leadership");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfOrganizationalLeadership = driver.Title;
                String PageURLOfOrganizationalLeadership = driver.Url;
                Assert.AreEqual("Home - Organizational Leadership - LibGuides at Northcentral University", PageTitleOfOrganizationalLeadership);
                Assert.AreEqual("https://ncu.libguides.com/leadership", PageURLOfOrganizationalLeadership);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Organizational Leadership");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Organizational Leadership - Education");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfOrganizationalLeadershipEducation = driver.Title;
                String PageURLOfOrganizationalLeadershipEducation = driver.Url;
                Assert.AreEqual("Home - Organizational Leadership - Education - LibGuides at Northcentral University", PageTitleOfOrganizationalLeadershipEducation);
                Assert.AreEqual("https://ncu.libguides.com/organizationalleadershipedu", PageURLOfOrganizationalLeadershipEducation);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Organizational Leadership - Education");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Organizing Research & Citations");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfOrganizingResearchCitations = driver.Title;
                String PageURLOfOrganizingResearchCitations = driver.Url;
                Assert.AreEqual("Home - Organizing Research & Citations - LibGuides at Northcentral University", PageTitleOfOrganizingResearchCitations);
                Assert.AreEqual("https://ncu.libguides.com/organize_research", PageURLOfOrganizingResearchCitations);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Organizing Research & Citations");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Performance Improvement");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfPerformanceImprovement = driver.Title;
                String PageURLOfPerformanceImprovement = driver.Url;
                Assert.AreEqual("Home - Performance Improvement - LibGuides at Northcentral University", PageTitleOfPerformanceImprovement);
                Assert.AreEqual("https://ncu.libguides.com/pi", PageURLOfPerformanceImprovement);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Performance Improvement");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("PK-12 Policy");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfPK12Policy = driver.Title;
                String PageURLOfPK12Policy = driver.Url;
                Assert.AreEqual("Home - PK-12 Policy - LibGuides at Northcentral University", PageTitleOfPK12Policy);
                Assert.AreEqual("https://ncu.libguides.com/pk12policy", PageURLOfPK12Policy);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("PK-12 Policy");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("PK-12 Principal Leadership");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfPK12PrincipalLeadership = driver.Title;
                String PageURLOfPK12PrincipalLeadership = driver.Url;
                Assert.AreEqual("Home - PK-12 Principal Leadership - LibGuides at Northcentral University", PageTitleOfPK12PrincipalLeadership);
                Assert.AreEqual("https://ncu.libguides.com/principalleadership", PageURLOfPK12PrincipalLeadership);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("PK-12 Principal Leadership");



                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Project Management");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfProjectManagement = driver.Title;
                String PageURLOfProjectManagement = driver.Url;
                Assert.AreEqual("Home - Project Management - LibGuides at Northcentral University", PageTitleOfProjectManagement);
                Assert.AreEqual("https://ncu.libguides.com/pm", PageURLOfProjectManagement);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Project Management");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Psychotherapy");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfPsychotherapy = driver.Title;
                String PageURLOfPsychotherapy = driver.Url;
                Assert.AreEqual("Home - Psychotherapy - LibGuides at Northcentral University", PageTitleOfPsychotherapy);
                Assert.AreEqual("https://ncu.libguides.com/psychotherapy", PageURLOfPsychotherapy);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Psychotherapy");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Public Administration");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfPublicAdministration = driver.Title;
                String PageURLOfPublicAdministration = driver.Url;
                Assert.AreEqual("Home - Public Administration - LibGuides at Northcentral University", PageTitleOfPublicAdministration);
                Assert.AreEqual("https://ncu.libguides.com/public_admin", PageURLOfPublicAdministration);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Public Administration");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Reading Education");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfReadingEducation = driver.Title;
                String PageURLOfReadingEducation = driver.Url;
                Assert.AreEqual("Home - Reading Education - LibGuides at Northcentral University", PageTitleOfReadingEducation);
                Assert.AreEqual("https://ncu.libguides.com/readingeducation", PageURLOfReadingEducation);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Reading Education");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("RefWorks");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfRefWorks = driver.Title;
                String PageURLOfRefWorks = driver.Url;
                Assert.AreEqual("Welcome to RefWorks! - RefWorks - LibGuides at Northcentral University", PageTitleOfRefWorks);
                Assert.AreEqual("https://ncu.libguides.com/organize_research/refworks", PageURLOfRefWorks);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("RefWorks");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Research Consultations");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfResearchConsultations = driver.Title;
                String PageURLOfResearchConsultations = driver.Url;
                Assert.AreEqual("Home - Research Consultations - LibGuides at Northcentral University", PageTitleOfResearchConsultations);
                Assert.AreEqual("https://ncu.libguides.com/researchconsultations", PageURLOfResearchConsultations);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Research Consultations");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Research Methods  & Design");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfResearchMethodsDesign = driver.Title;
                String PageURLOfResearchMethodsDesign = driver.Url;
                Assert.AreEqual("Research Methodology - Research Methods & Design - LibGuides at Northcentral University", PageTitleOfResearchMethodsDesign);
                Assert.AreEqual("https://ncu.libguides.com/methods", PageURLOfResearchMethodsDesign);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Research Methods  & Design");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Research Process");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfResearchProcess = driver.Title;
                String PageURLOfResearchProcess = driver.Url;
                Assert.AreEqual("Home - Research Process - LibGuides at Northcentral University", PageTitleOfResearchProcess);
                Assert.AreEqual("https://ncu.libguides.com/researchprocess", PageURLOfResearchProcess);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Research Process");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("School of Health Sciences");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfSchoolofHealthSciences = driver.Title;
                String PageURLOfSchoolofHealthSciences = driver.Url;
                Assert.AreEqual("Home - School of Health Sciences - LibGuides at Northcentral University", PageTitleOfSchoolofHealthSciences);
                Assert.AreEqual("https://ncu.libguides.com/SoHSResources", PageURLOfSchoolofHealthSciences);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("School of Health Sciences");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("School Safety");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfSchoolSafety = driver.Title;
                String PageURLOfSchoolSafety = driver.Url;
                Assert.AreEqual("Home - School Safety - LibGuides at Northcentral University", PageTitleOfSchoolSafety);
                Assert.AreEqual("https://ncu.libguides.com/schoolsafety", PageURLOfSchoolSafety);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("School Safety");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Secondary Education");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfSecondaryEducation = driver.Title;
                String PageURLOfSecondaryEducation = driver.Url;
                Assert.AreEqual("Home - Secondary Education - LibGuides at Northcentral University", PageTitleOfSecondaryEducation);
                Assert.AreEqual("https://ncu.libguides.com/secondaryeducation", PageURLOfSecondaryEducation);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Secondary Education");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Small Business Development & Entrepreneurship");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfSmallBusinessDevelopmentEntrepreneurship = driver.Title;
                String PageURLOfSmallBusinessDevelopmentEntrepreneurship = driver.Url;
                Assert.AreEqual("Home - Small Business Development & Entrepreneurship - LibGuides at Northcentral University", PageTitleOfSmallBusinessDevelopmentEntrepreneurship);
                Assert.AreEqual("https://ncu.libguides.com/smallbusiness", PageURLOfSmallBusinessDevelopmentEntrepreneurship);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Small Business Development & Entrepreneurship");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Social Emotional Learning");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfSocialEmotionalLearning = driver.Title;
                String PageURLOfSocialEmotionalLearning = driver.Url;
                Assert.AreEqual("Home - Social Emotional Learning - LibGuides at Northcentral University", PageTitleOfSocialEmotionalLearning);
                Assert.AreEqual("https://ncu.libguides.com/socialemotional", PageURLOfSocialEmotionalLearning);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Social Emotional Learning");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Social Work");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfSocialWork = driver.Title;
                String PageURLOfSocialWork = driver.Url;
                Assert.AreEqual("Home - Social Work - LibGuides at Northcentral University", PageTitleOfSocialWork);
                Assert.AreEqual("https://ncu.libguides.com/socialwork", PageURLOfSocialWork);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Social Work");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Special Education");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfSpecialEducation = driver.Title;
                String PageURLOfSpecialEducation = driver.Url;
                Assert.AreEqual("Home - Special Education - LibGuides at Northcentral University", PageTitleOfSpecialEducation);
                Assert.AreEqual("https://ncu.libguides.com/specialeducation", PageURLOfSpecialEducation);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Special Education");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Sport and Athletic Management");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfSportandAthleticManagement = driver.Title;
                String PageURLOfSportandAthleticManagement = driver.Url;
                Assert.AreEqual("Home - Sport and Athletic Management - LibGuides at Northcentral University", PageTitleOfSportandAthleticManagement);
                Assert.AreEqual("https://ncu.libguides.com/sportmanagement", PageURLOfSportandAthleticManagement);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Sport and Athletic Management");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Strategic Management");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfStrategicManagement = driver.Title;
                String PageURLOfStrategicManagement = driver.Url;
                Assert.AreEqual("Home - Strategic Management - LibGuides at Northcentral University", PageTitleOfStrategicManagement);
                Assert.AreEqual("https://ncu.libguides.com/strategic", PageURLOfStrategicManagement);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Strategic Management");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Systemic Leadership");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfSystemicLeadership = driver.Title;
                String PageURLOfSystemicLeadership = driver.Url;
                Assert.AreEqual("Home - Systemic Leadership - LibGuides at Northcentral University", PageTitleOfSystemicLeadership);
                Assert.AreEqual("https://ncu.libguides.com/systemicleadership", PageURLOfSystemicLeadership);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Systemic Leadership");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Systemic Sex Therapy");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfSystemicSexTherapy = driver.Title;
                String PageURLOfSystemicSexTherapy = driver.Url;
                Assert.AreEqual("Home - Systemic Sex Therapy - LibGuides at Northcentral University", PageTitleOfSystemicSexTherapy);
                Assert.AreEqual("https://ncu.libguides.com/systemicsextherapy", PageURLOfSystemicSexTherapy);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Systemic Sex Therapy");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Systemic Treatment of Addictions");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfSystemicTreatmentofAddictions = driver.Title;
                String PageURLOfSystemicTreatmentofAddictions = driver.Url;
                Assert.AreEqual("Home - Systemic Treatment of Addictions - LibGuides at Northcentral University", PageTitleOfSystemicTreatmentofAddictions);
                Assert.AreEqual("https://ncu.libguides.com/systemictreatmentaddictions", PageURLOfSystemicTreatmentofAddictions);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Systemic Treatment of Addictions");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Teaching Internationally");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfTeachingInternationally = driver.Title;
                String PageURLOfTeachingInternationally = driver.Url;
                Assert.AreEqual("Home - Teaching Internationally - LibGuides at Northcentral University", PageTitleOfTeachingInternationally);
                Assert.AreEqual("https://ncu.libguides.com/teachinginternationally", PageURLOfTeachingInternationally);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Teaching Internationally");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Technology Management");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfTechnologyManagement = driver.Title;
                String PageURLOfTechnologyManagement = driver.Url;
                Assert.AreEqual("Home - Technology Management - LibGuides at Northcentral University", PageTitleOfTechnologyManagement);
                Assert.AreEqual("https://ncu.libguides.com/TM", PageURLOfTechnologyManagement);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Technology Management");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Training & Development");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfTrainingDevelopment = driver.Title;
                String PageURLOfTrainingDevelopment = driver.Url;
                Assert.AreEqual("Home - Training & Development - LibGuides at Northcentral University", PageTitleOfTrainingDevelopment);
                Assert.AreEqual("https://ncu.libguides.com/trainingdevelopment", PageURLOfTrainingDevelopment);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Training & Development");


                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Trauma and Disaster Relief");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfTraumaandDisasterRelief = driver.Title;
                String PageURLOfTraumaandDisasterRelief = driver.Url;
                Assert.AreEqual("Home - Trauma and Disaster Relief - LibGuides at Northcentral University", PageTitleOfTraumaandDisasterRelief);
                Assert.AreEqual("https://ncu.libguides.com/disasterrelief", PageURLOfTraumaandDisasterRelief);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Trauma and Disaster Relief");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Trauma and Family Therapy");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfTraumaandFamilyTherapy = driver.Title;
                String PageURLOfTraumaandFamilyTherapy = driver.Url;
                Assert.AreEqual("Home - Trauma and Family Therapy - LibGuides at Northcentral University", PageTitleOfTraumaandFamilyTherapy);
                Assert.AreEqual("https://ncu.libguides.com/traumafamilytherapy", PageURLOfTraumaandFamilyTherapy);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Trauma and Family Therapy");

                MiddleMenuObjects.switchingtoiFrameLibraryGuide();
                MiddleMenuObjects.dropdownSelectvaluesFromSelectAGuide("Trauma-Focused Systemic Therapy");
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.Clicking_btnGoInLibraryGuide();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String PageTitleOfTraumaFocusedSystemicTherapy = driver.Title;
                String PageURLOfTraumaFocusedSystemicTherapy = driver.Url;
                Assert.AreEqual("Home - Trauma-Focused Systemic Therapy - LibGuides at Northcentral University", PageTitleOfTraumaFocusedSystemicTherapy);
                Assert.AreEqual("https://ncu.libguides.com/traumasystemictherapy", PageURLOfTraumaFocusedSystemicTherapy);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Trauma-Focused Systemic Therapy");

 

                /**** Ask a Librarian Tab *****/

                MiddleMenuObjects.Clicking_AskAlibrarianTab();
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                MiddleMenuObjects.VerifyingTextInAskAlibrarianTab();

                MiddleMenuObjects.EnterKeywordInAskALibrarianTextBox();

                MiddleMenuObjects.Clicking_btnAskInAskAlibrarianTab();

                driver.SwitchTo().Window(driver.WindowHandles.Last());
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                String PageRedirectToanotherTabURL = driver.Url;

                if (environmentSetup == "QA")
                {
                    Assert.AreEqual("https://ncu.libanswers.com/search/?q=test+information&faqid=0&src=4&t=0&refer=https%3A%2F%2Flibrary.qa1.ncu.edu%2F", PageRedirectToanotherTabURL);
                }
                if (environmentSetup == "QA2")
                {
                    Assert.AreEqual("https://ncu.libanswers.com/search/?q=test+information&faqid=0&src=4&t=0&refer=https%3A%2F%2Flibrary.qa2.ncu.edu%2F", PageRedirectToanotherTabURL);
                }
                if (environmentSetup == "UAT")
                {
                    Assert.AreEqual("https://ncu.libanswers.com/search/?q=test+information&faqid=0&src=4&t=0&refer=https%3A%2F%2Flibrary.uat1.ncu.edu%2F", PageRedirectToanotherTabURL);
                }
                String PageTitleofRoadRunnerSerach = driver.Title;
                Assert.AreEqual("NCU Library - Ask Us!", PageTitleofRoadRunnerSerach);
                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                MiddleMenuObjects.Clicking_linkRoadrunnerSearchInAskAlibrarianTab();

                driver.SwitchTo().Window(driver.WindowHandles.Last());
                PageServices.WaitForPageToCompletelyLoaded(driver, 90);
                String PageRedirectToanotherTabRoadRunnerSearchlinkClickedURL = driver.Url;
                String PageRedirectToanotherTabRoadRunnerSearchlinkClickedTitle = driver.Title;

                Assert.AreEqual("What is Roadrunner Search? How do I use it? - Ask Us!", PageRedirectToanotherTabRoadRunnerSearchlinkClickedTitle);
                Assert.AreEqual("https://ncu.libanswers.com/faq/168487", PageRedirectToanotherTabRoadRunnerSearchlinkClickedURL);

                driver.SwitchTo().Window(driver.WindowHandles.Last()).Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Console.WriteLine("Ask Librarian END");

                Console.WriteLine("End MiddleMenuTest_LibGuides");

                //LogOut

                //LoginToLibrary.Clicking_LogoutLink();
                //Console.WriteLine("LogOut Success");

            }

            catch (Exception ex)
            {
                Console.WriteLine("RESULT: FAILED - CHECK THE LOG FILE");
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                string path = errorPath + TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".png";
                ss.SaveAsFile(path);
                TestContext.AddTestAttachment(path);
                test.Fail(ex.StackTrace);
                ErrorLog.SaveExeptionToLog(ex, TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup, errorPath + TestContext.CurrentContext.Test.MethodName + "_" + environmentSetup + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xml");
                //test.Fail(ex.StackTrace);
                //ErrorLog.SaveExeptionToLog(ex, TestContext.CurrentContext.Test.Name + "_" + environmentSetup, errorPath + TestContext.CurrentContext.Test.Name + "_" + environmentSetup + ".xml");
            }
        }




        [TearDown]
        public void ShutDown()
        {
            try
            {
                driver.Close();
                driver.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine(TestContext.CurrentContext.Result.Message, e.Message);
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}

