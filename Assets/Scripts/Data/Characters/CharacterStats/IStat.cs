namespace Data.Characters
{
    public interface IStat
    {
        public StatTypes Id { get; }

        public int InitialValue { get; }

        public int PurchasedLevels { get; set; }

        public int CurrentValue { get; }
        public int Modifier { get; set; }
    }

    
}
