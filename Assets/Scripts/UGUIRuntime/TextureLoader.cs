using System;
using UnityEngine;

namespace UGUIRuntime
{
    public class TextureLoader
    {
        public static void LoadFromUrl(string url, Action<Texture2D> callback)
        {
            Http.Download(url, (bytes) =>
            {
                var texture = new Texture2D(0, 0);
                texture.LoadImage(bytes);
                callback?.Invoke(texture);
            });
        }
    }
}
