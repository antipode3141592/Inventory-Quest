using InventoryQuest.Managers;
using Zenject;

namespace InventoryQuest.Testing
{
    public class AdventureManagerStubInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAdventureManager>()
                .To<AdventureManagerStub>().FromComponentInHierarchy().AsSingle();
        }
    }
}
