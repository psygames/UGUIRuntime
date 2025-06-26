using UnityEngine;
using UnityEngine.UI;

namespace UGUIRuntime
{
    public class Switch : MonoBehaviour
    {
        public Toggle toggle;
        public Image knob;
        public Vector2 knobOffPosition;
        public Vector2 knobOnPosition;

        private void LerpTo(Vector2 pos)
        {
            if (!knob || knob.rectTransform.anchoredPosition == pos)
                return;
            knob.rectTransform.anchoredPosition =
                Vector2.Lerp(knob.rectTransform.anchoredPosition, pos, Time.deltaTime * 20);
        }

        private void Update()
        {
            if (toggle.isOn)
            {
                LerpTo(knobOnPosition);
            }
            else
            {
                LerpTo(knobOffPosition);
            }
        }
    }

    public static partial class UGUIRuntimeExtensions
    {
        public static Switch SetSprite(this Switch toggle, string background, string checkmark)
        {
            toggle.GetBackground().SetSprite(background);
            toggle.GetCheckmark().SetSprite(checkmark);
            return toggle;
        }

        public static Image GetBackground(this Switch _switch)
        {
            var bg = _switch.GetRectTransform()
                .GetOrAddNode("Background")
                .GetOrAddComponent<Image>();
            _switch.toggle.targetGraphic = bg;
            return bg;
        }

        public static Image GetCheckmark(this Switch _switch)
        {
            var mark = _switch.GetBackground().GetRectTransform()
                .GetOrAddNode("Checkmark")
                .GetOrAddComponent<Image>();
            _switch.toggle.graphic = mark;
            return mark;
        }

        public static Image GetKnob(this Switch _switch)
        {
            var knob = _switch.GetBackground().GetRectTransform()
                .GetOrAddNode("Knob")
                .GetOrAddComponent<Image>();
            _switch.knob = knob;
            return knob;
        }

        public static Switch SetKnobPosition(this Switch _switch, float offPosition, float onPosition)
        {
            _switch.knobOnPosition = new Vector2(onPosition, 0);
            _switch.knobOffPosition = new Vector2(offPosition, 0);
            return _switch;
        }

        public static Switch SetListener(this Switch _switch, UnityEngine.Events.UnityAction<bool> action)
        {
            _switch.toggle.onValueChanged.RemoveAllListeners();
            _switch.toggle.onValueChanged.AddListener(action);
            return _switch;
        }

        public static Switch SetValue(this Switch _switch, bool isOn)
        {
            _switch.toggle.isOn = isOn;
            return _switch;
        }
    }
}
