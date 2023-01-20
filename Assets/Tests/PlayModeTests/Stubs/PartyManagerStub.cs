using Data.Characters;
using InventoryQuest.Managers;
using UnityEngine;

namespace InventoryQuest.Testing
{
    public class PartyManagerStub : MonoBehaviour, IPartyManager
    {
        Party currentParty;

        public Party CurrentParty => currentParty;

        public bool IsPartyDead { get; set; }

        void Awake()
        {
            currentParty = new Party();
        }

        public void AddCharacterToPartyById(string id)
        {
            
        }

        public void AddCharacterToParty(ICharacterStats characterStats)
        {
            currentParty.AddCharacter(CharacterFactory.GetCharacter(characterStats));
        }

        public double CountItemInCharacterInventories(string itemId)
        {
            throw new System.NotImplementedException();
        }
    }
}
