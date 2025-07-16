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
        gc.SwapHeadArrowToNormal();
        keepBeforeChangeState = 0.0f;
        stateTimeKeep = 0;
        stateDone = false;
        SetBGMusic();
        Debug.Log("Enter Gameplay State");
        
    }

    public override void UpdateState(GameController gc)
    {
        if (!stateDone) {
            if (!gc.checkQTE())
            {
                CharacterLogicHandling();

                if (gc.getP1Health() <= 0)
                {
                    gc.addP1WinScore(false);
                    stateDone = true;
                }

                if (gc.getP2Health() <= 0)
                {
                    gc.addP1WinScore(true);
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

    void SetBGMusic()
    {
        CharacterState characterState = gameController.GetCharacterState();
        AudioManager audioManager = gameController.audioManager;
        BPMData bpmData = audioManager.GetBPMData(characterState.ToString());
        Debug.Log("Enum Value " + characterState.ToString());
        gameController.SetBPM(bpmData.BPM, bpmData.ToHitTime);
        switch (characterState)
        {
            
            case CharacterState.Chessur:
                
                gameController.audioManager.PlayMusic("Chessur");
                break;
            case CharacterState.Pinocchio:
                gameController.audioManager.PlayMusic("Pinocchio");
                break;
            case CharacterState.Cinderella:
                gameController.audioManager.PlayMusic("Cinderella");
                break;
        }
        
    }

    void GoNextState(GameController gc)
    {
        gc.stopCurrentMusic();

        if (gc.stateCount >= 2)
        {
            //gc.stopAllBeatsNoRetract(true);
            Time.timeScale = 0.25f;
            keepBeforeChangeState += Time.unscaledDeltaTime;
            if (keepBeforeChangeState > timeBeforeChangeState)
            {
                Time.timeScale = 1.0f;
                gc.stopAllBeatsNoRetract(false);
                gc.SwitchState(gc.gameOverState);
            }
        }
        else
        {
            //gc.stopAllBeatsNoRetract(true);
            Time.timeScale = 0.25f;
            keepBeforeChangeState += Time.unscaledDeltaTime;
            if (keepBeforeChangeState > timeBeforeChangeState)
            {
                Debug.Log("what");
                Time.timeScale = 1.0f;
                gc.SwapHeadArrowToNormal();
                gc.AddStateCount();
                gc.stopAllBeatsNoRetract(false);
                gc.SwitchState(gc.randomCharacterState);
            }
        }
    }
}
