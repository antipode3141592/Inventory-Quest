using Data.Shapes;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    internal static class ImageUtilities
    {

        public static void RotateSprite(Facing targetFacing, Image image, Vector3 offset = default)
        {
            float h = image.preferredHeight;
            float w = image.preferredWidth;
            switch (targetFacing)
            {
                case Facing.Right:
                    image.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    image.rectTransform.anchoredPosition = offset;
                    break;
                case Facing.Down:
                    image.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    image.rectTransform.anchoredPosition = new Vector3(offset.x + h, offset.y, offset.z);
                    break;
                case Facing.Left:
                    image.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
                    image.rectTransform.anchoredPosition = new Vector3(offset.x + w, offset.y - h, offset.z);
                    break;
                case Facing.Up:
                    image.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -270));
                    image.rectTransform.anchoredPosition = new Vector3(offset.x, offset.y - w, offset.z);
                    break;
                default:
                    break;
            }
        }
    }
}