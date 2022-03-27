using Data;
using Data.Interfaces;
using InventoryQuest.Characters;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class PartyManager: MonoBehaviour
    {
        ICharacterDataSource _characterDataSource;
        IItemDataSource _itemDataSource;

        Party _party;
        Character Player;
        Character Minion;

        public Party CurrentParty => _party;

        [Inject]
        public void Init(ICharacterDataSource characterDataSource, IItemDataSource itemDataSource)
        {
            _itemDataSource = itemDataSource;
            _characterDataSource = characterDataSource;
        }

        private void Awake()
        {
            Player = CharacterFactory.GetCharacter(_characterDataSource.GetCharacterStats("Player"),
                (ContainerStats)_itemDataSource.GetItemStats("adventure backpack"));
            Minion = CharacterFactory.GetCharacter(_characterDataSource.GetCharacterStats("Minion"),
                (ContainerStats)_itemDataSource.GetItemStats("small backpack"));

            _party = new Party(new Character[] { Player, Minion });
        }
    }
}
