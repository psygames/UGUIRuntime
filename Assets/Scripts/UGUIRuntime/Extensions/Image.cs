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

        public static Image SetPosition(this Image comp, float x, float y)
        {
            comp.GetRectTransform().SetPosition(x, y);
            return comp;
        }

        public static Image SetSize(this Image comp, float w, float h)
        {
            comp.GetRectTransform().SetSize(w, h);
            return comp;
        }

        public static Image SetType(this Image comp, Image.Type type, bool fillCenter = true)
        {
            comp.type = type;
            comp.fillCenter = fillCenter;
            return comp;
        }
    }
}
