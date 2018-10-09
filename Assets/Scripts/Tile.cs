using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    MapGenerator.TileType type;
    int number;

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
        }
    }
}
