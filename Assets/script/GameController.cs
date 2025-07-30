using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using TMPro;
using static GameController;
using MoreMountains.Feedbacks;

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
    string previousCharacter;
    public string getPrevCharacter() { return previousCharacter; }
    public CharacterState GetCharacterState()
    {
        return currentCharacter;
    }

    private List<int> randCharacter = new List<int>();
    public int stateCount { get; private set; }

    private int[] player1ScoreEachStage = { -1, -1, -1 };
    public int getP1ScoreEachStage(int i) { return player1ScoreEachStage[i];  }
    public int getP1TotalScore() { return player1TotalScore; }

    private int player1TotalScore = 0;
    private int[] player2ScoreEachStage = { -1, -1, -1 };
    public int getP2ScoreEachStage(int i) { return player2ScoreEachStage[i]; }
    private int player2TotalScore = 0;
    public int getP2TotalScore() { return player2TotalScore; }

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
    [SerializeField] private float qteWindUpTime;
    [SerializeField] private float qteCooldownMin;
    [SerializeField] private float qteCooldownMax;
    private float qteCooldown;
    private float qteCooldownKeep = 0.0f;

    private int damageThisBeatP1 = -1;
    private int damageThisBeatP2 = -1;

    [SerializeField] playerFightDisplay animatorP1Fight;
    [SerializeField] playerFightDisplay animatorP2Fight;
    [SerializeField] Canvas p1Canvas;
    [SerializeField] Canvas p2Canvas;

    [Header("EndState")]

    [SerializeField] private GameObject EndStateObjects;
    [SerializeField] private TextMeshProUGUI textWin;
    [SerializeField] private GameObject KO;
    Animator animator;

    [Header("Game States")]
    public RandomCharacterState randomCharacterState = new RandomCharacterState();
    public GameplayState gameplayState;
    public GameOverState gameOverState = new GameOverState();
    [SerializeField] MMF_Player mMF_CameraShake;

    private void Awake()
    {
        animator = KO.GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        instance = this;
        gameplayState = new GameplayState(stateTime);
        uiManager = FindFirstObjectByType<UIManager>();
        audioManager = FindFirstObjectByType<AudioManager>();
        qteCooldown = UnityEngine.Random.Range(qteCooldownMin, qteCooldownMax);
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
    public void SetBPM(float bpm, float toHit)
    {
        this.bpm = bpm;
        toHitBoxDuration = toHit;
        float distance = Mathf.Abs(spawnPoints[0].position.y - hitBoxPoint.position.y);
        speed = distance / toHitBoxDuration;

        timeBetweenBeats = 60f / bpm;
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
        int r = randCharacter[UnityEngine.Random.Range(0, randCharacter.Count)];
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
        switch (currentCharacter)
        {
            case CharacterState.Chessur:
                previousCharacter = "Chessur";
                break;
            case CharacterState.Pinocchio:
                previousCharacter = "Pinocchio";
                break;
            case CharacterState.Cinderella:
                previousCharacter = "Cinderella";
                break;
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
            if (damageThisBeatP1 == 0 && damageThisBeatP2 > 0)
            {
                p1Canvas.sortingOrder = 0;
                p2Canvas.sortingOrder = 1;
                animatorP1Fight.playMiss();
                animatorP2Fight.playSlap();
                StartCoroutine(impactDamageDelay(SetP1Health, getP1Health() - (damageThisBeatP2)));
            }
            else if (damageThisBeatP2 == 0 && damageThisBeatP1 > 0)
            {
                p1Canvas.sortingOrder = 1;
                p2Canvas.sortingOrder = 0;
                animatorP1Fight.playSlap();
                animatorP2Fight.playMiss();

                StartCoroutine(impactDamageDelay(SetP2Health, getP2Health() - (damageThisBeatP1)));
            }
            else if (damageThisBeatP1 > damageThisBeatP2)
            {
                p1Canvas.sortingOrder = 1;
                p2Canvas.sortingOrder = 0;
                animatorP1Fight.playSlapMissed();
                animatorP2Fight.playHurt();

                StartCoroutine(impactDamageDelay(SetP2Health, getP2Health() - (damageThisBeatP1 - damageThisBeatP2)));
            }
            else if (damageThisBeatP2 > damageThisBeatP1)
            {
                p1Canvas.sortingOrder = 0;
                p2Canvas.sortingOrder = 1;
                animatorP1Fight.playHurt();
                animatorP2Fight.playSlapMissed();

                StartCoroutine(impactDamageDelay(SetP1Health, getP1Health() - (damageThisBeatP2 - damageThisBeatP1)));
            }
            else if (damageThisBeatP1 == damageThisBeatP2 && damageThisBeatP1 != 0)
            {
                //equal damage
                p1Canvas.sortingOrder = 1;
                p2Canvas.sortingOrder = 0;
                animatorP1Fight.playSlap();
                animatorP2Fight.playHurt();

            }
            else
            {
                //both misses
                animatorP1Fight.playIdle();
                animatorP2Fight.playIdle();
            }

            damageThisBeatP1 = -1; damageThisBeatP2 = -1;
        }
        else
        {
            
        }
    }

    public void setActiveKO()
    {
        KO.SetActive(true);
    }

    IEnumerator impactDamageDelay(Action<int> setHealth, int i)
    {
        yield return new WaitForSeconds(0.15f);
        setHealth(i);
        if (getP1Health() <= 0 || getP2Health() <= 0)
        {
            AudioManager.Instance.PlaySFX("EchoSlap");
        }
        else
        {
            AudioManager.Instance.PlaySFX("Slap");
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
            int randFour = four[UnityEngine.Random.Range(0, 3)];

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
                int chosen = UnityEngine.Random.Range(0, rander.Count);
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
            headArrows[0].setToChessur(headArrows[keeper[0]].getRectPos());
            headArrows[1].setToChessur(headArrows[keeper[1]].getRectPos());
            headArrows[2].setToChessur(headArrows[keeper[2]].getRectPos());
            headArrows[3].setToChessur(headArrows[keeper[3]].getRectPos());

            headArrows[4].setToChessur(headArrows[keeper[0] + 4].getRectPos());
            headArrows[5].setToChessur(headArrows[keeper[1] + 4].getRectPos());
            headArrows[6].setToChessur(headArrows[keeper[2] + 4].getRectPos());
            headArrows[7].setToChessur(headArrows[keeper[3] + 4].getRectPos());
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
                    qteCooldown = UnityEngine.Random.Range(qteCooldownMin, qteCooldownMax);
                    stopAllBeats(false);
                }
            }
        }
        return beatIsStop;
    }

    public bool checkQTEWindUp()
    {
        if (qteCooldownKeep > qteCooldown - qteWindUpTime)
        {
            Debug.Log("QTE Cooldown Keep: "+qteCooldownKeep + " qteCooldown: "+ qteCooldown+ " QTE WindUp Time: "+ qteWindUpTime);
            return true;
        }
        else
        {
            return false;
        }
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

    public void stopCurrentMusic()
    {
        switch (currentCharacter)
        {
            case CharacterState.Chessur:
                AudioManager.Instance.StopMusic("Chessur");
                break;
            case CharacterState.Pinocchio:
                AudioManager.Instance.StopMusic("Pinocchio");
                break;
            case CharacterState.Cinderella:
                AudioManager.Instance.StopMusic("Cinderella");
                break;
        }
    }

    public void pauseCurrentMusic()
    {
        switch (currentCharacter)
        {
            case CharacterState.Chessur:
                AudioManager.Instance.PauseMusic("Chessur");
                break;
            case CharacterState.Pinocchio:
                AudioManager.Instance.PauseMusic("Pinocchio");
                break;
            case CharacterState.Cinderella:
                AudioManager.Instance.PauseMusic("Cinderella");
                break;
        }
    }

    public void resumeCurrentMusic()
    {
        switch (currentCharacter)
        {
            case CharacterState.Chessur:
                AudioManager.Instance.ResumeMusic("Chessur");
                break;
            case CharacterState.Pinocchio:
                AudioManager.Instance.ResumeMusic("Pinocchio");
                break;
            case CharacterState.Cinderella:
                AudioManager.Instance.ResumeMusic("Cinderella");
                break;
        }
    }

    public void cameraShake()
    {
        mMF_CameraShake.PlayFeedbacks();
    }

    public void destroyBuggedArrows()
    {
        foreach (RectTransform rt in spawnPoints)
        {
            // Create a temporary list to store children, so we don’t modify the collection while iterating
            List<Transform> children = new List<Transform>();

            foreach (Transform child in rt)
            {
                children.Add(child);
            }

            foreach (Transform child in children)
            {
                Destroy(child.gameObject);
            }
        }
    }


}
