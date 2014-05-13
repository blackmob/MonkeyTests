using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreaterHeights.MonkeyTests.PageObjects
{
    using OpenQA.Selenium;

    public class PageBase
    {

        /// <summary>
        ///     The web driver.
        /// </summary>
        protected IWebDriver driver;
        
        /// <summary>
        ///     The navigate to home page.
        /// </summary>
        /// <returns>
        ///     The <see cref="HomePage" />.
        /// </returns>
        public HomePage NavigateToHomePage()
        {
            IWebElement homeMenuItem = this.driver.FindElement(By.LinkText("Home"));
            homeMenuItem.Click();
            return new HomePage(this.driver);
        }
    }
}
