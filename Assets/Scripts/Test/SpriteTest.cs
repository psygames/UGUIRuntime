using psyhack;
using UnityEngine;

public class SpriteTest : MonoBehaviour
{
    private void Awake()
    {
        Root.Create();
        Http.Create();
        Root.instance.SetRemoteUrlHost("http://39.105.150.229:8741/modern_clean_gui/");
        Build();
    }

    public void Build()
    {
        var ui = Root.instance.canvasRoot.AddNode("Top");
        ui.SetSize(Vector2.one * 400);

        var background = ui.AddImage("background")
            .SetSize(400, 400)
            .SetSprite("https://z3.ax1x.com/2021/07/02/R6CXlQ.png");

        var button = ui.AddButton("button")
            .SetPos(20, 40).SetSize(300, 100)
            .SetSprite("https://z3.ax1x.com/2021/07/02/R6CIeI.png")
            .SetText("测试按钮")
            .SetListener(() =>
            {
                Debug.LogError("test");
            }); ;

        var text = ui.AddText()
            .SetPos(100, 120).SetSize(100, 50)
            .SetFont(35).SetText("哈哈哈");

        var toggle = ui.AddToggle()
            .SetPos(10, 200).SetSize(44, 44)
            .SetSprite("Buttons/Button_TickBox_Off_Rounded", "Buttons/Button_TickBox_On_Rounded")
            .SetText("测试Toggle")
            .SetListener(isOn =>
            {
                Debug.LogError("Toggle: " + isOn);
            });
    }
}
