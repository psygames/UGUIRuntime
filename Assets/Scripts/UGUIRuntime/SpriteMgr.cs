using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UGUIRuntime
{
    public static class SpriteMgr
    {
        private static Dictionary<string, Sprite> m_sprites = new Dictionary<string, Sprite>();
        public static void AddSprite(string name, Texture2D texture, Vector4 border)
        {
            var rect = new Rect(0, 0, texture.width, texture.height);
            var sprite = Sprite.Create(texture, rect, Vector2.zero, 100, 0, SpriteMeshType.FullRect, border);
            m_sprites[name] = sprite;
        }

        public static void AddSpriteByBase64(string name, string base64, int width, int height, Vector4 border)
        {
            var bytes = System.Convert.FromBase64String(base64);
            var texture = new Texture2D(width, height);
            texture.LoadImage(bytes);
            AddSprite(name, texture, border);
        }

        public static Sprite GetSprite(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            return m_sprites.TryGetValue(name, out var sprite) ? sprite : null;
        }

        private static void CreateRect()
        {
            var texture = new Texture2D(5, 5);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    texture.SetPixel(i, j, Color.white);
                }
            }
            texture.Apply();
            AddSprite("rect", texture, new Vector4(2, 2, 2, 2));
        }

        private static void CreateTriangle()
        {
            var b64 = "iVBORw0KGgoAAAANSUhEUgAAAA0AAAANCAYAAABy6+R8AAAAnElEQVQoFWNgGBCwZcuWBZs3b1YlZDkjsgKgpntA/l8gXvnr16/+oKCgt8jyMDY2TWA5RkbGz//+/Zv+/fv3eWFhYb9gGkA0EzIHmf3//39eoMYyTk7OvVu3bvVBlsOpCaYIqFEaaMAkoNOrYWIsMAYe+glQY5e3t/cWmBqcmvD5CZsmcOj9/PkTZ+ihazoEdH+rr6/vbZhTBpYGAOeIQzDR8qr4AAAAAElFTkSuQmCC";
            AddSpriteByBase64("triangle", b64, 13, 13, Vector4.zero);
        }

        public static void Init()
        {
            CreateRect();
            CreateTriangle();
        }
    }
}
