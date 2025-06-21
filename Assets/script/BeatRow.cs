using System.Collections;
using UnityEngine;

public class BeatRow : MonoBehaviour
{
    bool isPressAble = false;
    [SerializeField] private float wrongWindowTime;
    private float wrongKeep;
    [SerializeField] private float correctWindowTime;
    private float correctKeep;
    private bool isP1;

    private void Start()
    {
        wrongKeep = wrongWindowTime;
        correctKeep = correctWindowTime;
    }

    private void Update()
    {
        if (isPressAble)
        {
            wrongKeep -= Time.deltaTime;
            if (isP1)
            {
                //pressed
            }
            else
            {
                //pressed
            }
        }
        else
        {
            wrongKeep = wrongWindowTime;
            correctKeep = correctWindowTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Beat")
            {
                isPressAble = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Beat")
            {
                isPressAble = false;
            }
        }
    }

    public void setIsP1(bool b)
    {
        isP1 = b;
    }
}
