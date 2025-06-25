using UGUIRuntime;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        UGUI.Create();
        var hierarchy = gameObject.AddComponent<Hierarchy>();
        var inspector = gameObject.AddComponent<Inspector>();
        hierarchy.onSelectNode += (node) =>
        {
            var trans = node.data as Transform;
            if (trans)
            {
                inspector.SetObject(trans.gameObject);
            }
        };
    }
}
