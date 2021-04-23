using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace UGUIRuntime
{
    public static class UGUIRuntimeExtensions
    {
        private static async void _SetSpriteUrl(this Image image, string url,
            bool setNativeSize = true, bool showLoading = true)
        {
            image.enabled = false;
            var _maskId = 0;
            if (showLoading)
            {
                _maskId = LoadingMask.Show(image.rectTransform.position, image.rectTransform.rect.size);
            }
            image.sprite = await SpriteLoader.LoadFromUrl(url);
            image.enabled = true;
            if (setNativeSize)
            {
                image.type = Image.Type.Simple;
                image.SetNativeSize();
            }
            if (showLoading)
            {
                LoadingMask.Hide(_maskId);
            }
        }

        public static Image SetSpriteUrl(this Image image, string url,
            bool setNativeSize = true, bool showLoading = true)
        {
            image._SetSpriteUrl(url, setNativeSize, showLoading);
            return image;
        }

        public static Button SetSpriteUrl(this Button button, string url,
            bool setNativeSize = true, bool showLoading = true)
        {
            if (button.image)
            {
                button.image.SetSpriteUrl(url, setNativeSize, showLoading);
            }
            return button;
        }

        public static Image SetSprite(this Image image, string name)
        {
            var spriteHost = Boot.instance.remoteUrlHost;
            image.SetSpriteUrl(spriteHost + name);
            return image;
        }

        public static Button SetSprite(this Button button, string name)
        {
            var spriteHost = Boot.instance.remoteUrlHost;
            button.SetSpriteUrl(spriteHost + name);
            return button;
        }

        public static RectTransform AddNode(this RectTransform rectTransform, string name = null)
        {
            var node = Boot.instance.Create<RectTransform>(name, rectTransform);
            node.SetParent(rectTransform);
            node.Reset();
            return node;
        }

        public static Image AddImage(this RectTransform rectTransform, string name = null)
        {
            var image = Boot.instance.Create<Image>(name, rectTransform);
            image.rectTransform.SetParent(rectTransform);
            image.rectTransform.Reset();
            image.raycastTarget = false;
            return image;
        }

        public static Button AddButton(this RectTransform rectTransform, string name = null)
        {
            var button = Boot.instance.Create<Button>(name, rectTransform);
            button.GetComponent<RectTransform>().SetParent(rectTransform);
            button.GetComponent<RectTransform>().Reset();
            return button;
        }

        private static Font fontDefault = null;
        public static Text AddText(this RectTransform rectTransform, string name = null)
        {
            if (fontDefault == null) fontDefault = Font.CreateDynamicFontFromOSFont("Arial", 24);
            var text = Boot.instance.Create<Text>(name, rectTransform);
            text.rectTransform.SetParent(rectTransform);
            text.rectTransform.Reset();
            text.font = fontDefault;
            text.alignment = TextAnchor.MiddleCenter;
            text.fontSize = 24;
            text.raycastTarget = false;
            return text;
        }

        public static RectTransform SetAnchoredPosition(this RectTransform rectTransform, Vector2 position)
        {
            rectTransform.anchoredPosition = position;
            return rectTransform;
        }

        public static Button SetAnchoredPosition(this Button button, Vector2 position)
        {
            button.GetComponent<RectTransform>().anchoredPosition = position;
            return button;
        }

        public static Image SetAnchoredPosition(this Image image, Vector2 position)
        {
            image.rectTransform.anchoredPosition = position;
            return image;
        }

        public static RectTransform SetSizeDelta(this RectTransform rectTransform, Vector2 sizeDelta)
        {
            rectTransform.sizeDelta = sizeDelta;
            return rectTransform;
        }

        public static Button SetSizeDelta(this Button button, Vector2 sizeDelta)
        {
            button.GetComponent<RectTransform>().sizeDelta = sizeDelta;
            return button;
        }

        public static Image SetSizeDelta(this Image image, Vector2 sizeDelta)
        {
            image.rectTransform.sizeDelta = sizeDelta;
            return image;
        }

        public static Text SetSizeDelta(this Text text, Vector2 sizeDelta)
        {
            text.rectTransform.sizeDelta = sizeDelta;
            return text;
        }

        public static RectTransform SetSiblingIndex(this RectTransform rectTransform, int index)
        {
            rectTransform.SetSiblingIndex(index);
            return rectTransform;
        }

        public static Button SetSiblingIndex(this Button button, int index)
        {
            button.GetComponent<RectTransform>().SetSiblingIndex(index);
            return button;
        }

        public static Image SetSiblingIndex(this Image image, int index)
        {
            image.rectTransform.SetSiblingIndex(index);
            return image;
        }

        public static Text SetSiblingIndex(this Text text, int index)
        {
            text.rectTransform.SetSiblingIndex(index);
            return text;
        }

        public static RectTransform SetAnchor(this RectTransform rectTransform, Vector2 min, Vector2 max)
        {
            rectTransform.anchorMin = min;
            rectTransform.anchorMax = max;
            return rectTransform;
        }

        public static Button SetAnchor(this Button button, Vector2 min, Vector2 max)
        {
            button.GetComponent<RectTransform>().anchorMin = min;
            button.GetComponent<RectTransform>().anchorMax = max;
            return button;
        }

        public static Image SetAnchor(this Image image, Vector2 min, Vector2 max)
        {
            image.rectTransform.anchorMin = min;
            image.rectTransform.anchorMax = max;
            return image;
        }

        public static Text SetAnchor(this Text text, Vector2 min, Vector2 max)
        {
            text.rectTransform.anchorMin = min;
            text.rectTransform.anchorMax = max;
            return text;
        }

        public static Button SetListener(this Button button, UnityAction<Button> action)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => action?.Invoke(button));
            return button;
        }

        public static Button SetText(this Button button, string text)
        {
            Text comp;
            if (button.transform.childCount > 0 && button.transform.GetChild(0)
                && button.transform.GetChild(0).name == "Text"
                && button.transform.GetChild(0).GetComponent<Text>())
            {
                comp = button.transform.GetChild(0).GetComponent<Text>();
            }
            else
            {
                comp = button.GetComponent<RectTransform>().AddText("Text")
                    .SetAnchor(Vector2.zero, Vector2.one)
                    .SetSizeDelta(Vector2.zero);
            }
            comp.text = text;
            return button;
        }

        internal static void Reset(this RectTransform rectTransform)
        {
            rectTransform.anchoredPosition3D = Vector3.zero;
            rectTransform.localScale = Vector3.one;
            rectTransform.localRotation = Quaternion.identity;
        }
    }
}
