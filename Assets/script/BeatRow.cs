using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BeatRow : MonoBehaviour
{
    bool isPressAble = false;
    bool isFake = false;
/*    [SerializeField] private float wrongWindowTime;
    private float wrongKeep;
    [SerializeField] private float correctWindowTime;
    private float correctKeep;*/
    [SerializeField] private bool isP1;

    public ArrowChecker correctCol;
    public ArrowChecker goodCol;

    KeyCode keyToPress;

    private void Start()
    {
/*        wrongKeep = wrongWindowTime;
        correctKeep = correctWindowTime;*/
    }

    private void Update()
    {
        //int currentDirection = GameController.instance.getCurrentBeatDirection(isP1);
        //Debug.Log(currentDirection);

        if (isPressAble && !isFake)
        {
            if (Input.GetKeyDown(keyToPress) && correctCol.getBeatIsTouched())
            {
                resolveBeat("perfect");
            }
            else if (Input.GetKeyDown(keyToPress) && goodCol.getBeatIsTouched())
            {
                resolveBeat("good");
            }
            else if (Input.GetKeyDown(keyToPress))
            {
                resolveBeat("miss");
            }
        }else if (isPressAble && !isFake && GameController.instance.checkQTE())
        {
            resolveBeat("miss"); // pressing while qte
        }
        else if (isPressAble && isFake)
        {
            if (isP1) {
                if (Input.GetKeyDown(KeyCode.W) ||
                    Input.GetKeyDown(KeyCode.A) ||
                    Input.GetKeyDown(KeyCode.S) ||
                    Input.GetKeyDown(KeyCode.D))
                {
                    Debug.Log("Press Fake");
                    resolveBeat("miss");
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Debug.Log("Press Fake");
                    resolveBeat("miss");
                }
            }
        }
        else
        {
            /*            wrongKeep = wrongWindowTime;
                        correctKeep = correctWindowTime;*/
            if (Input.GetKeyDown(KeyCode.W) || 
                Input.GetKeyDown(KeyCode.A) || 
                Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.UpArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.RightArrow))
            {
                //no beat sound feed back
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Beat")
            {
                isPressAble = true;
                int currentDirection = GameController.instance.getCurrentBeatDirection(isP1);
                switch (currentDirection)
                {
                    case 0:
                        if (isP1) { keyToPress = KeyCode.W; }
                        else { keyToPress = KeyCode.UpArrow; }
                        break;
                    case 1:
                        if (isP1) { keyToPress = KeyCode.A; }
                        else { keyToPress = KeyCode.LeftArrow; }
                        break;
                    case 2:
                        if (isP1) { keyToPress = KeyCode.S; }
                        else { keyToPress = KeyCode.DownArrow; }
                        break;
                    case 3:
                        if (isP1) { keyToPress = KeyCode.D; }
                        else { keyToPress = KeyCode.RightArrow; }
                        break;
                }
            }else if (collision.gameObject.tag == "Fake")
            {
                isPressAble = true;
                isFake = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision != null)
        {
            if (collision.gameObject.tag == "Beat" && !collision.GetComponent<BeatArrow>().getIsDone())
            {
                
                //Debug.Log(collision.gameObject.name+" Exit collider " + "is Player 1: "+isP1);
                if (isPressAble)
                {
                    
                    resolveBeat("miss");
                }
            }
            else if (collision.gameObject.tag == "Fake" && !collision.GetComponent<BeatArrow>().getIsDone())
            {
                resolveBeat("good"); // Count as good for now, (This is a scenario where you don't press the fake pinoccio beat arrow)
            }
        }
    }

    private void resolveBeat(string s)
    {
        switch (s)
        {
            case "perfect":
                GameController.instance.PlayCurrentBeatDeadAnimation(isP1, true);
                GameController.instance.DoneCurrentBeat(isP1, 100);
                break;
            case "good":
                GameController.instance.PlayCurrentBeatDeadAnimation(isP1, true);
                GameController.instance.DoneCurrentBeat(isP1, 50);
                break;
            case "miss":
                GameController.instance.PlayCurrentBeatDeadAnimation(isP1, false);
                GameController.instance.DoneCurrentBeat(isP1, 0);
                break;
        }
            
        isPressAble = false;
        isFake = false;
    }

    public void setIsP1(bool b)
    {
        isP1 = b;
    }
}
