using Data;
using Data.Encounters;
using Data.Interfaces;
using InventoryQuest.Managers;
using InventoryQuest.UI;
using Zenject;

namespace InventoryQuest
{
    public class ManagersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IDataSource>().FromInstance(new DataSourceTest()).AsSingle().NonLazy();
            Container.Bind<IEncounterDataSource>().FromInstance(new EncounterDataSourceTest()).AsSingle().NonLazy();
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PartyManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<EncounterManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ContainerDisplayManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CharacterStatsDisplay>().FromComponentInHierarchy().AsSingle();
        }
    }
}