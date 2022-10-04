namespace Data.Shapes

{
    public enum Facing { Right, Down, Left, Up}

    public static class FacingExtensions
    {
        public static Facing RotateCCW(this Facing facing)
        {
            return facing == Facing.Right ? Facing.Up : facing - 1;
        }

        public static Facing RotateCW(this Facing facing)
        {
            return facing == Facing.Up ? Facing.Right : facing + 1;
        }

        public static Facing Flip(this Facing facing)
        {
            return (int)facing < 2 ? facing + 2 : facing - 2;
        }
    }
}
