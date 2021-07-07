using UnityEngine;
using UnityEngine.UI;

namespace psyhack
{
    public static partial class UGUIRuntimeExtensions
    {
        public static Image GetBackground(this Slider slider)
        {
            var bg = slider.GetRectTransform().GetOrAddNode("Background").GetOrAddComponent<Image>();
            return bg;
        }

        public static Image GetFill(this Slider slider)
        {
            var area = slider.GetRectTransform().GetOrAddNode("Fill Area");
            var fill = area.GetOrAddNode("Fill").GetOrAddComponent<Image>();
            slider.fillRect = fill.GetRectTransform();
            return fill;
        }

        public static Image GetHandle(this Slider slider)
        {
            var area = slider.GetRectTransform().GetOrAddNode("Handle Slide Area");
            var position = area.GetOrAddNode("Position");
            var handle = position.GetOrAddNode("Handle").GetOrAddComponent<Image>();
            slider.targetGraphic = handle;
            slider.handleRect = position;
            return handle;
        }

        public static Slider SetPadding(this Slider slider, float padding)
        {
            var handleArea = slider.GetRectTransform().GetOrAddNode("Handle Slide Area");
            handleArea.SetSizeDelta(Vector2.left * padding * 2);
            var fillArea = slider.GetRectTransform().GetOrAddNode("Fill Area");
            fillArea.SetSizeDelta(Vector2.left * padding * 2);
            var fill = fillArea.GetOrAddNode("Fill");
            fill.SetSizeDelta(Vector2.right * padding * 2);
            fill.SetAnchoredPosition(Vector2.left * padding);
            return slider;
        }

        public static Slider SetListener(this Slider slider, UnityEngine.Events.UnityAction<float> action)
        {
            slider.onValueChanged.RemoveAllListeners();
            slider.onValueChanged.AddListener(action);
            return slider;
        }

        public static Slider SetPos(this Slider comp, float x, float y)
        {
            comp.GetRectTransform().SetPos(x, y);
            return comp;
        }

        public static Slider SetSize(this Slider comp, float w, float h)
        {
            comp.GetRectTransform().SetSize(w, h);
            return comp;
        }
    }
}
