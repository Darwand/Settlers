using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour 
{
    public enum TileType
    {
        NONE, Desert, Wool, Clay, Wood, Iron, Wheat, Water, MAX
    }

    

    TileSet tiles;

    System.Random rand;

	// Use this for initialization
	public void StartGenerate (World world, int seed) 
	{
        rand = new System.Random(seed);
        Generate(world);
	}

    public void StartGenerate(World world)
    {
        int seed = Random.Range(-5000, 5000);
        rand = new System.Random();

        print(seed);
        Generate(world);
    }

    void Generate( World world )
    {
        FetchTileSet();
        AddTiles(world);

        world.ResetTileStep();

        AddNumbers(world);
    }

    void FetchTileSet()
    {
        tiles = Resources.Load<TileSet>("MainTiles");
    }

    void AddTiles( World world )
    {
        List<TileType> tilesToUse = SetupTilesToUse();

        while (tilesToUse.Count > 0)
        {
            int index = rand.Next(0, tilesToUse.Count);
            world.AddTile(tiles.sprites[tilesToUse[index]], tilesToUse[index]);
            tilesToUse.RemoveAt(index);
        }

    }

    void AddNumbers( World world )
    {
        List<int> numbersToUse = new List<int>();

        for(int i = 0; i < 5; i++)
        {
            numbersToUse.Add(2 + i);
            numbersToUse.Add(12 - i);

            if(i != 0)
            {
                numbersToUse.Add(2 + i);
                numbersToUse.Add(12 - i);
            }
        }
        
        while (numbersToUse.Count > 0)
        {
            int i = rand.Next(0, numbersToUse.Count);
            world.AddNumberBrick(numbersToUse[i]);
            numbersToUse.RemoveAt(i);
        }
    }

    List<TileType> SetupTilesToUse()
    {
        List<TileType> tiles = new List<TileType>();

        AddNumberOfValue(tiles, 1, TileType.Desert);
        AddNumberOfValue(tiles, 3, TileType.Clay);
        AddNumberOfValue(tiles, 4, TileType.Wheat);
        AddNumberOfValue(tiles, 4, TileType.Wool);
        AddNumberOfValue(tiles, 4, TileType.Wood);
        AddNumberOfValue(tiles, 3, TileType.Iron);

        return tiles;
    }

    void AddNumberOfValue( List<TileType> tileList, int amount, TileType type)
    {
        for(int i = 0; i < amount; i++)
        {
            tileList.Add(type);
        }
    }
    
}
