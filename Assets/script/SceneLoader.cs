using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour
{
    private void Start()
    {

    }

    public void LoadToScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadToScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadToSceneAdd(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }

    private void Update()
    {

    }
}
