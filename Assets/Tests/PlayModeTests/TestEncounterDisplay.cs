using Zenject;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace InventoryQuest.Testing
{
    public class TestEncounterDisplay : SceneTestFixture
    {
        [UnityTest]
        public IEnumerator TestScene()
        {
            yield return LoadScene("InsertSceneNameHere");

            // TODO: Add assertions here now that the scene has started
            // Or you can just uncomment to simply wait some time to make sure the scene plays without errors
            //yield return new WaitForSeconds(1.0f);

            // Note that you can use SceneContainer.Resolve to look up objects that you need for assertions
        }
    }
}