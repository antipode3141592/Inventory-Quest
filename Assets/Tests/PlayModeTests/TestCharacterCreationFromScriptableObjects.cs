using Data;
using Data.Characters;
using System.Collections;
using UnityEngine.TestTools;
using Zenject;
using NUnit.Framework;

public class TestCharacterCreationFromScriptableObjects : SceneTestFixture
{

    ICharacterStats playerStats;

    void CommonInstall()
    {
        StaticContext.Container.Bind<ICharacterDataSource>()
            .To<CharacterSODataSource>().FromComponentInHierarchy().AsSingle();

    }
    

    [UnityTest]
    public IEnumerator TestCharacterStatRetrievable()
    {
        CommonInstall();

        

        yield return LoadScene("Test_Characters");

        var characterDataSource = SceneContainer.Resolve<ICharacterDataSource>();
        playerStats = characterDataSource.GetById("player");
        Assert.That(playerStats is not null);
    }
}