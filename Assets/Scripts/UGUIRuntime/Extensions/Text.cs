using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace psyhack
{
    public static partial class UGUIRuntimeExtensions
    {
        private static Dictionary<int, Font> fonts = new Dictionary<int, Font>();

        public static Text SetFont(this Text text, int size = 24)
        {
            if (!fonts.TryGetValue(size, out var font))
            {
                font = Font.CreateDynamicFontFromOSFont("Arial", size);
                fonts.Add(size, font);
            }
            text.font = font;
            text.fontSize = size;
            return text;
        }

        public static Text SetText(this Text comp, string text)
        {
            comp.text = text;
            return comp;
        }

        public static Text SetAlignment(this Text comp, TextAnchor alignment)
        {
            comp.alignment = alignment;
            return comp;
        }

        public static Text SetColor(this Text text, Color32 color)
        {
            text.color = color;
            return text;
        }

        public static Text SetPosition(this Text comp, float x, float y)
        {
            comp.GetRectTransform().SetPosition(x, y);
            return comp;
        }

        public static Text SetSize(this Text comp, float w, float h)
        {
            comp.GetRectTransform().SetSize(w, h);
            return comp;
        }
    }
}
