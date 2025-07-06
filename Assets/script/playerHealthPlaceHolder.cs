using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerHealthPlaceHolder : MonoBehaviour
{
    //TextMeshProUGUI textMeshProUGUI;
    Slider health;
    public Gradient gradient;
    public Image fill;
    [SerializeField] private bool isP1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        health = GetComponent<Slider>();
        health.maxValue = GameController.instance.getMaxPlayerHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (isP1)
        {
            health.value = GameController.instance.getP1Health();
            fill.color = gradient.Evaluate(health.normalizedValue);
            //textMeshProUGUI.text = GameController.instance.getP1Health().ToString();
        }
        else
        {
            health.value = GameController.instance.getP2Health();
            fill.color = gradient.Evaluate(health.normalizedValue);
            //textMeshProUGUI.text = GameController.instance.getP2Health().ToString();
        }
        
    }
}
