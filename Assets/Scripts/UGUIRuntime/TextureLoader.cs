using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace UGUIRuntime
{
    public class TextureLoader
    {
        public static async Task<Texture2D> LoadFromUrl(string url)
        {
            var req = UnityWebRequest.Get(url);
            req.SendWebRequest();
            while (!req.isDone)
            {
                await Task.Yield();
            }
            if (req.isHttpError || req.isNetworkError)
            {
                Debug.LogError($"request faild code({req.responseCode}): {url}");
                return null;
            }
            var bytes = req.downloadHandler.data;
            req.Dispose();
            var texture = new Texture2D(0, 0);
            texture.LoadImage(bytes);
            return texture;
        }
    }
}
