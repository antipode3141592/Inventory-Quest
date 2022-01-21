namespace Data
{
    public interface IDataSource
    {
        public IItemStats GetRandomItemStats(Rarity rarity);

        public CharacterStats GetCharacterStats(string id);

    }
}
