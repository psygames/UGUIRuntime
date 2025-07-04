﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UGUIRuntime
{
    public class UGUI : MonoBehaviour
    {
        public const int UI_LAYER = 5;
        public const string NAME = "[UGUI Runtime]";
        public static RectTransform root { get { return instance.rootCanvas.GetComponent<RectTransform>(); } }
        public static Canvas canvas { get { return instance.rootCanvas; } }
        public static CanvasGroup canvasGroup { get { return instance.rootCanvasGroup; } }
        public static CanvasScaler scaler { get { return instance.rootCanvasScaler; } }

        private static UGUI instance;
        private Canvas rootCanvas;
        private CanvasScaler rootCanvasScaler;
        private CanvasGroup rootCanvasGroup;

        public static UGUI Create()
        {
            if (instance) return instance;

            SpriteMgr.Init();

            var obj = new GameObject(NAME);
            GameObject.DontDestroyOnLoad(obj);
            instance = obj.AddComponent<UGUI>();

            // canvas
            var canvasObj = new GameObject("Canvas");
            canvasObj.layer = UI_LAYER;
            canvasObj.AddComponent<RectTransform>();
            var canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 10000;
            instance.rootCanvas = canvas;

            // canvas scaler
            var canvasScaler = canvasObj.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.scaleFactor = 1;
            canvasScaler.referencePixelsPerUnit = 100;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            instance.rootCanvasScaler = canvasScaler;

            // canvas group
            var canvasGroup = canvasObj.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0.5f;
            instance.rootCanvasGroup = canvasGroup;

            // graphic raycaster
            var graphicRaycaster = canvasObj.AddComponent<GraphicRaycaster>();
            graphicRaycaster.ignoreReversedGraphics = true;

            // event system
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

            return instance;
        }

        public static void Destroy()
        {
            if (!instance) return;
            GameObject.Destroy(instance.gameObject);
            instance = null;
        }
    }
}
