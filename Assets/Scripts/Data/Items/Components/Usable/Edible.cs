using Data.Characters;

namespace Data.Items.Components
{
    class Edible : IUsable
    {
        EdibleStats _edibleStats;

        public IItem Item { get; }

        public Edible(EdibleStats edibleStats, IItem parentItem)
        {
            this._edibleStats = edibleStats;
            Item = parentItem;
        }

        public bool TryUse(ICharacter useByCharacter)
        {
            useByCharacter.HealDamage(_edibleStats.RestorationStrength);
            Item.Quantity--;
            return true;
        }
    }
}
