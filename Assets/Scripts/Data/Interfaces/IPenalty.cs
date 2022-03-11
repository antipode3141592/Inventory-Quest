namespace Data.Interfaces
{
    public interface IPenalty
    {
        public string Id { get; }

        public void ApplyTo(Character character);
    }
}
