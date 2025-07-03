using TMPro;
using UnityEngine;

public class playerHealthPlaceHolder : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private bool isP1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isP1)
        {
            textMeshProUGUI.text = GameController.instance.getP1Health().ToString();
        }
        else
        {
            textMeshProUGUI.text = GameController.instance.getP2Health().ToString();
        }
        
    }
}
