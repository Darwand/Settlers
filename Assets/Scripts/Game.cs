using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    PlayerColor[] order;
    int currentPlayer;

    Dictionary<PlayerColor, PlayerData> data;

    public delegate void NewTurn( PlayerColor nextPlayer );
    public event NewTurn OnNewTurn;

    static Game instance;

    World world;

    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;


        world = gameObject.AddComponent<World>();

        CreatePlayerData();
        AddController();

        currentPlayer = 0;
        order = new PlayerColor[4];

        order[0] = PlayerColor.Blue;
        order[1] = PlayerColor.Blue;
        order[2] = PlayerColor.Blue;
        order[3] = PlayerColor.Blue;

        OnNewTurn.Invoke(order[currentPlayer]);

        //StartCoroutine(DeterminePlayerOrder(3));
    }

    void OnDisable()
    {
        if(instance == this)
        {
            instance = null;
        }
    }

    public static Game GetGame()
    {
        if(!instance)
        {
            GameObject g = new GameObject("Game");
            instance = g.AddComponent<Game>();
            g.transform.position = Vector3.zero;
        }

        return instance;
    }

    IEnumerator DeterminePlayerOrder(int timePerRoll)
    {
        Dictionary<PlayerColor, int> rolls = new Dictionary<PlayerColor, int>();
        int roll;
        
        roll = DiceManager.GetDice().RollDice(false);
        rolls.Add(PlayerColor.Blue, roll);
        yield return new WaitForSeconds(timePerRoll);

        roll = DiceManager.GetDice().RollDice(false);
        rolls.Add(PlayerColor.Red, roll);
        yield return new WaitForSeconds(timePerRoll);

        roll = DiceManager.GetDice().RollDice(false);
        rolls.Add(PlayerColor.Yellow, roll);
        yield return new WaitForSeconds(timePerRoll);

        roll = DiceManager.GetDice().RollDice(false);
        rolls.Add(PlayerColor.Green, roll);
        yield return new WaitForSeconds(timePerRoll);

        while(rolls.Count > 0)
        {
            int n = 0;

            PlayerColor c = GetLowestRoll(rolls);
            rolls.Remove(c);

            order[n++] = c;
        }
    }

    PlayerColor GetLowestRoll(Dictionary<PlayerColor, int> rolls)
    {
        int lowest = 100;
        PlayerColor color = default(PlayerColor);

        foreach (var r in rolls)
        {
            if(r.Value < lowest)
            {
                color = r.Key;
            }
        }

        return color;
    }

    public PlayerColor GetCurrentPlayersTurn()
    {
        return order[currentPlayer];
    }

    public void EndTurn()
    {
        currentPlayer++;
        if(currentPlayer >= 4)
        {
            currentPlayer = 0;
        }

        DiceManager.GetDice().RollDice();

        OnNewTurn(order[currentPlayer]);

    }

    void CreatePlayerData()
    {
        data = new Dictionary<PlayerColor, PlayerData>();

        CreatePlayer(PlayerColor.Blue);
        CreatePlayer(PlayerColor.Green);
        CreatePlayer(PlayerColor.Red);
        CreatePlayer(PlayerColor.Yellow);
    }

    void CreatePlayer(PlayerColor c)
    {
        PlayerData p = gameObject.AddComponent<PlayerData>();
        p.SetColor(c);
        data.Add(c, p);
    }

    void AddController()
    {
        Controller con = gameObject.AddComponent<PCController>();
        con.SetData(data[PlayerColor.Blue]);
        OnNewTurn += con.TurnChange;
    }

    public void BuildSettlement(PlayerColor player, int x, int y)
    {
        world.BuildSettlement(player, x, y);

        world.GetSettlement(x, y).OnPlayerResource += data[player].ReceiveResource;
    }

    public void GivePlayerResource(PlayerColor player, Resource res)
    {
        data[player].ReceiveResource(res);
    }
}
