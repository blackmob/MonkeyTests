using System;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Moq;
using MvcContrib.TestHelper;
using NUnit.Framework;
using Rhino.Mocks;
using TechTalk.SpecFlow;
using GreaterHeights.Domain;
using GreaterHeights.Interfaces;
using GreaterHeights.WebMonkey.Controllers;

namespace GreaterHeights.MonkeyTests
{
    using MvcContrib;

    [Binding]
    public class SimpleStoryControllerTestSteps 
    {
        private AccidentReport report = null;

        private AccidentReport savedReport = null;

        private Mock<IAccidentRepository> repoMock = null;

        private ViewResult result;

        [BeforeScenario()]
        public void Setup()
        {
        }

        [AfterScenario()]
        public void TearDown()
        {
        }

        [Given(@"That I am a user authorised to create Accident Reports")]
        public void GivenThatIamauserauthorisedtocreateAccidentReports()
        {
            this.repoMock = new Mock<IAccidentRepository>();
            this.repoMock.SetupGet(m => m.Authorised).Returns(true);
            this.repoMock.Setup(m => m.ReportAccident(It.IsAny<AccidentReport>())).Returns(new AccidentReport { Id = 1, Description = "Someone fell over" });
 
            Assert.IsTrue(repoMock.Object.Authorised, "Not authorised");
        }

        [When(@"I navigate to the accident entry page")]
        public void WhenINavigateToTheAccidentEntryPage()
        {
            var accidentController = new AccidentController(repoMock.Object);

            this.result = accidentController.Create() as ViewResult;

            this.result.AssertResultIs<ViewResult>();
        }

        [Then(@"the result should be the accident entry view")]
        public void ThenTheResultShouldBeTheAccidentEntryView()
        {
            var model = this.result.Model as AccidentReport;

            this.result.AssertViewRendered();

            Assert.IsInstanceOf<AccidentReport>(model);
        }

        [Given(@"That I am a user not authorised to create Accident Reports")]
        public void GivenThatIAmAUserNotAuthorisedToCreateAccidentReports()
        {
            this.repoMock = new Mock<IAccidentRepository>();
            this.repoMock.SetupGet(m => m.Authorised).Returns(false);
            Assert.IsFalse(this.repoMock.Object.Authorised,"User is authorised");
        }

        [Then(@"the result shoulbe be a (.*)")]
        public void ThenTheResultShoulbeBeA(int p0)
        {
            var model = this.result.Model as AccidentReport;
            Assert.IsNull(model);
        }
    }
}
