using InventoryQuest.Managers;
using Zenject;

namespace InventoryQuest.Testing
{
    public class GameManagerStubInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameManager>()
                .To<GameManagerStub>().FromComponentInHierarchy().AsSingle();
        }
    }
}
