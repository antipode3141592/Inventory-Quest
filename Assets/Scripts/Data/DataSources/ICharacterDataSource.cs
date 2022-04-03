namespace Data.Interfaces
{
    public interface ICharacterDataSource
    {
        public CharacterStats GetRandomCharacterStats(Rarity rarity);
        public CharacterStats GetCharacterStats(string id);
    }
}
