using Data.Characters;

namespace Data.Penalties

{
    public interface IPenalty
    { 
        public void ApplyTo(ref ICharacter character);
    }
}
