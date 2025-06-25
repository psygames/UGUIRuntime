using System.Collections.Generic;
using UnityEngine;

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

        private static void CreateNormal()
        {
            var triangle = "iVBORw0KGgoAAAANSUhEUgAAAA0AAAANCAYAAABy6+R8AAAAnElEQVQoFWNgGBCwZcuWBZs3b1YlZDkjsgKgpntA/l8gXvnr16/+oKCgt8jyMDY2TWA5RkbGz//+/Zv+/fv3eWFhYb9gGkA0EzIHmf3//39eoMYyTk7OvVu3bvVBlsOpCaYIqFEaaMAkoNOrYWIsMAYe+glQY5e3t/cWmBqcmvD5CZsmcOj9/PkTZ+ihazoEdH+rr6/vbZhTBpYGAOeIQzDR8qr4AAAAAElFTkSuQmCC";
            AddSpriteByBase64("triangle", triangle, 13, 13, Vector4.zero);
            var checkmark = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAA8ElEQVQ4EZWOIQsCQRSEDzEcBjGY/QFiFIPpgsFg8CeYDOYLIhaziOGiUUz+DLPRIAYxGExGg8j5re7Cu/U8bh8Mb2fezNx5nuPEceyDCPQco187wQUws3QqIRWAl0mzj7kLMJfBGZhRRYFLwcok9Y5cwl0rfIKXchVgrICrKFC/3k6EEeqgkxA1QV8DOfOEj0sL3IFqHsojvA/kHCC+9HgIF+FQJSNlYFfBTdyevJuJsDaGwqSenxL21tJnP2EjYJxYZpvuEYrGn7oxTO2U5g92IzVki39KQtuXySkZ6y+rtQOFzEDakdAAbEAt7S61N1wRinJXJnllAAAAAElFTkSuQmCC";
            AddSpriteByBase64("checkmark", checkmark, 13, 13, Vector4.zero);
        }

        public static void Init()
        {
            CreateRect();
            CreateNormal();
        }
    }
}
