using GreaterHeights.Domain;
using GreaterHeights.Interfaces;

namespace GreaterHeights.MonkeyTests
{
    using System;

    using Moq;

    using NUnit.Framework;

    using OpenQA.Selenium.Firefox;

    using TechTalk.SpecFlow;

    [Binding]
    public class SimpleStorySteps : TestsBase
    {
        private AccidentReport report = null;

        private AccidentReport savedReport = null;

        private Mock<IAccidentRepository> repoMock = null;
        
        public SimpleStorySteps() :base("GreaterHeights.WebMonkey",7128)
        {

        }

        [BeforeScenario()]
        public void Setup()
        {
            this.repoMock = new Mock<IAccidentRepository>();
            this.repoMock.SetupGet(m => m.Authorised).Returns(true);
            this.repoMock.Setup(m => m.ReportAccident(It.IsAny<AccidentReport>())).Returns(new AccidentReport { Id = 1, Description = "Someone fell over" });
        }

        [AfterScenario()]
        public void TearDown()
        {
        }

        #region Public Methods and Operators

        [Given(@"I have entered a description of the accident and a date and time")]
        public void GivenIHaveEnteredADescriptionOfTheAccidentAndADateAndTime()
        {
            report = new AccidentReport { Description = "Someone fell over", DateOfAccident = DateTime.UtcNow };
            
            Assert.True(report.DateOfAccident >= DateTime.UtcNow, "Invalid accident date");
            Assert.True(!String.IsNullOrEmpty(report.Description), "No description");
        }

        [Given(@"That I am an authorised user")]
        public void GivenThatIAmAnAuthorisedUser()
        {
            var homePage = this.NavigateToHomePage();
            
            Assert.IsTrue(repoMock.Object.Authorised, "The user is not authorised");
        }

        [Then(@"the record should be created and displayed on the screen\.")]
        public void ThenTheRecordShouldBeCreatedAndDisplayedOnTheScreen_()
        {
            Assert.True(savedReport.Description == report.Description);
        }

        [When(@"I press save")]
        public void WhenIPressSave()
        {
            savedReport = repoMock.Object.ReportAccident(report);
            Assert.True(savedReport.Id > 0, "Accident was not saved");

            repoMock.Verify();
        }

        #endregion
    }
}