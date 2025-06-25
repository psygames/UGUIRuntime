using UGUIRuntime;
using UnityEngine;
using static UnityEngine.UIElements.VisualElement;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        UGUI.Create();
        CreateHierarchy();
    }

    private void CreateInspector()
    {
        var wind = UGUI.root.AddWindow().SetTitle("Inspector");
    }

    private void CreateHierarchy()
    {
        var wind = UGUI.root.AddWindow().SetTitle("Hierarchy");
        wind.RT().SetRect(new Rect(100, 200, 880, 600));
        var tw = wind.body.AddTreeView();
        tw.RT().Margin(2);
        var rootObj = GameObject.Find("Canvas");
        var node = tw.AddNode(rootObj.name);
        node.data = rootObj.transform;
        node.onFolded += OnFolded;
        node.onSelected += OnSelected;
    }

    private void OnFolded(TreeNode node, bool isFold)
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
                child.onFolded += OnFolded;
            }
        }
    }

    private void OnSelected(TreeNode node)
    {

    }
}
