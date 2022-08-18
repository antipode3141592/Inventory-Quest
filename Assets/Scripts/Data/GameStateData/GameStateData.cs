using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Data
{
    [Serializable]
    public class GameStateData
    {
        public List<string> PartyMemberIds;
        public List<string> PartyDisplayOrder;

        public string CurrentLocationId;
        public string DestinationLocationId;

    }

    [Serializable]
    public class PlayableCharacterData
    {
        public string Id;
    }


}