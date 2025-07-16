using UnityEngine;

public class ResultScreen : MonoBehaviour
{
    public GameObject p1Succ;
    public GameObject p2Succ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.stateCount != 0)
        {
            if (GameController.instance.getP1ScoreEachStage(GameController.instance.stateCount - 1) == 1)
            {
                p1Succ.SetActive(true);
                p2Succ.SetActive(false);
            }
            else if (GameController.instance.getP2ScoreEachStage(GameController.instance.stateCount - 1) == 1)
            {
                p2Succ.SetActive(true);
                p1Succ.SetActive(false);
            }
        }
    }
    public void setResultScreenOff()
    {
        this.gameObject.SetActive(false);
    }
}
