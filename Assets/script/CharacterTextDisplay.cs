using TMPro;
using UnityEngine;

public class CharacterTextDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private GameObject chessurImage;
    [SerializeField] private GameObject pinoccioImage;
    [SerializeField] private GameObject cinderellaImage;

    [SerializeField] private string chessurDescription;
    [SerializeField] private string pinoccioDescription;
    [SerializeField] private string cinderellaDescription;

    GameController gameController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameController = GameController.instance;
        chessurImage.SetActive(false);
        pinoccioImage.SetActive(false);
        cinderellaImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameController.GetCurrentCharState()) {
            case 0:
                nameText.text = "Chessur";
                titleText.text = "The Mischievous Cat";
                descriptionText.text = chessurDescription;
                chessurImage.SetActive(true);
                pinoccioImage.SetActive(false);
                cinderellaImage.SetActive(false);
                break;
            case 1:
                nameText.text = "Pinocchio";
                titleText.text = "The Sweet Lie";
                descriptionText.text = pinoccioDescription;
                chessurImage.SetActive(false);
                pinoccioImage.SetActive(true);
                cinderellaImage.SetActive(false);
                break;
            case 2:
                nameText.text = "Cinderella";
                titleText.text = "The Transient Dream";
                descriptionText.text = cinderellaDescription;
                chessurImage.SetActive(false);
                pinoccioImage.SetActive(false);
                cinderellaImage.SetActive(true);
                break;
        }
    }
}
