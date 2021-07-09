using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace psyhack
{
    public static class UIBuilder
    {
        public static float space = 3;
        public static float height = 50;
        public static float position = 0;
        public static RectTransform root;

        private const int titleWidth = 400;
        private const int contentWidth = 400;
        private const int rootWidth = 800;

        private const int fontSize = 24;
        private static string fontname = "Microsoft YaHei";

        public static RectTransform CreateRoot()
        {
            root = UGUI.CreateUIRoot();
            root.SetSize(rootWidth, 1080);
            return root;
        }

        public static Image CreateBackground()
        {
            var item = root.AddImage()
                .SetSprite("rect")
                .SetSize(root.rect.width, root.rect.height)
                .SetColor(Color.black * 0.8f);
            return item;
        }

        private static Text AddText(RectTransform root, string text)
        {
            return root.AddText()
                .SetText(text)
                .SetPosition(10, -4)
                .SetSize(titleWidth - 20, height)
                .SetFont(fontSize, fontname);
        }

        public static RectTransform CreateItem()
        {
            var item = root.AddImage()
                .SetSprite("rect", 10)
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

        public static Switch CreateSwitch(string title, UnityAction<bool> listener)
        {
            var item = CreateItem();
            AddText(item, title);

            var _switch = item.AddSwitch()
                .SetPosition(titleWidth + (contentWidth - 82) / 2, 2)
                .SetSize(82, height - 4)
                .SetKnobPosition(-17, 17);

            _switch.GetBackground().SetSprite("circle", 20).SetColor(Color.white * 0.5f);
            _switch.GetCheckmark().SetSize(68, 36).SetSprite("circle", 20).SetColor(new Color32(80, 180, 80, 255));
            _switch.GetKnob().SetSize(38, 38).SetSprite("circle").SetColor(Color.white * 0.9f);

            return _switch;
        }

        public static Slider CreateSlider(string title, UnityAction<float> listener)
        {
            var item = CreateItem();
            AddText(item, title);

            var slider = item.AddSlider()
                .SetPosition(titleWidth + 10, (height - 20) / 2)
                .SetSize(contentWidth - 20, 20)
                .SetPadding(10);

            slider.GetBackground().SetSprite("rectr", 10).SetColor(Color.gray);
            slider.GetFill().SetSprite("rectr", 10).SetColor(Color.gray);
            slider.GetHandle().SetSprite("rectr", 10).SetSize(20, 48);

            return slider;
        }

        public static Dropdown CreateDropdown(string title, string[] options, UnityAction<int> listener)
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
                .SetTextPadding(0);

            dropdown.GetBackground().SetSprite("rect", 10).SetColor(Color.clear);
            dropdown.GetLabel().SetPosition(0, -4).SetAlignment(TextAnchor.MiddleCenter).SetFont(fontSize, fontname).SetColor(Color.white);
            dropdown.GetDropdownBackground().SetSprite("rect", 10).SetColor(Color.clear);
            dropdown.GetItemBackground().SetSprite("rect", 10).SetColor(new Color32(85, 85, 85, 240));
            dropdown.GetItemCheckmark().SetSprite("rect", 10).SetColor(Color.black * 0.2f);
            dropdown.GetItemLabel().SetPosition(0, -4).SetAlignment(TextAnchor.MiddleCenter).SetFont(fontSize, fontname).SetColor(Color.white);
            return dropdown;
        }
    }
}
