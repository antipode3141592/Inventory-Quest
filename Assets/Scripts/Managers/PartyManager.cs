using Data.Interfaces;
using InventoryQuest.Characters;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class PartyManager: MonoBehaviour
    {
        IDataSource _dataSource;

        Party _party;
        Character Player;
        Character Minion;

        public Party CurrentParty => _party;

        [Inject]
        public void Init(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        private void Awake()
        {
            Player = CharacterFactory.GetCharacter(_dataSource.GetCharacterStats("Player"),
                (ContainerStats)_dataSource.GetItemStats("adventure backpack"));
            Minion = CharacterFactory.GetCharacter(_dataSource.GetCharacterStats("Minion"),
                (ContainerStats)_dataSource.GetItemStats("small backpack"));

            _party = new Party(new Character[] { Player, Minion });
        }
    }
}
