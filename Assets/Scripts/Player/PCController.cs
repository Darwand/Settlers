using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : Controller
{
    protected override void Click()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        

        switch(currentAction)
        {
            case PlayerAction.BuildSettlement:

                RaycastHit2D hit = RaycastPosition(pos);
                Settlement s = GetTypeFromRaycast<Settlement>(hit);

                if(s)
                {
                    print("Attempting build");
                    BuildSettlement(s);
                }

                break;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            //print("Clicked");
            if(Game.GetGame().GetCurrentPlayersTurn() == data.GetColor())
            {
                Game.GetGame().EndTurn();
            }
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            currentAction = PlayerAction.BuildSettlement;
        }

        if(Input.GetMouseButtonDown(0))
        {
            Click();
        }
    }
}
