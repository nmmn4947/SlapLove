using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Character State")]

    [SerializeField] private GameObject CharacterStateObjects;

    public UIManager uiManager { get; private set; }
    [SerializeField] private ReadyCheck readyCheckP1;
    [SerializeField] private ReadyCheck readyCheckP2;

    [Header("Slap/Gameplay State")]

    [SerializeField] private GameObject SlapStateObjects;

    public void SetCharacterStage(bool state)
    {
        if (CharacterStateObjects != null)
        {
            CharacterStateObjects.SetActive(state);
        }
        else
        { 
            Debug.LogWarning("CharacterStateObjects is not assigned in the UIManager.");
        }
    }

    public void SetGameplayState(bool state)
    {
        if (SlapStateObjects != null)
        {
            SlapStateObjects.SetActive(state);
        }
        else
        {
            Debug.LogWarning("SlapStateObjects is not assigned in the UIManager.");
        }
    }

    public bool GetP1ReadyState()
    {
        return readyCheckP1.getIsReady();
    }
    public bool GetP2ReadyState()
    {
        return readyCheckP2.getIsReady();
    }

}
