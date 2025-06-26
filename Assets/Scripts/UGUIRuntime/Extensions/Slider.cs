using UnityEngine;
using UnityEngine.UI;

namespace UGUIRuntime
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

        public static Slider SetMargin(this Slider slider, float margin)
        {
            var handleArea = slider.GetRectTransform().GetOrAddNode("Handle Slide Area");
            handleArea.SetSizeDelta(Vector2.left * margin * 2);
            var fillArea = slider.GetRectTransform().GetOrAddNode("Fill Area");
            fillArea.SetSizeDelta(Vector2.left * margin * 2);
            var fill = fillArea.GetOrAddNode("Fill");
            fill.SetSizeDelta(Vector2.right * margin * 2);
            fill.SetAnchoredPosition(Vector2.left * margin);
            return slider;
        }

        public static Slider SetValue(this Slider slider, float value)
        {
            slider.value = value;
            return slider;
        }

        public static Slider SetListener(this Slider slider, UnityEngine.Events.UnityAction<float> action)
        {
            slider.onValueChanged.RemoveAllListeners();
            slider.onValueChanged.AddListener(action);
            return slider;
        }
    }
}
