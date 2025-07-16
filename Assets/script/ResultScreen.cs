using TMPro;
using UnityEngine;

public class ResultScreen : MonoBehaviour
{
    public GameObject p1Succ;
    public GameObject p2Succ;

    public TextMeshProUGUI dialogueText1;
    public TextMeshProUGUI nameText1;
    public TextMeshProUGUI dialogueText2;
    public TextMeshProUGUI nameText2;

    public string chessurDialogue;
    public string pinocchioDialogue;
    public string cinderrellaDialogue;

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
                setDialogueP1Succ(true);
            }
            else if (GameController.instance.getP2ScoreEachStage(GameController.instance.stateCount - 1) == 1)
            {
                p2Succ.SetActive(true);
                p1Succ.SetActive(false);
                setDialogueP1Succ(false);
            }
        }

    }
    public void setResultScreenOff()
    {
        this.gameObject.SetActive(false);
    }

    private void setDialogueP1Succ(bool b)
    {
        TextMeshProUGUI a;
        TextMeshProUGUI a2;
        if (b)
        {
            a = dialogueText1;
            a2 = nameText1;
        }
        else
        {
            a = dialogueText2;
            a2 = nameText2;
        }
        switch (GameController.instance.getPrevCharacter())
        {
            case "Chessur":
                a.text = chessurDialogue;
                a2.text = "Chessur";
                break;
            case "Pinocchio":
                a.text = pinocchioDialogue;
                a2.text = "Pinocchio";
                break;
            case "Cinderella":
                a.text = cinderrellaDialogue;
                a2.text = "Cinderella";
                break;
        }
    }
}
