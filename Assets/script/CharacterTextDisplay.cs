using TMPro;
using UnityEngine;

public class CharacterTextDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
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
        switch (gameController.getCurrentCharState()) {
            case 0:
                nameText.text = "Chessur";
                descriptionText.text = chessurDescription;
                break;
            case 1:
                nameText.text = "Pinoccio";
                descriptionText.text = pinoccioDescription;
                break;
            case 2:
                nameText.text = "Cinderella";
                descriptionText.text = cinderellaDescription;
                break;
        }
    }
}
