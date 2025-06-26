using UnityEngine;

public abstract class GameBaseState
{
    public abstract void EnterState(GameController gc);
    public abstract void UpdateState(GameController gc);
}

