namespace Data
{
    public interface IDataSource
    {
        public IItemStats GetRandomItemStats(Rarity rarity);

        public IItemStats GetItemStats(string id);

        public CharacterStats GetRandomCharacterStats(Rarity rarity);
        public CharacterStats GetCharacterStats(string id);

    }
}
