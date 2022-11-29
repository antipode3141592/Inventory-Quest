using Data.Characters;
using Data.Health;

namespace InventoryQuest.Health
{
    public interface IHealthManager
    {
        public void DealDamage(IDamagable damagable, int damageAmount, DamageType damageType);

        public void HealDamage(IDamagable damagable, int healAmount);
    }
}
