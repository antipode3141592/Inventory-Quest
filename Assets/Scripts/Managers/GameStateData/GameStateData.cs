using System;
using System.Collections.Generic;

namespace InventoryQuest
{
    [Serializable]
    public class GameStateData
    {
        public List<string> PartyMemberIds;
        public List<string> PartyDisplayOrder;

        public string CurrentLocationId;
        public string DestinationLocationId;

    }


}