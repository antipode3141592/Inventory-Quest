using Data.Characters;
using System;

namespace Data.Items.Components
{
    public class UsableStats : IUsable
    {
        public IItem Item => throw new NotImplementedException();

        public bool TryUse(ICharacter usedByCharacter)
        {
            throw new NotImplementedException();
        }
    }
}
