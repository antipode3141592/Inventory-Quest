using Data;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    internal static class ImageUtilities
    {

        public static void RotateSprite(Facing targetFacing, Image image)
        {
            float h = image.preferredHeight;
            float w = image.preferredWidth;
            switch (targetFacing)
            {
                case Facing.Right:
                    image.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    image.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                    break;
                case Facing.Down:
                    image.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    image.rectTransform.anchoredPosition = new Vector3(h, 0, 0);
                    break;
                case Facing.Left:
                    image.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
                    image.rectTransform.anchoredPosition = new Vector3(w, -h, 0);
                    break;
                case Facing.Up:
                    image.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -270));
                    image.rectTransform.anchoredPosition = new Vector3(0, -w, 0);
                    break;
                default:
                    break;
            }
        }
    }
}