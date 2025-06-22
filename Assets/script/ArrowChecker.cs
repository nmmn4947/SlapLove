using UnityEngine;

public class ArrowChecker : MonoBehaviour
{
    [SerializeField] private bool isCorrect; //Literally useless?
    private bool touchedBeat = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Beat")
            {
                touchedBeat = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Beat")
            {
                touchedBeat = false;
            }
        }
    }

    public bool getBeatIsTouched()
    {
        return touchedBeat;
    }
}
