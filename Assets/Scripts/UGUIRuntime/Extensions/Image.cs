using UnityEngine.UI;
namespace psyhack
{
    public static partial class UGUIRuntimeExtensions
    {
        private static void SetSpriteUrl(this Image image, string url,
            bool setNativeSize, bool showLoading)
        {
            image.enabled = false;
            var _maskId = 0;
            if (showLoading)
            {
                _maskId = LoadingMask.Show(image.rectTransform.position, image.rectTransform.rect.size);
            }
            SpriteLoader.LoadFromUrl(url, (sprite) =>
            {
                image.sprite = sprite;
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
            });
        }

        public static Image SetSprite(this Image image, string name,
            bool setNativeSize = false, bool showLoading = false)
        {
            var spriteHost = Root.instance.remoteUrlHost ?? "";
            var url = name.StartsWith("http") ? name : spriteHost + name + ".png";
            image.SetSpriteUrl(url, setNativeSize, showLoading);
            return image;
        }

        public static Image SetPos(this Image comp, float x, float y)
        {
            comp.GetRectTransform().SetPos(x, y);
            return comp;
        }

        public static Image SetSize(this Image comp, float w, float h)
        {
            comp.GetRectTransform().SetSize(w, h);
            return comp;
        }
    }
}
