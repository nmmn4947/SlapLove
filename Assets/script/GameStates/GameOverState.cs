using UnityEngine;

public class GameOverState : GameBaseState
{
    public override void EnterState(GameController gc)
    {
        gc.AddStateCount();
        gc.uiManager.SetCharacterStage(false);
        gc.uiManager.SetGameplayState(false);
        //set gameOVerState GameOBJS true
        Debug.Log("Entering RandomCharacterState");
    }

    public override void UpdateState(GameController gc)
    {

        if (gc.stateCount >= 3)
        {

            gc.uiManager.SetEndStateObjects(true);

        }
        else
        {
            gc.SwitchState(gc.randomCharacterState);
        }
    }
}
