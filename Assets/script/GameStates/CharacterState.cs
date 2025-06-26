using System;
using UnityEngine;

public class CharacterState : GameBaseState
{
    ReadyCheck p1;
    ReadyCheck p2;
    GameController gameController;

    public override void EnterState(GameController gc)
    {
        gameController = gc;
    }

    public override void UpdateState(GameController gc)
    {
        if(gc.ReadyCheckP1.getIsReady() && gc.ReadyCheckP2.getIsReady())
        {
            resetHealth();
        }
    }

    private void resetHealth()
    {
        gameController.setP1Health(3);
        gameController.setP2Health(3);
    }

}
