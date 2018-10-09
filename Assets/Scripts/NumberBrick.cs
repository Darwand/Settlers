using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberBrick : MonoBehaviour
{
    public void SetNumber(int number)
    {
        Text t = GetComponentInChildren<Text>();
        t.text = number.ToString();

        if(number == 6 || number == 8)
        {
            t.color = Color.red;
        }

        Destroy(this);
    }
}
