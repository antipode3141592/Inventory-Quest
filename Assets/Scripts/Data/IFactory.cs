namespace Data
{
    public interface IFactory<T, U>
    {
        public T Create(U stats);
    }
}
