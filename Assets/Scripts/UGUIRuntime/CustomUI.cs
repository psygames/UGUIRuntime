using UnityEngine;
using UnityEngine.UI;

namespace UGUIRuntime
{
    public class CustomUI : MonoBehaviour
    {
        public RectTransform root { get { return GetComponent<RectTransform>(); } }

    }
}
