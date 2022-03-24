using System.Collections.Generic;
using System.Linq;


namespace Data.Encounters
{
    public class CombatEncounter : Encounter
    {
        public CombatEncounter(CombatEncounterStats stats) : base(stats) {
            CharacterIds = stats.CharacterIds;
            Characters = stats.Characters;
        }

        public IList<string> CharacterIds { get; }

        public IList<Character> Characters { get; }


        public override bool Resolve(Party party)
        {
            return Characters.Count(x => x.IsIncapacitated) == Characters.Count;
        }


    }
}
