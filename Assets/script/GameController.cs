using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    enum CharacterState
    {
        Chessur,
        Pinocchio,
        Cinderella
    };
    CharacterState currentCharacter;

    enum GameState
    {
        CharacterStage,
        SlapState
    };
    GameState currentState;

    int p1Health = 3;
    int p2Health = 3;

    [SerializeField] private GameObject arrowUpPrefab;
    [SerializeField] private GameObject arrowLeftPrefab;
    [SerializeField] private GameObject arrowDownPrefab;
    [SerializeField] private GameObject arrowRightPrefab;

    Queue<BeatArrow> spawnedArrow; // The first one

    public BeatRow p1Row;
    public BeatRow p2Row;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = GameState.CharacterStage;
        p1Row.setIsP1(true);
        p2Row.setIsP1(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameState.CharacterStage:
                currentCharacter = (CharacterState)Random.Range(0, 2);
                switch (currentCharacter)
                {
                    case CharacterState.Chessur:

                        break;
                    case CharacterState.Pinocchio:

                        break;
                    case CharacterState.Cinderella:

                        break;
                }
                break;
            case GameState.SlapState:

                break;
        }
    }
}
