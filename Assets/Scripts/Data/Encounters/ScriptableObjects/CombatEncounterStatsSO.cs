using Data.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Encounters
{
    [CreateAssetMenu(menuName = "InventoryQuest/EncounterStats/Combat", fileName = "combat")]
    public class CombatEncounterStatsSO : EncounterStatsSO, ICombatEncounterStats
    {
        [SerializeField] List<string> characterIds;
        [SerializeField] List<ICharacter> characters;

        public List<string> CharacterIds => characterIds;
        public List<ICharacter> Characters => characters;
    }


}