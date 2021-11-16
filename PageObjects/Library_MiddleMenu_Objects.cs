using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace LibraryAutomation.PageObjects
{
    class Library_MiddleMenu_Objects
    {


        IWebDriver WebDriver;
        string EnterTextInSerchfield = "test";

        public Library_MiddleMenu_Objects(IWebDriver driver)
        {
            WebDriver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(WebDriver, TimeSpan.FromSeconds(90)));
        }

        /*****Middle Menu Objects *****/

        [FindsBy(How = How.CssSelector, Using = ".glyphicon-chevron-right")]
        public IWebElement RightArrowButton { get; set; }


        [FindsBy(How = How.CssSelector, Using = ".glyphicon-chevron-left")]
        public IWebElement LeftArrowButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"myCarousel\"]/div/div[1]/a/img")]
        public IWebElement FirstCarousel { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"myCarousel\"]/div/div[2]/a/img")]
        public IWebElement SecondCarousel { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"myCarousel\"]/div/div[3]/a/img")]
        public IWebElement ThirdCarousel { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"myCarousel\"]/div/div[4]/a/img")]
        public IWebElement FourthCarousel { get; set; }

        [FindsBy(How = How.LinkText, Using = "New? Start Here!")]
        public IWebElement LinkNewStartHere { get; set; }

        [FindsBy(How = How.LinkText, Using = "Quick Start Videos")]
        public IWebElement LinkQuickStartVideos { get; set; }

        [FindsBy(How = How.LinkText, Using = "Searching 101")]
        public IWebElement LinkSearching101 { get; set; }

        [FindsBy(How = How.LinkText, Using = "Recorded Library Workshops")]
        public IWebElement LinkRecordedLibraryWorkshops { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[class] [class='col-md-6']:nth-of-type(2) .list-group-item:nth-of-type(2) [target]")]
        public IWebElement LinkResearchProcess { get; set; }

        [FindsBy(How = How.LinkText, Using = "Tutorials")]
        public IWebElement LinkTutorials{ get; set; }

        [FindsBy(How = How.CssSelector, Using = "[class='form-control col-lg-6']")]
        public IWebElement TypeKeywordHereTextField { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = ".btn-primary.btn-sm")]
        public IWebElement ButtonSubmit { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "[onclick='limittoFullText(this.form)']")]
        public IWebElement CheckBoxFullText { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[onclick='limittoScholarly(this.form)']")]
        public IWebElement CheckBoxScholarlyPeerReviewedJournals { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "[onchange='bquery']")]
        public IWebElement DropdowntSelectValueFrom { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#common_FT")]
        public IWebElement CheckBoxFullInResultsPage { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#common_RV")]
        public IWebElement CheckBoxScholarlyPeerReviewedJournalInResultsPage { get; set; }


        [FindsBy(How = How.LinkText, Using = "What is Roadrunner Search?")]
        public IWebElement WhatisRoadrunnerSearch { get; set; }

        
              [FindsBy(How = How.LinkText, Using = "Advanced Search")]
        public IWebElement AdvancedSearch { get; set; }



        [FindsBy(How = How.CssSelector, Using = "[href='#TabsId-2']")]
        public IWebElement FindaResource { get; set; }


        [FindsBy(How = How.CssSelector, Using = ".tab-content strong")]
        public IWebElement TextOnFindaResource  { get; set; }

        

         [FindsBy(How = How.CssSelector, Using = "[class='form-control col-lg-8']")]
        public IWebElement TextInTypeJournalOrBookTitleHere { get; set; }


        [FindsBy(How = How.CssSelector, Using = ".tab-content div:nth-child(3) [type='submit']")]
        public IWebElement ButtonSearch { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[href='#TabsId-4']")]
        public IWebElement AskAlibrarian { get; set; }

        
              [FindsBy(How = How.CssSelector, Using = "[class='col-md-11'] h2")]
        public IWebElement TextInAskAlibrarianTab { get; set; }

        [FindsBy(How = How.Id, Using = "s-la-content-search-query-5682")]
        public IWebElement textFieldKeywordInAskAlibrarianTab { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".input-group-btn [type]")]
        public IWebElement btnAskInAskAlibrarianTab { get; set; }

        [FindsBy(How = How.LinkText, Using = "What is Roadrunner Search?")]
        public IWebElement linkRoadrunnerSearchInAskAlibrarianTab { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[href='#TabsId-3']")]
        public IWebElement libraryGuidesTab { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".s-lg-widget-title.navbar.navbar-fixed-top")]
        public IWebElement displayedTxtLibraryGuideGuideList { get; set; }


        [FindsBy(How = How.CssSelector, Using = ".sr-only")]
        public IWebElement displayedTxtOnLibraryGuideSelectaGuide { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".form-control > option:nth-child(1)")]
        public IWebElement dropdownDefaultValuesGuideList { get; set; }


        [FindsBy(How = How.CssSelector, Using = "iframe[id^='s-lg-widget-frame']")]
        public IWebElement iframeLibraryGuides { get; set; }

        [FindsBy(How = How.CssSelector, Using = "select[id^='s-lg-sel-guide-widget-']")]
        public IWebElement SelectValueFromGuide { get; set; }

        
        [FindsBy(How = How.CssSelector, Using = "div.s-lg-widget-content>div>form > button")]

        //[FindsBy(How = How.CssSelector, Using = "#s-lg-frm-guide-widget-1594335025107 > button")]

        public IWebElement btnGoInLibraryGuide { get; set; }

        [FindsBy(How = How.Id, Using = "details-button")]
        public IWebElement btnProxyDetails { get; set; }

        [FindsBy(How = How.Id, Using = "proceed-link")]
        public IWebElement btnProxyProceed { get; set; }




        /*****Start Of  Middle Menu Methods *****/

        public void Clicking_btnGoInLibraryGuide()
        {
            btnGoInLibraryGuide.Click();

        }
        public void switchingtoiFrameLibraryGuide()
        {
         WebDriver.SwitchTo().Frame(iframeLibraryGuides);


        }
        public void Clicking_libraryGuidesTab()
        {
            libraryGuidesTab.Click();


        }

        public void MiddleMenuVerification(string txtToSearch, string titleToVerify,string PrivacyError,bool FullTextCheckBox,bool ScholarlyPeerReviewed)
        {
            Text_TypeKeywordHereTextField();
            SelectingValueFromDropdownRoadRunnerSearchTab(txtToSearch);

            //SelectingValueFromDropdownRoadRunnerSearchTab("Keyword");
            if(FullTextCheckBox)
            {
                Clicking_CheckBoxFullText();

            }
           if(ScholarlyPeerReviewed)
            {
                Clicking_CheckBoxScholarlyPeerReviewedJournals();
            }
            Clicking_ButtonSubmit();
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles.Last());
            Thread.Sleep(10000);
            if (WebDriver.Title.ToString() == PrivacyError)
            {
                Console.WriteLine("WARNING: Certificate/Proxy Server Message Displayed");
                ProxyMessage();
                //Thread.Sleep(5000);
            }          
            string PageTitleWithKeywordTestAndFullcheckboxChecked = WebDriver.Title;
            Console.WriteLine(PageTitleWithKeywordTestAndFullcheckboxChecked);
            Assert.AreEqual(titleToVerify, PageTitleWithKeywordTestAndFullcheckboxChecked);

        }

        public void VerifyingdisplayedTxtLibraryGuideGuideList()
        {
            string GetLibraryGuideGuideListText = displayedTxtLibraryGuideGuideList.GetAttribute("innerText");
            Console.WriteLine(GetLibraryGuideGuideListText);
            Assert.AreEqual("Library Guide List", GetLibraryGuideGuideListText);

        }

        public void VerifyingdisplayedTxtOnLibraryGuideSelectaGuide()
        {
            string GetdisplayedOnLibraryGuideSelectaGuideText = displayedTxtOnLibraryGuideSelectaGuide.GetAttribute("innerText");
            Console.WriteLine(GetdisplayedOnLibraryGuideSelectaGuideText);
            Assert.AreEqual("Select a Guide...", GetdisplayedOnLibraryGuideSelectaGuideText);
        }

        //public void dropdownSelectvaluesFromSelectAGuide(string selectGuideList)
        //{
        //    SelectElement GetdropdownSelectvaluesFromSelectAGuide = new SelectElement(commonDriver.FindElement(By.CssSelector("#s-lg-sel-guide-widget-1491330773671")));

        //    GetdropdownSelectvaluesFromSelectAGuide.SelectByText(selectGuideList);
        //}

        public void dropdownSelectvaluesFromSelectAGuide(string selectGuideList)
        {
            // SelectElement GetdropdownSelectvaluesFromSelectAGuide = new SelectElement(commonDriver.FindElement(By.CssSelector("#s-lg-sel-guide-widget-1491330773671")));
            new SelectElement(SelectValueFromGuide).SelectByText(selectGuideList);
           // GetdropdownSelectvaluesFromSelectAGuide.SelectByText(selectGuideList);
        }

        public void dropdownVerifyingDefaultValueInGuideList()
        {
            string GetvaluedropdownDefaultValuesGuideList = dropdownDefaultValuesGuideList.GetAttribute("innerText");
            Console.WriteLine(GetvaluedropdownDefaultValuesGuideList);
            Assert.AreEqual("Select a Guide...", GetvaluedropdownDefaultValuesGuideList);

           
        }


        public void Clicking_AskAlibrarianTab()
        {
            AskAlibrarian.Click();


        }

        public void VerifyingTextInAskAlibrarianTab()
        {
            string GetTextInAskLibrarian = TextInAskAlibrarianTab.GetAttribute("innerText");
          
            Console.WriteLine(GetTextInAskLibrarian);
            Assert.AreEqual("Ask A Librarian", GetTextInAskLibrarian);

            Console.WriteLine("Verification completed");


        }


        public void EnterKeywordInAskALibrarianTextBox()
        {
            textFieldKeywordInAskAlibrarianTab.SendKeys("test information");


        }

        public void Clicking_btnAskInAskAlibrarianTab()
        {
            btnAskInAskAlibrarianTab.Click();


        }

        public void Clicking_linkRoadrunnerSearchInAskAlibrarianTab()
        {
            linkRoadrunnerSearchInAskAlibrarianTab.Click();


        }


        public void Clicking_SearchButtonInFindResource()
        {
            ButtonSearch.Click();

            
        }


        public void Clear_TextInTypeJournalOrBookTitleHereInFindresource()
        {
            TextInTypeJournalOrBookTitleHere.Clear();


        }



        public void DropdownOptionInFindaResource(string optionsInFindResource )
        {


            SelectElement ObjectSelect= new SelectElement(WebDriver.FindElement(By.CssSelector("[name='S']")));

            ObjectSelect.SelectByText(optionsInFindResource);

        }

        public void Enter_InTypeJournalOrBookTitleHere(string EnterTextInJournalOrBookTitleHere)
        {

            TextInTypeJournalOrBookTitleHere.SendKeys(EnterTextInJournalOrBookTitleHere);


        }




        public void Clicking_AdvancedSearch()
        {

            AdvancedSearch.Click();


        }


        public void Clicking_FindaResource()
        {

            FindaResource.Click();


        }
        public void VerifyDispalyedtInTextOnFindaResource()
        {

          String GetvalueFromPage = TextOnFindaResource.GetAttribute("innerText");
            Console.WriteLine(GetvalueFromPage);
            Assert.AreEqual("Search by journal or book title NOT by article title", GetvalueFromPage);

            Console.WriteLine("matching passed2");


        }

        public void Clicking_WhatisRoadrunnerSearch()
        {

            WhatisRoadrunnerSearch.Click();


        }


        public void Clear_TypeKeywordHereTextField()
        {

            TypeKeywordHereTextField.Clear();


        }

        public void Clear_FullCheckBox()
        {

            CheckBoxFullText.Click();
            

        }


        public void Clear_BoxScholarlyPeerReviewedJournals()
        {
            CheckBoxScholarlyPeerReviewedJournals.Click();


        }
        public void Verifying_CheckBoxFullTextisCheckedInResultPageisChecked()
        {

            bool ChechekdFullinResultsPage = CheckBoxFullInResultsPage.Selected;

            Console.WriteLine(ChechekdFullinResultsPage);
            Assert.True(ChechekdFullinResultsPage);

        }

        public void Verifying_CheckBoxScholarlyPeerReviewedJournalsResultsPageisChecked()
        {

            bool CheckedScholarinResultsPage = CheckBoxScholarlyPeerReviewedJournalInResultsPage.Selected;
            Assert.True(CheckedScholarinResultsPage);
            Console.WriteLine(CheckedScholarinResultsPage);


        }
        public void Verifying_CheckBoxFullTextisCheckedInResultPageNotChecked()
        {

            bool NotCheckedFullinResultsPage = CheckBoxFullInResultsPage.Selected;
          
            Console.WriteLine(NotCheckedFullinResultsPage);
            Assert.False(NotCheckedFullinResultsPage);

        }

        public void Verifying_CheckBoxScholarlyPeerReviewedJournalsResultsPageNotChecked()
        {

            bool NotCheckedScholarinResultsPage = CheckBoxScholarlyPeerReviewedJournalInResultsPage.Selected;
            Assert.False(NotCheckedScholarinResultsPage);
            Console.WriteLine(NotCheckedScholarinResultsPage);


        }

       


        public void SelectingValueFromDropdownRoadRunnerSearchTab(String SearchPrefix)
        {
        new SelectElement(DropdowntSelectValueFrom).SelectByText(SearchPrefix);                       

        }

        public void Clicking_CheckBoxFullText()
        {

            CheckBoxFullText.Click();

        }
        public void Clicking_CheckBoxScholarlyPeerReviewedJournals()
        {

            CheckBoxScholarlyPeerReviewedJournals.Click();

        }

        public void Clicking_ButtonSubmit()
        {

            ButtonSubmit.Click();

        }
        public void Text_TypeKeywordHereTextField()
        {
            TypeKeywordHereTextField.SendKeys(EnterTextInSerchfield);
        }

        public void Clicking_NewStartHere()
        {

            LinkNewStartHere.Click();

        }
        public void Clicking_Tutorials()
        {

            LinkTutorials.Click();

        }
        public void Clicking_ReserachProcess()
        {

            LinkResearchProcess.Click();

        }
        public void Clicking_RecordedLibraryWorkshops()
        {

            LinkRecordedLibraryWorkshops.Click();

        }

        public void Clicking_Searching101()
        {

            LinkSearching101.Click();

        }

        public void Clicking_QuickStartVideos()
        {

            LinkQuickStartVideos.Click();

        }

        public void Clicking_RightArrow()
        {

            RightArrowButton.Click();
                       
        }

        public void Clicking_LeftArrow()
        {

            LeftArrowButton.Click();

        }

        public void Clicking_FirstCarousel()
        {

            FirstCarousel.Click();

        }

        public void Clicking_SecondCarousel()
        {

            SecondCarousel.Click();

        }

        public void Clicking_ThirdCarousel()
        {
             

            ThirdCarousel.Click();

        }
        public void Clicking_FourthCarousel()
        {


            FourthCarousel.Click();

        }
        public void ProxyMessage()
        {
            btnProxyDetails.Click();
            btnProxyProceed.Click();
            Thread.Sleep(6000);
        }

    }
}


     

        




   

