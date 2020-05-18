using System.IO;
using UnityEditor;
using UnityEngine;

public class HogeWindow : EditorWindow
{
    private Hoge m_hoge;

    private const string ASSET_PATH = "Assets/Resources/Hoge.asset";

    [MenuItem("Hoge/Hoge")]
    private static void Create()
    {
        GetWindow<HogeWindow>("Hoge");
    }

    private void OnGUI()
    {
        if (m_hoge == null)
        {
            Import();
        }

        Color defaultColor = GUI.backgroundColor;

        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUI.backgroundColor = Color.gray;

            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label("数値操作");
            }

            GUI.backgroundColor = defaultColor;

            m_hoge.IntValue = EditorGUILayout.IntField("Hoge", m_hoge.IntValue);
        }

        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUI.backgroundColor = Color.gray;

            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label("ファイル操作");
            }

            GUI.backgroundColor = defaultColor;

            GUILayout.Label("Hogeの保存先：" + ASSET_PATH);

            using (new GUILayout.HorizontalScope(GUI.skin.box))
            {
                if (GUILayout.Button("インポート"))
                {
                    Import();
                }

                if (GUILayout.Button("エクスポート"))
                {
                    Export();
                }
            }
        }
    }

    private void Import()
    {
        if (m_hoge == null)
        {
            m_hoge = ScriptableObject.CreateInstance<Hoge>();
        }

        Hoge a_hoge = AssetDatabase.LoadAssetAtPath<Hoge>(ASSET_PATH);

        if (a_hoge == null)
        {
            return;
        }

        EditorUtility.CopySerialized(a_hoge, m_hoge);
    }

    private void Export()
    {
        Hoge a_hoge = AssetDatabase.LoadAssetAtPath<Hoge>(ASSET_PATH);

        if (a_hoge == null)
        {
            a_hoge = ScriptableObject.CreateInstance<Hoge>();
        }

        if (!AssetDatabase.Contains(a_hoge as UnityEngine.Object))
        {
            string directory = Path.GetDirectoryName(ASSET_PATH);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            AssetDatabase.CreateAsset(a_hoge, ASSET_PATH);
        }

        EditorUtility.CopySerialized(m_hoge, a_hoge);

        a_hoge.hideFlags = HideFlags.NotEditable;

        EditorUtility.SetDirty(a_hoge);

        AssetDatabase.SaveAssets();

        AssetDatabase.Refresh();
    }
}
