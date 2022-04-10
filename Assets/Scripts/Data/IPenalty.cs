using Data.Characters;

namespace Data.Penalties

{
    public interface IPenalty
    {
        public string Id { get; }

        public void ApplyTo(ICharacter character);
    }
}
