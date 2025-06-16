using UGUIRuntime;
using UnityEngine;

public class SpriteTest : MonoBehaviour
{
    private void Awake()
    {
        UGUI.Create();
        // SimpleBuild();
        Window.Create("Test", new Rect(100, 200, 800, 600));
    }

    public void SimpleBuild()
    {
        Layout.CreateRoot();

        Layout.indentLevel = 0;
        Layout.CreateBackground();
        Layout.CreateText("简单构建UI");
        Layout.CreateSwitch("开关功能", true, (isOn) => { Debug.Log("开关: " + isOn); });

        Layout.indentLevel = 1;
        Layout.CreateSwitch("开关功能", true, (isOn) => { Debug.Log("开关: " + isOn); });
        Layout.CreateSwitch("开关功能", true, (isOn) => { Debug.Log("开关: " + isOn); });
        Layout.indentLevel = 2;
        Layout.CreateSwitch("开关功能", true, (isOn) => { Debug.Log("开关: " + isOn); });

        Layout.indentLevel = 0;
        Layout.CreateSlider("滑动条功能", 0.2f, (val) => { Debug.Log("滑动条: " + val); });

        var options = new string[] { "选项1", "选项2", "选项3", "选项4", "选项5", "选项6" };
        Layout.CreateDropdown("选项卡功能", 1, options, (index) => { Debug.Log(options[index]); });
    }

    private void OnGUI()
    {
        GUI.Button(new Rect(10, 10, 100, 30), "按钮1");
    }
}
