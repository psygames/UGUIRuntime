using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace psyhack
{
    public class Root : MonoBehaviour
    {
        public static Root instance { get; private set; }

        public Canvas canvas { get; private set; }
        public CanvasScaler scaler { get; private set; }
        public RectTransform canvasRoot { get; private set; }
        public string remoteUrlHost { get; private set; }
        public const int UI_LAYER = 5;

        public void SetRemoteUrlHost(string host)
        {
            remoteUrlHost = host;
        }

        public static Root Create()
        {
            if (instance) return instance;

            var obj = new GameObject("[UGUI Runtime]");
            GameObject.DontDestroyOnLoad(obj);
            instance = obj.AddComponent<Root>();

            // canvas
            var canvasObj = new GameObject("Canvas");
            canvasObj.layer = UI_LAYER;
            instance.canvasRoot = canvasObj.AddComponent<RectTransform>();
            var canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 10000;
            instance.canvas = canvas;

            // canvas scaler
            var canvasScaler = canvasObj.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.scaleFactor = 1;
            canvasScaler.referencePixelsPerUnit = 100;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            instance.scaler = canvasScaler;

            // graphic raycaster
            var graphicRaycaster = canvasObj.AddComponent<GraphicRaycaster>();
            graphicRaycaster.ignoreReversedGraphics = true;

            // evemt system
            var eventSystemObj = new GameObject("EventSystem");
            var eventSystem = eventSystemObj.AddComponent<EventSystem>();
            eventSystem.sendNavigationEvents = true;
            eventSystem.pixelDragThreshold = 10;

            // input module
            var inputModule = eventSystemObj.AddComponent<StandaloneInputModule>();
            inputModule.inputActionsPerSecond = 10;
            inputModule.repeatDelay = 0.5f;

            // set parent
            canvasObj.transform.SetParent(obj.transform);
            eventSystemObj.transform.SetParent(obj.transform);

            LoadingMask.Create();

            return instance;
        }

        public static void Destroy()
        {
            LoadingMask.Destroy();

            if (!instance) return;
            GameObject.Destroy(instance.gameObject);
            instance = null;
        }
    }
}
