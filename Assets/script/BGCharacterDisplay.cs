using TMPro;
using UnityEngine;

public class BGCharacterDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;

    //The character sprite

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameController.instance.GetCurrentCharState())
        {
            case 0:
                nameText.text = "Chessur";
                break;
            case 1:
                nameText.text = "Pinoccio";
                break;
            case 2:
                nameText.text = "Cinderella";
                break;
        }
    }
}
