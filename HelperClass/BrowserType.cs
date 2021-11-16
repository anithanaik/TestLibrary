using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;


namespace LibraryAutomation

{
    public static class BrowserType
    {
        public static IWebDriver SelectBrowser(string browser)
        {
            IWebDriver driver;
            string binPath = System.AppDomain.CurrentDomain.BaseDirectory;            
            switch (browser)
            {
                case "chrome":
                    driver = new ChromeDriver();
                    break;

                case "iexplorer":
                    driver = new InternetExplorerDriver();
                    break;                
                default:
                    driver = new FirefoxDriver();
                    break;
            }
            return driver;
        }
    }
}
