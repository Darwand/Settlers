using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : MonoBehaviour
{

    enum Type
    {
        Village, City
    }

    Type currentType = Type.Village;
    int resourcesPerGain = 1;

    public delegate void GivePlayerResource( Resource res );
    public event GivePlayerResource OnPlayerResource;

    public SpriteRenderer outline;
    public SpriteRenderer center;

    int posX;
    int posY;

    PlayerColor owningPlayer;

    SpriteRenderer sr;

	// Use this for initialization
	void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        owningPlayer = PlayerColor.None;
	}

    public void Claim(PlayerColor color)
    {
        owningPlayer = color;
        Color c = Color.white;

        switch (color)
        {
            case PlayerColor.Blue:
                c = Color.blue;
                break;

            case PlayerColor.Green:
                c = Color.green;
                break;

            case PlayerColor.Red:
                c = Color.red;
                break;

            case PlayerColor.Yellow:
                c = Color.yellow;
                break;
        }

        center.color = c;
    }

    public void Construct(List<Tile> adjacentTiles, int x, int y)
    {
        foreach(Tile t in adjacentTiles)
        {
            t.OnRolled += GetResources;
        }

        Unclaim();

        posX = x;
        posY = y;
    }

    public void Unclaim()
    {
        center.color = Color.white;
        outline.color = Color.black;
        owningPlayer = PlayerColor.None;
    }

    public void Upgrade()
    {
        resourcesPerGain = 2;
        currentType = Type.City;
    }

    void GetResources(List<Resource> resources)
    {
        for(int i = 0; i < resourcesPerGain; i++)
        {
            if(OnPlayerResource != null)
            {
                OnPlayerResource(resources[i]);
            }
        }
    }

    public int GetX()
    {
        return posX;
    }

    public int GetY()
    {
        return posY;
    }
}
