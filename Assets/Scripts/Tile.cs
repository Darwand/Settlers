using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resource
{
    Wool, Clay, Wood, Wheat, Iron
}


public class Tile : MonoBehaviour
{
    MapGenerator.TileType type;
    int number;

    public delegate void GiveResources( List<Resource> resources );
    public event GiveResources OnRolled;

    public void SetNumber(int num)
    {
        number = num;
        DiceManager.GetDice().OnResourceRoll += ResourceRollResult;
    }

    public int GetNumber()
    {
        return number;
    }

    public void SetType(MapGenerator.TileType type)
    {
        this.type = type;
    }

    public MapGenerator.TileType GetTileType()
    {
        return type;
    }

    void ResourceRollResult(int sum)
    {
        if(sum == number)
        {
            print(type.ToString());
            if(OnRolled != null)
            {
                OnRolled.DynamicInvoke(GetResourceList());
            }
            
        }
    }

    List<Resource> GetResourceList()
    {
        List<Resource> res = new List<Resource>();

        switch (type)
        {
            case MapGenerator.TileType.Clay:
                res.Add(Resource.Clay);
                res.Add(Resource.Clay);
                break;

            case MapGenerator.TileType.Iron:
                res.Add(Resource.Iron);
                res.Add(Resource.Iron);
                break;

            case MapGenerator.TileType.Wheat:
                res.Add(Resource.Wheat);
                res.Add(Resource.Wheat);
                break;

            case MapGenerator.TileType.Wood:
                res.Add(Resource.Wood);
                res.Add(Resource.Wood);
                break;

            case MapGenerator.TileType.Wool:
                res.Add(Resource.Wool);
                res.Add(Resource.Wool);
                break;
        }

        return res;

    }
}
