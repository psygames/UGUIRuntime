﻿using psyhack;
using UnityEngine;

public class SpriteTest : MonoBehaviour
{
    private void Awake()
    {
        Root.Create();
        Http.Create();
        Root.instance.SetRemoteUrlHost("http://39.105.150.229:8741/psyhack_img/");
        Build();
    }

    public void Build()
    {
        var ui = Root.instance.canvasRoot.AddNode("Top");
        ui.SetSize(Vector2.one * 400);

        var background = ui.AddImage("background")
            .SetSize(400, 400)
            .SetSprite("rect", 10)
            .SetColor(new Color32(85, 95, 115, 255));

        var button = ui.AddButton("button")
            .SetPosition(30, 40).SetSize(300, 100)
            .SetSprite("rect", 10)
            .SetText("测试按钮")
            .SetColor(Color.gray)
            .SetListener(() =>
            {
                Debug.Log("Button click");
            });

        var text = ui.AddText()
            .SetPosition(250, 120).SetSize(100, 50)
            .SetFont(35).SetText("哈哈哈");

        var toggle = ui.AddToggle()
            .SetPosition(30, 200).SetSize(44, 44)
            .SetText("测试Toggle")
            .SetListener(isOn =>
            {
                Debug.Log("Toggle: " + isOn);
            });

        toggle.GetBackground().SetSprite("radiobutton");
        toggle.GetCheckmark().SetSize(30, 30).SetSprite("circle").SetColor(Color.black);


        var checkbox = ui.AddToggle()
            .SetPosition(30, 250).SetSize(44, 44)
            .SetText("测试Checkbox")
            .SetListener(isOn =>
            {
                Debug.Log("checkbox: " + isOn);
            });

        checkbox.GetBackground().SetSprite("checkbox");
        checkbox.GetCheckmark().SetSize(30, 30).SetSprite("checked").SetColor(Color.black);


        var _switch = ui.AddSwitch()
            .SetPosition(30, 300).SetSize(88, 44)
            .SetKnobPosition(-18, 18)
            .SetListener(isOn =>
            {
                Debug.Log("switch: " + isOn);
            });

        _switch.GetBackground().SetSprite("circle", 20).SetColor(Color.gray);
        _switch.GetCheckmark().SetSize(68, 32).SetSprite("circle", 20).SetColor(Color.green);
        _switch.GetKnob().SetSize(30, 30).SetSprite("circle");


        var slider = ui.AddSlider()
            .SetPosition(30, 350).SetSize(200, 20)
            .SetPadding(15)
            .SetListener((val) =>
            {
                Debug.Log("slider: " + val);
            });

        slider.GetBackground().SetSprite("rect", 10);
        slider.GetFill().SetSprite("rect", 10).SetColor(Color.gray);
        slider.GetHandle().SetSprite("circle").SetSize(30, 30);
    }
}
