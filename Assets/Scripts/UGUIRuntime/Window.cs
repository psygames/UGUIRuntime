using UnityEngine;
using UnityEngine.UI;

namespace UGUIRuntime
{
    public class Window : CustomUI
    {
        public RectTransform body;
        public Window SetTitle(string title)
        {
            // title
            root.AddImage("title").SetColor(Color.green).SetType(Image.Type.Sliced, false).RT().AnchorTop(40)
                .AddImage("bg").SetColor(Color.black * 0.6f).RT().Margin(2)
                .AddText().SetText(title).SetAlignment(TextAnchor.UpperCenter).RT().Margin(4, 4, 4, 20);
            return this;
        }

        private void Awake()
        {
            // bg
            root.AddImage("bg").SetColor(Color.black).RT().Margin();
            root.AddImage("border").SetColor(Color.green).SetType(Image.Type.Sliced, false).RT().Margin();
            body = root.AddNode("body").Margin(40, 0, 0, 0);
        }
    }
}
