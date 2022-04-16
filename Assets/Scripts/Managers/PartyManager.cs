using Data.Characters;
using Data.Items;

using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class PartyManager : MonoBehaviour, IPartyManager
    {
        ICharacterDataSource _characterDataSource;
        IItemDataSource _itemDataSource;

        Party _party = new();
        PlayableCharacter Player;
        PlayableCharacter Minion;

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
                new IEquipable[] { (IEquipable)ItemFactory.GetItem(_itemDataSource.GetItemStats("adventure backpack")) });
            Minion = CharacterFactory.GetCharacter(_characterDataSource.GetCharacterStats("Minion"),
                new IEquipable[] { (IEquipable)ItemFactory.GetItem(_itemDataSource.GetItemStats("small backpack")) });
            _party.AddCharacter(Player);
            _party.AddCharacter(Minion);
            //_party = new Party(new PlayableCharacter[] { Player, Minion });
        }
    }
}
