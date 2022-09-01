namespace Data.Shapes
{
    public class Pentomino_X : Shape
    {
        public Pentomino_X(Facing defaultFacing = Facing.Right)
        {
            Id = "pentomino_x";
            MinoCount = 5;
            IsChiral = false;
            IsRotationallySymmetric = true;
            Masks = new()
            {
                {
                    Facing.Right,
                    new BitMask(
                    new bool[,]
                    {
                        {false, true, false },
                        {true, true, true },
                        {false, true, false }
                    })
                },
                {
                    Facing.Down,
                    new BitMask(
                    new bool[,]
                    {
                        {false, true, false },
                        {true, true, true },
                        {false, true, false }
                    })
                },
                {
                    Facing.Left,
                    new BitMask(
                    new bool[,]
                    {
                        {false, true, false },
                        {true, true, true },
                        {false, true, false }
                    })
                },
                {
                    Facing.Up,
                    new BitMask(
                    new bool[,]
                    {
                        {false, true, false },
                        {true, true, true },
                        {false, true, false }
                    })
                },
            };
            CurrentFacing = defaultFacing;

        }
    }
}
