using UnityEngine;
using UnityEngine.UI;

public class playerPoint : MonoBehaviour
{
    public Image[] pointDisplay;
    public bool isP1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateScore(0);
        updateScore(1);
        updateScore(2);
    }

    private void updateScore(int i)
    {
        int j;
        if (isP1)
        {
            j = GameController.instance.getP1ScoreEachStage()[i];
        }
        else
        {
            j = GameController.instance.getP2ScoreEachStage()[i];
        }
        switch (j)
        {
            case -1:
                pointDisplay[i].color = Color.gray;
                break;
            case 0:
                pointDisplay[i].color = Color.red;
                break;
            case 1:
                pointDisplay[i].color = Color.white;
                break;
        }
    }
}
