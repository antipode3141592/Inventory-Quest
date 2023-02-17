using InventoryQuest.Managers;
using Zenject;

namespace InventoryQuest.Testing
{
    public class PartyManagerStubInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPartyManager>()
                .To<PartyManagerStub>().FromComponentInHierarchy().AsSingle();
        }
    }
}
