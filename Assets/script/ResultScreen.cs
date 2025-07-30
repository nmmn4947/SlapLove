using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    public GameObject p1Succ;
    public GameObject p2Succ;
    public Image p1Image;
    public Image p2Image;

    public TextMeshProUGUI dialogueText1;
    public TextMeshProUGUI nameText1;
    public GameObject[] nameTags1;
    public TextMeshProUGUI dialogueText2;
    public TextMeshProUGUI nameText2;
    public GameObject[] nameTags2;

    public string chessurDialogue;
    public string pinocchioDialogue;
    public string cinderrellaDialogue;

    public Image displayImage;
    public Sprite chessurSprite;
    public Sprite pinocchioSprite;
    public Sprite cinderrellaSprite;

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
            nameTags1[0].SetActive(true);
            a = dialogueText1;
            a2 = nameText1;
            displayImage = p1Image;
            switch (GameController.instance.getPrevCharacter())
            {
                case "Chessur":
                    a.text = chessurDialogue;
                    nameTags1[0].SetActive(true);
                    displayImage.sprite = chessurSprite;
                    break;
                case "Pinocchio":
                    a.text = pinocchioDialogue;
                    nameTags1[1].SetActive(true);
                    displayImage.sprite = pinocchioSprite;
                    break;
                case "Cinderella":
                    a.text = cinderrellaDialogue;
                    nameTags1[2].SetActive(true);
                    displayImage.sprite = cinderrellaSprite;
                    break;
            }
        }
        else
        {
            a = dialogueText2;
            a2 = nameText2;
            displayImage = p2Image;
            switch (GameController.instance.getPrevCharacter())
            {
                case "Chessur":
                    a.text = chessurDialogue;
                    nameTags2[0].SetActive(true);
                    displayImage.sprite = chessurSprite;
                    break;
                case "Pinocchio":
                    a.text = pinocchioDialogue;
                    nameTags2[1].SetActive(true);
                    displayImage.sprite = pinocchioSprite;
                    break;
                case "Cinderella":
                    a.text = cinderrellaDialogue;
                    nameTags2[2].SetActive(true);
                    displayImage.sprite = cinderrellaSprite;
                    break;
            }
        }
        
    }

    public void setNameTagsOff()
    {
        foreach (GameObject tag in nameTags1)
        {
            tag.SetActive(false);
        }
        foreach (GameObject tag in nameTags2)
        {
            tag.SetActive(false);
        }
    }
}
