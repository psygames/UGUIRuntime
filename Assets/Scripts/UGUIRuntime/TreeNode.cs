using System;
using System.Collections;
using System.Collections.Generic;
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
        private List<TreeNode> children = new List<TreeNode>();
        private int level = 0;

        public Action<TreeNode, bool> onFolded;
        public Action<TreeNode> onSelected;
        public object data;
        public static TreeNode currentSelected;

        private void Awake()
        {
            root.AnchorTop(20);
            root.SetVerticalLayoutGroup();
            content = root.AddNode("content").SetSize(root.rect.width, 20);

            select = content.AddButton("select").SetListener(OnSelect);
            select.RT().Margin();
            select.image.color = Color.clear;

            label = select.RT().AddText();
            label.SetFont(18);
            label.RT().Margin(0, 0, 0, 24 + level * 20);
            foldout = content.AddButton("fold").SetListener(OnFold);
            foldout.RT().SetSize(20, 20).SetPivotCenter().SetAnchorMinMax(Vector2.up * 0.5f, Vector2.up * 0.5f);
            foldout.RT().SetAnchoredPosition(new Vector2(10 + level * 20, 0));
            foldout.image.SetSprite("triangle");

            sub = root.VLG("sub");
        }

        private void OnSelect()
        {
            SetSelected(true);
            Debug.Log("OnSelect: " + name);
            onSelected?.Invoke(this);
        }

        private void OnFold()
        {
            isFolded = !isFolded;
            Debug.Log("OnFold: " + name + ", isFolded: " + isFolded);
            foldout.RT().localRotation = Quaternion.Euler(0, 0, isFolded ? 0 : -90);
            sub.gameObject.SetActive(!isFolded);
            StartCoroutine(DelayedFix());
            onFolded?.Invoke(this, isFolded);
        }

        private IEnumerator DelayedFix()
        {
            var vlg = GetComponentInParent<ScrollRect>().content.GetComponent<VerticalLayoutGroup>();
            for (int i = 0; i < 5; i++)
            {
                vlg.enabled = false;
                yield return null;
                vlg.enabled = true;
            }
        }

        public TreeNode AddNode(string label, string name = null)
        {
            var node = sub.AddTreeNode(name);
            node.RT().SetSize(root.rect.width, 20);
            node.SetLabel(label);
            node.level = level + 1;
            node.label.RT().Margin(0, 0, 0, 24 + node.level * 20);
            node.foldout.RT().SetAnchoredPosition(new Vector2(10 + node.level * 20, 0));
            children.Add(node);
            return node;
        }

        public void SetLabel(string content)
        {
            label.text = content;
        }

        public void SetCanFold(bool canFold)
        {
            foldout.gameObject.SetActive(canFold);
        }

        public void SetSelected(bool selected)
        {
            isSelected = selected;
            select.image.color = isSelected ? Color.green * 0.5f : Color.clear;
            if (isSelected)
            {
                if (currentSelected != null && currentSelected != this)
                {
                    currentSelected.SetSelected(false);
                }
                currentSelected = this;
            }
            else
            {
                if (currentSelected == this)
                {
                    currentSelected = null;
                }
            }
        }

        public void ClearChildren()
        {
            foreach (var child in children)
            {
                Destroy(child.gameObject);
            }
            children.Clear();
        }
    }
}
