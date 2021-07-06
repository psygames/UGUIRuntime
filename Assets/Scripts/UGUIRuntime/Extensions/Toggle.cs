using UnityEngine;
using UnityEngine.UI;

namespace psyhack
{
    public static partial class UGUIRuntimeExtensions
    {
        public static Toggle SetSprite(this Toggle toggle, string background, string checkmark)
        {
            var bg = toggle.GetRectTransform()
                .GetOrAddNode("Background")
                .SetPadding()
                .GetOrAddComponent<Image>()
                .SetSprite(background);

            var mark = bg.GetRectTransform()
                .GetOrAddNode("Checkmark")
                .SetPadding()
                .GetOrAddComponent<Image>()
                .SetSprite(checkmark);

            toggle.targetGraphic = bg;
            toggle.graphic = mark;

            return toggle;
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
                .SetPadding()
                .SetAnchoredPosition(new Vector2(toggleWidth + 3, -0.5f))
                .SetSizeDelta(new Vector2(width - toggleWidth, 0));
            comp.horizontalOverflow = HorizontalWrapMode.Overflow;
            comp.verticalOverflow = VerticalWrapMode.Overflow;
            return toggle;
        }

        public static Toggle SetListener(this Toggle toggle, UnityEngine.Events.UnityAction<bool> action)
        {
            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener(action);
            return toggle;
        }


        public static Toggle SetPos(this Toggle comp, float x, float y)
        {
            comp.GetRectTransform().SetPos(x, y);
            return comp;
        }

        public static Toggle SetSize(this Toggle comp, float w, float h)
        {
            comp.GetRectTransform().SetSize(w, h);
            return comp;
        }
    }
}
