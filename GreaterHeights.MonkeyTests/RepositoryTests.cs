namespace Tlc.MonkeyTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Remoting;

    using GreaterHeights.Domain;
    using GreaterHeights.Interfaces;
    using GreaterHeights.MonkeyTests;
    using GreaterHeights.Repositories;

    using Moq;

    using NUnit.Framework;

    using Rhino.Mocks;
    using Rhino.Mocks.Expectations;

    [TestFixture]
    public class RepositoryTests
    {
        #region Public Methods and Operators

        [Test]
        public void CanQueryReports()
        {
            var context = new Moq.Mock<IMonkeyContext>();

            var reports = new List<AccidentReport>();
            reports.Add(
                new AccidentReport { Id = 1, DateOfAccident = DateTime.UtcNow.AddDays(1), Description = "Accident 1" });
            reports.Add(
                new AccidentReport { Id = 2, DateOfAccident = DateTime.UtcNow.AddDays(2), Description = "Accident 2" });
            reports.Add(
                new AccidentReport { Id = 3, DateOfAccident = DateTime.UtcNow.AddDays(3), Description = "Accident 3" });
            reports.Add(
                new AccidentReport { Id = 4, DateOfAccident = DateTime.UtcNow.AddDays(4), Description = "Accident 4" });

            context.SetupGet(c => c.Reports).Returns(Helpers.CreateMockDbSet(reports).Object);

            var repo = new AccidentRepository(context.Object);

            var returnedReports = repo.GetAllAccidents().Where(a => a.Id == 1);

            Assert.AreEqual(returnedReports.First().Description, "Accident 1");

            Assert.AreEqual(returnedReports.Count(), 1);
        }

        [Test]
        public void CanSaveNewReports()
        {
            var context = new Mock<IMonkeyContext>();
            context.Setup(c => c.Reports.Add(It.IsAny<AccidentReport>()));
            context.Setup(c => c.Save()).Returns(1);

            var repo = new AccidentRepository(context.Object);

            var accident = new AccidentReport { Id = 1, DateOfAccident = DateTime.UtcNow.AddDays(1), Description = "Accident 1" };

            accident = repo.ReportAccident(accident);

            context.VerifyAll();

            Assert.AreEqual(accident.Id, 1);
        }

        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void TeardDown()
        {
        }

        #endregion
    }
}