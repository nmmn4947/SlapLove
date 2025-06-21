using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
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

    [SerializeField] private GameObject[] arrowPrefabs;

    [SerializeField] private GameObject[] headArrows;

    Queue<BeatArrow> spawnedArrow1 = new Queue<BeatArrow>(); // .front
    Queue<BeatArrow> spawnedArrow2 = new Queue<BeatArrow>(); // .front

    public BeatRow p1Row;
    public BeatRow p2Row;

    [SerializeField] private float stateTime = 120.0f;
    private float stateTimeKeep;

    [SerializeField] private int beatTempo;

    public RectTransform[] spawnPoints;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        currentState = GameState.CharacterStage;
        p1Row.setIsP1(true);
        p2Row.setIsP1(false);
        stateTimeKeep = stateTime;
        spawnBeats(0);
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

                currentState = GameState.SlapState;
                break;
            case GameState.SlapState:

                break;
        }
    }

    public int getCurrentBeatDirection(bool isP1)
    {
        if (isP1)
        {
            return spawnedArrow1.Peek().getDirection(); // peek = front
        }
        else
        {
            return spawnedArrow2.Peek().getDirection(); // peek = front
        }
        
    }

    public void minusPlayerHealth(bool p1)
    {
        if (p1)
        {
            p1Health -= 1;
        }
        else
        {
            p2Health -= 1;
        }
        
    }

    public void dequeCurrentBeat(bool isP1, bool correct)
    {
        
        if (isP1)
        {
            spawnedArrow1.Peek().beatDone(correct);
            spawnedArrow1.Dequeue();
        }
        else
        {
            spawnedArrow2.Peek().beatDone(correct);
            spawnedArrow2.Dequeue();
        }
        
    }

    private void spawnBeats()
    {
        int rand = Random.Range(0, 3);
        GameObject p1 = Instantiate(arrowPrefabs[rand], spawnPoints[rand]);
        BeatArrow p1BA = p1.GetComponent<BeatArrow>();
        p1BA.setBeatTempo(beatTempo);
        p1BA.setDirection(rand);
        spawnedArrow1.Enqueue(p1BA);

        GameObject p2 = Instantiate(arrowPrefabs[rand], spawnPoints[rand + 4]);
        BeatArrow p2BA = p2.GetComponent<BeatArrow>();
        p2BA.setBeatTempo(beatTempo);
        p2BA.setDirection(rand);
        spawnedArrow2.Enqueue(p2BA);
    }

    private void spawnBeats(int i)
    {
        //int rand = Random.Range(0, 3);
        int rand = i;
        GameObject p1 = Instantiate(arrowPrefabs[rand], spawnPoints[rand]);
        BeatArrow p1BA = p1.GetComponent<BeatArrow>();
        p1BA.setBeatTempo(beatTempo);
        p1BA.setDirection(rand);
        spawnedArrow1.Enqueue(p1BA);

        GameObject p2 = Instantiate(arrowPrefabs[rand], spawnPoints[rand + 4]);
        BeatArrow p2BA = p2.GetComponent<BeatArrow>();
        p2BA.setBeatTempo(beatTempo);
        p2BA.setDirection(rand);
        spawnedArrow2.Enqueue(p2BA);
    }
}
