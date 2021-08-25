using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameObjectPoolingManager))]
public class GameObjectPoolingManagerEditor : Editor
{
    GameObjectPoolingManager gameObjectPoolingManager = null;
    private void OnEnable()
    {
        if (AssetDatabase.Contains(target))
        {
            gameObjectPoolingManager = null;
        }
        else
        {
            gameObjectPoolingManager = (GameObjectPoolingManager)target;
        }
    }



    public override void OnInspectorGUI()
    {
        GUI.color = Color.yellow;

        if (GUILayout.Button("Setting"))
        {
            gameObjectPoolingManager.ObjectSetting();
        };

        GUILayout.Space(10);

        GUI.color = Color.white;

        base.OnInspectorGUI();
    }
}
