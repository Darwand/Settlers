using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAction
{
    None, BuildSettlement, UpgradeSettlement, MoveRobber, WaitingTurn
}

public abstract class Controller : MonoBehaviour
{
    protected PlayerData data;
    protected PlayerAction currentAction = PlayerAction.WaitingTurn;

    public void SetData(PlayerData data)
    {
        this.data = data;
        UIManager.GetUI().SetData(data);
    }

    public void BeginTurn()
    {
        print("Starting turn");
    }

    public void TurnChange( PlayerColor c )
    {
        if(c == data.GetColor())
        {
            StartTurn();
        }
    }

    protected virtual void StartTurn()
    {
        currentAction = PlayerAction.None;
    }

    protected void BuildSettlement(Settlement settlement)
    {
        int x = settlement.GetX();
        int y = settlement.GetY();

        Game.GetGame().BuildSettlement(data.GetColor(), x, y);
    }

    protected void UpgradeSettlement(Settlement settlement)
    {

    }

    protected void MoveRobber()
    {
        throw new System.NotImplementedException();
    }

    protected RaycastHit2D RaycastPosition(Vector2 inputLocation)
    {
        return Physics2D.Raycast(inputLocation, Vector2.zero);
    }

    protected abstract void Click();

    protected T GetTypeFromRaycast<T>(RaycastHit2D hit) where T : MonoBehaviour
    {
        if(hit.collider)
        {
            return hit.collider.GetComponent<T>();
        }

        return null;
    }
}
