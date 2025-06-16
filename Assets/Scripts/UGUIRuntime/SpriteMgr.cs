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

        public static Sprite GetSprite(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            return m_sprites.TryGetValue(name, out var sprite) ? sprite : null;
        }

        private static void CreateDefaultRect()
        {
            var texture = new Texture2D(5, 5);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    texture.SetPixel(i, j, Color.clear);
                }
            }
            AddSprite("rect", texture, new Vector4(2, 2, 2, 2));
        }

        public static void Init()
        {
            CreateDefaultRect();
        }
    }
}
