using Data.Characters;
using Data.Health;
using UnityEngine;

namespace InventoryQuest.Health
{
    public class HealthManager : MonoBehaviour, IHealthManager
    {
        public void DealDamage(IDamagable damagable, int damageAmount, DamageType damageType)
        {
            damagable.DealDamage(damageAmount, damageType);
        }

        public void Heal(IDamagable damagable, int healAmount)
        {
            damagable.HealDamage(healAmount);
        }
    }
}
