using Data;
using Data.Encounters;
using Data.Interfaces;
using Data.Rewards;
using InventoryQuest.Managers;
using InventoryQuest.UI;
using Zenject;

namespace InventoryQuest
{
    public class ManagersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IItemDataSource>()
                .FromInstance(new ItemDataSourceTest()).AsSingle().NonLazy();
            Container.Bind<ICharacterDataSource>()
                .FromInstance(new CharacterDataSourceTest()).AsSingle().NonLazy();
            Container.Bind<IEncounterDataSource>()
                .FromInstance(new EncounterDataSourceTest()).AsSingle().NonLazy();
            Container.Bind<IRewardDataSource>()
                .FromInstance(new RewardDataSourceTest()).AsSingle().NonLazy();
            Container.Bind<ILootTableDataSource>()
                .FromInstance(new LootTableDataSourceTest()).AsSingle().NonLazy();
            Container.Bind<GameManager>()
                .FromComponentInHierarchy().AsSingle();
            Container.Bind<RewardManager>()
                .FromComponentInHierarchy().AsSingle();
            Container.Bind<PartyManager>()
                .FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<EncounterManager>()
                .FromComponentInHierarchy().AsSingle();
            Container.Bind<ContainerDisplayManager>()
                .FromComponentInHierarchy().AsSingle();
            Container.Bind<CharacterStatsDisplay>()
                .FromComponentInHierarchy().AsSingle();
        }
    }
}