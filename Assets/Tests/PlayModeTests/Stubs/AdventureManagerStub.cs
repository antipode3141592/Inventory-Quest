using InventoryQuest.Managers;
using Zenject;
using UnityEngine;
using InventoryQuest.Managers.States;

namespace InventoryQuest.Testing
{
    public class AdventureManagerStub : MonoBehaviour, IAdventureManager
    {
        public Idle Idle => throw new System.NotImplementedException();

        public Pathfinding Pathfinding => throw new System.NotImplementedException();

        public Adventuring Adventuring => throw new System.NotImplementedException();
    }
}
