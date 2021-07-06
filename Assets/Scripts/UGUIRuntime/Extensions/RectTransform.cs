using UnityEngine;
using UnityEngine.UI;

namespace psyhack
{
    public static partial class UGUIRuntimeExtensions
    {
        private static RectTransform SetAnchoredPosition(this RectTransform rectTransform, Vector2 position)
        {
            rectTransform.anchoredPosition = position;
            return rectTransform;
        }

        private static RectTransform SetSizeDelta(this RectTransform rectTransform, Vector2 sizeDelta)
        {
            rectTransform.sizeDelta = sizeDelta;
            return rectTransform;
        }

        private static RectTransform SetAnchor(this RectTransform rectTransform, Vector2 min, Vector2 max)
        {
            rectTransform.anchorMin = min;
            rectTransform.anchorMax = max;
            return rectTransform;
        }

        private static RectTransform SetPadding(this RectTransform rectTransform, float paddingAll = 0f)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.sizeDelta = Vector2.one * -paddingAll;
            return rectTransform;
        }

        internal static void Reset(this RectTransform rectTransform)
        {
            rectTransform.sizeDelta = Vector2.zero;
            rectTransform.anchoredPosition3D = Vector3.zero;
            rectTransform.localScale = Vector3.one;
            rectTransform.localRotation = Quaternion.identity;
            rectTransform.pivot = Vector2.up;
            rectTransform.anchorMin = Vector2.up;
            rectTransform.anchorMax = Vector2.up;
        }

        private static RectTransform GetOrAddNode(this RectTransform rectTransform, string name)
        {
            var node = rectTransform.Find(name);
            if (!node)
            {
                node = rectTransform.AddNode(name);
            }
            return node.GetRectTransform();
        }

        private static T GetOrAddComponent<T>(this RectTransform rectTransform) where T : Component
        {
            var comp = rectTransform.GetComponent<T>();
            if (!comp)
            {
                comp = rectTransform.gameObject.AddComponent<T>();
            }
            return comp;
        }

        private static RectTransform GetRectTransform(this Component component)
        {
            return component.GetComponent<RectTransform>();
        }

        #region Set Items
        public static RectTransform SetIndex(this RectTransform rectTransform, int index)
        {
            rectTransform.SetSiblingIndex(index);
            return rectTransform;
        }

        public static RectTransform SetPos(this RectTransform rectTransform, float x, float y)
        {
            return rectTransform.SetPos(new Vector2(x, y));
        }

        public static RectTransform SetPos(this RectTransform rectTransform, Vector2 pos)
        {
            rectTransform.SetAnchoredPosition(new Vector2(pos.x, -pos.y));
            return rectTransform;
        }

        public static RectTransform SetSize(this RectTransform rectTransform, float w, float h)
        {
            rectTransform.SetSize(new Vector2(w, h));
            return rectTransform;
        }

        public static RectTransform SetSize(this RectTransform rectTransform, Vector2 size)
        {
            rectTransform.SetSizeDelta(size);
            return rectTransform;
        }
        #endregion

        #region Add Items
        public static RectTransform AddNode(this RectTransform rectTransform, string name = null)
        {
            var go = new GameObject(name ?? "node");
            var node = go.AddComponent<RectTransform>();
            node.SetParent(rectTransform);
            node.Reset();
            return node;
        }

        public static Image AddImage(this RectTransform rectTransform, string name = null)
        {
            var node = rectTransform.AddNode(name ?? "image");
            var image = node.gameObject.AddComponent<Image>();
            image.raycastTarget = false;
            return image;
        }

        public static Button AddButton(this RectTransform rectTransform, string name = null)
        {
            var node = rectTransform.AddNode(name ?? "button");
            var image = node.gameObject.AddComponent<Image>();
            var button = node.gameObject.AddComponent<Button>();
            button.image = image;
            return button;
        }

        private static Font fontDefault = null;
        public static Text AddText(this RectTransform rectTransform, string name = null)
        {
            if (fontDefault == null) fontDefault = Font.CreateDynamicFontFromOSFont("Arial", 24);
            var node = rectTransform.AddNode(name ?? "text");
            var text = node.gameObject.AddComponent<Text>();
            text.font = fontDefault;
            text.alignment = TextAnchor.MiddleLeft;
            text.fontSize = 24;
            text.raycastTarget = false;
            return text;
        }

        public static Toggle AddToggle(this RectTransform rectTransform, string name = null)
        {
            var node = rectTransform.AddNode(name ?? "toggle");
            node.AddNode("Background").AddNode("Checkmark");
            var toggle = node.gameObject.AddComponent<Toggle>();
            return toggle;
        }
        #endregion

    }
}
