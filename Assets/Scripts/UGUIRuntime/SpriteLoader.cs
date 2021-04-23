using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace UGUIRuntime
{
    public static class SpriteLoader
    {
        static Dictionary<string, Sprite> _cachedSprites = new Dictionary<string, Sprite>();
        static Dictionary<string, bool> _cachedSpritesLoading = new Dictionary<string, bool>();

        private static bool IsLoading(string url)
        {
            return _cachedSpritesLoading.TryGetValue(url, out var loading) && loading;
        }

        private static void SetLoading(string url, bool isLoading)
        {
            _cachedSpritesLoading[url] = isLoading;
        }

        public static async Task<Sprite> LoadFromUrl(string url, bool cached = false)
        {
            if (!cached)
            {
                var texture = await TextureLoader.LoadFromUrl(url);
                if (texture == null) return null;
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            }

            if (!_cachedSprites.TryGetValue(url, out var sprite))
            {
                if (IsLoading(url))
                {
                    await Task.Yield();
                    while (IsLoading(url))
                    {
                        await Task.Yield();
                    }
                    _cachedSprites.TryGetValue(url, out sprite);
                }
                else
                {
                    SetLoading(url, true);
                    var texture = await TextureLoader.LoadFromUrl(url);
                    if (texture == null)
                    {
                        SetLoading(url, false);
                        return null;
                    }
                    sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    _cachedSprites.Add(url, sprite);
                    SetLoading(url, false);
                }
            }
            return sprite;
        }
    }
}
