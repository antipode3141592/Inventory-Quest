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
            Container.Bind<IItemDataSource>()
                    .FromInstance(new ItemDataSourceTest()).AsSingle().NonLazy();
            Container.Bind<ICharacterDataSource>()
                    .FromInstance(new CharacterDataSourceTest()).AsSingle().NonLazy();
            Container.Bind<IEncounterDataSource>()
                    .FromInstance(new EncounterDataSourceJSON()).AsSingle().NonLazy();
            Container.Bind<IRewardDataSource>()
                    .FromInstance(new RewardDataSourceTest()).AsSingle().NonLazy();
            Container.Bind<ILootTableDataSource>()
                    .FromInstance(new LootTableDataSourceTest()).AsSingle().NonLazy();
            Container.Bind<IPathDataSource>()
                    .FromInstance(new PathDataSourceTest()).AsSingle().NonLazy();
            Container.Bind<ILocationDataSource>()
                    .FromInstance(new LocationDataSourceTest()).AsSingle().NonLazy();
            Container.Bind<IQuestDataSource>()
                    .FromInstance(new QuestDataSourceTest()).AsSingle().NonLazy();
        }
    }
}
