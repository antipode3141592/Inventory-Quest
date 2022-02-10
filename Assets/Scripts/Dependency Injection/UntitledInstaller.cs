using UnityEngine;
using Zenject;
using Data.Interfaces;

namespace InventoryQuest
{
    public class UntitledInstaller : MonoInstaller<UntitledInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IDataSource>().AsSingle();


        }
    }
}