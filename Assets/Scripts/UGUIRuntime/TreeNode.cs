using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UGUIRuntime
{
    public class TreeNode : CustomUI
    {
        private bool isFolded = true;
        private bool isSelected = false;
        private Button foldout;
        private Button select;
        private Text label;
        private RectTransform content;
        private RectTransform sub;

        public Action<TreeNode, bool> onFolded;
        public Action<TreeNode, bool> onSelected;

        private void Awake()
        {
            root.AnchorTop(20);
            root.VerticalLayout();
            content = root.AddNode("content").SetSize(root.rect.width, 20);

            select = content.AddButton("select").SetListener(() =>
            {
                Select(!isSelected);
                onSelected?.Invoke(this, isSelected);
            });
            select.RT().Margin();
            select.colors = new ColorBlock
            {
                normalColor = Color.clear,
                highlightedColor = Color.white * 0.5f,
                pressedColor = Color.blue * 0.5f,
                selectedColor = Color.blue * 0.5f,
                disabledColor = Color.clear,
                colorMultiplier = 1
            };

            label = select.RT().AddText();
            label.RT().Margin(0, 0, 0, 26);
            label.SetFont(16);

            foldout = content.AddButton("fold").SetListener(() =>
            {
                Fold(!isFolded);
                onFolded?.Invoke(this, isFolded);
            });
            foldout.RT().SetSize(18, 18).SetPivotCenter().SetAnchorMinMax(Vector2.up * 0.5f, Vector2.up * 0.5f).SetAnchoredPosition(new Vector2(10, 0));
            foldout.image.SetSprite("triangle");
            foldout.gameObject.SetActive(false);

            sub = root.AddNode("sub");
            sub.VerticalLayout(20);
        }

        public void SetLabel(string content)
        {
            label.text = content;
        }

        public TreeNode Select(bool selected)
        {
            isSelected = selected;
            return this;
        }

        public TreeNode Fold(bool isFolded)
        {
            this.isFolded = isFolded;
            foldout.RT().localRotation = Quaternion.Euler(0, 0, isFolded ? 0 : -90);
            sub.gameObject.SetActive(!isFolded);
            StartCoroutine(DelayedFix());
            return this;
        }

        private IEnumerator DelayedFix()
        {
            var vlg = GetComponentInParent<ScrollRect>().content.GetComponent<VerticalLayoutGroup>();
            vlg.enabled = false;
            yield return null;
            vlg.enabled = true;
            yield return null;
            vlg.enabled = false;
            yield return null;
            vlg.enabled = true;
        }

        public TreeNode AddNode(string label, string name = null)
        {
            foldout.gameObject.SetActive(true);
            var node = sub.AddTreeNode(name);
            node.RT().SetSize(root.rect.width, 20);
            node.SetLabel(label);
            node.Fold(false);
            return node;
        }
    }
}
