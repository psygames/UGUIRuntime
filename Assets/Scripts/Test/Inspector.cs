using UGUIRuntime;
using UnityEngine;
using UnityEngine.UI;

public class Inspector : MonoBehaviour
{
    private Window wind;
    private RectTransform root;
    private GameObject target;

    private void Awake()
    {
        CreateWindow();
    }

    private void CreateWindow()
    {
        wind = UGUI.root.AddWindow().SetTitle("Inspector");
        wind.RT().SetRect(new Rect(1000, 200, 880, 600));
        var sv = wind.body.AddScrollView();
        sv.horizontal = false;
        sv.content.GetComponent<VerticalLayoutGroup>().childControlHeight = true;
        sv.RT().Margin();
        root = sv.content;
    }

    public void SetObject(GameObject go)
    {
        target = go;
        ClearRoot();
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
        var vle = root.VLE(40);
        vle.AddToggle("active").SetValue(target.activeSelf).SetListener((isOn) =>
        {
            target?.SetActive(isOn);
        });

        vle.AddText("name").SetText(target.name).RT().Margin(0, 0, 0, 50);
    }

    private void AddLine()
    {
        root.VLE(10).AddImage("line").RT();
    }

    private void AddComp(Component comp)
    {
        if (comp is MonoBehaviour)
        {
            var mono = comp as MonoBehaviour;
            root.AddToggle().SetValue(mono.enabled).SetListener((isOn) =>
            {
                mono.enabled = isOn;
            });
        }
        root.AddText().SetText(comp.GetType().Name);
    }

    private void ClearRoot()
    {
        for (int i = root.childCount - 1; i >= 0; i--)
        {
            var child = root.GetChild(i);
            if (child)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
