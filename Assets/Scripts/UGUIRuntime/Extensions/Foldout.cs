using UnityEngine;
using UnityEngine.UI;

namespace UGUIRuntime
{
    public class Foldout : MonoBehaviour
    {
        public Toggle toggle;
        public Vector2 knobOffPosition;
        public Vector2 knobOnPosition;

        private void LerpTo(Vector2 pos)
        {

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
        public static Foldout SetListener(this Foldout foldout, UnityEngine.Events.UnityAction<bool> action)
        {
            foldout.toggle.onValueChanged.RemoveAllListeners();
            foldout.toggle.onValueChanged.AddListener(action);
            return foldout;
        }

        public static Foldout SetPosition(this Foldout comp, float x, float y)
        {
            comp.GetRectTransform().SetPosition(x, y);
            return comp;
        }

        public static Foldout SetSize(this Foldout comp, float w, float h)
        {
            comp.GetRectTransform().SetSize(w, h);
            return comp;
        }
    }
}
