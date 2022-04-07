using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Encounters
{
    public class DialogueEncounter : Encounter
    {
        public DialogueEncounter(IEncounterStats encounterStats) : base(encounterStats)
        {
        }

        public override bool Resolve(Party party)
        {
            throw new NotImplementedException();
        }
    }

    public class DecisionEncounter : Encounter
    {
        public DecisionEncounter(IEncounterStats encounterStats) : base(encounterStats)
        {
        }

        public override bool Resolve(Party party)
        {
            throw new NotImplementedException();
        }
    }

    public class RestEncounter : Encounter
    {
        public RestEncounter(IEncounterStats encounterStats) : base(encounterStats)
        {
        }

        public override bool Resolve(Party party)
        {
            return true;
        }
    }
}
