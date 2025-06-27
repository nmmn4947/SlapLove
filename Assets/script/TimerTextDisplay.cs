
using TMPro;
using UnityEngine;

public class TimerTextDisplay : MonoBehaviour
{
    TextMeshProUGUI m_TextMeshProUGUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        m_TextMeshProUGUI.text = GameController.instance.GetCurrentStateTime().ToString();
    }
}
