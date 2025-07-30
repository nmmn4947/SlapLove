using TMPro;
using UnityEngine;

public class CharacterTextDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private string chessurDescription;
    [SerializeField] private string pinoccioDescription;
    [SerializeField] private string cinderellaDescription;

    GameController gameController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameController = GameController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameController.GetCurrentCharState()) {
            case 0:
                nameText.text = "Chessur";
                titleText.text = "The Mischievous Cat";
                descriptionText.text = chessurDescription;
                break;
            case 1:
                nameText.text = "Pinocchio";
                titleText.text = "The Sweet Lie";
                descriptionText.text = pinoccioDescription;
                break;
            case 2:
                nameText.text = "Cinderella";
                titleText.text = "The Transient Dream";
                descriptionText.text = cinderellaDescription;
                break;
        }
    }
}
