namespace GreaterHeights.MonkeyTests.PageObjects
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    public class HomePage : PageBase
    {
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            var wait = new WebDriverWait(this.driver, TestTimeout.Timeout);
            wait.Until(d => d.Title.Contains("Home"));
        }
        
        //public InstanceGridPage ClickInstanceMaintenance()
        //{
        //    IWebElement instanceMaintMenuItem = this.driver.FindElement(By.LinkText("Instance Maintenance"));

        //    string href = instanceMaintMenuItem.GetAttribute("href");

        //    this.driver.Navigate().GoToUrl(href);

        //    return new InstanceGridPage(this.driver);
        //}
    }
}