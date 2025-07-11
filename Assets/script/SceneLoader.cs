using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void LoadToScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
