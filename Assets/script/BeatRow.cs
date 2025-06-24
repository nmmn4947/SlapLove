using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BeatRow : MonoBehaviour
{
    bool isPressAble = false;
/*    [SerializeField] private float wrongWindowTime;
    private float wrongKeep;
    [SerializeField] private float correctWindowTime;
    private float correctKeep;*/
    [SerializeField] private bool isP1;

    public ArrowChecker correctCol;

    KeyCode keyToPress;

    private void Start()
    {
/*        wrongKeep = wrongWindowTime;
        correctKeep = correctWindowTime;*/
    }

    private void Update()
    {
        if (isPressAble)
        {
            if (Input.GetKeyDown(keyToPress) && correctCol.getBeatIsTouched())
            {
                //right
                Debug.Log("Correct" + isP1);

                resolveBeat(true);
            }
            else if (Input.GetKeyDown(keyToPress))
            {
                //wrong
                Debug.Log("Wrong" + isP1);
                resolveBeat(false);
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
                //no beat sound?? just incase
                Debug.Log("no beat" + isP1);
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
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision != null)
        {
            if (collision.gameObject.tag == "Beat" && !collision.GetComponent<BeatArrow>().GetIsDone())
            {
                
                Debug.Log(collision.gameObject.name+" Exit collider " + "is Player 1: "+isP1);
                if (isPressAble)
                {
                    resolveBeat(false);
                }
            }
        }
    }

    private void resolveBeat(bool correct)
    {
        if (!correct)
        {
            GameController.instance.minusPlayerHealth(isP1);
        }
        GameController.instance.doneCurrentBeat(isP1, correct);
        isPressAble = false;
    }

    public void setIsP1(bool b)
    {
        isP1 = b;
    }
}
