using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{

    struct TileData
    {
        public SpriteRenderer sprite;
        public MapGenerator.TileType type;
        public int number;


        public TileData( SpriteRenderer sprite, MapGenerator.TileType type )
        {
            this.sprite = sprite;
            this.type = type;
            number = 0;
        }
    }


    //the difference for the size in x and y for a tile
    float yPerPosition = 0.859375f;

    Tile[,] tiles;

    int nextX = 1;
    int nextY = 0;

    void Start()
    {
        tiles = new Tile[5, 5];

        MapGenerator gen = gameObject.AddComponent<MapGenerator>();
        gen.StartGenerate(this);

        Destroy(gen);

        StartCoroutine(DoRolls());
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

        brick.gameObject.transform.parent = tiles[nextX, nextY].gameObject.transform;
        brick.gameObject.transform.localPosition = Vector3.zero;

        brick.SetNumber(number);

        NextTileStep();
    }

    IEnumerator DoRolls()
    {
        while(true)
        {
            yield return new WaitForSeconds(8);

            DiceManager.GetDice().RollDice();
        }
    }
}
