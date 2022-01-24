namespace Data
{
    public abstract class CompositeStat : IStat
    {
        public float InitialValue { get; }

        public float Modifier { get; set; }

        public float CurrentValue => InitialValue + Modifier + ConnectedStat.CurrentValue;

        public IStat ConnectedStat { get; }

        public CompositeStat(float initialValue, IStat stat)
        {
            InitialValue = initialValue;
            ConnectedStat = stat;
        }
    }

    public class Attack : CompositeStat
    {
        public Attack(float initialValue, IStat stat) : base(initialValue, stat)
        {
        }
    }
}
