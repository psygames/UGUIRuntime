using UGUIRuntime;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        UGUI.Create();
        UGUI.root.AddWindow().RT().SetRect(new Rect(100, 200, 880, 600));
    }
}
