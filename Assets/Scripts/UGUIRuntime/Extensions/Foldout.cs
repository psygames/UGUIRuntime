using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UGUIRuntime
{
    public class Foldout : MonoBehaviour
    {
        public Toggle toggle;
    }

    public static partial class UGUIRuntimeExtensions
    {
        public static RectTransform RT(this Foldout comp)
        {
            return comp.GetRectTransform();
        }

        public static Foldout SetListener(this Foldout foldout, UnityAction<bool> action)
        {
            foldout.toggle.onValueChanged.AddListener((isOn) => { action(!isOn); });
            return foldout;
        }

        public static Foldout AddFoldout(this RectTransform rectTransform, string name = "foldout")
        {
            var toggle = rectTransform.AddToggle();
            toggle.GetCheckmark().SetColor(Color.clear);
            toggle.GetBackground().SetSprite("triangle");
            var foldout = toggle.RT().GetOrAddComponent<Foldout>();
            foldout.toggle = toggle;
            foldout.toggle.GetBackground().RT().SetPivotCenter();
            foldout.SetListener((isFolded) =>
            {
                foldout.SetValue(isFolded);
            });
            foldout.SetValue(true);
            return foldout;
        }

        public static Foldout SetValue(this Foldout foldout, bool isFolded)
        {
            foldout.toggle.GetBackground().RT().localRotation = Quaternion.Euler(0, 0, isFolded ? 0 : -90);
            return foldout;
        }
    }
}
