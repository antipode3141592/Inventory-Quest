using Data;
using Data.Characters;
using Data.Encounters;
using Data.Items;
using Data.Locations;
using Data.Quests;
using Data.Rewards;
using Zenject;

namespace InventoryQuest
{
    public class DataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {


            LocationDataSourceTest location = new LocationDataSourceTest();
            PathDataSourceTest path = new PathDataSourceTest();

            Container.Bind<ILocationDataSource>()
                .FromInstance(location).AsSingle();
            Container.Bind<IItemDataSource>()
                .FromInstance(new ItemDataSourceTest()).AsSingle();
            Container.Bind<ICharacterDataSource>()
                .FromInstance(new CharacterDataSourceTest()).AsSingle();
            Container.Bind<IEncounterDataSource>()
                .FromInstance(new EncounterDataSourceJSON()).AsSingle();
            Container.Bind<IRewardDataSource>()
                .FromInstance(new RewardDataSourceTest()).AsSingle();
            Container.Bind<ILootTableDataSource>()
                .FromInstance(new LootTableDataSourceTest()).AsSingle();
            Container.Bind<IPathDataSource>()
                .FromInstance(path).AsSingle();
            Container.Bind<IQuestDataSource>()
                .FromInstance(new QuestDataSourceTest()).AsSingle();
            Container.Bind<IGameStateDataSource>()
                .FromInstance(new GameStateDataSource(location, path)).AsSingle();
        }
    }
}
