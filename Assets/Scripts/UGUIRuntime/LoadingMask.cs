using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace psyhack
{
    internal class LoadingMask : MonoBehaviour
    {
        private static LoadingMask instance;
        private Dictionary<int, LoadingMaskItem> items = new Dictionary<int, LoadingMaskItem>();

        public static LoadingMask Create()
        {
            if (instance) return instance;
            var go = new GameObject("[Loading Mask]");
            go.layer = UGUI.UI_LAYER;
            GameObject.DontDestroyOnLoad(go);
            instance = go.AddComponent<LoadingMask>();
            go.AddComponent<RectTransform>();
            go.transform.SetParent(UGUI.canvas.transform);
            go.GetComponent<RectTransform>().Reset();
            return instance;
        }

        public static void Destroy()
        {
            if (!instance) return;
            instance.items.Clear();
            GameObject.Destroy(instance.gameObject);
            instance = null;
        }

        public static int Show(Vector2 position, Vector2 size)
        {
            if (!instance) Create();
            int id = 0;
            foreach (var kv in instance.items)
            {
                if (!kv.Value.isActiveAndEnabled)
                {
                    id = kv.Key;
                    break;
                }
            }
            if (id == 0)
            {
                id = instance.items.Count + 1;
                var item = LoadingMaskItem.Create(instance.transform);
                instance.items.Add(id, item);
            }
            instance.items[id].gameObject.SetActive(true);
            instance.items[id].rectTransform.position = position;
            instance.items[id].rectTransform.sizeDelta = size;
            return id;
        }

        public static void Hide(int id)
        {
            instance.items[id].gameObject.SetActive(false);
        }
    }

    internal class LoadingMaskItem : MonoBehaviour
    {
        private static int count = 0;
        public RectTransform rectTransform;
        public Image image;
        public Text text;

        void Update()
        {
            image.color = Color.Lerp(image.color, Color.black * 0.5f, Time.deltaTime);
        }

        private void OnEnable()
        {
            if (!image) return;
            image.color = Color.clear;
            image.rectTransform.anchorMin = Vector2.zero;
            image.rectTransform.anchorMax = Vector2.one;
            image.rectTransform.sizeDelta = Vector2.zero;
        }

        private static LoadingMaskItem _template;
        public static LoadingMaskItem Create(Transform parent)
        {
            if (_template == null)
            {
                var _go = new GameObject("template");
                _template = _go.AddComponent<LoadingMaskItem>();
                _template.rectTransform = _go.AddComponent<RectTransform>();
                _template.image = _template.rectTransform.AddImage();
                _template.text = _template.rectTransform.AddText();
                _template.text.rectTransform.sizeDelta = new Vector2(200, 60);
                _template.text.text = "Loading...";
                _template.rectTransform.SetParent(parent);
                _template.rectTransform.Reset();
                _template.gameObject.SetActive(false);
            }
            var item = GameObject.Instantiate(_template, parent);
            item.name = $"load mask {++count}";
            return item;
        }
    }
}
