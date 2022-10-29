using Data.Characters;
using Data.Health;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryQuest.Health
{
    public interface IHealthManager
    {
        public void DealDamage(ICharacter charcter, int damageAmount, DamageType damageType);

        public void HealDamage(ICharacter character, int healAmount, DamageType damageType);
    }
}
