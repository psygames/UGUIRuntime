using System;
using System.Collections.Generic;
using UnityEngine;

namespace psyhack
{
    public static class SpriteLoader
    {
        static Dictionary<string, Texture2D> _cachedTextures = new Dictionary<string, Texture2D>();
        static Dictionary<string, Sprite> _cachedSprites = new Dictionary<string, Sprite>();
        static Dictionary<string, List<LoadingInfo>> _cachedLoading = new Dictionary<string, List<LoadingInfo>>();

        class LoadingInfo
        {
            public string url;
            public int border;
            public Action<Sprite> callback;

            public void Invoke(Sprite sprite)
            {
                callback?.Invoke(sprite);
            }
        }

        public static void ClearCache()
        {
            _cachedTextures.Clear();
            _cachedSprites.Clear();
            _cachedLoading.Clear();
        }

        private static Sprite CreateSprite(Texture2D texture, float borderAll)
        {
            var rect = new Rect(0, 0, texture.width, texture.height);
            var border = new Vector4(borderAll, borderAll, borderAll, borderAll);
            return Sprite.Create(texture, rect, Vector2.zero, 100, 0, SpriteMeshType.FullRect, border);
        }

        private static bool CacheLoading(string url, int border, Action<Sprite> callback)
        {
            var info = new LoadingInfo() { url = url, border = border, callback = callback };
            var isLoading = _cachedLoading.TryGetValue(url, out var list);
            if (!isLoading)
            {
                list = new List<LoadingInfo>();
                _cachedLoading[url] = list;
            }
            list.Add(info);
            return isLoading;
        }

        private static bool TryGetSprite(string url, float border, out Sprite sprite)
        {
            if (_cachedSprites.TryGetValue(url + border, out sprite))
            {
                return true;
            }
            return false;
        }

        private static void CacheSprite(string url, float border, Sprite sprite)
        {
            _cachedSprites.Add(url + border, sprite);
        }

        private static bool TryGetTexture(string url, out Texture2D texture)
        {
            if (_cachedTextures.TryGetValue(url, out texture))
            {
                return true;
            }
            return false;
        }

        private static void CacheTexture(string url, Texture2D texture)
        {
            _cachedTextures.Add(url, texture);
        }

        public static void LoadFromUrl(string url, int border, Action<Sprite> callback, bool cache = true)
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

            if (TryGetSprite(url, border, out var _sprite))
            {
                callback?.Invoke(_sprite);
                return;
            }

            if (TryGetTexture(url, out var _texture))
            {
                var sprite = CreateSprite(_texture, border);
                CacheSprite(url, border, sprite);
                callback?.Invoke(sprite);
                return;
            }

            if (CacheLoading(url, border, callback))
            {
                return;
            }

            TextureLoader.LoadFromUrl(url, (texture) =>
            {
                if (texture != null)
                {
                    CacheTexture(url, texture);
                }

                if (!_cachedLoading.TryGetValue(url, out var list))
                {
                    return;
                }

                if (texture == null)
                {
                    foreach (var info in list)
                    {
                        info.Invoke(null);
                    }
                    list.Clear();
                    _cachedLoading.Remove(url);
                }
                else
                {

                    foreach (var info in list)
                    {
                        if (!TryGetSprite(info.url, info.border, out var sprite))
                        {
                            sprite = CreateSprite(texture, info.border);
                            CacheSprite(url, info.border, sprite);
                        }
                        info.Invoke(sprite);
                    }
                    list.Clear();
                    _cachedLoading.Remove(url);
                }
            });
        }
    }
}
