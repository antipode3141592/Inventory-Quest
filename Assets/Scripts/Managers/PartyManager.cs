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
            Player = CharacterFactory.GetCharacter(characterStats: _characterDataSource.GetCharacterStats("Player"),
                startingEquipment: new IEquipable[] { 
                    (IEquipable)ItemFactory.GetItem(_itemDataSource.GetItemStats("adventure backpack")),
                    (IEquipable)ItemFactory.GetItem(_itemDataSource.GetItemStats("sandal_1")),
                    (IEquipable)ItemFactory.GetItem(_itemDataSource.GetItemStats("shirt_1")),
                    (IEquipable)ItemFactory.GetItem(_itemDataSource.GetItemStats("pants_1"))
                },
                startingInventory: new IItem[]
                {
                    ItemFactory.GetItem(_itemDataSource.GetItemStats("questitem_1")),
                    ItemFactory.GetItem(_itemDataSource.GetItemStats("apple_fuji")),
                    ItemFactory.GetItem(_itemDataSource.GetItemStats("apple_fuji")),
                    ItemFactory.GetItem(_itemDataSource.GetItemStats("apple_fuji"))
                }
            );
            //Minion = CharacterFactory.GetCharacter(_characterDataSource.GetCharacterStats("Minion"),
            //    new IEquipable[] { (IEquipable)ItemFactory.GetItem(_itemDataSource.GetItemStats("small backpack")) });
            _party.AddCharacter(Player);
            //_party.AddCharacter(Minion);
            //_party = new Party(new PlayableCharacter[] { Player, Minion });
        }
    }
}
