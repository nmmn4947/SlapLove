using UnityEngine;
using UnityEngine.UI;

public class MyThing : MonoBehaviour
{
    [SerializeField] Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!PlayerPrefs.HasKey("HasLaunchedBefore"))
        {
            // First time launching the game
            PlayerPrefs.SetFloat("Master", 1);
            PlayerPrefs.SetFloat("Music", 1);
            PlayerPrefs.SetFloat("SFX", 1);

            // Mark as launched
            PlayerPrefs.SetInt("HasLaunchedBefore", 1);
            PlayerPrefs.Save(); // Optional but good practice to save right away
        }
        slider.value = PlayerPrefs.GetFloat("Master");
    }

    // Update is called once per frame
    void Update()
    {
        if (slider = null)
        {
            if (slider.gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetFloat("Master", slider.value);
                AudioManagerMenu.Instance.SetMasterVolume(slider.value);
            }
        }
    }
}
