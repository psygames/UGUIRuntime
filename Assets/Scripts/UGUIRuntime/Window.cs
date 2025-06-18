using UnityEngine;
using UnityEngine.UI;

namespace UGUIRuntime
{
    public class Window : CustomUI
    {
        public Window SetTitle(string title)
        {
            // title
            root.AddImage().SetColor(Color.green).SetType(Image.Type.Sliced, false).RT().AnchorTop(40)
                .AddImage().SetColor(Color.black * 0.6f).RT().Margin(2)
                .AddText(title).RT().Margin(0, 0, 6, 10);
            return this;
        }

        private void Awake()
        {
            // bg
            root.AddImage().SetColor(Color.gray).RT().Margin();
            root.AddImage().SetColor(Color.green).SetType(Image.Type.Sliced, false).RT().Margin();
        }
    }
}
