using UnityEngine;
using UnityEditor;

public static class IconTools
{
    [MenuItem("Assets/IconTools/IconToBytes", priority = -1)]
    public static void IconToBytes()
    {
        var target = Selection.activeObject;
        if (target == null)
        {
            Debug.LogError("No object selected.");
            return;
        }
        var path = AssetDatabase.GetAssetPath(target);
        Debug.Log(path);
        var bytes = System.IO.File.ReadAllBytes(path);
        // bytes to base64
        var base64 = System.Convert.ToBase64String(bytes);
        // copy to clipboard
        GUIUtility.systemCopyBuffer = base64;
        // editor notification
        EditorUtility.DisplayDialog("Icon Tools", "Icon bytes copied to clipboard.", "OK");
    }
}
