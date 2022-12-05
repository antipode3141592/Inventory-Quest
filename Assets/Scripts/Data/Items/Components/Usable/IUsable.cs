using Data.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Items.Components
{
    public interface IUsable: IItemComponent
    {


        public bool TryUse(ICharacter usedByCharacter);
    }
}
