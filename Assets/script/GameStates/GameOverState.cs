using UnityEngine;

public class GameOverState : GameBaseState
{
    public override void EnterState(GameController gc)
    {
        gc.uiManager.SetCharacterStage(false);
        gc.uiManager.SetGameplayState(false);
        gc.uiManager.SetEndStateObjects(true);
        Debug.Log("Enter Game OverState");
        //set gameOVerState GameOBJS true
    }

    public override void UpdateState(GameController gc)
    {
        gc.updateEndStateWinText();
    }
}
