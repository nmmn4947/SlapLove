using UnityEngine;
using UnityEngine.UI;

public class qteVisual : MonoBehaviour
{
    [SerializeField] private Sprite _spriteOpen;
    [SerializeField] private Sprite _spriteWindUp;
    [SerializeField] private Sprite _spriteClose;
    Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.checkQTE())
        {
            image.sprite = _spriteOpen;
        }
        else if (GameController.instance.checkQTEWindUp())
        {
            image.sprite = _spriteWindUp;
        }
        else
        {
            image.sprite = _spriteClose;
        }
    }
}
