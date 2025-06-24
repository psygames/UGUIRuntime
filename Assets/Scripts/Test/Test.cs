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
        var rootObj = GameObject.Find("Canvas");
        var node = tw.AddNode(rootObj.name);
        node.data = rootObj.transform;
        node.onFolded += FoldNode;
    }

    private void FoldNode(TreeNode node, bool isFold)
    {
        if (isFold)
        {
            node.ClearChildren();
        }
        else
        {
            var refTrans = node.data as Transform;
            for (int i = 0; i < refTrans.childCount; i++)
            {
                var t = refTrans.GetChild(i);
                var child = node.AddNode(t.name, t.name);
                child.SetCanFold(t.childCount > 0);
                child.data = t;
                child.onFolded += FoldNode;
            }
        }
    }
}
