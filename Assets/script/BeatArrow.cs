using UnityEngine;

public class BeatArrow : MonoBehaviour
{
    private int direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDirection(string s)
    {
        switch (s)
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
        }
    }

    public int getDirection() { return direction; }

    public void beatDone(bool isCorrect) {
        if (isCorrect)
        {
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("Wrong");
        }
    }


}
