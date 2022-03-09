using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileMapShadow))]
public class ShadowCASTER : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TileMapShadow generator = (TileMapShadow)target;
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();


        if (GUILayout.Button("Generate"))
        {

            generator.Generate();

        }

        EditorGUILayout.Space();
        if (GUILayout.Button("Destroy All Children"))
        {

            generator.DestroyAllChildren();

        }
    }

}