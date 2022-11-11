using Data.Characters;
using Zenject;

public class CharacterInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ICharacterDataSource>()
            .To<CharacterDataSourceSO>().FromComponentInHierarchy().AsSingle();
    }
}
