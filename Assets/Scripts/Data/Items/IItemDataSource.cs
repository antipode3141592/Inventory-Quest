namespace Data.Items
{
    public interface IItemDataSource
    {
        public IItemStats GetRandomItemStats(Rarity rarity);

        public IItemStats GetItemStats(string id);

        

    }
}
