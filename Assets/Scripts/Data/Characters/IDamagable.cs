using Data.Health;
using System;

namespace Data.Characters
{
    public interface IDamagable
    {
        public event EventHandler<int> DamageTaken;
        public event EventHandler<int> DamageHealed;

        public void DealDamage(int damageAmount, DamageType damageType);
        public void HealDamage(int healAmount);

    }
}