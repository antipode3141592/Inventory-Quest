using UnityEngine;
using Data.Health;

namespace Data.Penalties
{
    public class DamagePenaltyStats : IDamagePenaltyStats
    {
        [SerializeField] DamageType damageType;
        [SerializeField] PenaltyType penaltyType;
        [SerializeField] int damageAmount;

        public DamageType DamageType { get; }

        public int DamageAmount { get; }

        public PenaltyType PenaltyType { get; }
    }
}
