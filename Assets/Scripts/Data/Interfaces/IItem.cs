namespace Data

{
    public interface IItem
    {
        public string Id { get; }  //unique-ish object identifier
        public string Name { get; }    //descriptive name for logging
        public float Weight { get; }   //item weight
        public float Value { get; }    //item value
        public IItemStats Stats { get; }

        public Shape Shape { get; }
    }
}
