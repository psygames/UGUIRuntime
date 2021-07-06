using System;
using System.Collections.Generic;
using UnityEngine;

namespace psyhack
{
    public static class SpriteLoader
    {
        static Dictionary<string, Sprite> _cachedSprites = new Dictionary<string, Sprite>();
        static Dictionary<string, bool> _cachedSpritesLoading = new Dictionary<string, bool>();
        static Dictionary<string, Action<Sprite>> _cachedSpriteLoadingCallback = new Dictionary<string, Action<Sprite>>();

        private static bool IsLoading(string url)
        {
            return _cachedSpritesLoading.TryGetValue(url, out var loading) && loading;
        }

        private static void SetLoading(string url, bool isLoading)
        {
            _cachedSpritesLoading[url] = isLoading;
        }

        // 
        private static Rect GetBorder()
        {
            return default;
        }

        public static void Clear()
        {
            _cachedSprites.Clear();
            _cachedSpritesLoading.Clear();
            _cachedSpriteLoadingCallback.Clear();

        }

        public static void LoadFromUrl(string url, Action<Sprite> callback, bool cached = false)
        {
            if (!cached)
            {
                TextureLoader.LoadFromUrl(url, (texture) =>
                {
                    if (texture == null)
                    {
                        callback?.Invoke(null);
                        return;
                    }
                    var sprite = Sprite.Create(texture,
                        new Rect(0, 0, texture.width, texture.height),
                        Vector2.zero);
                    callback?.Invoke(sprite);
                });
                return;
            }

            if (_cachedSprites.TryGetValue(url, out var _sprite))
            {
                callback?.Invoke(_sprite);
                return;
            }

            if (IsLoading(url))
            {
                if (_cachedSpriteLoadingCallback.ContainsKey(url))
                {
                    _cachedSpriteLoadingCallback[url] += callback;
                }
                else
                {
                    _cachedSpriteLoadingCallback[url] = callback;
                }
                return;
            }

            SetLoading(url, true);
            TextureLoader.LoadFromUrl(url, (texture) =>
            {
                SetLoading(url, false);
                if (texture == null)
                {
                    if (_cachedSpriteLoadingCallback.TryGetValue(url, out var cache))
                    {
                        cache?.Invoke(null);
                        _cachedSpriteLoadingCallback.Remove(url);
                    }
                    callback?.Invoke(null);
                }
                else
                {
                    var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    _cachedSprites.Add(url, sprite);
                    if (_cachedSpriteLoadingCallback.TryGetValue(url, out var cache))
                    {
                        cache?.Invoke(sprite);
                        _cachedSpriteLoadingCallback.Remove(url);
                    }
                    callback?.Invoke(sprite);
                }
            });
        }
    }
}
