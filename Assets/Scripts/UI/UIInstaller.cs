using Zenject;

namespace InventoryQuest.UI.Menus
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
