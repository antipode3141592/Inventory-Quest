using FiniteStateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryQuest.Managers.States
{
    public class ResolvingHarvest: IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public bool IsDone;

        public void OnEnter()
        {
            IsDone = false;
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {
        }

        void Resolve()
        {
            //do some calculations on harvested stuff
            IsDone = true;
        }
    }
}
