using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UGUIRuntime
{
    public static class Layout
    {
        public static float space = 3;
        public static float height = 50;
        public static float position = 0;
        public static RectTransform root;
        public static float indentLevel = 1;

        private const int titleWidth = 400;
        private const int contentWidth = 400;
        private const int rootWidth = 800;
        private const int rootHeight = 2400;

        private static float indent => 10 + indentLevel * 20;

        public static RectTransform CreateRoot()
        {
            root = UGUI.canvas.GetComponent<RectTransform>().AddNode("SB");
            root.SetSize(rootWidth, rootHeight);
            return root;
        }

        public static Image CreateBackground()
        {
            var item = root.AddImage()
                .SetSize(root.rect.width, root.rect.height)
                .SetColor(Color.black * 0.8f);
            return item;
        }

        private static Text AddText(RectTransform root, string text)
        {
            return root.AddText(text)
                .SetPosition(indent, -4)
                .SetSize(titleWidth - indent, height);
        }

        public static RectTransform CreateItem()
        {
            var item = root.AddImage()
                .SetPosition(0, position)
                .SetSize(rootWidth, height)
                .SetColor(Color.white * 0.3f);
            position += height + space;
            return item.rectTransform;
        }

        public static Text CreateText(string text)
        {
            return AddText(CreateItem(), text);
        }

        public static Switch CreateSwitch(string title, bool isOn, UnityAction<bool> listener)
        {
            var item = CreateItem();
            AddText(item, title);

            var _switch = item.AddSwitch()
                .SetPosition(titleWidth + (contentWidth - 80) / 2, 4)
                .SetSize(80, height - 8)
                .SetKnobPosition(-17, 17)
                .SetListener(listener)
                .SetValue(isOn);

            _switch.GetBackground().SetSprite("circle").SetColor(Color.white * 0.5f);
            _switch.GetCheckmark().SetSize(72, 36).SetSprite("circle").SetColor(new Color32(80, 180, 80, 255));
            _switch.GetKnob().SetSize(38, 38).SetSprite("circle").SetColor(Color.white * 0.9f);

            return _switch;
        }

        public static Slider CreateSlider(string title, float value, UnityAction<float> listener)
        {
            var item = CreateItem();
            AddText(item, title);

            var slider = item.AddSlider()
                .SetPosition(titleWidth + 10, (height - 20) / 2)
                .SetSize(contentWidth - 20, 20)
                .SetMargin(10)
                .SetListener(listener)
                .SetValue(value);

            slider.GetBackground().SetSprite("rectr").SetColor(Color.white * 0.5f);
            slider.GetFill().SetSprite("rectr").SetColor(Color.white * 0.8f);
            slider.GetHandle().SetSprite("rectr").SetSize(26, 40);

            return slider;
        }

        public static Dropdown CreateDropdown(string title, int index, string[] options, UnityAction<int> listener)
        {
            var item = CreateItem();
            AddText(item, title);

            var dropdown = item.AddDropdown()
                .SetOptions(options)
                .SetListener(listener)
                .SetPosition(titleWidth, 0)
                .SetSize(contentWidth, height)
                .SetDropdownHeight(160)
                .SetDropdownPaddingTop(0)
                .SetItemHeight(40)
                .SetTextMargin(0)
                .SetValue(index);

            dropdown.GetBackground().SetSprite("rect").SetColor(Color.clear);
            dropdown.GetLabel().SetPosition(0, -4).SetAlignment(TextAnchor.MiddleCenter).SetColor(Color.white);
            dropdown.GetDropdownBackground().SetSprite("rect").SetColor(Color.clear);
            dropdown.GetItemBackground().SetSprite("rect").SetColor(new Color32(85, 85, 85, 240));
            dropdown.GetItemCheckmark().SetSprite("rect").SetColor(Color.black * 0.2f);
            dropdown.GetItemLabel().SetPosition(0, -4).SetAlignment(TextAnchor.MiddleCenter).SetColor(Color.white);
            return dropdown;
        }
    }
}
