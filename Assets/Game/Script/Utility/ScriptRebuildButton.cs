#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class ScriptRebuildButton : EditorWindow
{
    [SerializeField] private bool _clearConsole = true; // Option to clear console before rebuild

    [MenuItem("Tools/Rebuild Scripts")]
    public static void ShowWindow()
    {
        GetWindow<ScriptRebuildButton>("Script Rebuild");
    }

    private void OnGUI()
    {
        _clearConsole = EditorGUILayout.Toggle("Clear Console Before Rebuild", _clearConsole);

        if (!GUILayout.Button("Rebuild Scripts"))
            return;
        
        if (_clearConsole)
            ClearConsole();

        RebuildScripts();
    }

    private static void RebuildScripts()
    {
        AssetDatabase.Refresh();
        Debug.Log("Scripts Rebuilt");
    }
    
    private static void ClearConsole()
    {
        var logEntries = System.Type.GetType("UnityEditor.LogEntries,UnityEditor.dll");
        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        clearMethod.Invoke(null, null);
    }
}
#endif
