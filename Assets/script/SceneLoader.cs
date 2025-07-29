using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
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
}
