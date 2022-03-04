using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
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
