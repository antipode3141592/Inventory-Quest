using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Data.Characters;
using Data.Items;
using Data.Locations;
using Data.Quests;
using System.Collections.Generic;

namespace InventoryQuest.UI.Quests
{
    public class QuestDisplay : MonoBehaviour
    {
        IItemDataSource _itemDataSource;
        ILocationDataSource _locationDataSource;
        ICharacterDataSource _characterDataSource;

        [SerializeField] TextMeshProUGUI QuestNameText;
        [SerializeField] TextMeshProUGUI QuestDescriptionText;
        [SerializeField] TextMeshProUGUI QuestTypeText;
        [SerializeField] TextMeshProUGUI QuestExperienceText;
        [SerializeField] Image QuestStatusIcon;

        [SerializeField] DeliveryQuestDetailDisplay deliveryQuestDisplayPrefab;
        [SerializeField] GatheringQuestDetailDisplay gatheringQuestDisplayPrefab;
        //[SerializeField] BountyQuestDetailDisplay bountyQuestDetailDisplayPrefab;
        //[SerializeField] EscortQuestDetailDisplay escortQuestDetailDisplayPrefab;

        [SerializeField] RectTransform parentRect;
        IQuestDetailDisplay questDetails;

        public void Init(IItemDataSource itemDataSource, ILocationDataSource locationDataSource, ICharacterDataSource characterDataSource)
        {
            _itemDataSource = itemDataSource;
            _locationDataSource = locationDataSource;
            _characterDataSource = characterDataSource;
        }

        public void SetDisplay(IQuest quest)
        {
            QuestNameText.text = quest.Name;
            QuestDescriptionText.text = quest.Description;
            QuestTypeText.text = quest.GetType().Name;
            QuestExperienceText.text = $"{quest.Stats.Experience} xp";

            questDetails = CreateQuestDetails(quest);
        }

        IQuestDetailDisplay CreateQuestDetails(IQuest quest)
        {
            DeliveryQuest deliveryQuest = quest as DeliveryQuest;
            if (deliveryQuest is not null)
            {
                var deliveryQuestDisplay = Instantiate<DeliveryQuestDetailDisplay>(deliveryQuestDisplayPrefab, parentRect);
                //************************************
                // should create prefab factory to inject this 
                deliveryQuestDisplay.Init(_itemDataSource, _locationDataSource, _characterDataSource);
                //************************************
                deliveryQuestDisplay.SetDisplay(
                    questStats: quest.Stats);
                return deliveryQuestDisplay;
            }
            GatheringQuest gatheringQuest = quest as GatheringQuest;
            if (gatheringQuest is not null)
            {
                var gatheringQuestDisplay = Instantiate<GatheringQuestDetailDisplay>(gatheringQuestDisplayPrefab, parentRect);
                //************************************
                // should create prefab factory to inject this 
                gatheringQuestDisplay.Init(_itemDataSource, _locationDataSource);
                //************************************
                gatheringQuestDisplay.SetDisplay(
                    questStats: quest.Stats);
                return gatheringQuestDisplay;
            }
            BountyQuest bountyQuest = quest as BountyQuest;
            if (bountyQuest is not null)
            {

            }
            EscortQuest escortQuest = quest as EscortQuest;
            if (escortQuest is not null)
            {

            }

            return null;
        }
    }
}