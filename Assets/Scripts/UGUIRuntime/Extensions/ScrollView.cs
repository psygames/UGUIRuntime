using UnityEngine;
using UnityEngine.UI;

namespace UGUIRuntime
{
    public static partial class UGUIRuntimeExtensions
    {
        public static RectTransform RT(this ScrollRect sr)
        {
            return sr.GetRectTransform();
        }

        public static ScrollRect AddScrollView(this RectTransform rectTransform, string name = null)
        {
            var node = rectTransform.AddNode(name ?? "scrollview");
            var bg = node.GetOrAddComponent<Image>();
            bg.SetColor(Color.gray * 0.5f);
            var comp = node.GetOrAddComponent<ScrollRect>();
            var vp = node.AddImage("viewport");
            vp.gameObject.AddComponent<Mask>().showMaskGraphic = false;
            var content = vp.RT().AddNode("content");
            content.SetAnchorMinMax(Vector2.up, Vector2.one);
            comp.viewport = vp.RT();
            comp.content = content;
            content.GetOrAddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            var vlg = content.GetOrAddComponent<VerticalLayoutGroup>();
            vlg.childForceExpandWidth = false;
            vlg.childForceExpandHeight = false;
            vlg.childControlWidth = false;
            vlg.childControlHeight = false;

            var hor = node.AddImage("Scrollbar Horizontal").SetColor(Color.blue * 0.5f);
            hor.RT().AnchorBottom(20).SetPivot(Vector2.zero);
            var horbar = hor.RT().GetOrAddComponent<Scrollbar>();
            horbar.direction = Scrollbar.Direction.LeftToRight;
            var horhandle = hor.RT().AddNode("Sliding Area").SetPivotCenter().Margin(10)
                .AddImage("Handle").SetRaycast().SetColor(Color.red * 0.5f).RT().SetPivotCenter().Margin(-10);
            horbar.handleRect = horhandle;
            comp.horizontalScrollbar = horbar;
            comp.horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;

            var ver = node.AddImage("Scrollbar Vertical").SetColor(Color.blue * 0.5f);
            ver.RT().AnchorRight(20).SetPivot(Vector2.one);
            var verbar = ver.RT().GetOrAddComponent<Scrollbar>();
            verbar.direction = Scrollbar.Direction.BottomToTop;
            var verhandle = ver.RT().AddNode("Sliding Area").SetPivotCenter().Margin(10)
                .AddImage("Handle").SetRaycast().SetColor(Color.red * 0.5f).RT().SetPivotCenter().Margin(-10);
            verbar.handleRect = verhandle;
            comp.verticalScrollbar = verbar;
            comp.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            return comp;
        }

    }
}
