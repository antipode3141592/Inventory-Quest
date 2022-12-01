using Data.Encounters;
using NUnit.Framework;

namespace InventoryQuest.Testing
{
    [TestFixture]
    public class TestPath
    {
        IPathStats stats;
        IPathDataSource dataSource;
        private readonly string pathId = "intro_path";

        [SetUp]
        public void SetUp()
        {
            dataSource = new PathDataSourceTest();
            stats = dataSource.GetPathById(pathId);
        }

        [TearDown]
        public void TearDown()
        {
            dataSource = null;
            stats = null;
        }

        [Test]
        public void PathCreateSuccess()
        {
            Assert.That(stats is not null);
        }

        [Test]
        public void PathHasNonEmptyEncounters()
        {
            if (stats.EncounterStats.Count == 0) Assert.Fail();
            foreach (var encounter in stats.EncounterStats)
            {
                if (encounter is null) Assert.Fail();
            }
            Assert.Pass();
        }
    }
}