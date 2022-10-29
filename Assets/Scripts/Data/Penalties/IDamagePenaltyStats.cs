using Data.Health;

namespace Data.Penalties
{
    public interface IDamagePenaltyStats : IPenaltyStats
    {
        public DamageType DamageType { get; }
        public int DamageAmount { get; }
    }
}
