using Data.Characters;
using System.Collections.Generic;
using System.Linq;


namespace Data.Encounters
{
    public class CombatEncounter : Encounter
    {
        public CombatEncounter(ICombatEncounterStats stats) : base(stats) {
            CharacterIds = stats.CharacterIds;
            Characters = stats.Characters;
        }

        public List<string> CharacterIds { get; }

        public List<ICharacter> Characters { get; }


        public override bool Resolve(Party party)
        {
            Fight(party);
            return Characters.Count(x => x.IsIncapacitated) == Characters.Count;
        }

        void Fight(Party party)
        {

            while (true)
            {
                break;
            }
        }
    }
}
