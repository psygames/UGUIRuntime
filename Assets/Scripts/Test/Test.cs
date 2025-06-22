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
        tw.RT().Margin(2);
        var node = tw.AddNode("Canvas");

        RecursiveTreeNode(node, GameObject.Find("Canvas").transform);
    }

    private static void RecursiveTreeNode(TreeNode node, Transform t)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            var t1 = t.GetChild(i);
            var child = node.AddNode(t1.name, t1.name);
            RecursiveTreeNode(child, t1);
        }
    }
}
