using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour
{
    [SerializeField] Slider slider;

    private void Start()
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

    public void LoadToScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadToScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }

    private void Update()
    {
        PlayerPrefs.SetFloat("Master", slider.value);
        AudioManagerMenu.Instance.SetMasterVolume(slider.value);
    }
}
