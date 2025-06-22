using System.Collections;
using UnityEngine;

public class BeatArrow : MonoBehaviour
{
    private int direction;

    private int beatTempo = 120;

    RectTransform rect;

    private bool isDone = false;

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
            rect.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
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
        }
        else
        {
            //Debug.Log("Wrong");
        }
        Debug.Log("Destroy");
        Destroy(this.gameObject);
    }

    public void setBeatTempo(int bpm)
    {
        beatTempo = bpm;
    }

    IEnumerator deadAnimation()
    {
        yield return WaitForSeconds(1.0f);
    }
}
