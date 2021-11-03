using UnityEngine;
using UnityEngine.UI;
namespace UGUIRuntime
{
    public static partial class UGUIRuntimeExtensions
    {
        public static Button SetListener(this Button button, UnityEngine.Events.UnityAction action)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(action);
            return button;
        }

        public static Button SetText(this Button button, string text)
        {
            var comp = button.GetRectTransform().GetOrAddNode("Text").GetOrAddComponent<Text>();
            comp.GetRectTransform().SetMargin().SetPivotCenter();
            comp.SetFont();
            comp.text = text;
            comp.alignment = TextAnchor.MiddleCenter;
            return button;
        }

        public static Button SetSprite(this Button button, string name, int border = 0)
        {
            if (button.image)
            {
                button.image.SetSprite(name, border);
            }
            return button;
        }

        public static Button SetColor(this Button button, Color32 color)
        {
            if (button.image)
            {
                button.image.SetColor(color);
            }
            return button;
        }

        public static Button SetPosition(this Button comp, float x, float y)
        {
            comp.GetRectTransform().SetPosition(x, y);
            return comp;
        }

        public static Button SetSize(this Button comp, float w, float h)
        {
            comp.GetRectTransform().SetSize(w, h);
            return comp;
        }
    }
}
