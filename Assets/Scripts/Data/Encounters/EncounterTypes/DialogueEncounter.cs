﻿using Data.Characters;
using System;

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
}
