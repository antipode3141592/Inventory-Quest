namespace Data.Interfaces
{
    public interface IReward
    {
        public string Id { get; }

        public void GrantTo(Character character)
        {

        }
    }
}
