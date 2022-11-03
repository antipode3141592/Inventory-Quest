namespace Data.Items
{
    public interface IItemDataSource: IDataSource<IItemStats>
    {
        public IItemStats GetItemByRarity(Rarity rarity);
    }
}
