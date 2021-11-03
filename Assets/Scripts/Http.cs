using System;

namespace UGUIRuntime
{
    public class Http
    {
        static HttpMono instance;

        public static void Create()
        {
            if (instance) return;
            var go = new UnityEngine.GameObject("[Http]");
            UnityEngine.Object.DontDestroyOnLoad(go);
            instance = go.AddComponent<HttpMono>();
        }

        public static void Destroy()
        {
            if (!instance) return;
            UnityEngine.Object.Destroy(instance.gameObject);
            instance = null;
        }

        public static void Download(string url, Action<byte[]> callback)
        {

            instance.Create(url, callback);
        }
    }

    class HttpMono : UnityEngine.MonoBehaviour
    {
        private System.Collections.Generic.List<Op> opList = new System.Collections.Generic.List<Op>();

        public void Create(string url, Action<byte[]> callback)
        {
            var op = new Op();
            op.Start(url, callback);
            opList.Add(op);
        }

        public void OnDestroy()
        {
            opList.Clear();
        }

        public void Update()
        {
            foreach (var op in opList)
            {
                op.Update(UnityEngine.Time.deltaTime);
            }

            for (int i = opList.Count - 1; i >= 0; i--)
            {
                if (opList[i].isDone)
                {
                    opList.RemoveAt(i);
                }
            }
        }

        class Op
        {
            public string url { get; private set; }
            public byte[] data { get; private set; }
            public bool isDone { get; private set; }

            private float timeout;
            private Action<byte[]> syncCallback;
            private bool needCallback;

            private void OnAsyncDownloaded(byte[] _bytes)
            {
                data = _bytes;
                isDone = true;
                needCallback = true;
            }

            public void Start(string url, Action<byte[]> syncCallback, float timeout = 10f)
            {
                this.timeout = timeout;
                this.url = url;
                this.syncCallback = syncCallback;
                InnerHttp.AsyncDownload(url, OnAsyncDownloaded);
            }

            public void Update(float deltaTime)
            {
                if (needCallback)
                {
                    syncCallback?.Invoke(data);
                    needCallback = false;
                }

                if (isDone) return;

                timeout -= deltaTime;
                if (timeout <= 0)
                {
                    isDone = true;
                    syncCallback?.Invoke(null);
                }
            }
        }

        class InnerHttp
        {
            private const string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";

            private static bool isProtocolFixed;
            private static void FixProtocol()
            {
                if (isProtocolFixed) return;
                System.Net.ServicePointManager.ServerCertificateValidationCallback =
                    new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });
                System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)4080;
                isProtocolFixed = true;
            }


            public static byte[] Download(string url)
            {
                try
                {
                    FixProtocol();
                    var request = System.Net.WebRequest.Create(url) as System.Net.HttpWebRequest;
                    request.UserAgent = userAgent;
                    var response = request.GetResponse() as System.Net.HttpWebResponse;
                    var responseStream = response.GetResponseStream();
                    byte[] bArr = new byte[1024 * 8];
                    int size = responseStream.Read(bArr, 0, bArr.Length);
                    var ms = new System.IO.MemoryStream();
                    while (size > 0)
                    {
                        ms.Write(bArr, 0, size);
                        size = responseStream.Read(bArr, 0, bArr.Length);
                    }
                    var bytes = ms.ToArray();
                    ms.Close();
                    responseStream.Close();
                    response.Close();
                    return bytes;
                }
                catch
                {

                }
                return null;
            }

            public static void AsyncDownload(string url, Action<byte[]> callback)
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    var bytes = Download(url);
                    callback?.Invoke(bytes);
                })).Start();
            }
        }
    }
}
