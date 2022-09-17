using System.Collections.Generic;
using System;

namespace Data.Characters
{
    public class Attack : CompositeStat
    {
        public override Type Type => typeof(Attack);
        public override CharacterStatTypes Id => CharacterStatTypes.Attack;
        public Attack(int initialValue, ICollection<IStat> stat) : base(initialValue, stat)
        {
        }
    }
}
