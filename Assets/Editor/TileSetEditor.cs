using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileSet))]
public class TileSetEditor : Editor
{
    TileSet targetSet;

    void OnEnable()
    {
        targetSet = (TileSet)target;
    }

    public override void OnInspectorGUI()
    {

        SerializedObject obj = new SerializedObject(targetSet);
        obj.Update();
        EditorGUILayout.BeginVertical();
        {
            Dictionary<MapGenerator.TileType, Sprite> dictCopy = new Dictionary<MapGenerator.TileType, Sprite>(targetSet.sprites);

            foreach(var v in dictCopy)
            {
                 targetSet.sprites[v.Key] = (Sprite)EditorGUILayout.ObjectField(v.Key.ToString(), v.Value, typeof(Sprite), false);
            }
        }

        if (GUILayout.Button("Create keys"))
        {

            //loop through all valid types of TileTypes
            for(int i = 1; i < (int)MapGenerator.TileType.MAX; i++)
            {
                MapGenerator.TileType t = (MapGenerator.TileType)i;
                
                if(!targetSet.sprites.ContainsKey(t))
                {
                    targetSet.sprites.Add(t, null);
                }
            }
        }

        obj.Update();
        EditorGUILayout.EndVertical();

        
    }
}
