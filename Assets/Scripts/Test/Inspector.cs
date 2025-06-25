using UGUIRuntime;
using UnityEngine;

public class Inspector : MonoBehaviour
{
    private Window wind;
    private RectTransform root;
    private GameObject target;
    private int offset = 0;

    private void Awake()
    {
        CreateWindow();
    }

    private void CreateWindow()
    {
        wind = UGUI.root.AddWindow().SetTitle("Inspector");
        wind.RT().SetRect(new Rect(1000, 200, 880, 600));
        root = wind.body.AddNode("root").Margin();
    }

    public void SetObject(GameObject go)
    {
        target = go;
        offset = 0;

        if (root)
        {
            GameObject.Destroy(root.gameObject);
            root = wind.body.AddNode("root").Margin(20, 10);
        }

        AddTiltle();
        AddLine();

        foreach (var comp in go.GetComponents<Component>())
        {
            AddComp(comp);
            AddLine();
        }
    }

    private void AddTiltle()
    {
        root.AddToggle("active").SetValue(target.activeSelf).SetListener((isOn) =>
        {
            target?.SetActive(isOn);
        });

        root.AddText("name").SetText(target.name).RT().Margin(0, 0, 0, 50);
        offset += 40;
    }

    private void AddLine()
    {
        root.AddImage("line").RT().AnchorTop(2, offset);
        offset += 10;
    }

    private void AddComp(Component comp)
    {
        if (comp is MonoBehaviour)
        {
            var mono = comp as MonoBehaviour;
            root.AddToggle().SetValue(mono.enabled).SetListener((isOn) =>
            {
                mono.enabled = isOn;
            }).SetPosition(0, offset);
        }
        root.AddText().SetText(comp.GetType().Name).RT().Margin(offset, 0, 0, 50);
        offset += 40;
    }
}
