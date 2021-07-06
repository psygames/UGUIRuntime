﻿using UnityEngine;
using UnityEngine.UI;
namespace psyhack
{
    public static partial class UGUIRuntimeExtensions
    {
        public static Button SetListener(this Button button, UnityEngine.Events.UnityAction action)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(action);
            return button;
        }

        public static Button SetText(this Button button, string text)
        {
            var comp = button.GetRectTransform().GetOrAddNode("Text").GetOrAddComponent<Text>();
            comp.GetRectTransform().SetPadding();
            comp.SetFont();
            comp.text = text;
            comp.alignment = TextAnchor.MiddleCenter;
            return button;
        }

        public static Button SetSprite(this Button button, string name,
            bool setNativeSize = false, bool showLoading = false)
        {
            if (button.image)
            {
                button.image.SetSprite(name, setNativeSize, showLoading);
            }
            return button;
        }

        public static Button SetPos(this Button comp, float x, float y)
        {
            comp.GetRectTransform().SetPos(x, y);
            return comp;
        }

        public static Button SetSize(this Button comp, float w, float h)
        {
            comp.GetRectTransform().SetSize(w, h);
            return comp;
        }
    }
}