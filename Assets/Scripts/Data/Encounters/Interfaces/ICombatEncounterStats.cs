using Data.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Encounters
{
    public interface ICombatEncounterStats: IEncounterStats
    {

        public List<string> CharacterIds { get; }

        public List<ICharacter> Characters { get; }
    }
}
