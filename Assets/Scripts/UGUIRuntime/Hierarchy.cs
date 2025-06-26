using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UGUIRuntime
{
    public class Hierarchy : MonoBehaviour
    {
        public event UnityAction<TreeNode> onSelectNode;

        private void Awake()
        {
            CreateWindow();
        }

        private void CreateWindow()
        {
            var wind = UGUI.root.AddWindow().SetTitle("Hierarchy");
            wind.RT().SetRect(new Rect(100, 200, 880, 600));
            var tw = wind.body.AddTreeView();
            tw.RT().Margin(2);

            List<GameObject> roots = new List<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects());
            roots.AddRange(UGUI.root.gameObject.scene.GetRootGameObjects());
            foreach (var root in roots)
            {
                if (root.name == UGUI.NAME)
                    continue;
                var node = tw.AddNode(root.name);
                node.SetCanFold(root.transform.childCount > 0);
                node.data = root.transform;
                node.onFolded += OnFolded;
                node.onSelected += OnSelected;
            }
        }

        private void OnFolded(TreeNode node, bool isFold)
        {
            if (isFold)
            {
                node.ClearChildren();
            }
            else
            {
                var refTrans = node.data as Transform;
                for (int i = 0; i < refTrans.childCount; i++)
                {
                    var t = refTrans.GetChild(i);
                    var child = node.AddNode(t.name, t.name);
                    child.SetCanFold(t.childCount > 0);
                    child.data = t;
                    child.onFolded += OnFolded;
                    child.onSelected += OnSelected;
                }
            }
        }

        private void OnSelected(TreeNode node)
        {
            onSelectNode?.Invoke(node);
        }
    }
}