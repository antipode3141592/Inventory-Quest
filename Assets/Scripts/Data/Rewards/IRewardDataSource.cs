namespace Data.Rewards
{
    public interface IRewardDataSource
    {
        public IRewardStats GetRewardById(string id);
    }
}
