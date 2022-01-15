namespace Data

{
    public interface IItem
    {
        string Id { get; }  //unique-ish object identifier
        string Name { get; }    //descriptive name for logging
        float Weight { get; }   //item weight
        float Value { get; }    //item value
    }
}
