using Data.Items;
using Zenject;

namespace InventoryQuest.Testing
{
    public class ItemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IItemDataSource>()
                .To<ItemDataSourceSO>().FromComponentInHierarchy().AsSingle();
        }
    }
}
