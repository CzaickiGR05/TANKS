using UnityEditor;
using UnityEngine;

public class ConvertURPShadersToStandard
{
    [MenuItem("Tools/Convert URP Shaders to Standard")]
    static void Convert()
    {
        Shader standardShader = Shader.Find("Standard");
        if (standardShader == null)
        {
            Debug.LogError("Standard Shader not found!");
            return;
        }

        string[] materialGuids = AssetDatabase.FindAssets("t:Material");
        int converted = 0;

        foreach (string guid in materialGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat != null && mat.shader != standardShader)
            {
                if (mat.shader.name.Contains("Universal"))
                {
                    mat.shader = standardShader;
                    EditorUtility.SetDirty(mat);
                    converted++;
                }
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"Converted {converted} materials to Standard shader.");
    }
}