using System;
using UnityEngine;

public class RandomCharacterState : GameBaseState
{
    GameController gameController;
    bool once = false;
    public override void EnterState(GameController gc)
    {
        Debug.Log("Entering RandomCharacterState");
        gameController = gc;
        gc.characterRand();
        once = false;
        gc.uiManager.SetGameplayState(false);
        if (gc.GetStateCount() <1)
        {
            gc.uiManager.SetCharacterStage(true);
        }
        if(gc.GetStateCount() >= 1)
        {
            gc.uiManager.SetResultScreen(true);
        }
        //gc.uiManager.SetGameplayState(true);
        qteVisual.instance.spawnCharacterSprite();
        gc.uiManager.ResetP1ReadyState();
        gc.uiManager.ResetP2ReadyState();
    }

    public override void UpdateState(GameController gc)
    {
        if (!once)
        {
            gc.ClearBeats();
            once = true;
        }
        if(gc.uiManager.GetP1ReadyState() && gc.uiManager.GetP2ReadyState())
        {
            ResetHealth();
            gc.SwitchState(gc.gameplayState);
        }
    }

    private void ResetHealth()
    {
        gameController.SetP1Health(gameController.getMaxPlayerHealth());
        gameController.SetP2Health(gameController.getMaxPlayerHealth());
    }

}
