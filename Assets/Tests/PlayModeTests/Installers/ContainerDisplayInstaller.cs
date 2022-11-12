using Data.Items;
using InventoryQuest.UI;
using Zenject;

namespace InventoryQuest.Testing
{
    class ContainerDisplayInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ContainerDisplay>()
                .To<ContainerDisplay>().FromComponentInHierarchy().AsSingle();
        }
    }
}
