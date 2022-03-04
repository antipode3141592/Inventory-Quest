namespace Data.Interfaces

{
    public interface IRotatable
    {
        public Facing Rotate(int direction);  //+1 CW, -1CCW
    }
}
