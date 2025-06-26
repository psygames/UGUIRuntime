using System;
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
        var vlg = sv.content.GetComponent<VerticalLayoutGroup>();
        vlg.childControlHeight = true;
        vlg.padding = new RectOffset(10, 10, 10, 10);
        sv.RT().Margin(2);
        root = sv.content;
    }

    public void SetObject(GameObject go)
    {
        target = go;
        root.DestroyChildren();
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
        }).RT().SetPosition(10, 0);
        vle.AddText("name").SetText(target.name).RT().AnchorTop(30, 0, 50);
    }

    private void AddLine()
    {
        root.VLE(10).AddImage("line").RT().AnchorTop(2);
    }

    private void AddSlimLine()
    {
        root.VLE(5).AddImage("line").SetColor(Color.gray).RT().AnchorTop(1);
    }

    private void AddComp(Component comp)
    {
        var vle = root.VLE(35);
        var vlg = root.VLG();
        vlg.GetComponent<VerticalLayoutGroup>().childControlHeight = true;
        vle.AddFoldout().SetListener((isFolded) =>
        {
            if (isFolded)
            {
                vlg.DestroyChildren();
            }
            else
            {
                ReflectType(vlg, comp.GetType(), comp);
            }
        }).RT().SetPosition(10, 0);
        if (comp is MonoBehaviour)
        {
            var mono = comp as MonoBehaviour;
            vle.AddToggle().SetValue(mono.enabled).SetListener((isOn) =>
            {
                mono.enabled = isOn;
            }).RT().SetPosition(50, 0);
        }
        var type = comp.GetType();
        vle.AddText().SetText(type.Name).RT().AnchorTop(30, 0, 90);
        AddSlimLine();
    }

    private void ReflectType(RectTransform node, Type type, object obj)
    {
        var fields = type.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly);
        foreach (var item in fields)
        {
            var vle = node.VLE(30);
            vle.AddText().SetText(item.Name + ": " + item.GetValue(obj)).RT().AnchorTop(30, 0, 50);
        }

        var props = type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly);
        foreach (var item in props)
        {
            var vle = node.VLE(30);
            vle.AddText().SetText(item.Name + ": " + item.GetValue(obj)).RT().AnchorTop(30, 0, 50);
        }
    }
}
