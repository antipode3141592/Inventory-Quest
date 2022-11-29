using UnityEngine;
using Data.Health;

namespace Data.Penalties
{
    public class DamagePenaltyStats : IDamagePenaltyStats
    {
        [SerializeField] DamageType damageType;
        [SerializeField] PenaltyType penaltyType;
        [SerializeField] int damageAmount;

        public DamageType DamageType => damageType;

        public int DamageAmount => damageAmount;

        public PenaltyType PenaltyType => penaltyType;
    }
}
