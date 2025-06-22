using UnityEngine;

namespace UGUIRuntime
{
    public class TreeView : CustomUI
    {
        private RectTransform contentRoot;

        private void Awake()
        {
            var sv = root.AddScrollView();
            sv.horizontal = false;
            sv.RT().Margin();
            contentRoot = sv.content;
        }

        public TreeNode AddNode(string label, string name = null)
        {
            var node = contentRoot.AddTreeNode(name);
            node.RT().SetSize(root.rect.width, 20);
            node.SetLabel(label);
            return node;
        }
    }
}
