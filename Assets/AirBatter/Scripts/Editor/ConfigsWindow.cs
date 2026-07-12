using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConfigsWindow : EditorWindow
{
    private int _selectedTab = 0;
    private string[] _tabNames = { "Ability" };
    private List<ScriptableObject> _configs;

    [MenuItem("Tools/Configs Window")]
    public static void ShowWindow()
    {
        GetWindow<ConfigsWindow>("Game Configs");
    }

    private void OnEnable()
    {
        _configs = new List<ScriptableObject>()
        {
           LoadConfiga<AbilityConfig>("Damage")
        };

    }

    private T LoadConfiga<T>(string nameConfig) where T : ScriptableObject
    {

        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
        if (guids.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }
        return null;
    }

    private void OnGUI()
    {
        // Рисуем "панель вкладок" (Toolbar) в верхней части окна [citation:9]
        _selectedTab = GUILayout.Toolbar(_selectedTab, _tabNames);

        // Рисуем рамку вокруг активного содержимого
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        // В зависимости от выбранной вкладки, показываем нужный конфиг
        switch (_selectedTab)
        {
            case 0:
                if (_configs[0] != null)
                    DrawConfig(_configs[0] as AbilityConfig);
                else
                    EditorGUILayout.HelpBox("Ability not found!", MessageType.Error);
                break;

        }

        EditorGUILayout.EndVertical();
    }

    private void DrawConfig(ScriptableObject config)
    {
        // Создаем объект для работы с сериализованными данными (поддерживает Undo/Redo и префабы) [citation:2]
        SerializedObject serializedConfig = new SerializedObject(config);
        SerializedProperty property = serializedConfig.GetIterator();
        property.NextVisible(true); // Пропускаем скрытое поле m_Script

        EditorGUI.BeginChangeCheck();

        // Отображаем все публичные поля конфига
        while (property.NextVisible(false))
        {
            EditorGUILayout.PropertyField(property, true);
        }

        // Если были изменения, применяем их и помечаем ассет как измененный для сохранения
        if (EditorGUI.EndChangeCheck())
        {
            serializedConfig.ApplyModifiedProperties();
            EditorUtility.SetDirty(config);
        }
    }


}
