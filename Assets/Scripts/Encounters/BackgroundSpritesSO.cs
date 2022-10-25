using System.Collections.Generic;
using UnityEngine;

namespace InventoryQuest.Backgrounds
{
    [CreateAssetMenu(menuName = "BackgroundSpritesSO")]
    public class BackgroundSpritesSO : ScriptableObject
    {
        public string Id;
        public List<Sprite> BackgroundSprites;
    }
}