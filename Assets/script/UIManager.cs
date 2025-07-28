using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [Header("Character State")]

    [SerializeField] private GameObject CharacterStateObjects;
    [SerializeField] private GameObject ResultObjects;

    public UIManager uiManager { get; private set; }
    [SerializeField] private ReadyCheck readyCheckP1;
    [SerializeField] private ReadyCheck readyCheckP2;

    [Header("Slap/Gameplay State")]

    [SerializeField] private GameObject SlapStateObjects;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("End State")]
    [SerializeField] private GameObject EndGameObjects;

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

    public void SetResultScreen(bool state)
    {
        if (ResultObjects != null)
        {
            ResultObjects.SetActive(state);
        }
        else
        {
            Debug.LogWarning("ResultObjects is not assigned in the UIManager.");
        }
    }
    public void SetResultStage(bool state)
    {
        if (ResultObjects != null)
        {
            ResultObjects.SetActive(state);
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

    public void SetEndStateObjects(bool state)
    {
        if (EndGameObjects != null)
        {
            EndGameObjects.SetActive(state);
        }
        else
        {
            Debug.LogWarning("EndGameObjects is not assigned in the UIManager.");
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

    public void ResetP1ReadyState()
    {
        readyCheckP1.resetReady();
    }
    public void ResetP2ReadyState()
    {
        readyCheckP2.resetReady();
    }

    public void UpdateTimer(float time)
    {
        if (timerText != null)
        {
            timerText.text = time.ToString(); // Format to 1 decimal places
        }
        else
        {
            Debug.LogWarning("TimerText is not assigned in the UIManager.");
        }
    }

}
