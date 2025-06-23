using UnityEngine;

public class ReadyCheck : MonoBehaviour
{
    [SerializeField] private bool isP1;
    public GameObject[] arrows;
    public GameObject readyText;
    private bool isReady = false;

    private KeyCode[] setP1 = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D}; // P Nonthi said you should use Input system off unity :( (Me forgor)
    private KeyCode[] setP2 = { KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isP1)
        {
            if (Input.GetKeyDown(setP1[0]))
            {
                arrows[0].SetActive(false); //place holder
            }
            if (Input.GetKeyDown(setP1[1]))
            {
                arrows[1].SetActive(false);
            }
            if (Input.GetKeyDown(setP1[2]))
            {
                arrows[2].SetActive(false);
            }
            if (Input.GetKeyDown(setP1[3]))
            {
                arrows[3].SetActive(false);
            } 
        }
        else
        {
            if (Input.GetKeyDown(setP2[0]))
            {
                arrows[0].SetActive(false);
            }
            if (Input.GetKeyDown(setP2[1]))
            {
                arrows[1].SetActive(false);
            }
            if (Input.GetKeyDown(setP2[2]))
            {
                arrows[2].SetActive(false);
            }
            if (Input.GetKeyDown(setP2[3]))
            {
                arrows[3].SetActive(false);
            }
        }

        if (!arrows[0].activeInHierarchy && !arrows[1].activeInHierarchy && !arrows[2].activeInHierarchy && !arrows[3].activeInHierarchy)
        {
            readyText.SetActive(true);
            isReady = true;
        }
    }

    public bool getIsReady() {  return isReady; }

    public void resetReady()
    {
        arrows[0].SetActive(true);
        arrows[1].SetActive(true);
        arrows[2].SetActive(true);
        arrows[3].SetActive(true);
        readyText.SetActive(false);
        readyText.SetActive(false);
        isReady = false;
    }
}
