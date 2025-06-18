using UGUIRuntime;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        UGUI.Create();
        var wind = UGUI.root.AddWindow().SetTitle("TEST");
        wind.RT().SetRect(new Rect(100, 200, 880, 600));
        var tw = wind.body.AddTreeView();
        tw.RT().Margin();
        tw.RT().AddTreeNode("sss");
    }
}
