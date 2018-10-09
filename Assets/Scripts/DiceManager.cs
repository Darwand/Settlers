using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager
{
    public delegate void DiceRoll(int rolledNumber);

    public event DiceRoll OnRolledFirst;
    public event DiceRoll OnRolledSecond;

    public delegate void SummOfRolls( int sum );
    public event SummOfRolls OnResourceRoll;

    static DiceManager instance;

    System.Random rand;

    public static DiceManager GetDice()
    {
        if (instance == null)
        {
            instance = new DiceManager();
        }

        return instance;
    }

    public int RollDice(bool forResources = true)
    {
        int first = Roll();
        int second = Roll();

        OnRolledFirst(first);
        OnRolledSecond(second);

        if(forResources)
        {
            OnResourceRoll(first + second);
        }

        return first + second;
    }

    int Roll()
    {
        return rand.Next(1, 7);
    }

    DiceManager()
    {
        rand = new System.Random();
    }

    ~DiceManager()
    {
        instance = null;
        rand = null;
    }
}
