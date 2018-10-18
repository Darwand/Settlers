using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{

    [SerializeField]
    Text woodText;

    [SerializeField]
    Text wheatText;

    [SerializeField]
    Text woolText;

    [SerializeField]
    Text oreText;

    [SerializeField]
    Text clayText;

    PlayerData data;

    // Use this for initialization
    void Start ()
    {
        data = UIManager.GetUI().GetData();

        UpdateText();
	}

    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        if(data)
        {
            woodText.text = "Wood: " + data.GetAmountOfResource(Resource.Wood);
            woolText.text = "Wool: " + data.GetAmountOfResource(Resource.Wool);
            wheatText.text = "Wheat: " + data.GetAmountOfResource(Resource.Wheat);
            oreText.text = "Ore: " + data.GetAmountOfResource(Resource.Iron);
            clayText.text = "Clay: " + data.GetAmountOfResource(Resource.Clay);
        }
        else
        {
            data = UIManager.GetUI().GetData();
        }
    }
}
