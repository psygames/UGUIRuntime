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
            comp.GetRectTransform().Margin().SetPivotCenter();
            comp.SetFont();
            comp.text = text;
            comp.alignment = TextAnchor.MiddleCenter;
            return button;
        }
    }
}
