namespace Data.Shapes
{
    public class RotationEventArgs
    {
        public Facing TargetFacing;

        public RotationEventArgs (Facing targetFacing)
        {
            TargetFacing = targetFacing;
        }
    }
}
