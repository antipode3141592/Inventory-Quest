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

        [SerializeField] Color inactivePathColor = new Color(0.5f, 0.5f, 0.5f, 1f);
        [SerializeField] Color activePathColor = new Color(1f, 1f, 1f, 1f);
        [SerializeField] Color hiddenPathColor = new Color(1f, 1f, 1f, 0f);

        readonly List<Image> encounterBackgroundIcons = new();

        public string LocationAId => locationAId;
        public string LocationBId => locationBId;

        public void ActivatePath(IPathStats pathStats)
        {
            BackgroundImage.color = activePathColor;
            DestroyEncounterMarkers();
            for (int i = 0; i < pathStats.EncounterStats.Count; i++)
                encounterBackgroundIcons.Add(Instantiate(encounterIcon, transform));
        }

        void DestroyEncounterMarkers()
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
            BackgroundImage.color = hiddenPathColor;
        }

        public void DeactivatePath()
        {
            DestroyEncounterMarkers();
            BackgroundImage.color = inactivePathColor;
        }


    }
}