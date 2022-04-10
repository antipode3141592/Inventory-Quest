using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Stats.Skills
{
    public class SkillStat : IStat
    {
        public int InitialValue { get; }

        public int PurchasedLevels { get; set; }

        public int CurrentValue => InitialValue + PurchasedLevels + Modifier;

        public int Modifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
