using Data.Characters;
using InventoryQuest.Managers;
using UnityEngine;

namespace InventoryQuest.Testing
{
    public class PartyManagerStub : MonoBehaviour, IPartyManager
    {
        Party currentParty;

        public Party CurrentParty => currentParty;

        void Awake()
        {
            currentParty = new Party();
        }

        public void AddCharacterToParty(string id)
        {
            
        }

        public void AddCharacterToParty(ICharacter character)
        {
            currentParty.AddCharacter(character);
        }
    }
}
