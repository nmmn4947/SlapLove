using System;
using UnityEngine;
using static GameController;

public class GameplayState : GameBaseState
{
    GameController gameController;
    float stateTimeKeep;
    float stateTime; // Duration of the gameplay state
    private float timeBeforeChangeState = 1.5f;
    private float keepBeforeChangeState = 0.0f;
    bool stateDone = false;

    public GameplayState(float stateTime)
    {
        this.stateTime = stateTime;
    }
    public override void EnterState(GameController gc)
    {
        gameController = gc;
        gc.uiManager.SetCharacterStage(false);
        gc.uiManager.SetGameplayState(true);
        keepBeforeChangeState = 0.0f;
        stateTimeKeep = 0;
        stateDone = false;
        Debug.Log("Enter Gameplay State");
    }

    public override void UpdateState(GameController gc)
    {
        if (!stateDone) {
            if (!gc.checkQTE())
            {
                CharacterLogicHandling();

                if (gc.getP1Health() <= 0 || gc.getP2Health() <= 0)
                {
                    stateDone = true;
                }

                stateTimeKeep += Time.deltaTime;
                gc.uiManager.UpdateTimer(stateTime - stateTimeKeep);
                if (stateTimeKeep > stateTime)
                {
                    stateDone = true;
                }
            }
            gc.checkIfDamagable();
        }
        else
        {
            GoNextState(gc);
        }
    }

    void CharacterLogicHandling()
    {
        CharacterState characterState = gameController.GetCharacterState();
        switch (characterState)
        {
            case CharacterState.Chessur:
                gameController.SwapHeadArrowRandomly();
                gameController.BeatSpawning(gameController.SpawnBeats);
                break;
            case CharacterState.Pinocchio:
                gameController.BeatSpawning(gameController.SpawnPinoccioBeats);
                break;
            case CharacterState.Cinderella:
                gameController.BeatSpawning(gameController.SpawnCinderellaBeats);
                break;
        }
    }

    void DamageHandling()
    {

    }

    void GoNextState(GameController gc)
    {
        if (gc.stateCount >= 2)
        {
            gc.SwitchState(gc.gameOverState);
        }
        else
        {
            gc.stopAllBeatsNoRetract(true);
            keepBeforeChangeState += Time.deltaTime;
            if (keepBeforeChangeState > timeBeforeChangeState)
            {
                gc.SwapHeadArrowToNormal();
                gc.AddStateCount();
                gc.stopAllBeatsNoRetract(false);
                gc.SwitchState(gc.randomCharacterState);
            }
        }
    }
}
