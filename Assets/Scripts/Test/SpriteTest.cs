using UGUIRuntime;
using UnityEngine;

public class SpriteTest : MonoBehaviour
{
    private void Awake()
    {
        UGUI.Create();
        SimpleBuild();
    }

    public void SimpleBuild()
    {
        SimpleBuilder.CreateRoot();

        SimpleBuilder.indentLevel = 0;
        SimpleBuilder.CreateBackground();
        SimpleBuilder.CreateText("简单构建UI");
        SimpleBuilder.CreateSwitch("开关功能", true, (isOn) => { Debug.Log("开关: " + isOn); });

        SimpleBuilder.indentLevel = 1;
        SimpleBuilder.CreateSwitch("开关功能", true, (isOn) => { Debug.Log("开关: " + isOn); });
        SimpleBuilder.CreateSwitch("开关功能", true, (isOn) => { Debug.Log("开关: " + isOn); });
        SimpleBuilder.indentLevel = 2;
        SimpleBuilder.CreateSwitch("开关功能", true, (isOn) => { Debug.Log("开关: " + isOn); });

        SimpleBuilder.indentLevel = 0;
        SimpleBuilder.CreateSlider("滑动条功能", 0.2f, (val) => { Debug.Log("滑动条: " + val); });

        var options = new string[] { "选项1", "选项2", "选项3", "选项4", "选项5", "选项6" };
        SimpleBuilder.CreateDropdown("选项卡功能", 1, options, (index) => { Debug.Log(options[index]); });
    }
}
