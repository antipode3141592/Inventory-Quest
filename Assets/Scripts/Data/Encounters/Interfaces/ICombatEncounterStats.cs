using Data.Characters;
using System.Collections.Generic;

namespace Data.Encounters
{
    public interface ICombatEncounterStats: IEncounterStats
    {

        public List<string> CharacterIds { get; }

        public List<ICharacter> Characters { get; }
    }
}
