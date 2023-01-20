using InventoryQuest.Managers;
using Zenject;
using UnityEngine;
using InventoryQuest.Managers.States;

namespace InventoryQuest.Testing
{
    public class AdventureManagerStub : MonoBehaviour, IAdventureManager
    {
        public AdventureManagerStart AdventureManagerStart { get; }
        public InLocation InLocation { get; }
        public Pathfinding Pathfinding { get; }
        public Adventuring Adventuring { get; }
    }
}
