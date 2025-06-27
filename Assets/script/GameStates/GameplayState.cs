using System;
using UnityEngine;
using static GameController;

public class GameplayState : GameBaseState
{
    GameController gameController;
    float stateTimeKeep = 0;
    float stateTime; // Duration of the gameplay state
    public GameplayState(float stateTime)
    {
        this.stateTime = stateTime;
    }
    public override void EnterState(GameController gc)
    {
        gameController = gc;
        gc.uiManager.SetCharacterStage(false);
        gc.uiManager.SetGameplayState(true);
    }

    public override void UpdateState(GameController gc)
    {
        CharacterLogicHandling();

        if (gc.getP1Health() <= 0 || gc.getP2Health() <= 0)
        {
            gc.SwitchState(gc.gameOverState);
        }
        stateTimeKeep += Time.deltaTime;
        if (stateTimeKeep > stateTime)
        {
            gc.AddStateCount();
            if (gc.stateCount >= 3)
            {
                gc.uiManager.SetCharacterStage(false);
                gc.uiManager.SetGameplayState(false);

                gc.SwitchState(gc.gameOverState);
            }
            else
            {
                gc.SwitchState(gc.randomCharacterState);
                gc.ReadyCheckP1.resetReady();
                gc.ReadyCheckP2.resetReady();

                gc.characterRand();
            }
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
}
