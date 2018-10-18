using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileSet", menuName = "TileSet")]
public class TileSet : ScriptableObject, ISerializationCallbackReceiver
{
    
    public Dictionary<MapGenerator.TileType, Sprite> sprites = new Dictionary<MapGenerator.TileType, Sprite>();

    //custom serialization for dictionarty

    [SerializeField]
    int[] t;

    [SerializeField]
    Sprite[] s;

    public void OnAfterDeserialize()
    {
        for(int i = 0; i < t.Length; i++)
        {
            sprites.Add((MapGenerator.TileType)t[i], s[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        int c = sprites.Count;
        
        t = new int[c];
        s = new Sprite[c];

        c = 0;

        foreach(var v in sprites)
        {
            t[c] = (int)v.Key;
            s[c++] = v.Value;
        }

    }
}
