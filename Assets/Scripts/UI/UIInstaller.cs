using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace InventoryQuest.UI
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MenuManager>()
                    .FromComponentInHierarchy().AsSingle();
            Container.Bind<ContainerDisplayManager>()
                    .FromComponentInHierarchy().AsSingle();
            Container.Bind<CharacterStatsDisplay>()
                    .FromComponentInHierarchy().AsSingle();
        }
    }
}
