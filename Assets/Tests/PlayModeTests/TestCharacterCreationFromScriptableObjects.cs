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
        //StaticContext.Container.Bind<ICharacterDataSource>()
        //    .To<CharacterSODataSource>().FromComponentInHierarchy().AsSingle();

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

    [UnityTest]
    public IEnumerator TestCharacterFactoryCreationSuccess()
    {
        CommonInstall();

        yield return LoadScene("Test_Characters");

        var characterDataSource = SceneContainer.Resolve<ICharacterDataSource>();
        playerStats = characterDataSource.GetById("player");
        var player = CharacterFactory.GetCharacter(playerStats);

        Assert.That(player is not null);
    }

    [UnityTest]
    public IEnumerator TestCreatedCharacterWithInitialStatsSuccess()
    {
        CommonInstall();
        int addedStrength = 5;


        yield return LoadScene("Test_Characters");

        var characterDataSource = SceneContainer.Resolve<ICharacterDataSource>();
        playerStats = characterDataSource.GetById("player");
        playerStats.InitialStats.Add(StatTypes.Strength, addedStrength);
        int targetStrength = playerStats.SpeciesBaseStats.BaseStats[StatTypes.Strength] + addedStrength;

        var player = CharacterFactory.GetCharacter(playerStats);

        Assert.IsTrue(player.StatDictionary[StatTypes.Strength].CurrentValue == targetStrength);
    }

    
}