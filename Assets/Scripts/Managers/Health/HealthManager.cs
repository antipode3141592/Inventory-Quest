using Data.Characters;
using Data.Health;
using System;
using UnityEngine;

namespace InventoryQuest.Health
{
    public class HealthManager : MonoBehaviour, IHealthManager
    {
        public void DealDamage(ICharacter charcter, int damageAmount, DamageType damageType)
        {
            throw new NotImplementedException();
        }

        public void HealDamage(ICharacter character, int healAmount, DamageType damageType)
        {
            throw new NotImplementedException();
        }
    }
}
