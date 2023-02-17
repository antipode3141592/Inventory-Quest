using InventoryQuest.Managers;
using Zenject;

public class PartyManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IPartyManager>().To<PartyManager>()
            .FromComponentInHierarchy().AsSingle();
    }
}
