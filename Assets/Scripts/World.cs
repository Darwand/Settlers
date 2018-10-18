using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{

    //the difference for the size in x and y for a tile
    float yPerPosition = 0.859375f;

    Tile[,] tiles;
    Settlement[,] settlements;

    int nextX = 1;
    int nextY = 0;

    int size = 5;

    void Start()
    {
        tiles = new Tile[size, size];

        settlements = new Settlement[(size * 2) + 1, size + 1];

        MapGenerator gen = gameObject.AddComponent<MapGenerator>();
        gen.StartGenerate(this);

        PlaceSettlements();

        Destroy(gen);
    }

    public void AddTile( Sprite sprite, MapGenerator.TileType type )
    {

        GameObject go = new GameObject("Tile " + nextX + " : " + nextY);

        tiles[nextX, nextY] = go.AddComponent<Tile>();
        tiles[nextX, nextY].SetType(type);

        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();

        sr.sprite = sprite;
        sr.sortingOrder = 0;

        go.transform.parent = transform;

        float posX = 2 - nextX;
        if (nextY % 2 == 1)
        {
            posX -= .5f;
        }

        float posY = (2 - nextY) * yPerPosition;

        go.transform.position = new Vector3(posX, posY, 0);

        NextTileStep();
    }

    void NextTileStep()
    {
        nextX++;
        if ((nextX >= 4 && nextY != 2) || nextX >= 5)
        {
            nextY++;
            if (nextY == 4)
            {
                nextX = 1;
            }
            else
            {
                nextX = 0;
            }
        }
    }

    public void ResetTileStep()
    {
        nextX = 1;
        nextY = 0;
    }

    public void AddNumberBrick( int number )
    {
        NumberBrick brick = Instantiate(Resources.Load<NumberBrick>("NumberBrick"));

        //go to next tile if its currently water or desert
        if(tiles[nextX, nextY].GetTileType() == MapGenerator.TileType.Desert || tiles[nextX, nextY].GetTileType() == MapGenerator.TileType.Water)
        {
            NextTileStep();
        }

        tiles[nextX, nextY].SetNumber(number);

        brick.gameObject.transform.SetParent(tiles[nextX, nextY].gameObject.transform);
        brick.gameObject.transform.localPosition = Vector3.zero;

        brick.SetNumber(number);

        NextTileStep();
    }

    public void BuildSettlement(PlayerColor color, int x, int y)
    {
        settlements[x, y].Claim(color);
    }

    public Settlement GetSettlement(int x, int y)
    {
        return settlements[x, y];
    }

    void PlaceSettlements()
    {
        for (int x = 0; x < (size * 2) + 1; x++)
        {
            for (int y = 0; y < size + 1; y++)
            {
                PlaceSettlement(x, y);
            }
        }
    }

    void PlaceSettlement(int x, int y)
    {
        List<Tile> tiles = GetAdjacentTilesToSettlement(x, y);

        if (tiles.Count > 0)
        {
            Settlement s = Instantiate(Resources.Load<Settlement>("Settlement"));
            s.transform.SetParent(transform);
            s.transform.localPosition = GetWorldPosForSettlement(x, y);
            
            s.Construct(tiles, x, y);

            settlements[x, y] = s;
        }
        
    }

    List<Tile> GetAdjacentTilesToSettlement(int x, int y)
    {
        List<Tile> tiles = new List<Tile>();

        int posX = (x / 2) - 1;
        

        //used to determine if it 2 adjacent tiles above or below the settlement
        bool isTop = x % 2 != y % 2;

        

        if (isTop)
        {

            int cx = posX;

            if(y % 2 == 0)
            {
                cx++;
            }

            int cy = y;

            if (IsValidTile(cx, cy))
            {
                tiles.Add(this.tiles[cx, cy]);
            }
            

            cy--;

            if (IsValidTile(cx, cy))
            {
                tiles.Add(this.tiles[cx, cy]);
            }

            if (y % 2 == 1)
            {
                cx++;
            }
            else
            {
                cx--;
            }

            if (IsValidTile(cx, cy))
            {
                tiles.Add(this.tiles[cx, cy]);
            }
        }
        else
        {
            int cx = posX;

            if(y % 2 == 1)
            {
                cx++;
            }

            int cy = y - 1;

            if (IsValidTile(cx, cy))
            {
                tiles.Add(this.tiles[cx, cy]);
            }
            cy++;

            if (IsValidTile(cx, cy))
            {
                tiles.Add(this.tiles[cx, cy]);
            }

            if (y % 2 == 1)
            {
                cx--;
            }
            else
            {
                cx++;
            }

            if (IsValidTile(cx, cy))
            {
                tiles.Add(this.tiles[cx, cy]);
            }
        }

        return tiles;
    }

    Vector3 GetWorldPosForSettlement(int x, int y)
    {
        Vector3 pos = Vector3.zero;

        pos.x = (size - x) * .5f;
        
        if(x % 2 == y % 2)
        {
            pos.y = ((3 - y) * yPerPosition) - .5f;
        }
        else
        {
            pos.y = ((2 - y) * yPerPosition) + .6f;
        }

        return pos;
    }

    bool IsValidTile(int x, int y)
    {
        if(x >= 0 && x < size && y >= 0 && y < size)
        {
            if(tiles[x,y] != null)
            {
                return true;
            }
        }
        return false;
    }
}
