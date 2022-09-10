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
            PathDataSourceTest path = new PathDataSourceTest();
            EncounterDataSourceJSON encounterSource = new EncounterDataSourceJSON();

            Container.Bind<ILocationDataSource>()
                .To<LocationDataSourceScriptableObjectTest>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IItemDataSource>()
                .FromInstance(new ItemDataSourceTest()).AsSingle();
            Container.Bind<ICharacterDataSource>()
                .FromInstance(new CharacterDataSourceTest()).AsSingle();
            Container.Bind<IEncounterDataSource>()
                .FromInstance(encounterSource).AsSingle();
            Container.Bind<IRewardDataSource>()
                .FromInstance(new RewardDataSourceTest()).AsSingle();
            Container.Bind<ILootTableDataSource>()
                .FromInstance(new LootTableDataSourceTest()).AsSingle();
            Container.Bind<IPathDataSource>()
                .FromInstance(path).AsSingle();
            Container.Bind<IQuestDataSource>()
                .To<QuestDataSourceSOTest>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IGameStateDataSource>()
                .To<GameStateDataSource>().FromComponentInHierarchy().AsSingle();
        }
    }
}
