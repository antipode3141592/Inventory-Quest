using InventoryQuest.Managers;
using InventoryQuest.Testing;
using Zenject;

namespace InventoryQuest.Installers
{
    public class InputManagerStubInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputManager>()
                .To<InputManagerStub>().FromComponentInHierarchy().AsSingle();
        }
    }
}
