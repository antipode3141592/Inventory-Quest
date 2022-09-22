using Data.Encounters;
using NUnit.Framework;

namespace InventoryQuest.Testing
{
    [TestFixture]
    public class TestPath
    {
        IPath path;
        IPathStats stats;
        IPathDataSource dataSource;
        private readonly string pathId = "intro_path";

        [SetUp]
        public void SetUp()
        {
            dataSource = new PathDataSourceTest();
            stats = dataSource.GetPathById(pathId);
            path = PathFactory.GetPath(stats: stats);
        }


        [Test]
        public void PathCreateSuccess()
        {
            Assert.That(path is not null);
        }

        [Test]
        public void PathHasNonEmptyEncounters()
        {
            if (path.EncounterIds.Count == 0) Assert.Fail();
            foreach (var encounter in path.EncounterIds)
            {
                if (encounter == "") Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void PathStatsCorrect()
        {
            if (path.Id != stats.Id) Assert.Fail();
            if (path.Name != stats.Name) Assert.Fail();
            if (path.StartLocationId != stats.StartLocationId) Assert.Fail();
            if (path.EndLocationId != stats.EndLocationId) Assert.Fail();
            Assert.Pass();
        }
    }
}