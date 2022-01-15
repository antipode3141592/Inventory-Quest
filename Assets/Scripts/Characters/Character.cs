namespace InventoryQuest.Characters
{
    //characters 
    public class Character
    {
        public CharacterStats Stats;
        
        public Container PrimaryContainer { get; set; }

        public Character(Container container, CharacterStats stats)
        {
            PrimaryContainer = container;
            Stats = stats;
        }

        //derived stats
        public float MaxEncumbrance => Stats.Strength.CurrentValue * 10f;
        public float MaxHealth => Stats.Durability.CurrentValue * 10f;
        public float CurrentEncumbrance => PrimaryContainer.TotalWeight;
        public float CurrentTotalGoldValue => PrimaryContainer.TotalWorth;

    }
}
