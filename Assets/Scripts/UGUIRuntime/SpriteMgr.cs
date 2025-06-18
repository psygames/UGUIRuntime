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

        private static void CreateRect()
        {
            var texture = new Texture2D(5, 5);
            texture.Apply();
            AddSprite("rect", texture, new Vector4(2, 2, 2, 2));
        }

        private static void CreateTriangle()
        {
            var texture = new Texture2D(9, 9);
            var colorMap = new int[][] {
                new int[]{ 0,0,0,0,0,0,0,0,0 },
                new int[]{ 0,0,0,0,0,0,0,0,0 },
                new int[]{ 1,1,1,1,1,1,1,1,1 },
                new int[]{ 0,1,1,1,1,1,1,1,0 },
                new int[]{ 0,0,1,1,1,1,1,0,0 },
                new int[]{ 0,0,0,1,1,1,0,0,0 },
                new int[]{ 0,0,0,0,1,0,0,0,0 },
                new int[]{ 0,0,0,0,0,0,0,0,0 },
                new int[]{ 0,0,0,0,0,0,0,0,0 },
            };
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (colorMap[i][j] == 1)
                        texture.SetPixel(i, j, Color.white);
                    else
                        texture.SetPixel(i, j, Color.clear);
                }
            }
            texture.Apply();
            AddSprite("triangle", texture, Vector4.zero);
        }

        public static void Init()
        {
            CreateRect();
            CreateTriangle();
        }
    }
}
