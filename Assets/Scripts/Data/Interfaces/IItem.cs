namespace Data.Interfaces

{
    public interface IItem
    {
        public string GuId { get; }  //unique-ish object identifier
        public string Id { get; }    //descriptive name for logging
        public float Weight { get; }   //item weight
        public float Value { get; }    //item value
        public IItemStats Stats { get; }

        public Shape Shape { get; }
    }
}
