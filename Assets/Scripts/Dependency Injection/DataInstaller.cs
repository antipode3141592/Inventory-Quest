using Data;
using Data.Characters;
using Data.Encounters;
using Data.Items;
using Data.Locations;
using Data.Rewards;
using Data.Shapes;
using UnityEngine;
using Zenject;

namespace InventoryQuest
{
    public class DataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILocationDataSource>()
                .To<LocationDataSourceSO>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IItemDataSource>()
                .To<ItemDataSourceSO>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ICharacterDataSource>()
                .To<CharacterDataSourceSO>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IEncounterDataSource>()
                .To<EncounterDataSourceSO>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ILootTableDataSource>()
                .FromInstance(new LootTableDataSourceTest()).AsSingle();
            Container.Bind<IPathDataSource>()
                .To<PathDataSourceSO>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IGameStateDataSource>()
                .To<GameStateDataSource>().FromComponentInHierarchy().AsSingle();
        }
    }
}
