using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UGUIRuntime
{
    public class Boot : MonoBehaviour
    {
        private static Boot _instance = null;
        public static Boot instance => _instance ?? Create();

        public Canvas canvas { get; private set; }
        public CanvasScaler scaler { get; private set; }
        public RectTransform canvasRoot { get; private set; }
        public string remoteUrlHost { get; private set; }

        private static Boot Create()
        {
            var obj = new GameObject("[UGUI Runtime]");
            var boot = obj.AddComponent<Boot>();
            _instance = boot;

            // canvas
            var canvasObj = new GameObject("Canvas");
            boot.canvasRoot = canvasObj.AddComponent<RectTransform>();
            var canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            boot.canvas = canvas;
            var canvasScaler = canvasObj.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
            canvasScaler.scaleFactor = 1;
            canvasScaler.referencePixelsPerUnit = 100;
            boot.scaler = canvasScaler;
            var graphicRaycaster = canvasObj.AddComponent<GraphicRaycaster>();
            graphicRaycaster.ignoreReversedGraphics = true;

            // evemt system
            var eventSystemObj = new GameObject("EventSystem");
            var eventSystem = eventSystemObj.AddComponent<EventSystem>();
            eventSystem.sendNavigationEvents = true;
            eventSystem.pixelDragThreshold = 10;
            var inputModule = eventSystemObj.AddComponent<StandaloneInputModule>();
            inputModule.inputActionsPerSecond = 10;
            inputModule.repeatDelay = 0.5f;

            // set parent
            canvasObj.transform.SetParent(obj.transform);
            eventSystemObj.transform.SetParent(obj.transform);

            return boot;
        }

        private int totalCreateCount = 0;
        private int GetCreateCount() { return ++totalCreateCount; }
        private RectTransform CreateRectTranform(string name, RectTransform parent)
        {
            var go = new GameObject(name);
            var rect = go.AddComponent<RectTransform>();
            rect.SetParent(parent);
            return rect;
        }

        public T Create<T>(string name, RectTransform parent) where T : Component
        {
            name = name ?? typeof(T).Name + GetCreateCount();
            var rectTransform = CreateRectTranform(name, parent);
            var obj = rectTransform.gameObject;
            if (typeof(T) == typeof(RectTransform))
            {
                return rectTransform as T;
            }
            else if (typeof(T) == typeof(Button))
            {
                var image = obj.AddComponent<Image>();
                var button = obj.AddComponent<Button>();
                button.image = image;
                return button as T;
            }
            else
            {
                return obj.AddComponent<T>();
            }
        }

        public void SetRemoteUrlHost(string host)
        {
            remoteUrlHost = host;
        }
    }
}
