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
        if (!gc.checkQTE())
        {
            CharacterLogicHandling();

            if (gc.getP1Health() <= 0 || gc.getP2Health() <= 0)
            {
                gc.SwitchState(gc.gameOverState);
            }
        
            stateTimeKeep += Time.deltaTime;
            gc.uiManager.UpdateTimer(stateTime - stateTimeKeep);
            if (stateTimeKeep > stateTime)
            {

                gc.AddStateCount();
                if (gc.stateCount >= 3)
                {


                    gc.SwitchState(gc.gameOverState);
                }
                else
                {



                    gc.SwitchState(gc.randomCharacterState);
                }
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
