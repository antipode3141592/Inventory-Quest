using InventoryQuest.Managers;
using Zenject;

namespace InventoryQuest.Testing
{
    public class HarvestManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IHarvestManager>()
                .To<HarvestManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}