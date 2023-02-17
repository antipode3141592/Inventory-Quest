using Data.Characters;

namespace Data.Items.Components
{
    public interface IUsable: IItemComponent
    {
        public bool IsConsumable { get; }

        public bool HasBeenUsed { get; }

        public bool TryUse(ref ICharacter usedByCharacter);
    }
}
