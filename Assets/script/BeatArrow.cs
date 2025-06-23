using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

public class BeatArrow : MonoBehaviour
{
    private int direction;

    private float speed = 120;

    RectTransform rect;

    private bool isDone = false;
    [SerializeField] private MMF_Player correct_MMFPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDone)
        {
            rect.position -= new Vector3(0f, speed * Time.deltaTime, 0f);
        }
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
            onCorrectAnimation();
        }
        else
        {
            //Debug.Log("Wrong");
            //StartCoroutine(deadWrongAnimation());
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
    IEnumerator deadCorrectAnimation()
    {
        rect.localScale -= new Vector3(0.25f, 0.25f, 0.25f); // place holder anim
        yield return new WaitForSeconds(0.5f);
        //Destroy(this.gameObject);
    }

    public void OnEndAnimation() //trigger at the end of both correct and wrong animations
    {
        Destroy(gameObject, 10f);
    }
}
