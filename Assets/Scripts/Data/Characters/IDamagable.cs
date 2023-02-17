using Data.Health;
using System;
using System.Collections.Generic;

namespace Data.Characters
{
    public interface IDamagable
    {
        IDictionary<DamageType, DamageResistance> Resistances { get; }

        public event EventHandler<int> DamageTaken;
        public event EventHandler<int> DamageHealed;

        public void DealDamage(int damageAmount, DamageType damageType);
        public void HealDamage(int healAmount);

        public void ApplyModifiers(IList<ResistanceModifier> modifiers);
        public void RemoveModifiers(IList<ResistanceModifier> modifiers);

    }
}