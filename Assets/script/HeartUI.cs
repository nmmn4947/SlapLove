using UnityEngine;

public class HeartUI : MonoBehaviour
{
    public GameObject[] heartP1;
    public GameObject[] heartP2;

    GameController gameController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameController = GameController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.getP1Health() < 1)
        {
            heartP1[0].SetActive(false);
            heartP1[1].SetActive(false);
            heartP1[2].SetActive(false);
        }
        if (gameController.getP1Health() == 1)
        {
            heartP1[0].SetActive(true);
            heartP1[1].SetActive(false);
            heartP1[2].SetActive(false);
        }
        if (gameController.getP1Health() == 2)
        {
            heartP1[0].SetActive(true);
            heartP1[1].SetActive(true);
            heartP1[2].SetActive(false);
        }
        if (gameController.getP1Health() == 3)
        {
            heartP1[0].SetActive(true);
            heartP1[1].SetActive(true);
            heartP1[2].SetActive(true);
        }
        if (gameController.getP2Health() < 1)
        {
            heartP2[0].SetActive(false);
            heartP2[1].SetActive(false);
            heartP2[2].SetActive(false);
        }
        if (gameController.getP2Health() == 1)
        {
            heartP2[0].SetActive(true);
            heartP2[1].SetActive(false);
            heartP2[2].SetActive(false);
        }
        if (gameController.getP2Health() == 2)
        {
            heartP2[0].SetActive(true);
            heartP2[1].SetActive(true);
            heartP2[2].SetActive(false);
        }
        if (gameController.getP2Health() == 3)
        {
            heartP2[0].SetActive(true);
            heartP2[1].SetActive(true);
            heartP2[2].SetActive(true);
        }
    }
}
