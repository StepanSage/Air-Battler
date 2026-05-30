using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class PlayMode 
{
    static PlayMode()
    {
        if(EditorBuildSettings.scenes.Length > 0)
        {
            string startScence = EditorBuildSettings.scenes[0].path;
            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(startScence);
            EditorSceneManager.playModeStartScene = sceneAsset;
        }
    }

}
