using Zenject;

namespace InventoryQuest.UI
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MenuController>()
                    .FromComponentInHierarchy().AsSingle();
            Container.Bind<ContainerDisplayController>()
                    .FromComponentInHierarchy().AsSingle();
            Container.Bind<CharacterStatsDisplay>()
                    .FromComponentInHierarchy().AsSingle();
        }
    }
}
