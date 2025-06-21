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
    private bool isP1;

    public Transform wrongColTop;
    public Transform correctCol;
    public Transform wrongColBot;

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
            /*            wrongKeep -= Time.deltaTime;
                        if (wrongKeep < 0.0f)
                        {
                            correctKeep -= Time.deltaTime;
                        }*/

            if (Input.GetKeyDown(keyToPress) && wrongColTop != null)
            {
                if (wrongColTop.gameObject.tag != "Beat") { }
                else
                {
                    //wrong
                    GameController.instance.minusPlayerHealth(isP1);
                    GameController.instance.dequeCurrentBeat(isP1, false);
                }
            }
            else if (Input.GetKeyDown(keyToPress) && correctCol != null)
            {
                if (correctCol.gameObject.tag != "Beat") { }
                else
                {
                    //right
                    GameController.instance.dequeCurrentBeat(isP1, true);
                }
            }
            else if (Input.GetKeyDown(keyToPress) && wrongColBot != null)
            {
                if (wrongColBot.gameObject.tag != "Beat") { }
                else
                {
                    //wrong
                    GameController.instance.minusPlayerHealth(isP1);
                    GameController.instance.dequeCurrentBeat(isP1, false);
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
                //no beat sound?? just incase
                Debug.Log("no beat");
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
        Debug.Log("Out");
        if (collision != null)
        {
            if (collision.gameObject.tag == "Beat")
            {
                isPressAble = false;
                GameController.instance.dequeCurrentBeat(isP1, false);
            }
        }
    }

    public void setIsP1(bool b)
    {
        isP1 = b;
    }

    
}
