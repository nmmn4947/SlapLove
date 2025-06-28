using System;
using UnityEngine;

public class RandomCharacterState : GameBaseState
{
    GameController gameController;

    public override void EnterState(GameController gc)
    {
        Debug.Log("Entering RandomCharacterState");
        gameController = gc;
        gc.ClearBeats();
        gc.characterRand();
        
        gc.uiManager.SetCharacterStage(true);
        gc.uiManager.SetGameplayState(false);
        gc.uiManager.ResetP1ReadyState();
        gc.uiManager.ResetP2ReadyState();
    }

    public override void UpdateState(GameController gc)
    {
        if(gc.uiManager.GetP1ReadyState() && gc.uiManager.GetP2ReadyState())
        {
            ResetHealth();
            gc.SwitchState(gc.gameplayState);
        }
    }

    private void ResetHealth()
    {
        gameController.SetP1Health(3);
        gameController.SetP2Health(3);
    }

}
