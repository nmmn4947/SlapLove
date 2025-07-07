using System.Collections;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;
using UnityEngine;

public class BeatArrow : MonoBehaviour
{
    private int direction;

    private float speed = 50;

    RectTransform rect;
    private RectTransform thisHeadArrow;
    Collider2D col;

    private bool isDone = false;
    private bool isStop = false;

    private float stopTime = 1.0f;
    private float stopTimeKeep = 0.0f;
    private float keptStopYPosition;

    [SerializeField] private MMF_Player correct_MMFPlayer;
    [SerializeField] private MMF_Player wrong_MMFPlayer;

    private bool isCinderella;
    //private bool isChessur = false;
    private Vector3 cinderellaThreshHold;
    private Vector3 cinderellaGonePos;

    private CanvasGroup canvasGroup;
    private float cinderellaFadeTime = 3.5f;
    private float cinderellaFadeKeep = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = GetComponent<RectTransform>();
        col = GetComponent<Collider2D>();
        //isCinderella = false;
        canvasGroup = GetComponent<CanvasGroup>();
        cinderellaFadeKeep = cinderellaFadeTime;
    }

    // Update is called once per frame
    void Update()
    {
        keptStopYPosition = rect.position.y;
        if (isCinderella && !isDone)
        {
            float totalDistance = cinderellaThreshHold.y - cinderellaGonePos.y;
            float currentDistance = rect.position.y - cinderellaGonePos.y;
            float t = Mathf.Clamp01(currentDistance / totalDistance);
            canvasGroup.alpha = t;
        }
        else if (isCinderella && isDone)
        {
            canvasGroup.alpha = 1.0f;
        }


        if (!isDone && !isStop)
        {
            /*            if (isChessur)
                        {
                            rect.position = new Vector3(thisHeadArrow.position.x, rect.position.y - speed * Time.deltaTime, 0f);
                        }
                        else
                        {
                            rect.position -= new Vector3(0.0f, speed * Time.deltaTime, 0f);
                        }*/
            if (!GameController.instance.isReverse)
            {
                rect.position = new Vector3(thisHeadArrow.position.x, rect.position.y - speed * Time.deltaTime, 0f);
            }
            else
            {
                rect.position = new Vector3(thisHeadArrow.position.x, rect.position.y + speed * Time.deltaTime, 0f);
            }
        }
        else if (!isDone && isStop)
        {
            stopTimeKeep += Time.deltaTime;
            rect.position = new Vector3(thisHeadArrow.position.x, Mathf.Lerp(rect.position.y, keptStopYPosition, stopTimeKeep/stopTime), 0f);

        }
        
        /*else
        {
            doneLifeTime -= Time.deltaTime;
            if (doneLifeTime < 0.0f)
            {
                Destroy(this.gameObject);
            }
            rect.position = new Vector3(thisHeadArrow.position.x, rect.position.y, 0f);
        }*/
        //Debug.Log(thisHeadArrow.position);
    }

    public bool getIsDone()
    {
        return isDone;
    }
    public void setDirection(int s)
    {
/*        switch (s)
        {
            case "up":
                direction = 0;
                break;
            case "left":
                direction = 1;
                break;
            case "down":
                direction = 2;
                break;
            case "right":
                direction = 3;
                break;
        }*/

        direction = s;
    }

    public int getDirection() { return direction; }

    public void beatDone(bool isCorrect) {
        isDone = true;
        if (isCorrect)
        {
            //Debug.Log("Correct");
            //StartCoroutine(deadCorrectAnimation());
            //col.enabled = false; // disable collider so it doesn't trigger again
            onCorrectAnimation();
        }
        else
        {
            //Debug.Log("Wrong");
            //StartCoroutine(deadWrongAnimation());
            //col.enabled = false; // disable collider so it doesn't trigger again
            onWrongAnimation();
        }
        
    }

    public void killBeat()
    {
        Destroy(gameObject);
    }

    public void setSpeed(float bpm)
    {
        speed = bpm;
    }

    IEnumerator deadWrongAnimation()
    {
        rect.localScale += new Vector3(1.0f, 1.0f, 1.0f); // place holder anim
        yield return new WaitForSeconds(0.5f);
        //Destroy(this.gameObject);
    }

    void onCorrectAnimation()
    {
        correct_MMFPlayer.PlayFeedbacks(); // play correct animation
    }
    void onWrongAnimation()
    {
        wrong_MMFPlayer.PlayFeedbacks();
    }
    IEnumerator deadCorrectAnimation()
    {
        rect.localScale -= new Vector3(0.25f, 0.25f, 0.25f); // place holder anim
        yield return new WaitForSeconds(0.5f);
    }

    public void OnEndAnimation() //trigger at the end of both correct and wrong animations
    {
        Destroy(gameObject, 0.5f);
    }

    public void setToIsCinderella(Vector3 threshHold, Vector3 gonePos)
    {
        isCinderella = true;
        cinderellaThreshHold = threshHold;
        cinderellaGonePos = gonePos;
        //Debug.Log(isCinderella);
    }

    public void setToIsPinoccio()
    {
        this.gameObject.tag = "Fake";
    }

    public void setToChessur(RectTransform rct)
    {
        //isChessur = true;
        assignThisHeadArrow(rct);
    }

    public void assignThisHeadArrow(RectTransform rct)
    {
        thisHeadArrow = rct;
    }

    public void setIsStop(bool b)
    {
        if (b)
        {
            isStop = true;
            stopTimeKeep = 0;
            //keptStopYPosition = rect.position.y + 1.0f;
        }
        else
        {
            isStop = false;
        }

    }

    public void setIsStopNoRetract(bool b)
    {
        if (b)
        {
            isStop = true;
            stopTimeKeep = 0;
        }
        else
        {
            isStop = false;
        }

    }
}
