using System;

namespace UGUIRuntime
{
    public class TreeNode : CustomUI
    {
        private bool isFoldout = true;
        private bool isSelected = false;
        public Action<TreeNode, bool> onFoldout;
        public Action<TreeNode, bool> onSelected;
        private void Awake()
        {
            root.AnchorTop(60);

            root.AddButton("select").SetListener(() =>
            {
                Select(!isSelected);
                onSelected?.Invoke(this, isSelected);
            }).RT().Margin();

            root.AddButton("fold").SetSize(10, 10).SetListener(() =>
            {
                Foldout(!isFoldout);
                onFoldout?.Invoke(this, isFoldout);
            });
        }

        public TreeNode Select(bool selected)
        {
            isSelected = selected;
            return this;
        }

        public TreeNode Foldout(bool foldout)
        {
            isFoldout = foldout;
            return this;
        }
    }
}
