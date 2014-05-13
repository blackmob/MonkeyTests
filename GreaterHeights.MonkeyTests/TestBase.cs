using OpenQA.Selenium.IE;

namespace GreaterHeights.MonkeyTests
{
    using System;
    using System.Security.Claims;
    using System.Threading;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;

    using GreaterHeights.MonkeyTests.PageObjects;
    using System.Diagnostics;
    using System.IO;

    public class TestsBase : IDisposable
    {
        protected ClaimsPrincipal Principal;
        private readonly IWebDriver driver;
        private int iisPort = 2020;
        private string applicationName;
        private Process iisProcess;

        public TestsBase(string applicationName, int port) {
            this.iisPort = port;
            this.applicationName = applicationName;    
            this.driver = new InternetExplorerDriver();
            this.Principal = Thread.CurrentPrincipal as ClaimsPrincipal;
            
            try
            {
                // Start IISExpress
                this.StartIIS();
                this.Authenticate();
            }
            catch (Exception e)
            {
                this.driver.Close();
                this.driver.Quit();
                // Ensure IISExpress is stopped
                if (iisProcess.HasExited == false)
                {
                    iisProcess.Kill();
                }

                Console.WriteLine(e.ToString());
            }
        }
        
        public void Authenticate()
        {
            Console.WriteLine("Page.Authenticate(). Current URL: " + this.driver.Url);
            Console.WriteLine("Page.Authenticate(). Navigate to " + string.Format("http://localhost:{0}", iisPort));
            this.driver.Navigate().GoToUrl(string.Format("http://localhost:{0}", iisPort));
        }

        public void Dispose()
        {
            this.driver.Close();
            this.driver.Quit();
            // Ensure IISExpress is stopped
            if (iisProcess.HasExited == false)
            {
                iisProcess.Kill();
            }
        }

        protected HomePage NavigateToHomePage()
        {
            this.driver.Navigate().GoToUrl(string.Format("http://localhost:{0}", iisPort));
            return new HomePage(this.driver);
        } 

        private void StartIIS() {
            var applicationPath = GetApplicationPath(applicationName);
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
 
            iisProcess = new Process();
            iisProcess.StartInfo.FileName = Path.Combine(programFiles , @"IIS Express\iisexpress.exe");
            iisProcess.StartInfo.Arguments = string.Format(@"/path:{0} /port:{1}", applicationPath, iisPort);
            iisProcess.Start();
        } 
 
        protected virtual string GetApplicationPath(string applicationName) {
            var solutionFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)));
            return Path.Combine(solutionFolder, applicationName);
        } 
 
        public string GetAbsoluteUrl(string relativeUrl) {
            if (!relativeUrl.StartsWith("/")) {
                relativeUrl = "/" + relativeUrl;
            }
            return String.Format("http://localhost:{0}{1}", iisPort, relativeUrl);
        }
    }
}
