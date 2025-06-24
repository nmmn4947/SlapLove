using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

public class BeatArrow : MonoBehaviour
{
    private int direction;

    private float speed = 50;

    RectTransform rect;
    Collider2D col;

    private bool isDone = false;
    [SerializeField] private MMF_Player correct_MMFPlayer;
    [SerializeField] private MMF_Player wrong_MMFPlayer;

    private bool isCinderella;
    private bool isPinoccio;
    private Vector3 cinderellaThreshHold;

    private CanvasGroup canvasGroup;
    private float cinderellaFadeTime = 2.5f;
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
        if (isCinderella && !isDone)
        {
            //Debug.Log(rect.position.y - cinderellaThreshHold.y);
            if (rect.position.y - cinderellaThreshHold.y < 0.0f)
            {
                //set opacity zero
                Debug.Log("aa");
                cinderellaFadeKeep -= Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(0.0f, 1.0f, cinderellaFadeKeep/cinderellaFadeTime);
            }
        }
        else if (isCinderella && isDone)
        {
            canvasGroup.alpha = 1.0f;
        }

        

        if (!isDone)
        {
            rect.position -= new Vector3(0f, speed * Time.deltaTime, 0f);
        }
    }

    public bool GetIsDone()
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
            col.enabled = false; // disable collider so it doesn't trigger again
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
        //Destroy(this.gameObject);
    }

    public void OnEndAnimation() //trigger at the end of both correct and wrong animations
    {
        Destroy(gameObject, 0.3f);
    }

    public void setToIsCinderella(Vector3 threshHold)
    {
        isCinderella = true;
        cinderellaThreshHold = threshHold;
        //Debug.Log(isCinderella);
    }
}
