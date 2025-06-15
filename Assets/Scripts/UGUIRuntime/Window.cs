using UnityEngine;
using UnityEngine.UI;

namespace UGUIRuntime
{
    public class Window : MonoBehaviour
    {
        private RectTransform root;

        public static Window Create(string title, Rect rect)
        {
            var wind = new GameObject(title).AddComponent<Window>();
            wind.root.SetRect(rect);
            wind.SetTitle(title);
            return wind;
        }

        private void SetTitle(string title)
        {
            root.AddText(title);
        }

        private void Awake()
        {
            transform.SetParent(UGUI.canvas.transform, false);
            root = gameObject.AddComponent<RectTransform>();
            root.AddImage().SetColor(Color.gray * 0.8f).rectTransform.AnchorFull();
        }
    }
}
