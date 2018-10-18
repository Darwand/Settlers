using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    static UIManager instance;

    PlayerData data;
    
    public static UIManager GetUI()
    {
        if(!instance)
        {
        }

        return instance;
    }

    public void SetData(PlayerData d)
    {
        data = d;
    }

    public PlayerData GetData()
    {
        return data;
    }

    void OnEnable()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
    }

    void OnDisable()
    {
        if(instance == this)
        {
            instance = null;
        }
    }
}
