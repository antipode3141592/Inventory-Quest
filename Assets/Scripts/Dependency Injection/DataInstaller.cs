using Data;
using Data.Characters;
using Data.Encounters;
using Data.Items;
using Data.Locations;
using Data.Rewards;
using Zenject;

namespace InventoryQuest
{
    public class DataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILocationDataSource>()
                .To<LocationDataSourceScriptableObjectTest>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IItemDataSource>()
                .FromInstance(new ItemDataSourceTest()).AsSingle();
            Container.Bind<ICharacterDataSource>()
                .To<CharacterSODataSource>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IEncounterDataSource>()
                .To<EncounterDataSourceSOTest>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ILootTableDataSource>()
                .FromInstance(new LootTableDataSourceTest()).AsSingle();
            Container.Bind<IPathDataSource>()
                .To<PathDataSourceSOTest>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IGameStateDataSource>()
                .To<GameStateDataSource>().FromComponentInHierarchy().AsSingle();
        }
    }
}
