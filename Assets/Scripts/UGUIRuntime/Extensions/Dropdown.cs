using UnityEngine;
using UnityEngine.UI;

namespace UGUIRuntime
{
    public static partial class UGUIRuntimeExtensions
    {
        public static Image GetBackground(this Dropdown dropdown)
        {
            var bg = dropdown.GetRectTransform().GetOrAddComponent<Image>();
            dropdown.targetGraphic = bg;
            return bg;
        }

        public static Text GetLabel(this Dropdown dropdown)
        {
            var text = dropdown.GetRectTransform().GetOrAddNode("Label").GetOrAddComponent<Text>();
            dropdown.captionText = text;
            return text;
        }

        public static RectTransform GetDropdownArea(this Dropdown dropdown)
        {
            return dropdown.GetRectTransform()
                .GetOrAddNode("Template");
        }

        public static Image GetDropdownBackground(this Dropdown dropdown)
        {
            return dropdown.GetDropdownArea().GetOrAddComponent<Image>();
        }

        public static ScrollRect GetDropdownScrollRect(this Dropdown dropdown)
        {
            return dropdown.GetDropdownArea().GetOrAddComponent<ScrollRect>();
        }

        public static RectTransform GetViewport(this Dropdown dropdown)
        {
            return dropdown.GetDropdownArea()
                .GetOrAddNode("Viewport");
        }

        public static RectTransform GetContentArea(this Dropdown dropdown)
        {
            return dropdown.GetViewport()
                .GetOrAddNode("Content");
        }

        public static RectTransform GetItem(this Dropdown dropdown)
        {
            return dropdown.GetContentArea()
                .GetOrAddNode("Item");
        }

        public static Toggle GetItemToggle(this Dropdown dropdown)
        {
            return dropdown.GetItem().GetOrAddComponent<Toggle>();
        }

        public static Image GetItemCheckmark(this Dropdown dropdown)
        {
            var mark = dropdown.GetItem()
                .GetOrAddNode("Item Checkmark")
                .GetOrAddComponent<Image>();
            dropdown.GetItemToggle().graphic = mark;
            return mark;
        }

        public static Image GetItemBackground(this Dropdown dropdown)
        {
            var bg = dropdown.GetItem()
                .GetOrAddNode("Item Background")
                .GetOrAddComponent<Image>();
            dropdown.GetItemToggle().targetGraphic = bg;
            return bg;
        }

        public static Text GetItemLabel(this Dropdown dropdown)
        {
            var text = dropdown.GetItem()
                .GetOrAddNode("Item Label")
                .GetOrAddComponent<Text>();
            dropdown.itemText = text;
            return text;
        }

        public static Dropdown SetOptions(this Dropdown dropdown, params string[] titles)
        {
            if (dropdown.options == null)
            {
                dropdown.options = new System.Collections.Generic.List<Dropdown.OptionData>();
            }
            dropdown.options.Clear();
            foreach (var title in titles)
            {
                dropdown.options.Add(new Dropdown.OptionData(title));
            }
            return dropdown;
        }

        public static Dropdown SetListener(this Dropdown dropdown, UnityEngine.Events.UnityAction<int> action)
        {
            dropdown.onValueChanged.RemoveAllListeners();
            dropdown.onValueChanged.AddListener(action);
            return dropdown;
        }

        public static Dropdown SetValue(this Dropdown dropdown, int index)
        {
            dropdown.value = index;
            return dropdown;
        }

        public static Dropdown SetPosition(this Dropdown comp, float x, float y)
        {
            comp.GetRectTransform().SetPosition(x, y);
            return comp;
        }

        public static Dropdown SetSize(this Dropdown comp, float w, float h)
        {
            comp.GetRectTransform().SetSize(w, h);
            return comp;
        }

        public static Dropdown SetDropdownHeight(this Dropdown comp, float height)
        {
            comp.GetDropdownArea().SetSizeDelta(new Vector2(0, height));
            return comp;
        }

        public static Dropdown SetDropdownPaddingTop(this Dropdown comp, float padding)
        {
            comp.GetDropdownArea().anchoredPosition = new Vector2(0, padding);
            return comp;
        }

        public static Dropdown SetItemHeight(this Dropdown comp, float height)
        {
            comp.GetItem().SetSizeDelta(new Vector2(0, height));
            comp.GetContentArea().SetSizeDelta(new Vector2(0, height));
            return comp;
        }

        public static Dropdown SetTextMargin(this Dropdown comp, float horizontal)
        {
            comp.GetLabel().GetRectTransform().SetMargin(horizontal, 0);
            comp.GetItemLabel().GetRectTransform().SetMargin(horizontal, 0);
            return comp;
        }
    }
}
