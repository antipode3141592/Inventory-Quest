using InventoryQuest.Managers;
using Zenject;

namespace InventoryQuest
{
    public class InputManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputManager>()
                .To<InputManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}
