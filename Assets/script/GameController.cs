using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class GameController : MonoBehaviour
{
    //[Header("General")]
    public static GameController instance;
    enum CharacterState
    {
        Chessur,
        Pinocchio,
        Cinderella
    };
    CharacterState currentCharacter;
    private List<int> randCharacter = new List<int>();
    private int stateCount = 0;

    enum GameState
    {
        CharacterStage,
        SlapState,
        GameEndState
    };
    GameState currentState;

    int p1Health = 3;
    int p2Health = 3;

    [Header("CharacterState")]

    [SerializeField] private GameObject CharacterStateObjects;

    [SerializeField] private ReadyCheck ReadyCheckP1;
    [SerializeField] private ReadyCheck ReadyCheckP2;



    [Header("Slap/RhythmState")]

    [SerializeField] private GameObject SlapStateObjects;

    [SerializeField] private GameObject[] arrowPrefabs;

    [SerializeField] private GameObject[] headArrows;

    Queue<BeatArrow> spawnedArrow1 = new Queue<BeatArrow>(); // .front
    Queue<BeatArrow> spawnedArrow2 = new Queue<BeatArrow>(); // .front

    public BeatRow p1Row;
    public BeatRow p2Row;

    [SerializeField] private float stateTime = 120.0f;
    private float stateTimeKeep = 0.0f;

    [SerializeField] private float timeBetweenBeats = 0.5f;
    float totalTime;
    private float timeBetweenKeep = 0.0f;

    [SerializeField] private int beatTempo;
    float beatDuration;
    [SerializeField] float toHitBoxDuration;
    [SerializeField] float speed;

    public RectTransform[] spawnPoints;
    public RectTransform hitBoxPoint;


    [Header("EndState")]

    [SerializeField] private GameObject EndStateObjects;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        float distance = Mathf.Abs(spawnPoints[0].position.y - hitBoxPoint.position.y);
        speed = distance / toHitBoxDuration;

        currentState = GameState.CharacterStage;
        p1Row.setIsP1(true);
        p2Row.setIsP1(false);
        //StartCoroutine(debugSpawn());
        randCharacter.Add(0);
        randCharacter.Add(1);
        randCharacter.Add(2);
        characterRand();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameState.CharacterStage:

                CharacterStateObjects.SetActive(true);
                SlapStateObjects.SetActive(false);


                
                switch (currentCharacter)
                {
                    case CharacterState.Chessur:

                        break;
                    case CharacterState.Pinocchio:

                        break;
                    case CharacterState.Cinderella:

                        break;
                }

                if (ReadyCheckP1.getIsReady() && ReadyCheckP2.getIsReady())
                {
                    //Maybe some Animation to go to next state
                    resetHealth();
                    currentState = GameState.SlapState;
                    stateTimeKeep = 0.0f;
                }
                break;

            case GameState.SlapState:
                CharacterStateObjects.SetActive(false);
                SlapStateObjects.SetActive(true);

                defaultBeatSpawning();

                //Next state
                stateTimeKeep += Time.deltaTime;
                if (stateTimeKeep > stateTime || p1Health <= 0 || p2Health <= 0)
                {
                    stateCount++;
                    if (stateCount >= 3)
                    {
                        CharacterStateObjects.SetActive(false);
                        SlapStateObjects.SetActive(false);
                        EndStateObjects.SetActive(true);
                        currentState = GameState.GameEndState;
                    }
                    else
                    {
                        ReadyCheckP1.resetReady();
                        ReadyCheckP2.resetReady();
                        clearBeats();
                        characterRand();
                        currentState = GameState.CharacterStage;
                    }

                }
                break;

            case GameState.GameEndState:

                break;
        }
    }

    private void characterRand()
    {
        int r = randCharacter[Random.Range(0, randCharacter.Count)];
        currentCharacter = (CharacterState)r;
        randCharacter.Remove(r);
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

    IEnumerator dequeCurrentBeat(bool isP1)
    {
/*        if (spawnedArrow1.Count == 0)
        {
            yield return null;
        }*/
        yield return new WaitForSeconds(0.5f);
        if (isP1)
        {
            spawnedArrow1.Dequeue();
        }
        else
        {
            spawnedArrow2.Dequeue();
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

    public void doneCurrentBeat(bool isP1, bool correct)
    {
        if (isP1)
        {
            spawnedArrow1.Peek().beatDone(correct);
            //spawnedArrow1.Dequeue();
        }
        else
        {
            spawnedArrow2.Peek().beatDone(correct);
            //spawnedArrow2.Dequeue();
        }
        StartCoroutine(dequeCurrentBeat(isP1));
    }

    private void spawnBeats()
    {
        int rand = Random.Range(0, 3);
        GameObject p1 = Instantiate(arrowPrefabs[rand], spawnPoints[rand]);
        BeatArrow p1BA = p1.GetComponent<BeatArrow>();
        p1BA.setSpeed(speed);
        p1BA.setDirection(rand);
        spawnedArrow1.Enqueue(p1BA);

        GameObject p2 = Instantiate(arrowPrefabs[rand], spawnPoints[rand + 4]);
        BeatArrow p2BA = p2.GetComponent<BeatArrow>();
        p2BA.setSpeed(speed);
        p2BA.setDirection(rand);
        spawnedArrow2.Enqueue(p2BA);
    }

    private void spawnBeats(int i)
    {
        //int rand = Random.Range(0, 3);
        int rand = i;
        GameObject p1 = Instantiate(arrowPrefabs[rand], spawnPoints[rand]);
        BeatArrow p1BA = p1.GetComponent<BeatArrow>();
        p1BA.setSpeed(speed);
        p1BA.setDirection(rand);
        spawnedArrow1.Enqueue(p1BA);

        GameObject p2 = Instantiate(arrowPrefabs[rand], spawnPoints[rand + 4]);
        BeatArrow p2BA = p2.GetComponent<BeatArrow>();
        p2BA.setSpeed(speed);
        p2BA.setDirection(rand);
        spawnedArrow2.Enqueue(p2BA);
    }

    IEnumerator debugSpawn()
    {
        spawnBeats(0);
        yield return new WaitForSeconds(2.0f);
        spawnBeats(1);
    }

    public int getP1Health()
    {
        return p1Health;
    }
    public int getP2Health()
    {
        return p2Health;
    }

    private void resetHealth()
    {
        p1Health = 3;
        p2Health = 3;
    }

    public int getStateCount()
    {
        return stateCount;
    }

    public float getCurrentStateTime()
    {
        return stateTime - stateTimeKeep;
    }

    public int getCurrentCharState()
    {
        return (int)currentCharacter;
    }

    private void clearBeats()
    {
        foreach (BeatArrow arr in spawnedArrow1)
        {
            arr.killBeat();
        }
        spawnedArrow1.Clear();

        foreach (BeatArrow arr1 in spawnedArrow2)
        {
            arr1.killBeat();
        }
        spawnedArrow2.Clear();
    }

    private void defaultBeatSpawning()
    {
        timeBetweenKeep += Time.deltaTime;
        //Debug.Log(timeBetweenKeep);
        if (timeBetweenKeep > timeBetweenBeats)
        {
            spawnBeats();
            timeBetweenKeep = 0.0f;
        }
    }
}
