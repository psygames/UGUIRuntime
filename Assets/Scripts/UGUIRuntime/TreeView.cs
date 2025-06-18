using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace UGUIRuntime
{
    public class TreeView : MonoBehaviour
    {
        private RectTransform root;

        public static TreeView Create(string title, Rect rect)
        {
            var wind = new GameObject(title).AddComponent<TreeView>();
            wind.root.SetRect(rect);
            wind.SetTitle(title);
            return wind;
        }

        private void SetTitle(string title)
        {
            // title
            root.AddImage().SetColor(Color.green).SetType(Image.Type.Sliced, false).RT().AnchorTop(40)
                .AddImage().SetColor(Color.black * 0.6f).RT().Margin(2)
                .AddText(title).RT().Margin(0, 0, 6, 10);
        }

        private void Awake()
        {
            transform.SetParent(UGUI.canvas.transform, false);
            root = gameObject.AddComponent<RectTransform>();
            // bg
            root.AddImage().SetColor(Color.gray);
            root.AddImage().SetColor(Color.green).SetType(Image.Type.Sliced, false);
        }
    }
}
