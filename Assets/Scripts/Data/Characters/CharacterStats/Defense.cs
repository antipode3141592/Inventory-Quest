﻿using System.Collections.Generic;
using System;

namespace Data.Characters
{
    public class Defense : CompositeStat
    {
        public override Type Type => typeof(Defense);
        public override CharacterStatTypes Id => CharacterStatTypes.Defense;
        public Defense(int initialValue, ICollection<IStat> stat) : base(initialValue, stat)
        {
        }
    }
}
