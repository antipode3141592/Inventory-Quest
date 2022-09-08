using InventoryQuest.Managers;
using InventoryQuest.Traveling;
using Zenject;

namespace InventoryQuest
{
    public class ManagersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGroundController>().To<GroundController>()
                .FromComponentInHierarchy().AsSingle();
            Container.Bind<IPartyController>().To<TravelingPartyController>()
                .FromComponentInHierarchy().AsSingle();
            Container.Bind<IPartyManager>().To<PartyManager>()
                .FromComponentInHierarchy().AsSingle();
            Container.Bind<IGameManager>().To<GameManager>()
                .FromComponentInHierarchy().AsSingle();
            Container.Bind<IAdventureManager>().To<AdventureManager>()
                .FromComponentInHierarchy().AsSingle();
            Container.Bind<IRewardManager>().To<RewardManager>()
                .FromComponentInHierarchy().AsSingle();
            Container.Bind<IEncounterManager>().To<EncounterManager>()
                .FromComponentInHierarchy().AsSingle();
            Container.Bind<IQuestManager>().To<QuestManager>()
                .FromComponentInHierarchy().AsSingle();
        }
    }
}