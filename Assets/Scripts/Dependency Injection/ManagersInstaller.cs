using Data.Characters;
using Data.Encounters;
using Data.Items;
using Data.Rewards;
using InventoryQuest.Managers;

using Zenject;

namespace InventoryQuest
{
    public class ManagersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
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
            
        }
    }
}