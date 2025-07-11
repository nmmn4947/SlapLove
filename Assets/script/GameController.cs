using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class GameController : MonoBehaviour
{
    //[Header("General")]
    public static GameController instance;
    public bool isReverse;
    public enum CharacterState
    {
        Chessur,
        Pinocchio,
        Cinderella
    };
    CharacterState currentCharacter;
    public CharacterState GetCharacterState()
    {
        return currentCharacter;
    }

    private List<int> randCharacter = new List<int>();
    public int stateCount { get; private set; }

    private int[] player1ScoreEachStage = { -1, -1, -1 };
    private int player1TotalScore = 0;
    private int[] player2ScoreEachStage = { -1, -1, -1 };
    private int player2TotalScore = 0;

    enum GameState
    {
        CharacterStage,
        SlapState,
        GameEndState
    };
    GameBaseState currentState;

    [SerializeField] int playerMaxHealth = 1000;
    int p1Health = 1000;
    int p2Health = 1000;

    [Header("CharacterState")]

    [SerializeField] private GameObject CharacterStateObjects;

    public UIManager uiManager { get; private set; }
    [SerializeField] public ReadyCheck ReadyCheckP1 { get; private set; }
    [SerializeField] public ReadyCheck ReadyCheckP2 { get; private set; }



    [Header("Slap/RhythmState")]
    [SerializeField] public AudioManager audioManager { get; private set; }

    [SerializeField] private GameObject SlapStateObjects;

    [SerializeField] private GameObject[] arrowPrefabs;

    [SerializeField] private HeadArrow[] headArrows;

    Queue<BeatArrow> spawnedArrow1 = new Queue<BeatArrow>(); // .front
    Queue<BeatArrow> spawnedArrow2 = new Queue<BeatArrow>(); // .front

    public BeatRow p1Row;
    public BeatRow p2Row;

    [SerializeField] private float stateTime = 120.0f;
    private float stateTimeKeep = 0.0f;

    [SerializeField] private float bpm;
    private float timeBetweenBeats = 0.5f;
    float totalTime;
    private float timeBetweenKeep = 0.0f;

    //[SerializeField] private int beatTempo;
    float beatDuration;
    [SerializeField] float toHitBoxDuration; // 
    float speed;

    public RectTransform[] spawnPoints;
    public RectTransform hitBoxPoint;

    [SerializeField] private RectTransform cinderellaThreshHold;
    [SerializeField] private RectTransform cinderellaGonePos;

    [SerializeField] private int pinoccioChance;

    [SerializeField] private float chessurDurationTime;
    private float chessurDurationKeep = 0;
    [SerializeField] private float chessurCooldownTime;
    private float chessurCooldownKeep = 0;
    private bool chessurIsActive = false;

    private bool beatIsStop = false;
    [SerializeField] private float qteDuration;
    private float qteDurationKeep = 0.0f;
    [SerializeField] private float qteCooldownMin;
    [SerializeField] private float qteCooldownMax;
    private float qteCooldown;
    private float qteCooldownKeep = 0.0f;

    private int damageThisBeatP1 = -1;
    private int damageThisBeatP2 = -1;

    [SerializeField] playerFightDisplay animatorP1Fight;
    [SerializeField] playerFightDisplay animatorP2Fight;

    [Header("EndState")]

    [SerializeField] private GameObject EndStateObjects;
    [SerializeField] private TextMeshProUGUI textWin;

    [Header("Game States")]
    public RandomCharacterState randomCharacterState = new RandomCharacterState();
    public GameplayState gameplayState;
    public GameOverState gameOverState = new GameOverState();

    private void Awake()
    {
        instance = this;
        gameplayState = new GameplayState(stateTime);
        uiManager = FindFirstObjectByType<UIManager>();
        audioManager = FindFirstObjectByType<AudioManager>();
        qteCooldown = Random.Range(qteCooldownMin, qteCooldownMax);
        //Debug.Log(uiManager);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateCount = 0;
        float distance = Mathf.Abs(spawnPoints[0].position.y - hitBoxPoint.position.y);
        speed = distance / toHitBoxDuration;

        timeBetweenBeats = 60f / bpm;

        p1Row.setIsP1(true);
        p2Row.setIsP1(false);
        //StartCoroutine(debugSpawn());
        randCharacter.Add(0);
        randCharacter.Add(1);
        randCharacter.Add(2);
        currentState = randomCharacterState;
        currentState.EnterState(this);
    }
    private void Update()
    {
        currentState.UpdateState(this);
    }

    public int getMaxPlayerHealth()
    {
        return playerMaxHealth;
    }

    public void SwitchState(GameBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void characterRand()
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

    public void AddStateCount()
    {
        stateCount++;
    }

    IEnumerator dequeCurrentBeat(bool isP1)
    {
        spawnedArrow1.Dequeue();
        spawnedArrow2.Dequeue();
        yield return new WaitForSeconds(0f); //temporary remove delay
        /*        if (spawnedArrow1.Count == 0)
                {
                    yield return null;
                }*//*
        
        if (isP1)
        {
            
        }
        else
        {
            
        }*/

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

    public void PlayCurrentBeatDeadAnimation(bool isP1, bool isCorrect)
    {
        if (isP1)
        {
            spawnedArrow1.Peek().beatDone(isCorrect);
        }
        else
        {
            spawnedArrow2.Peek().beatDone(isCorrect);
        }
    }

    public void DoneCurrentBeat(bool isP1, int damage)
    {
        if (isP1)
        {
            damageThisBeatP1 = damage;
            spawnedArrow1.Dequeue();
        }
        else
        {
            damageThisBeatP2 = damage;
            spawnedArrow2.Dequeue();
        }
    }

    public void checkIfDamagable()
    {
        if (damageThisBeatP1 != -1 && damageThisBeatP2 != -1)
        {
            if (damageThisBeatP1 > damageThisBeatP2)
            {
                animatorP1Fight.playSlap();
                animatorP2Fight.playHurt();
                SetP2Health(getP2Health() - (damageThisBeatP1 - damageThisBeatP2));
            }
            else if (damageThisBeatP2 > damageThisBeatP1)
            {
                animatorP1Fight.playHurt();
                animatorP2Fight.playSlap();
                SetP1Health(getP1Health() - (damageThisBeatP2 - damageThisBeatP1));
            }
            else if (damageThisBeatP1 == damageThisBeatP2 && damageThisBeatP1 != 0)
            {
                //equal damage
                animatorP1Fight.playSlap();
                animatorP2Fight.playSlap();
            }
            else
            {
                //both misses
                animatorP1Fight.playMiss();
                animatorP2Fight.playMiss();
            }

            damageThisBeatP1 = -1; damageThisBeatP2 = -1;
        }
        else
        {
            
        }
    }

    public void SpawnBeats()
    {
        int rand = UnityEngine.Random.Range(0, 3);
        GameObject p1 = Instantiate(arrowPrefabs[rand], spawnPoints[rand]);
        BeatArrow p1BA = p1.GetComponent<BeatArrow>();
        p1BA.assignThisHeadArrow(headArrows[rand].getRectTransform());
        p1BA.setSpeed(speed);
        p1BA.setDirection(rand);
        spawnedArrow1.Enqueue(p1BA);

        GameObject p2 = Instantiate(arrowPrefabs[rand], spawnPoints[rand + 4]);
        BeatArrow p2BA = p2.GetComponent<BeatArrow>();
        p2BA.assignThisHeadArrow(headArrows[rand + 4].getRectTransform());
        p2BA.setSpeed(speed);
        p2BA.setDirection(rand);
        spawnedArrow2.Enqueue(p2BA);
    }

    private void SpawnBeats(int i)
    {
        //int rand = Random.Range(0, 3);
        int rand = i;
        GameObject p1 = Instantiate(arrowPrefabs[rand], spawnPoints[rand]);
        BeatArrow p1BA = p1.GetComponent<BeatArrow>();
        p1BA.assignThisHeadArrow(headArrows[rand].getRectTransform());
        p1BA.setSpeed(speed);
        p1BA.setDirection(rand);
        spawnedArrow1.Enqueue(p1BA);

        GameObject p2 = Instantiate(arrowPrefabs[rand], spawnPoints[rand + 4]);
        BeatArrow p2BA = p2.GetComponent<BeatArrow>();
        p2BA.assignThisHeadArrow(headArrows[rand + 4].getRectTransform());
        p2BA.setSpeed(speed);
        p2BA.setDirection(rand);
        spawnedArrow2.Enqueue(p2BA);
    }

    public void SpawnPinoccioBeats()
    {
        int randPinoc = UnityEngine.Random.Range(0, pinoccioChance);
        int rand = UnityEngine.Random.Range(0, 3);
        List<int> four = new List<int>();
        four.Add(0);
        four.Add(1);
        four.Add(2);
        four.Add(3);
        
        if (randPinoc == 2) // 1/pinoccioChance - chance
        {
            four.Remove(rand);
            int randFour = four[Random.Range(0, 3)];

            GameObject p1 = Instantiate(arrowPrefabs[rand], spawnPoints[randFour]);
            BeatArrow p1BA = p1.GetComponent<BeatArrow>();
            p1BA.assignThisHeadArrow(headArrows[randFour].getRectTransform());
            p1BA.setSpeed(speed);
            p1BA.setDirection(rand);
            p1BA.setToIsPinoccio();
            spawnedArrow1.Enqueue(p1BA);

            GameObject p2 = Instantiate(arrowPrefabs[rand], spawnPoints[randFour + 4]);
            BeatArrow p2BA = p2.GetComponent<BeatArrow>();
            p2BA.assignThisHeadArrow(headArrows[randFour + 4].getRectTransform());
            p2BA.setSpeed(speed);
            p2BA.setDirection(rand);
            p2BA.setToIsPinoccio();
            spawnedArrow2.Enqueue(p2BA);
        }
        else
        {
            SpawnBeats(rand);
        }
    }

    public void SpawnCinderellaBeats()
    {
        int rand = UnityEngine.Random.Range(0, 3);
        GameObject p1 = Instantiate(arrowPrefabs[rand], spawnPoints[rand]);
        BeatArrow p1BA = p1.GetComponent<BeatArrow>();
        p1BA.assignThisHeadArrow(headArrows[rand].getRectTransform());
        p1BA.setSpeed(speed);
        p1BA.setDirection(rand);
        p1BA.setToIsCinderella(cinderellaThreshHold.position, cinderellaGonePos.position);
        spawnedArrow1.Enqueue(p1BA);

        GameObject p2 = Instantiate(arrowPrefabs[rand], spawnPoints[rand + 4]);
        BeatArrow p2BA = p2.GetComponent<BeatArrow>();
        p2BA.assignThisHeadArrow(headArrows[rand + 4].getRectTransform());
        p2BA.setSpeed(speed);
        p2BA.setDirection(rand);
        p2BA.setToIsCinderella(cinderellaThreshHold.position, cinderellaGonePos.position);
        spawnedArrow2.Enqueue(p2BA);
    }

    public void SpawnChessurBeats()
    {
        int rand = UnityEngine.Random.Range(0, 3);
        GameObject p1 = Instantiate(arrowPrefabs[rand], spawnPoints[rand]);
        BeatArrow p1BA = p1.GetComponent<BeatArrow>();
        p1BA.assignThisHeadArrow(headArrows[rand].getRectTransform());
        p1BA.setSpeed(speed);
        p1BA.setDirection(rand);
        //p1BA.setToChessur(spawnPoints[rand]);
        spawnedArrow1.Enqueue(p1BA);

        GameObject p2 = Instantiate(arrowPrefabs[rand], spawnPoints[rand + 4]);
        BeatArrow p2BA = p2.GetComponent<BeatArrow>();
        p2BA.assignThisHeadArrow(headArrows[rand + 4].getRectTransform());
        p2BA.setSpeed(speed);
        p2BA.setDirection(rand);
        //p2BA.setToChessur(spawnPoints[rand]);
        spawnedArrow2.Enqueue(p2BA);
    }

    public void SwapHeadArrowRandomly()
    {
        chessurCooldownKeep += Time.deltaTime;
        if (chessurCooldownKeep > chessurCooldownTime && !chessurIsActive)
        {
            List<int> rander = new List<int>();
            rander.Add(0);
            rander.Add(1);
            rander.Add(2);
            rander.Add(3);
            List<int> keeper = new List<int>();
            List<Color> color = new List<Color>();
            for (int i = 0; i < 4; i++)
            {
                int chosen = Random.Range(0, rander.Count);
                keeper.Add(rander[chosen]);
                switch (rander[chosen])
                {
                    case 0:
                        color.Add(Color.magenta);
                        break;
                    case 1:
                        color.Add(Color.blue);
                        break;
                    case 2:
                        color.Add(Color.green);
                        break;
                    case 3:
                        color.Add(Color.red);
                        break;
                }
                //Debug.Log(rander[chosen]);
                rander.RemoveAt(chosen);
                
            }
            headArrows[0].setToChessur(headArrows[keeper[0]].getRectPos(), color[0]);
            headArrows[1].setToChessur(headArrows[keeper[1]].getRectPos(), color[1]);
            headArrows[2].setToChessur(headArrows[keeper[2]].getRectPos(), color[2]);
            headArrows[3].setToChessur(headArrows[keeper[3]].getRectPos(), color[3]);

            headArrows[4].setToChessur(headArrows[keeper[0] + 4].getRectPos(), color[0]);
            headArrows[5].setToChessur(headArrows[keeper[1] + 4].getRectPos(), color[1]);
            headArrows[6].setToChessur(headArrows[keeper[2] + 4].getRectPos(), color[2]);
            headArrows[7].setToChessur(headArrows[keeper[3] + 4].getRectPos(), color[3]);
            chessurDurationKeep = 0.0f;
            keeper.Clear();
            chessurIsActive = true;
        }
        else
        {
            chessurDurationKeep += Time.deltaTime;
            if (chessurDurationKeep > chessurDurationTime && chessurIsActive)
            {
                headArrows[0].setToNormal(Color.magenta);
                headArrows[1].setToNormal(Color.blue);
                headArrows[2].setToNormal(Color.green);
                headArrows[3].setToNormal(Color.red);

                headArrows[4].setToNormal(Color.magenta);
                headArrows[5].setToNormal(Color.blue);
                headArrows[6].setToNormal(Color.green);
                headArrows[7].setToNormal(Color.red);
                chessurCooldownKeep = 0.0f;
                chessurIsActive = false;
            }
        }
    }

    public void SwapHeadArrowToNormal()
    {
        headArrows[0].setToNormal(Color.magenta);
        headArrows[1].setToNormal(Color.blue);
        headArrows[2].setToNormal(Color.green);
        headArrows[3].setToNormal(Color.red);

        headArrows[4].setToNormal(Color.magenta);
        headArrows[5].setToNormal(Color.blue);
        headArrows[6].setToNormal(Color.green);
        headArrows[7].setToNormal(Color.red);
    }
    public int getP1Health()
    {
        return p1Health;
    }
    public int getP2Health()
    {
        return p2Health;
    }

    public void SetP1Health(int amount)
    {
        p1Health = amount;
    }

    public void SetP2Health(int amount)
    {
        p2Health = amount;
    }

    public int GetStateCount()
    {
        return stateCount;
    }

    public float GetCurrentStateTime()
    {
        return stateTime - stateTimeKeep;
    }

    public int GetCurrentCharState()
    {
        return (int)currentCharacter;
    }

    public void ClearBeats()
    {
        foreach (BeatArrow arr in spawnedArrow1)
        {
            Debug.Log("gone");
            arr.killBeat();
        }
        spawnedArrow1.Clear();

        foreach (BeatArrow arr1 in spawnedArrow2)
        {
            arr1.killBeat();
        }
        spawnedArrow2.Clear();
    }

    public delegate void BeatArrowDelegate();

    public void BeatSpawning(BeatArrowDelegate operation)
    {
        timeBetweenKeep += Time.deltaTime;
        //Debug.Log(timeBetweenKeep);
        if (timeBetweenKeep > timeBetweenBeats)
        {
            operation();
            timeBetweenKeep = 0.0f;
        }
    }
    
    public Vector3 GetHeadArrowPosition(int i)
    {
        return headArrows[i].getRectPos();
    }

    public void setBeatToStop(bool b)
    {
        beatIsStop = b;
    }

    public bool checkQTE()
    {
        if (currentState is GameplayState) {
            if (!beatIsStop) {
                qteCooldownKeep += Time.deltaTime;
                if (qteCooldownKeep > qteCooldown)
                {
                    beatIsStop = true;
                    qteDurationKeep = 0.0f;
                    audioManager.onQTE.PlayFeedbacks();
                    stopAllBeats(true);
                }
            }
            else
            {
                qteDurationKeep += Time.deltaTime;
                if (qteDurationKeep > qteDuration)
                {
                    beatIsStop = false;
                    qteCooldownKeep = 0.0f;
                    qteCooldown = Random.Range(qteCooldownMin, qteCooldownMax);
                    stopAllBeats(false);
                }
            }
        }
        return beatIsStop;
    }

    public void stopAllBeats(bool bull)
    {
        foreach (BeatArrow b in spawnedArrow1)
        {
            b.setIsStop(bull);
        }
        foreach (BeatArrow b2 in spawnedArrow2)
        {
            b2.setIsStop(bull);
        }
    }

    public void stopAllBeatsNoRetract(bool bull)
    {
        foreach (BeatArrow b in spawnedArrow1)
        {
            b.setIsStopNoRetract(bull);
        }
        foreach (BeatArrow b2 in spawnedArrow2)
        {
            b2.setIsStopNoRetract(bull);
        }
    }

    public void addP1WinScore(bool isP1)
    {
        if (isP1)
        {
            player1ScoreEachStage[stateCount] = 1;
            player2ScoreEachStage[stateCount] = 0;
            player1TotalScore++;
        }
        else
        {
            player1ScoreEachStage[stateCount] = 0;
            player2ScoreEachStage[stateCount] = 1;
            player2TotalScore++;
        }
    }

    public int[] getP1ScoreEachStage()
    {
        return player1ScoreEachStage;
    }

    public int getTotalP1Score()
    {
        return player1TotalScore;
    }

    public int[] getP2ScoreEachStage()
    {
        return player2ScoreEachStage;
    }

    public int getTotalP2Score()
    {
        return player2TotalScore;
    }

    public void updateEndStateWinText()
    {
        if (getTotalP1Score() > getTotalP2Score())
        {
            textWin.text = "Player1 Wins!";
        }
        else
        {
            textWin.text = "Player2 Wins!";
        }
        
    }
}
