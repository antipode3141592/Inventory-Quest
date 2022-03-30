namespace Data.Rewards
{
    public class RarityRange
    {
        public Rarity Rarity;
        public float Min;
        public float Max;

        public RarityRange(Rarity rarity, float min, float max)
        {
            Rarity = rarity;
            Min = min;
            Max = max;
        }
    }

    
}
