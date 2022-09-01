namespace Data
{
    public interface IDataSource<T>
    {
        public T GetById(string id);
        public T GetRandom();
    }
}
