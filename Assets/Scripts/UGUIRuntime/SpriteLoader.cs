using System;
using System.Collections.Generic;
using UnityEngine;

namespace psyhack
{
    public static class SpriteLoader
    {
        static Dictionary<string, Sprite> _cachedSprites = new Dictionary<string, Sprite>();
        static Dictionary<string, Action<Sprite>> _cachedLoading = new Dictionary<string, Action<Sprite>>();

        public static void Clear()
        {
            _cachedSprites.Clear();
            _cachedLoading.Clear();
        }

        private static Sprite CreateSprite(Texture2D texture, float borderAll)
        {
            var rect = new Rect(0, 0, texture.width, texture.height);
            var border = new Vector4(borderAll, borderAll, borderAll, borderAll);
            return Sprite.Create(texture, rect, Vector2.zero, 100, 0, SpriteMeshType.FullRect, border);
        }

        private static bool CacheLoading(string url, Action<Sprite> callback)
        {
            if (_cachedLoading.ContainsKey(url))
            {
                _cachedLoading[url] += callback;
                return true;
            }
            else
            {
                _cachedLoading[url] = callback;
                return false;
            }
        }

        private static void InvokeLoading(string url, Sprite sprite)
        {
            if (_cachedLoading.TryGetValue(url, out var cache))
            {
                cache?.Invoke(sprite);
                _cachedLoading.Remove(url);
            }
        }

        public static void LoadFromUrl(string url, float border, Action<Sprite> callback, bool cache = true)
        {
            if (!cache)
            {
                TextureLoader.LoadFromUrl(url, (texture) =>
                {
                    if (texture == null)
                    {
                        callback?.Invoke(null);
                    }
                    else
                    {
                        var sprite = CreateSprite(texture, border);
                        callback?.Invoke(sprite);
                    }
                });
                return;
            }

            if (_cachedSprites.TryGetValue(url, out var _sprite))
            {
                callback?.Invoke(_sprite);
                return;
            }

            if (CacheLoading(url, callback))
            {
                return;
            }

            TextureLoader.LoadFromUrl(url, (texture) =>
            {
                if (texture == null)
                {
                    InvokeLoading(url, null);
                }
                else
                {
                    var sprite = CreateSprite(texture, border);
                    _cachedSprites.Add(url, sprite);
                    InvokeLoading(url, sprite);
                }
                _cachedLoading.Remove(url);
            });
        }
    }
}
