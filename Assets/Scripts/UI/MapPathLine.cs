using Data.Encounters;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class MapPathLine : MonoBehaviour
    {
        [SerializeField] Image BackgroundImage;
        [SerializeField] Image encounterIcon;

        [SerializeField] string locationAId;
        [SerializeField] string locationBId;

        List<Image> encounterBackgroundIcons = new();

        public string LocationAId => locationAId;
        public string LocationBId => locationBId;

        public void DisplayPath(IPathStats pathStats)
        {
            BackgroundImage.color = Color.white;
            DestroyEncounterMarkers();
            foreach (var encounter in pathStats.EncounterIds)
            {
                var icon = Instantiate(encounterIcon, transform);
                encounterBackgroundIcons.Add(icon);
            }

        }

        private void DestroyEncounterMarkers()
        {
            for (int i = 0; i < encounterBackgroundIcons.Count; i++)
            {
                Destroy(encounterBackgroundIcons[i].gameObject);
            }
            encounterBackgroundIcons.Clear();
        }

        public void HidePath()
        {
            DestroyEncounterMarkers();
            BackgroundImage.color = Color.clear;
        }
    }
}