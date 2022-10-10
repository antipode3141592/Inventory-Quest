using Zenject;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Data.Items;

namespace InventoryQuest.Testing
{
    public class TestScriptableObjectItems : SceneTestFixture
    {
        readonly string itemId = "";
        readonly string sceneName = "Test_Characters";

        void CommonInstall()
        {

        }

        [UnityTest]
        public IEnumerator TestItemShapeRetrievable()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            var itemDataSource = SceneContainer.Resolve<IItemDataSource>();


        }
    }
}
