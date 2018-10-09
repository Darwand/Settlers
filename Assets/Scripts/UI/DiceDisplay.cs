using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceDisplay : MonoBehaviour
{
    [SerializeField]
    Text dieOne;

    [SerializeField]
    Text dieTwo;

    void Start()
    {
        DiceManager.GetDice().OnRolledFirst += DieRollOne;
        DiceManager.GetDice().OnRolledSecond += DieRollTwo;
    }

    void DieRollOne(int roll)
    {
        dieOne.text = roll.ToString();
    }

    void DieRollTwo(int roll)
    {
        dieTwo.text = roll.ToString();
    }
	
}
