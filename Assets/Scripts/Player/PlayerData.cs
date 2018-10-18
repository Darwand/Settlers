using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerColor
{
    None, Blue, Red, Green, Yellow
}

public class PlayerData : MonoBehaviour
{
    Dictionary<Resource, int> resources = new Dictionary<Resource, int>();

    int totalResources = 0;

    PlayerColor color;

    public void SetColor(PlayerColor c)
    {
        color = c;
    }

    public PlayerColor GetColor()
    {
        return color;
    }

    public int GetAmountOfResource(Resource res)
    {
        if(!resources.ContainsKey(res))
        {
            resources.Add(res, 0);
        }

        return resources[res];
    }

    public void ReceiveResource(Resource res)
    {
        if(!resources.ContainsKey(res))
        {
            resources.Add(res, 0);
        }

        totalResources++;
        resources[res]++;
    }

    public void UseResources(Dictionary<Resource, int> res)
    {
        foreach(var r in res)
        {
            resources[r.Key] -= r.Value;
        }
    }

    public bool HasResources(Dictionary<Resource, int> res)
    {

        foreach(var r in res)
        {
            if(resources[r.Key] < r.Value)
            {
                return false;
            }
        }

        return true;
    }
}
