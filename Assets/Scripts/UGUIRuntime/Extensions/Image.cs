using UnityEngine;
using UnityEngine.UI;
namespace UGUIRuntime
{
    public static partial class UGUIRuntimeExtensions
    {
        public static Image SetSprite(this Image image, string name)
        {
            image.sprite = SpriteMgr.GetSprite(name);
            return image;
        }

        public static Image SetColor(this Image image, Color32 color)
        {
            image.color = color;
            return image;
        }

        public static Image SetRaycast(this Image image, bool raycast = true)
        {
            image.raycastTarget = raycast;
            return image;
        }

        public static Image SetType(this Image comp, Image.Type type, bool fillCenter = true)
        {
            comp.type = type;
            comp.fillCenter = fillCenter;
            return comp;
        }
    }
}
