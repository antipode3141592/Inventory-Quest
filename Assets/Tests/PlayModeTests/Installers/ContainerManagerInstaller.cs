using InventoryQuest.Managers;
using Zenject;

public class ContainerManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IContainerManager>().To<ContainerManager>()
            .FromComponentInHierarchy().AsSingle();
    }

}
