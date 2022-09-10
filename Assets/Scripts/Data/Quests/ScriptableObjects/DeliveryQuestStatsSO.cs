using System.Collections.Generic;
using UnityEngine;

namespace Data.Quests
{
    [CreateAssetMenu(menuName = "InventoryQuest/QuestStats/Delivery", fileName = "Delivery_")]
    public class DeliveryQuestStatsSO : QuestStatsSO, IDeliveryQuestStats
    {
        [SerializeField] protected List<string> itemIds;
        [SerializeField] protected List<int> quantities;


        public List<string> ItemIds => itemIds;
        public List<int> Quantities => quantities;
    }
}