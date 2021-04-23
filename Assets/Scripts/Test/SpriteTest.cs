using UGUIRuntime;
using UnityEngine;
using UnityEngine.UI;

public class SpriteTest : MonoBehaviour
{
    public string url;
    public Image image;

    private void Awake()
    {
        Boot.instance.scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        Boot.instance.scaler.referenceResolution = new Vector2(1920, 1080);
        Boot.instance.scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        Build();
    }

    Image backgroud;
    void Build()
    {
        Boot.instance.SetRemoteUrlHost("http://39.105.150.229:8741/images/");
        var root = Boot.instance.canvasRoot.AddNode("Root");
        root.anchorMin = Vector2.zero;
        root.anchorMax = Vector2.one;

        root.AddImage("ability")
            .SetSprite("icons/icons_small_white/ability_icon.png")
            .SetAnchoredPosition(new Vector2(10, 100));

        var button1 = root.AddButton("button1").SetSprite("buttons/basic/filled/green_button.png")
            .SetAnchoredPosition(new Vector2(100, 200))
            .SetText("Load Backgroud")
            .SetListener((_button) =>
            {
                if (!backgroud)
                {
                    backgroud = root.AddImage("background")
                        .SetAnchoredPosition(new Vector2(0, 10))
                        .SetSizeDelta(new Vector2(1200, 1200))
                        .SetSiblingIndex(0)
                        .SetSpriteUrl(url);
                }
                else
                {
                    backgroud.SetSpriteUrl(url);
                }
            });

        var button2 = root.AddButton("button2").SetSprite("buttons/basic/outline/bluelight_button_outline.png")
            .SetAnchoredPosition(new Vector2(200, 300))
            .SetListener((_button) =>
            {
                button1.SetSprite("buttons/basic/filled/green_button.png");
            });
    }
}
