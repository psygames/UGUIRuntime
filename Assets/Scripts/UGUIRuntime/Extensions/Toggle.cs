using UnityEngine;
using UnityEngine.UI;

namespace UGUIRuntime
{
    public static partial class UGUIRuntimeExtensions
    {
        public static Image GetBackground(this Toggle toggle)
        {
            var bg = toggle.GetRectTransform()
                .GetOrAddNode("Background")
                .GetOrAddComponent<Image>();
            toggle.targetGraphic = bg;
            return bg;
        }

        public static Image GetCheckmark(this Toggle toggle)
        {
            var mark = toggle.GetBackground().GetRectTransform()
                .GetOrAddNode("Checkmark")
                .GetOrAddComponent<Image>();
            toggle.graphic = mark;
            return mark;
        }

        public static Toggle SetText(this Toggle toggle, string text, float width = 0f)
        {
            var comp = toggle.GetRectTransform().GetOrAddNode("Label").GetOrAddComponent<Text>();
            comp.SetFont();
            comp.text = text;
            comp.alignment = TextAnchor.MiddleLeft;
            var toggleWidth = toggle.GetRectTransform().rect.width;
            width = width == 0 ? toggleWidth * 2 : width;
            comp.GetRectTransform()
                .Margin()
                .SetAnchoredPosition(new Vector2(toggleWidth + 3, -0.5f))
                .SetSizeDelta(new Vector2(width - toggleWidth, 0));
            comp.horizontalOverflow = HorizontalWrapMode.Overflow;
            comp.verticalOverflow = VerticalWrapMode.Overflow;
            return toggle;
        }

        public static Toggle SetValue(this Toggle toggle, bool isOn)
        {
            toggle.isOn = isOn;
            return toggle;
        }

        public static Toggle SetListener(this Toggle toggle, UnityEngine.Events.UnityAction<bool> action)
        {
            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener(action);
            return toggle;
        }

        public static Toggle SetPosition(this Toggle comp, float x, float y)
        {
            comp.GetRectTransform().SetPosition(x, y);
            return comp;
        }

        public static Toggle SetSize(this Toggle comp, float w, float h)
        {
            comp.GetRectTransform().SetSize(w, h);
            return comp;
        }
    }
}
